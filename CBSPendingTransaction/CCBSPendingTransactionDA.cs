using CBSPendingTransaction.SIBLCBS;
using SIBLCommon.Common;
using SIBLCommon.Common.Entity.Result;
using SIBLCommon.Common.Util.Logger;
using SIBLCommon.SIBLCommon.Common.Entity.RemitGlobalAPI;
using SIBLRemit.DA.SIBLRemit;
using SIBLXoomCommon.SIBLXoom.Common.Entity.CPU;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBSPendingTransaction
{
    public class CCBSPendingTransactionDA
    {
        #region DB Connection AND CONSTANTS
        const string DB_CONNECTION = "ConStringAPP";

        /// <summary>
        /// Author: Mirazul Hasan
        /// Description: This function is used to connect to with database
        /// Date:18.05.2021
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


        public CResult ProcessOwnBankBEFTNPendingTransaction()
        {
            #region VariableDeclaration
            CResult oResult = new CResult();
            string sSql = "";
            string sRefTransactionId = "";
            string sTid = "";
            string sRemitCompanyName = "";
            string sAccountName = "";
            string sAccountNumber = "";
            string sRemitCompanyAccountNo = "";
            string sOtherBankAccountNo = "";
            string sAccountStatus = "";
            string sAmount = "";
            string sCharge = "";
            string sTransactionIdSIBL = "";
            string sBankName = "";
            string sBranchCode = "";
            string sTransactionType = "";
            decimal dBalance = 0;
            string sCurrency = "";
            decimal iAbabilBalance = 0;
            CSIBLRemitDA oDA = new CSIBLRemitDA();
            //string toAccountNoOtherBank = Convert.ToString(ConfigurationManager.AppSettings["toAccountNoOtherBank"]);
            int hasRows = 0;
            Console.ForegroundColor = ConsoleColor.White;

            #endregion VariableDeclaration

            // end of format date
            try
            {
                using (OracleConnection oConnection = new OracleConnection(GetConnectionStrings(DB_CONNECTION)))
                {
                    oConnection.Open();

                    OracleCommand oDbCommand = oConnection.CreateCommand();
                    OracleTransaction oTranasaction = oConnection.BeginTransaction();
                    oDbCommand.Transaction = oTranasaction;

                    try
                    {

                        #region Select RandomInstruction for CBS

                        sSql = "select t.* from (SELECT ti.TID, ti.TXNIDSIBL, ti.REFTXNID, ti.BANKACNO, ti.CBSSTATUS, " +
                          " ti.BANKNAME, ti.TRANSACTIONTYPE, ti.DISBURSEMENT_TYPE, " +
                          " ra.ACCOUNTNO, ra.CHARGE, ra.COMPANYNAME, ra.TOACCNUM " +
                          " FROM TRANSACTIONINFO ti join REMI_ACCOUNT ra on ti.COMPANYID = ra.COMPANYID " +
                          " join TRANSACTION_PENDING tp on ti.TID = tp.TID where " +
                          " ( ti.TRANSACTIONTYPE ='" + CurrentTransactionType.OWNBANK.ToString() + "' or " +
                          " ti.TRANSACTIONTYPE ='" + CurrentTransactionType.BEFTN.ToString() + "' ) " +
                          " and ti.TRANSSTATUS = '0' and tp.TRANSACTIONSTATUS = 0 " +
                          " order by dbms_random.value  ) t where rownum=1";


                        oDbCommand.CommandText = sSql;
                        oDbCommand.CommandType = CommandType.Text;
                        OracleDataReader oReader = oDbCommand.ExecuteReader();

                        while (oReader.Read())
                        {
                            sRefTransactionId = oReader["REFTXNID"].ToString();
                            sAccountNumber = oReader["BANKACNO"].ToString();
                            sRemitCompanyAccountNo = oReader["ACCOUNTNO"].ToString();
                            sCharge = oReader["CHARGE"].ToString();
                            sOtherBankAccountNo = oReader["TOACCNUM"].ToString();
                            sRemitCompanyName = oReader["COMPANYNAME"].ToString();
                            sTransactionIdSIBL = oReader["TXNIDSIBL"].ToString();
                            //sBranchCode = oReader["BRANCHCODE"].ToString();
                            sBankName = oReader["BANKNAME"].ToString();
                            sTransactionType = oReader["TRANSACTIONTYPE"].ToString();

                            hasRows = 1;
                        }

                        #endregion End of Select RandomInstruction for CBS

                        // new code dated on 17.09.2017
                        // iValidAccount 1: account number is valid
                        // iValidAccount 0: account number is invalid
                        int iValidAccount = 0;
                        string requestID = DateTime.Now.ToString("yyyyMMdd") + DateTime.Now.Ticks.ToString();
                        SIBLATSSoapClient tt = new SIBLATSSoapClient();
                        AccountInformationResponse accountResponse = new AccountInformationResponse();

                        //string OwnBank = "SOCIAL ISLAMI BANK LTD";

                        // set BEFTN ACCOUNT NUMBER
                        if (sTransactionType.Equals(CurrentTransactionType.BEFTN.ToString()))
                        {
                            sAccountNumber = sOtherBankAccountNo;

                            #region GetAccountDetails
                            // CBS Transaction Request                            
                            accountResponse = tt.GetAccountDetail(sAccountNumber, sAccountNumber, requestID);

                            // CBS Transaction Confirm
                            if (accountResponse.transactionStatus.ToString().Equals("SUCCESS"))
                            {
                                iValidAccount = 1;

                                //for test start
                                //sAccountName = accountResponse.accountDetail.accountName;
                                //var bala = Convert.ToDecimal(accountResponse.accountDetail.accountBalance.ToString());
                                //Console.WriteLine("Other Bank:: " + "Account Name: " + sAccountName + " Account No: " + sAccountNumber + " Balance before transaction " + bala);
                                //for test end
                            }
                            else
                            {
                                iValidAccount = 0;
                                Console.WriteLine("Other Bank Account Validation Fail");
                                CLog.Logger.Write(CLog.SUCCESS, this.ToString() + "/ProcessOwnBankBEFTNInstruction: Account Validation API Result for Other Bank: " + accountResponse.transactionStatus.ToString());
                            }
                            #endregion GetAccountDetails

                            if (iValidAccount == 0)
                            {
                                // invalid account number for own/other bank account

                                sSql = "Update TRANSACTIONINFO set CBSSTATUS=2,CBSDATE=sysdate " +
                                     " where TXNIDSIBL = '" + sTransactionIdSIBL + "'";

                                oDbCommand.CommandText = sSql;
                                oDbCommand.CommandType = CommandType.Text;
                                int ii = oDbCommand.ExecuteNonQuery();
                            }
                        }
                        // END OF SETTING BEFTN ACCOUNT NUMBER

                        #region CheckAccountNumber


                        else if (sTransactionType.Equals(CurrentTransactionType.OWNBANK.ToString()))
                        {
                            // Check account Number 

                            //iValidAccount = IsValidAccountNo(sAccountNumber);
                            try
                            {
                                #region GetAccountDetails
                                // CBS Transaction Request                            
                                accountResponse = tt.GetAccountDetail(sAccountNumber, sAccountNumber, requestID);

                                // CBS Transaction Confirm
                                if (accountResponse.transactionStatus.ToString().Equals("SUCCESS"))
                                {
                                    iValidAccount = 1;

                                    //for test start
                                    //sAccountName = accountResponse.accountDetail.accountName;
                                    //var bala = Convert.ToDecimal(accountResponse.accountDetail.accountBalance.ToString());
                                    //Console.WriteLine("Own Bank:: " + "Account Name: " + sAccountName + " Account No: " + sAccountNumber + " Balance before transaction " + bala);
                                    //for test end

                                }
                                else
                                {
                                    iValidAccount = 0;
                                    Console.WriteLine("Own Bank Account Validation Fail");
                                    CLog.Logger.Write(CLog.SUCCESS, this.ToString() + "/ProcessOwnBankBEFTNPendingTransaction: Account Validation API Result: " + accountResponse.transactionStatus.ToString());
                                }
                                #endregion GetAccountDetails
                            }
                            catch (Exception exp)
                            {
                                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/ProcessOwnBankBEFTNPendingTransaction: " + exp.Message);
                            }

                            if (iValidAccount == 0)
                            {
                                // invalid account number for own bank account

                                sSql = "Update TRANSACTIONINFO set CBSSTATUS=2,CBSDATE=sysdate " +
                                     " where TXNIDSIBL = '" + sTransactionIdSIBL + "'";

                                oDbCommand.CommandText = sSql;
                                oDbCommand.CommandType = CommandType.Text;
                                int ii = oDbCommand.ExecuteNonQuery();
                            }
                        }

                        
                        // End of new code dated on 17.09.2017
                        #endregion CheckAccountNumber

                        if (hasRows == 1 && iValidAccount == 1)
                        {
                            #region LockTable for Update Status
                            //lock table for update

                            oDbCommand.CommandText = " select * from TRANSACTIONINFO where TXNIDSIBL='" + sTransactionIdSIBL + "'  FOR UPDATE OF CBSSTATUS";
                            oDbCommand.CommandType = CommandType.Text;
                            OracleDataReader oReaderLock = oDbCommand.ExecuteReader();
                            // end of lock table for update
                            #endregion LockTable for Update Status


                            #region Get Amount

                            sSql = "select TID,AMOUNT,CURRENCY from TRANSACTIONINFO where TXNIDSIBL= '" + sTransactionIdSIBL + "'";

                            oDbCommand.CommandText = sSql;
                            oDbCommand.CommandType = CommandType.Text;
                            OracleDataReader oReader1 = oDbCommand.ExecuteReader();

                            while (oReader1.Read())
                            {
                                sTid = oReader1["TID"].ToString();
                                sAmount = oReader1["AMOUNT"].ToString();
                                sCurrency = oReader1["CURRENCY"].ToString();

                            }
                            #endregion End of Get Amount

                            #region Check Available Balance

                            if (sAmount != "")
                                dBalance = Convert.ToDecimal(sAmount);

                            // balance is available
                            try
                            {
                                #region GetAccountDetails
                                // CBS Transaction Request                            
                                accountResponse = tt.GetAccountDetail(sRemitCompanyAccountNo, sRemitCompanyAccountNo, requestID);

                                // CBS Transaction Confirm
                                if (accountResponse.transactionStatus.ToString().Equals("SUCCESS"))
                                {
                                    sAccountName = accountResponse.accountDetail.accountName;
                                    sAccountStatus = accountResponse.accountDetail.accountStatus;
                                    iAbabilBalance = Convert.ToDecimal(accountResponse.accountDetail.accountBalance.ToString());

                                    //for test start
                                    //Console.WriteLine("Remit Company:: " + "Account Name: " + sAccountName + " Account No: " + sRemitCompanyAccountNo + " Balance before transaction " + iAbabilBalance);
                                    //for test end

                                }
                                else
                                {
                                    Console.WriteLine(" Account Validation Fail");
                                    CLog.Logger.Write(CLog.SUCCESS, this.ToString() + "/ProcessOwnBankBEFTNPendingTransaction: Account Validation API Result: " + accountResponse.transactionStatus.ToString());
                                }
                                #endregion GetAccountDetails

                            }
                            catch (Exception exp)
                            {
                                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/ProcessOwnBankBEFTNPendingTransaction: " + exp.Message);
                            }

                            if (iAbabilBalance >= dBalance)
                            {
                                ///Till now ID is permitting Transaction though there is not enough balance.
                                ///Once we get clearance from ID that, payment will be only done if there is
                                ///available balance, this block of code will be active 

                                //Add DoAbabilTransaction Block


                                #region doAbabilTransaction

                                CAbabilTransactionResponse oAbabilTransactionResponse = new CAbabilTransactionResponse();
                                string fromAccountNo = sRemitCompanyAccountNo;
                                string toAccountNo = sAccountNumber;
                                string Charge = sCharge;
                                transactionResponse response = new transactionResponse();
                                CResult oResultCBSRequest = new CResult();
                                CResult oResultCBSResponse = new CResult();
                                try
                                {
                                    #region SaveAbabilRequest
                                    oAbabilTransactionResponse.RequestReference = sTransactionIdSIBL;
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
                                            response = tt.doCTSTransaction(fromAccountNo, toAccountNo, sAmount, Charge, sTransactionIdSIBL, accountType.Savings, accountType.Savings, sTransactionIdSIBL, requestID);
                                            #region Ababil Transaction
                                            if (response.transactionStatus.ToString().Equals("SUCCESS"))
                                            {
                                                oAbabilTransactionResponse = new CAbabilTransactionResponse();
                                                CLog.Logger.Write(CLog.SUCCESS, this.ToString() + "/ProcessOwnBankBEFTNPendingTransaction: CBS Transaction::Successfull");
                                                // ababil transaction is successfull


                                                //for test start
                                                //Console.WriteLine("Disbursement Account No: " + sAccountNumber + " Disbursement Amount " + dBalance);
                                                //accountResponse = tt.GetAccountDetail(sRemitCompanyAccountNo, sRemitCompanyAccountNo, requestID);
                                                //if (accountResponse.transactionStatus.ToString().Equals("SUCCESS"))
                                                //{
                                                //    sAccountName = accountResponse.accountDetail.accountName;
                                                //    var bala = Convert.ToDecimal(accountResponse.accountDetail.accountBalance.ToString());
                                                //    Console.WriteLine("Remit Company:: " + "Account Name: " + sAccountName + " Account No: " + sRemitCompanyAccountNo + " Balance after transaction " + bala);

                                                //}

                                                //accountResponse = tt.GetAccountDetail(sAccountNumber, sAccountNumber, requestID);
                                                //if (accountResponse.transactionStatus.ToString().Equals("SUCCESS"))
                                                //{
                                                //    sAccountName = accountResponse.accountDetail.accountName;
                                                //    var bala = Convert.ToDecimal(accountResponse.accountDetail.accountBalance.ToString());
                                                //    Console.WriteLine("Own/Other Bank:: " + "Account Name: " + sAccountName + " Account No: " + sAccountNumber + " Balance after transaction " + bala);

                                                //}
                                                //for test end


                                                #region UPdate CBS Status

                                                try
                                                {
                                                    sSql = "Update TRANSACTIONINFO set CBSSTATUS=1,CBSDATE=sysdate,TRANSSTATUS=1 " +
                                                           " where TXNIDSIBL = '" + sTransactionIdSIBL + "'";

                                                    oDbCommand.CommandText = sSql;
                                                    oDbCommand.CommandType = CommandType.Text;
                                                    int iCount = oDbCommand.ExecuteNonQuery();
                                                }
                                                catch (Exception exp)
                                                {
                                                    // abail reverse transaction is required here in case of any exception
                                                    CLog.Logger.Write(CLog.EXCEPTION, "/ProcessOwnBankBEFTNPendingTransaction: " + exp.Message);

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

                                                #region Update Pending List status

                                                sSql = " Update TRANSACTION_PENDING set TRANSACTIONSTATUS=1,STATUSUPDATEDATE=sysdate where " +
                                                          "  TID = '" + sTid + "'";

                                                oDbCommand.CommandText = sSql;
                                                oDbCommand.CommandType = CommandType.Text;
                                                int iiCount = oDbCommand.ExecuteNonQuery();

                                                #endregion Update Pending List status


                                                if (oResult.Result == true)
                                                {
                                                    CLog.Logger.Write(CLog.SUCCESS, this.ToString() + "/ProcessOwnBankBEFTNPendingTransaction: CBS Response Saved::Successfull");                                                    
                                                }
                                                Console.WriteLine("CBS Pending Transaction is successfull for ::" + sTransactionIdSIBL);
                                            }
                                            else if (response.transactionStatus.ToString().Equals("FAIL"))
                                            {
                                                #region SaveAbabilResponse
                                                oAbabilTransactionResponse = new CAbabilTransactionResponse();
                                                oAbabilTransactionResponse.Description = Convert.ToString(response.description);

                                                if (response.errorDetail != null)
                                                    oAbabilTransactionResponse.ErrorDetail = Convert.ToString(response.errorDetail.errorCode) + " " + Convert.ToString(response.errorDetail.errorMessage);
                                                //oAbabilTransactionResponse.PropertyChanged = response.PropertyChanged;

                                                if (response.requestReference != null)
                                                    oAbabilTransactionResponse.RequestReference = response.requestReference;
                                                else
                                                    oAbabilTransactionResponse.RequestReference = sTransactionIdSIBL;

                                                oAbabilTransactionResponse.TransactionStatus = Convert.ToString(response.transactionStatus);
                                                oAbabilTransactionResponse.TransactionStatusSpecified = Convert.ToString(response.transactionStatusSpecified);
                                                oAbabilTransactionResponse.OperationType = "REQDEL";//REVERSE, RESPONSE ,REQDEL
                                                oAbabilTransactionResponse.AppRequestId = requestID;
                                                oAbabilTransactionResponse.Description = sAmount;// 
                                                oResult = oDA.SaveDisbursementAbabilResponse(oAbabilTransactionResponse);
                                                #endregion SaveAbabilResponse

                                                #region FailErrorMessageTreatment
                                                if (response.errorDetail.errorMessage.Trim().ToLower().Contains(" DOES NOT EXIST".Trim().ToLower()))
                                                {
                                                    sSql = "Update TRANSACTIONINFO set CBSSTATUS=2,CBSDATE=sysdate " +
                                                           " where TXNIDSIBL = '" + sTransactionIdSIBL + "'";

                                                    oDbCommand.CommandText = sSql;
                                                    oDbCommand.CommandType = CommandType.Text;
                                                    int iCount1 = oDbCommand.ExecuteNonQuery();

                                                    Console.WriteLine(" Account Number does not exists");
                                                    CLog.Logger.Write(CLog.EXCEPTION, "/GetOwnBankInstructionList: Ababil Call is fail::Account Number does not exists");

                                                }
                                                else if (response.errorDetail.errorMessage.Trim().ToLower().Contains("AccountNotActive".Trim().ToLower()))
                                                {
                                                    sSql = "Update TRANSACTIONINFO set CBSSTATUS=5,CBSDATE=sysdate " +
                                                           " where TXNIDSIBL = " + sTransactionIdSIBL;

                                                    oDbCommand.CommandText = sSql;
                                                    oDbCommand.CommandType = CommandType.Text;
                                                    int iCount1 = oDbCommand.ExecuteNonQuery();

                                                    Console.WriteLine(" Account Number is not Active");
                                                    CLog.Logger.Write(CLog.EXCEPTION, "/ProcessOwnBankBEFTNPendingTransaction: Ababil Call is fail::Account Number is not Active");

                                                }
                                                else if (response.errorDetail.errorMessage.ToString().ToLower().Trim().Equals("TransactionNotAllowed".ToLower()))
                                                {
                                                    #region TransactionIsNotAllowed

                                                    sSql = "Update TRANSACTIONINFO set CBSSTATUS=3,CBSDATE=sysdate " +
                                                           " where TXNIDSIBL = '" + sTransactionIdSIBL + "'";

                                                    oDbCommand.CommandText = sSql;
                                                    oDbCommand.CommandType = CommandType.Text;
                                                    int iCount1 = oDbCommand.ExecuteNonQuery();

                                                    #region PendingList for CBS Transaction

                                                    //sSql = " INSERT INTO TRANSACTION_PENDING  (TID,ENTRYDATE,TRANSACTIONSTATUS) " +
                                                    //      " VALUES  ( " + sTid + ",sysdate,0)";

                                                    //oDbCommand.CommandText = sSql;
                                                    //oDbCommand.CommandType = CommandType.Text;
                                                    //iCount1 = oDbCommand.ExecuteNonQuery();

                                                    #endregion PendingList for CBS Transaction

                                                    #endregion TransactionIsNotAllowed
                                                    Console.WriteLine(" Transaction is Not Allowed!!!");
                                                    CLog.Logger.Write(CLog.EXCEPTION, "/ProcessOwnBankBEFTNPendingTransaction: Ababil Call is fail::TransactionNotAllowed");
                                                }
                                                else if (response.errorDetail.errorMessage.ToString().ToLower().Trim().Equals("Insufficient Balanace".ToLower()))
                                                {
                                                    #region Insufficient Fund
                                                    // this region will be used to check insufficient balance
                                                    // information. this option will be developed later.
                                                    //  CBSSTATUS 4: INSUFFICIENT BALANCE
                                                    //

                                                    sSql = "Update TRANSACTIONINFO set CBSSTATUS=4,CBSDATE=sysdate " +
                                                           " where TXNIDSIBL = '" + sTransactionIdSIBL + "'";

                                                    oDbCommand.CommandText = sSql;
                                                    oDbCommand.CommandType = CommandType.Text;
                                                    int iCount1 = oDbCommand.ExecuteNonQuery();

                                                    CLog.Logger.Write(CLog.EXCEPTION, "/ProcessOwnBankBEFTNPendingTransaction: Ababil Call is fail::Insufficient Balanace");

                                                    #endregion Insufficient Fund
                                                }
                                                Console.WriteLine(" CBS Transaction status::" + response.transactionStatus.ToString());
                                                CLog.Logger.Write(CLog.EXCEPTION, "/ProcessOwnBankBEFTNPendingTransaction: Ababil Call is fail" + oResult.Message);
                                                #endregion FailErrorMessageTreatment
                                            }

                                            else
                                            {
                                                // ababil transaction fail
                                                // Delete Request information from database

                                                oAbabilTransactionResponse = new CAbabilTransactionResponse();
                                                oAbabilTransactionResponse.RequestReference = sTransactionIdSIBL;
                                                oAbabilTransactionResponse.AppRequestId = requestID;
                                                oAbabilTransactionResponse.Description = sAmount;// 
                                                oAbabilTransactionResponse.OperationType = "REQDEL";
                                                oResult = new CResult();
                                                oResult = oDA.SaveDisbursementAbabilResponse(oAbabilTransactionResponse);
                                                CLog.Logger.Write(CLog.EXCEPTION, "/ProcessOwnBankBEFTNPendingTransaction: Ababil Call is fail-" + oResult.Message);
                                            }

                                            #endregion Ababil Transaction
                                        }
                                        catch (Exception exp)
                                        {
                                            // delete transaction request
                                            oAbabilTransactionResponse.RequestReference = sTransactionIdSIBL;
                                            oAbabilTransactionResponse.AppRequestId = requestID;
                                            oAbabilTransactionResponse.Description = sAmount;// 
                                            oAbabilTransactionResponse.OperationType = "REQDEL";
                                            oResult = new CResult();
                                            oResult = oDA.SaveDisbursementAbabilResponse(oAbabilTransactionResponse);

                                            CLog.Logger.Write(CLog.EXCEPTION, "/ProcessOwnBankBEFTNPendingTransaction: Ababil Call is fail-" + exp.Message);
                                        }
                                    }
                                    else
                                    {
                                        //
                                        // delete transaction request
                                        oAbabilTransactionResponse = new CAbabilTransactionResponse();
                                        oAbabilTransactionResponse.RequestReference = sTransactionIdSIBL;
                                        oAbabilTransactionResponse.AppRequestId = requestID;
                                        oAbabilTransactionResponse.Description = sAmount;// 
                                        oAbabilTransactionResponse.OperationType = "REQDEL";
                                        oResult = new CResult();
                                        oResult = oDA.SaveDisbursementAbabilResponse(oAbabilTransactionResponse);

                                        CLog.Logger.Write(CLog.ERROR, "/ProcessOwnBankBEFTNPendingTransaction:" + oResult.Message);
                                    }
                                }
                                catch (Exception exp)
                                {
                                    CLog.Logger.Write(CLog.INFORMATION, exp.Message);

                                }
                                #endregion End of doAbabilTransaction

                            }
                            else
                            {
                                //when iAbabilBalance= - 100  then Exception Occur in Connection or Convertion

                                //if (iAbabilBalance != -100)                                
                                {

                                    // balance is not available
                                    // There is not enough xoom balance for a transaction
                                    #region Insufficient Fund
                                    // this region will be used to check insufficient balance
                                    // information. this option will be developed later.
                                    //  CBSSTATUS 4: INSUFFICIENT BALANCE
                                    //

                                    sSql = "Update TRANSACTIONINFO set CBSSTATUS=4,CBSDATE=sysdate " +
                                           " where TXNIDSIBL = '" + sTransactionIdSIBL + "'";

                                    oDbCommand.CommandText = sSql;
                                    oDbCommand.CommandType = CommandType.Text;
                                    int iCount1 = oDbCommand.ExecuteNonQuery();
                                    Console.WriteLine("Remittance company insufficient balanace for#" + sTransactionIdSIBL);

                                    #endregion Insufficient Fund
                                }
                                CLog.Logger.Write(CLog.INFORMATION, "/ProcessOwnBankBEFTNPendingTransaction:Insufficient remittance company balance for#" + sTransactionIdSIBL);
                            }

                            #endregion Check Available Balance
                        }
                        else if (hasRows == 0 && iValidAccount == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(DateTime.Now.ToShortTimeString() + "::No More Transaction is available for CBS");
                            CLog.Logger.Write(CLog.INFORMATION, "/ProcessOwnBankBEFTNPendingTransaction: No More Transaction is available");
                        }

                    }
                    catch (Exception exp)
                    {
                        oTranasaction.Rollback();
                        CLog.Logger.Write(CLog.ERROR, exp.Message);
                    }
                    finally
                    {   // Added exception handling with Transaction Commit Error : A Rahim Khan
                        try
                        {
                            oTranasaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            CLog.Logger.Write(CLog.ERROR, "ProcessOwnBankBEFTNPendingTransaction::oTranasaction.Commit" + ex.Message);
                            oConnection.Close();
                            oConnection.Dispose();
                        }

                        oConnection.Close();
                        oConnection.Dispose();
                    }
                }

            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.INFORMATION, exp.Message);
            }

            return oResult;
        }
    }
}
