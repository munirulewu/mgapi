using Oracle.ManagedDataAccess.Client;
using PushToITCL.SIBLCBS;
using SIBLCommon.Common.Entity.Result;
using SIBLCommon.Common.Util.Logger;
using SIBLCommon.SIBLCommon.Common.Entity.ITCL;
using SIBLRemitDA.SIBLRemit.DA.MGAPIDA;
using SIBLXoomCommon.SIBLXoom.Common.Entity.CPU;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushToITCL
{
    public class CITCLDA
    {
        #region DB Connection AND CONSTANTS
        const string DB_CONNECTION_MGDB = "ConStringAPP";
        const string DB_TXN_REVERSAL_YES = "YES";
        const string DB_TXN_REVERSAL_NO = "NO";
        const string DB_TXN_PENDING = "PENDING";
        const string DB_TXN_ONHOLD = "ONHOLD";
        const string DB_TXN_REJECTED = "REJECTED";
        const string DB_TXN_COMPLETED = "COMPLETED";
        /// <summary>
        /// Author: Munirul Islam
        /// Description: This function is used to connect to with database
        /// Date:15-06-2020
        /// </summary>
        /// <param name="sConnectionName"></param>
        /// <returns></returns>
        static string GetConnectionStrings(string sConnectionName)
        {
            ConnectionStringSettingsCollection settings =
                ConfigurationManager.ConnectionStrings;

            string conName = "";
            if (settings != null)
            {
                foreach (ConnectionStringSettings cs in settings)
                {

                    if (cs.Name.ToUpper().Equals(sConnectionName.ToUpper()))
                    {
                        conName = cs.ConnectionString;
                    }

                }
            }
            return conName;
        }

        #endregion DB Connection

        public string GetICTLExtId()
        {
            #region VariableDeclaration
           
           
            string sSql = string.Empty;
            string ExtId = string.Empty;
            #endregion VariableDeclaration

            // end of format date
            try
            {
                using (OracleConnection oConnection = new OracleConnection(GetConnectionStrings(DB_CONNECTION_MGDB)))
                {
                    oConnection.Open();

                    OracleCommand oDbCommand = oConnection.CreateCommand();

                    try
                    {

                        // select a random instruction which beneficiary validation is not done
                        sSql = "  SELECT  concat(TO_CHAR (SYSDATE, 'yyyymmddHH'),LPAD (SEQ_ITCLID.NEXTVAL, 6, 0))   v_trackerno   FROM DUAL;";
                        oDbCommand.CommandText = sSql;
                        oDbCommand.CommandType = CommandType.Text;
                        object objId = oDbCommand.ExecuteScalar();
                        ExtId = objId.ToString();

                       
                        
                    }
                    catch (Exception exp)
                    {

                        CLog.Logger.Write(CLog.ERROR, exp.Message);
                    }
                    finally
                    {
                        oConnection.Close();
                        oConnection.Dispose();
                    }
                }

            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, exp.Message);
            }

            return ExtId;
        }

        public CResult TransferAmountGLToRemitAccount113(OracleCommand oDbCommand, string fromAccountNumber, string toAccountNumber, string amount, string mgiTransactionNumber, string siblTransactionId)
        {
            CResult oResult = new CResult();

            try
            {
                #region doAbabilTransaction

                CAbabilTransactionResponse oAbabilTransactionResponse = new CAbabilTransactionResponse();
                string sTrackingNumber = mgiTransactionNumber;
                string fromAccountNo = fromAccountNumber;
                string toAccountNo = toAccountNumber;
                string Charge = ConfigurationManager.AppSettings["Charge"];
                string requestID = DateTime.Now.ToString("yyyyMMdd") + DateTime.Now.Ticks.ToString();
                string sAmount = amount;
                SIBLATSSoapClient tt = new SIBLATSSoapClient();
                transactionResponse response = new transactionResponse();

                CResult oResultCBSRequest = new CResult();
                CResult oResultCBSResponse = new CResult();
                CMGAPIDA oDA = new CMGAPIDA();
                string sSql = "";
                try
                {
                    #region SaveAbabilRequest
                    oAbabilTransactionResponse.RequestReference = sTrackingNumber;
                    oAbabilTransactionResponse.AppRequestId = requestID;
                    oAbabilTransactionResponse.Description = sAmount;// 
                    oAbabilTransactionResponse.OperationType = "REQUEST";
                    //oAbabilTransactionResponse.User.CN = oUserSession.CN;
                    // save request information into database
                    oResultCBSRequest = oDA.SaveDisbursementAbabilResponse(oAbabilTransactionResponse);
                    #endregion SaveAbabilRequest

                    if (oResultCBSRequest.Result == true)
                    {
                        try
                        {
                            // CBS Transaction Request
                            response = tt.doCTSTransaction(fromAccountNo, toAccountNo, sAmount, Charge, sTrackingNumber, accountType.Gl, accountType.Savings, sTrackingNumber, requestID);
                            //response = tt.doCTSReverseTransaction(fromAccountNo, toAccountNo, sAmount, requestID, accountType.Savings,accountType.Gl, sTrackingNumber);
                            #region Ababil Transaction
                            if (response.transactionStatus.ToString().Equals("SUCCESS"))
                            {
                                oAbabilTransactionResponse = new CAbabilTransactionResponse();
                                CLog.Logger.Write(CLog.SUCCESS, this.ToString() + "/GetOwnBankInstructionList: CBS Transaction::Successfull for XOOM Tracking no: " + sTrackingNumber);

                                // ababil transaction is successfull


                                #region UPdate CBS Status

                                try
                                {

                                   
                                    string TrsanType = "GL_TO_REMITAC";
                                    sSql = "INSERT INTO cbstransaction ( pinno, entrydate, trans_type, apprequestid) VALUES (" +
                                           "'" + mgiTransactionNumber + "',  sysdate,'" + TrsanType + "', '" + requestID + "')";

                                    oDbCommand.CommandText = sSql;
                                    oDbCommand.CommandType = CommandType.Text;
                                   int iCount = oDbCommand.ExecuteNonQuery();


                                }
                                catch (Exception exp)
                                {
                                    // abail reverse transaction is required here in case of any exception
                                    CLog.Logger.Write(CLog.EXCEPTION, "/TransferAmountGLToRemitAccount113: " + exp.Message);

                                }




                                #endregion End of Update CBS Status

                                #region SaveAbabilResponse
                                oAbabilTransactionResponse.Description = Convert.ToString(response.description);

                                if (response.errorDetail != null)
                                    oAbabilTransactionResponse.ErrorDetail = Convert.ToString(response.errorDetail.errorCode) + " " + Convert.ToString(response.errorDetail.errorMessage);
                                //oAbabilTransactionResponse.PropertyChanged = response.PropertyChanged;
                                oAbabilTransactionResponse.RequestReference = response.requestReference;
                                oAbabilTransactionResponse.ResponseReference = response.responseReference;
                                oAbabilTransactionResponse.TransactionStatus = Convert.ToString(response.transactionStatus);
                                oAbabilTransactionResponse.TransactionStatusSpecified = Convert.ToString(response.transactionStatusSpecified);
                                oAbabilTransactionResponse.OperationType = "RESPONSE";
                                oAbabilTransactionResponse.AppRequestId = requestID;
                                oResult = oDA.SaveDisbursementAbabilResponse(oAbabilTransactionResponse);
                                #endregion SaveAbabilResponse

                                if (oResult.Result == true)
                                {
                                    CLog.Logger.Write(CLog.SUCCESS, this.ToString() + "/TransferAmountGLToRemitAccount113: CBS Response Saved::Successfull, Tracking Number:: " + sTrackingNumber);

                                }
                                Console.WriteLine("CBS Transaction is successfull for ::" + sTrackingNumber);
                            }

                            else
                            {
                                // ababil transaction fail

                                // Delete Request information from database


                                oAbabilTransactionResponse = new CAbabilTransactionResponse();
                                oAbabilTransactionResponse.RequestReference = sTrackingNumber;
                                oAbabilTransactionResponse.AppRequestId = requestID;
                                oAbabilTransactionResponse.Description = sAmount;// 
                                oAbabilTransactionResponse.OperationType = "REQDEL";
                                oResult = new CResult();
                                oResult = oDA.SaveDisbursementAbabilResponse(oAbabilTransactionResponse);

                                CLog.Logger.Write(CLog.EXCEPTION, "/GetOwnBankInstructionList: Ababil Call is fail-" + oResult.Message);


                            }


                            #endregion Ababil Transaction
                        }
                        catch (Exception exp)
                        {
                            // delete transaction request
                            oAbabilTransactionResponse.RequestReference = sTrackingNumber;
                            oAbabilTransactionResponse.AppRequestId = requestID;
                            oAbabilTransactionResponse.Description = sAmount;// 
                            oAbabilTransactionResponse.OperationType = "REQDEL";
                            oResult = new CResult();
                            oResult = oDA.SaveDisbursementAbabilResponse(oAbabilTransactionResponse);

                            CLog.Logger.Write(CLog.EXCEPTION, "/GetOwnBankInstructionList: Ababil Call is fail-" + exp.Message);
                        }
                    }
                    else
                    {
                        //
                        // delete transaction request
                        oAbabilTransactionResponse = new CAbabilTransactionResponse();
                        oAbabilTransactionResponse.RequestReference = sTrackingNumber;
                        oAbabilTransactionResponse.AppRequestId = requestID;
                        oAbabilTransactionResponse.Description = sAmount;// 
                        oAbabilTransactionResponse.OperationType = "REQDEL";
                        oResult = new CResult();
                        oResult = oDA.SaveDisbursementAbabilResponse(oAbabilTransactionResponse);

                        CLog.Logger.Write(CLog.ERROR, "/GetOwnBankInstructionList:" + oResult.Message);
                    }


                }
                catch (Exception exp)
                {
                    CLog.Logger.Write(CLog.INFORMATION, exp.Message);

                }
                #endregion End of doAbabilTransaction

            }
            catch (Exception exp)
            {

            }
            return oResult;
        }

        public CResult TransactionInQuiry(CTokenResponse oTokenResponse)
        {
            CResult oResult = new CResult();
            string sSql = "";
            try {

                using (OracleConnection oConnection = new OracleConnection(GetConnectionStrings(DB_CONNECTION_MGDB)))
                {
                    oConnection.Open();

                    OracleCommand oDbCommand = oConnection.CreateCommand();

                    try {

                        // select a random instruction which beneficiary validation is not done
                        sSql = " select TRANSID, SIBLTRASNSACTIONID, MGITRANSACTIONID, SOURCEOFFUND, ACCOUNTCODE,substr(accountcode,1,3) bankCode," +
                               " (SELECT BANK_SHORT_NAME FROM  bank_list_itcl where bank_active_status='Yes' and bank_code=substr(accountcode,1,3)) BANK_SHORT_NAME, " +
                               " (SELECT TRANSACTION_TYPE FROM  bank_list_itcl where  bank_active_status='Yes' and bank_code=substr(accountcode,1,3)) AAN_AAQ, " +
                               " ACCOUNTNUMBER, RECEIVERCOUNTRYCODE, RECEIVERAMOUNT,FSAPPREQID " +
                               " from (select * from transactioninfo where trasactiontype='NPSB' and PUSHDATE is null  and CBSSTATUS='1' order by dbms_random.value)  t where  rownum=1";

                        oDbCommand.CommandText = sSql;
                        oDbCommand.CommandType = CommandType.Text;
                        OracleDataReader oReader = oDbCommand.ExecuteReader();
                    }
                    catch (Exception exp)
                    { 
                    
                    }

                }
            }
            catch (Exception exp)
            {
            
            
            }
            return oResult;
        }

        public bool IsNPSBTransactionAvailable()
        {
            #region VariableDeclaration
            
            string sSql = string.Empty;
            bool retVal = false;
        
            int iTotalRecord = 0;
            #endregion VariableDeclaration

            // end of format date
            try
            {
                using (OracleConnection oConnection = new OracleConnection(GetConnectionStrings(DB_CONNECTION_MGDB)))
                {
                    oConnection.Open();

                    OracleCommand oDbCommand = oConnection.CreateCommand();

                    try
                    {

                        // select a random instruction which beneficiary validation is not done
                        sSql = " select count(*) totalRecord from transactioninfo where trasactiontype='NPSB' and PUSHDATE is null  and CBSSTATUS='1'";

                        oDbCommand.CommandText = sSql;
                        oDbCommand.CommandType = CommandType.Text;
                        OracleDataReader oReader = oDbCommand.ExecuteReader();

                        while (oReader.Read())
                        {
                            iTotalRecord = Convert.ToInt16( oReader["totalRecord"].ToString());
                        }

                        if (iTotalRecord > 0)
                            retVal = true;
                        else
                            retVal = false;


                       
                       
                    }
                    catch (Exception exp)
                    {

                        CLog.Logger.Write(CLog.ERROR, exp.Message);
                    }
                    finally
                    {
                        oConnection.Close();
                        oConnection.Dispose();
                    }
                }

            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, exp.Message);
            }

            return retVal;
        }
        public CResult DoNPSBTransaction(CTokenResponse oTokenResponse)
        {
            #region VariableDeclaration
            CResult oResult = new CResult();
            CAccDeposit oAccDeposit = new CAccDeposit();
            //BeneficiaryAccount oAccResponse = new BeneficiaryAccount();
            string sSql = string.Empty;
            int iHasRow = 0;
           // int iGenerateNewToken =1;
            string sAppRequestIdForReverseTransaction = "";
            int iSuccess = 0;
            //CTokenResponse oTokenResponse = new CTokenResponse();
            #endregion VariableDeclaration

            // end of format date
            try
            {
                using (OracleConnection oConnection = new OracleConnection(GetConnectionStrings(DB_CONNECTION_MGDB)))
                {
                    oConnection.Open();

                    OracleCommand oDbCommand = oConnection.CreateCommand();

                    try
                    {

                        // select a random instruction which beneficiary validation is not done
                        sSql = " select TRANSID, SIBLTRASNSACTIONID, MGITRANSACTIONID, SOURCEOFFUND, ACCOUNTCODE,substr(accountcode,1,3) bankCode,"+
                               " (SELECT BANK_SHORT_NAME FROM  bank_list_itcl where bank_active_status='Yes' and bank_code=substr(accountcode,1,3)) BANK_SHORT_NAME, " +
                               " (SELECT TRANSACTION_TYPE FROM  bank_list_itcl where  bank_active_status='Yes' and bank_code=substr(accountcode,1,3)) AAN_AAQ, " +
                               " ACCOUNTNUMBER, RECEIVERCOUNTRYCODE, RECEIVERAMOUNT,FSAPPREQID " +
                               " from (select * from transactioninfo where trasactiontype='NPSB' and PUSHDATE is null  and CBSSTATUS='1' order by dbms_random.value)  t where  rownum=1";

                        oDbCommand.CommandText = sSql;
                        oDbCommand.CommandType = CommandType.Text;
                        OracleDataReader oReader = oDbCommand.ExecuteReader();


                        while (oReader.Read())
                        {

                            oAccDeposit.SIBLtransactionNo = oReader["SIBLTRASNSACTIONID"].ToString();
                            oAccDeposit.mgiTransactionId = oReader["MGITRANSACTIONID"].ToString();
                            oAccDeposit.fullPan = oReader["ACCOUNTNUMBER"].ToString();
                            oAccDeposit.Amount = oReader["RECEIVERAMOUNT"].ToString();
                            oAccDeposit.bankName = oReader["BANK_SHORT_NAME"].ToString();
                            oAccDeposit.comment = oReader["AAN_AAQ"].ToString();
                            sAppRequestIdForReverseTransaction = oReader["FSAPPREQID"].ToString();
                           iHasRow = 1; // record found

                        }
                        if (iHasRow == 1)
                        {
                            #region DataFound
                           
                            //get unique transactionid 
                            sSql = "select GETITCLTRANSID from dual";
                            oDbCommand.CommandText = sSql;
                            oDbCommand.CommandType = CommandType.Text;
                            object oId = oDbCommand.ExecuteScalar();
                            oAccDeposit.extId= oId.ToString();


                          

                            if (oTokenResponse.keyID != "")
                            {

                              

                                int iPush = oDbCommand.ExecuteNonQuery();
                                oTokenResponse = CITCLAPI.accountDeposit(oAccDeposit, oTokenResponse);

                                sSql = " update TRANSACTIONINFO set PUSHTSTATUS =1, PUSHDATE=sysdate, ITCLREQID='" + oAccDeposit.extId + "' where SIBLTRASNSACTIONID='" + oAccDeposit.SIBLtransactionNo + "'";
                                oDbCommand.CommandText = sSql;
                                oDbCommand.CommandType = CommandType.Text;
                                iSuccess = oDbCommand.ExecuteNonQuery();

                                // Now updatting response 
                                oResult.Return = oTokenResponse;
                                oResult.Result = true;
                                if (oTokenResponse.OutParameter.AuthResponseCode == CITCLAPI.ITCL_SUCCESS)
                                {
                                    #region Success
                                    sSql = "update TRANSACTIONINFO set PUSHRESPONSE =1,MGSTATUS='1504', PUSHRESPONSEDATE=sysdate, FINALSTATUS='"+DB_TXN_COMPLETED+"', NPSBRESCODE='" + oTokenResponse.OutParameter.AuthResponseCode + "', ITCLTRANSID='" + oTokenResponse.OutParameter.TranId + "' where SIBLTRASNSACTIONID='" + oAccDeposit.SIBLtransactionNo + "'";
                                    oDbCommand.CommandText = sSql;
                                    oDbCommand.CommandType = CommandType.Text;
                                    iSuccess = oDbCommand.ExecuteNonQuery();
                                    Console.WriteLine(DateTime.Now.ToShortTimeString() + ":: Beneficiary account has been credited for SIBL Transaction NO: " + oAccDeposit.mgiTransactionId);
                                    CLog.Logger.Write(CLog.INFORMATION, ":: Beneficiary account has been credited for SIBL Transaction NO: " + oAccDeposit.mgiTransactionId);
                                    #endregion Success

                                }
                                else
                                {

                                    string ForeignRemitAccount = Convert.ToString(ConfigurationManager.AppSettings["ForeignRemitAccount"]);
                                    string NPSBAccountGLID = Convert.ToString(ConfigurationManager.AppSettings["NPSBGLID"]);

                                   
                                    if (oTokenResponse.OutParameter.AuthResponseCode == "0")
                                    {
                                        #region DeclineAndOnHold
                                        // Transaction will be on hold. will be settled my manual dispute process 
                                        sSql = "update TRANSACTIONINFO set PUSHRESPONSE ='1', MGSTATUS='1200', PUSHRESPONSEDATE=sysdate, FINALSTATUS='"+DB_TXN_ONHOLD+"', NPSBRESCODE='" + oTokenResponse.OutParameter.AuthResponseCode + "'  where SIBLTRASNSACTIONID='" + oAccDeposit.SIBLtransactionNo + "'";
                                        oDbCommand.CommandText = sSql;
                                        oDbCommand.CommandType = CommandType.Text;
                                        iSuccess = oDbCommand.ExecuteNonQuery();

                                        Console.WriteLine(DateTime.Now.ToShortTimeString() + ":: Transaction is in on hold. Received Decline from NPSB. Code: " + oTokenResponse.OutParameter.AuthResponseCode);
                                        CLog.Logger.Write(CLog.INFORMATION, ":: Transaction is in on hold. Received Decline from NPSB. Code: " + oTokenResponse.OutParameter.AuthResponseCode);
                                        #endregion DeclineAndOnHold
                                    }
                                    else if (oTokenResponse.OutParameter.AuthResponseCode == "56")
                                    {
                                        #region DeclineAndReject
                                        // Transaction will be Rejected for Invalid account Number
                                        sSql = "update TRANSACTIONINFO set PUSHRESPONSE ='1', MGSTATUS='1404', PUSHRESPONSEDATE=sysdate, FINALSTATUS='"+DB_TXN_REJECTED+"', NPSBRESCODE='" + oTokenResponse.OutParameter.AuthResponseCode + "'  where SIBLTRASNSACTIONID='" + oAccDeposit.SIBLtransactionNo + "'";
                                        oDbCommand.CommandText = sSql;
                                        oDbCommand.CommandType = CommandType.Text;
                                        iSuccess = oDbCommand.ExecuteNonQuery();

                                        Console.WriteLine(DateTime.Now.ToShortTimeString() + ":: Transaction is in on hold. Received Decline from NPSB. Code: " + oTokenResponse.OutParameter.AuthResponseCode);
                                        CLog.Logger.Write(CLog.INFORMATION, ":: Transaction is in on hold. Received Decline from NPSB. Code: " + oTokenResponse.OutParameter.AuthResponseCode);
                                        // Amount is Back to Remit Account
                                        TransferAmountGLToRemitAccount113(oDbCommand, NPSBAccountGLID, ForeignRemitAccount, oAccDeposit.Amount, oAccDeposit.mgiTransactionId, oAccDeposit.SIBLtransactionNo);

                                        #endregion DeclineAndReject
                                    }
                                    else
                                    {

                                        sSql = "SELECT count(*)   FROM itcl_response_codes where response_code='" + oTokenResponse.OutParameter.AuthResponseCode + "'";
                                        oDbCommand.CommandText = sSql;
                                        oDbCommand.CommandType = CommandType.Text;
                                        object objIsAvailable = oDbCommand.ExecuteScalar();
                                        int isIAvailable = Convert.ToInt16(objIsAvailable);
                                        if (isIAvailable > 0)
                                        {
                                            #region StatusISAvailable
                                            sSql = "SELECT debit_txn_reversal  FROM itcl_response_codes where response_code='" + oTokenResponse.OutParameter.AuthResponseCode + "'";
                                            oDbCommand.CommandText = sSql;
                                            oDbCommand.CommandType = CommandType.Text;
                                            object objYesNo = oDbCommand.ExecuteScalar();

                                            string DebitTxnReversal = objYesNo.ToString();
                                            string mgstatusCode = "1200";
                                            if (DebitTxnReversal.ToUpper().Equals(DB_TXN_REVERSAL_YES))
                                            {
                                                #region RoutedToBEFTNAndDebitTxnReversal
                                                // TRANSACTION ROUTED TO BEFTN CHANNEL

                                                sSql = "update TRANSACTIONINFO set PUSHTSTATUS ='', PUSHDATE='', PUSHRESPONSE ='', MGSTATUS='" + mgstatusCode + "',PUSHRESPONSEDATE='', TRASACTIONTYPE='BEFTN',FINALSTATUS='" + DB_TXN_PENDING + "', NPSBRESCODE='" + oTokenResponse.OutParameter.AuthResponseCode + "' where SIBLTRASNSACTIONID='" + oAccDeposit.SIBLtransactionNo + "'";
                                                oDbCommand.CommandText = sSql;
                                                oDbCommand.CommandType = CommandType.Text;
                                                iSuccess = oDbCommand.ExecuteNonQuery();

                                             
                                                TransferAmountGLToRemitAccount113(oDbCommand, NPSBAccountGLID, ForeignRemitAccount, oAccDeposit.Amount, oAccDeposit.mgiTransactionId, oAccDeposit.SIBLtransactionNo);

                                                Console.WriteLine(DateTime.Now.ToShortTimeString() + ":: Transaction is in routed to BEFTN.Transaction No: " + oAccDeposit.mgiTransactionId);
                                                CLog.Logger.Write(CLog.INFORMATION, ":: Transaction is in routed to BEFTN.Transaction No: " + oAccDeposit.mgiTransactionId);
                                                #endregion RoutedToBEFTNAndDebitTxnReversal
                                            }
                                            else
                                            {
                                                // TRANSACTION IS NOT ROUTED TO BEFTN CHANNEL. WILL BE SETTLED BY DISPUTE PROCESS
                                                // Transaction will be on hold. will be settled my manual dispute process 

                                                #region onHoldAndSettlementByDisputeProcess
                                                sSql = "update TRANSACTIONINFO set PUSHRESPONSE ='1', MGSTATUS='1200', PUSHRESPONSEDATE=sysdate, FINALSTATUS='"+DB_TXN_ONHOLD+"', NPSBRESCODE='" + oTokenResponse.OutParameter.AuthResponseCode + "'  where SIBLTRASNSACTIONID='" + oAccDeposit.SIBLtransactionNo + "'";
                                                oDbCommand.CommandText = sSql;
                                                oDbCommand.CommandType = CommandType.Text;
                                                iSuccess = oDbCommand.ExecuteNonQuery();

                                                Console.WriteLine(DateTime.Now.ToShortTimeString() + ":: Transaction is in on hold. will settled by  dispute process: " + oAccDeposit.mgiTransactionId);
                                                CLog.Logger.Write(CLog.INFORMATION, ":: Transaction is in on hold. will settled by  dispute process: " + oAccDeposit.mgiTransactionId);
                                                #endregion onHoldAndSettlementByDisputeProcess
                                            }
                                            #endregion StatusIsAvailable

                                        }
                                        else
                                        {
                                            // New Response code found. Transaction will not be processed. It will be process later on. Transaction status will be onHold.

                                            #region NewCodeFound

                                            sSql = "update TRANSACTIONINFO set PUSHRESPONSE ='1', MGSTATUS='1200', PUSHRESPONSEDATE=sysdate, FINALSTATUS='"+DB_TXN_ONHOLD+"', NPSBRESCODE='" + oTokenResponse.OutParameter.AuthResponseCode + "'  where SIBLTRASNSACTIONID='" + oAccDeposit.SIBLtransactionNo + "'";
                                            oDbCommand.CommandText = sSql;
                                            oDbCommand.CommandType = CommandType.Text;
                                            iSuccess = oDbCommand.ExecuteNonQuery();

                                            Console.WriteLine(DateTime.Now.ToShortTimeString() + ":: Transaction is in on hold. will settled by  dispute process: " + oAccDeposit.mgiTransactionId);
                                            CLog.Logger.Write(CLog.INFORMATION, ":: Transaction is in on hold. will settled by  dispute process: " + oAccDeposit.mgiTransactionId);
                                            #endregion NewCodeFound
                                        }
                                       


                                    }
                                   
                                }

                            }
                            #endregion DataFound
                        }
                        else
                        {
                            // Initialize variable
                            //oTokenResponse = new CTokenResponse();
                            oResult.Result = false;
                            oResult.Return = oTokenResponse;
                            #region NoDataFound
                            Console.WriteLine(DateTime.Now.ToShortTimeString() + ":: There is no data for NPSB Transaction");
                            //CLog.Logger.Write(CLog.INFORMATION, "There is no data for NPSB Transaction");
                            #endregion NoDataFound

                        }
                    }
                    catch (Exception exp)
                    {

                        CLog.Logger.Write(CLog.ERROR, exp.Message);
                    }
                    finally
                    {
                        oConnection.Close();
                        oConnection.Dispose();
                    }
                }

            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, exp.Message);
            }

            return oResult;
        }

    }
}
