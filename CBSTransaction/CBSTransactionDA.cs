/*
 * File name            : CBSTransactionDA
 * Author               : Md Munirul Islam
 * Date                 : 27 March  2016
 * Version              : 1.0
 *
 * Description          : This Class will be used to Do CBS Transaction for OWN_BANK and OTHER_BANK Account
 *                        Credit Transaction
 *                        
 *
 * Modification history :
 * Name                         Date                            Desc
 * Munirul Islam                 24.09.2017                      Bug Fix for Insufficient Xoom Balance 
 * Munirul Islam                 21.05.2019                      Update Blance Cheque Query for BDT balance   
 * Copyright (c) 2015-2017: SOCIAL ISLAMI BANK LIMITED
 */

using SIBLCommon.Common.Entity.Result;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIBLCommon.Common.Entity.Disbursement;
using System.Data;

using CBSTransaction.SIBLCBS;
using SIBLCommon.Common.Util.Logger;
using SIBLXoomCommon.SIBLXoom.Common.Entity.CPU;
using SIBLCommon.SIBLCommon.Common.Entity.RemitGlobalAPI;
using SIBLCommon.Common;
using SIBLRemitDA.SIBLRemit.DA.MGAPIDA;
using CBSBalanceCheck;
using Oracle.ManagedDataAccess.Client;


namespace CBSTransaction
{
    public class CBSTransactionDA
    {
        #region DB Connection AND CONSTANTS
        const string DB_CONNECTION = "ConStringAPP";
        const string DB_CONNECTION_CBS = "ACCConStringCBS";

        const string DB_OWN_BANK = "OWN_BANK";
        const string DB_OTHER_BANK = "OTHER_BANK";

        string sBEFTN = "BEFTN";
        string sNPSB = "NPSB";
        string sSIBL = "SIBL";

        string sGLTOREMIT = "1";
        /// <summary>
        /// Author: Munirul Islam
        /// Description: This function is used to connect to with database
        /// Date:05.05.2015
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

        public CResult TransferAmountRemitAccount113ToGL(OracleCommand oDbCommand, string fromAccountNumber, string toAccountNumber, string amount, string mgiTransactionNumber, string siblTransactionId)
        {
            CResult oResult = new CResult();
            int iInsert=0;

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


            try {
                #region doAbabilTransaction

                
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
                            response = tt.doCTSTransaction(fromAccountNo, toAccountNo, sAmount, Charge, sTrackingNumber, accountType.Savings, accountType.Gl, sTrackingNumber, requestID);
                            #region Ababil Transaction
                            if (response.transactionStatus.ToString().Equals("SUCCESS"))
                            {
                                oAbabilTransactionResponse = new CAbabilTransactionResponse();
                                CLog.Logger.Write(CLog.SUCCESS, this.ToString() + "/TransferAmountRemitAccount113ToGL: CBS Transaction::Successfull for mg pinno: " + sTrackingNumber);

                                // ababil transaction is successfull


                                #region UPdate CBS Status

                                try
                                {

                                    sSql = "Update TRANSACTIONINFO set CBSSTATUS=1,CBSDATE=sysdate ,FSAPPREQID='" +requestID+"'"+
                                                          " where SIBLTRASNSACTIONID = '" + siblTransactionId + "'";


                               
                                    oDbCommand.CommandText = sSql;
                                    oDbCommand.CommandType = CommandType.Text;
                                    int iCount = oDbCommand.ExecuteNonQuery();

                                    string TrsanType = "REMITAC_TO_GL";
                                    sSql = "INSERT INTO cbstransaction ( pinno, entrydate, trans_type, apprequestid) VALUES (" +
                                           "'" + mgiTransactionNumber + "',  sysdate,'" + TrsanType + "', '" + requestID + "')";

                                    oDbCommand.CommandText = sSql;
                                    oDbCommand.CommandType = CommandType.Text;
                                    iInsert = oDbCommand.ExecuteNonQuery();

                                    if (iInsert > 0)
                                    {
                                        CLog.Logger.Write(CLog.EXCEPTION, "/TransferAmountRemitAccount113ToGL:informataion is updated to cbstransaction:"+requestID);
                                    }


                                }
                                catch (Exception exp)
                                {
                                    // abail reverse transaction is required here in case of any exception
                                    CLog.Logger.Write(CLog.EXCEPTION, "/TransferAmountRemitAccount113ToGL: " + exp.Message);

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
                                    CLog.Logger.Write(CLog.SUCCESS, this.ToString() + "/TransferAmountRemitAccount113ToGL: CBS Response Saved::Successfull, Tracking Number:: " + sTrackingNumber);

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

                                CLog.Logger.Write(CLog.EXCEPTION, "/TransferAmountRemitAccount113ToGL: Ababil Call is fail-" + oResult.Message);


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

                            CLog.Logger.Write(CLog.EXCEPTION, "/TransferAmountRemitAccount113ToGL: Ababil Call is fail-" + exp.Message);
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

                        CLog.Logger.Write(CLog.ERROR, "/TransferAmountRemitAccount113ToGL:" + oResult.Message);
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

        public CResult TransferAmountGLToRemitAccount113(OracleCommand oDbCommand, string fromAccountNumber, string toAccountNumber, string amount, string mgiTransactionNumber, string siblTransactionId)
        {
            CResult oResult = new CResult();
            CResult oResultCBS = new CResult();
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
                            #region Ababil Transaction
                            if (response.transactionStatus.ToString().Equals("SUCCESS"))
                            {
                                oAbabilTransactionResponse = new CAbabilTransactionResponse();
                                CLog.Logger.Write(CLog.SUCCESS, this.ToString() + "/GetOwnBankInstructionList: CBS Transaction::Successfull for XOOM Tracking no: " + sTrackingNumber);

                                // ababil transaction is successfull

                                oResultCBS.ResponseCode= requestID;
                                #region UPdate CBS Status

                                try
                                {

                                    sSql = "Update TRANSACTIONINFO set GLTOREMITCBS=1,GLTOREMITCBSDATE=sysdate,GLAPPREQID='" + requestID + "'" +
                                                          " where SIBLTRASNSACTIONID = '" + siblTransactionId + "'";

                                    oDbCommand.CommandText = sSql;
                                    oDbCommand.CommandType = CommandType.Text;
                                    int iCount = oDbCommand.ExecuteNonQuery();

                                    string TrsanType="MGAC_TO_REMITAC";
                                     sSql = "INSERT INTO cbstransaction ( pinno, entrydate, trans_type, apprequestid) VALUES ("+
                                            "'" + mgiTransactionNumber + "',  sysdate,'" + TrsanType + "', '"+requestID+"')";

                                     oDbCommand.CommandText = sSql;
                                     oDbCommand.CommandType = CommandType.Text;
                                     iCount = oDbCommand.ExecuteNonQuery();
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
            return oResultCBS;
        }

        public CResult ProcessOwnBankTransaction( OracleCommand oDbCommand,string fromAccountNumber, string toAccountNumber, string amount, string mgiTransactionNumber, string siblTransactionId)
        {
            CResult oResult = new CResult();

            try {


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
                            response = tt.doCTSTransaction(fromAccountNo, toAccountNo, sAmount, Charge, sTrackingNumber, accountType.Savings, accountType.Savings, sTrackingNumber, requestID);
                            #region Ababil Transaction
                            if (response.transactionStatus.ToString().Equals("SUCCESS"))
                            {
                                oAbabilTransactionResponse = new CAbabilTransactionResponse();
                                CLog.Logger.Write(CLog.SUCCESS, this.ToString() + "/ProcessOwnBankTransaction: CBS Transaction::Successfull for mgi Tracking no: " + sTrackingNumber);

                                // ababil transaction is successfull


                                #region UPdate CBS Status

                                try
                                {

                                    sSql = "Update TRANSACTIONINFO set CBSSTATUS=1,CBSDATE=sysdate,FINALSTATUS='COMPLETED',MGSTATUS='1504',PUSHRESPONSE=1, PUSHRESPONSEDATE=sysdate " +
                                                          " where SIBLTRASNSACTIONID = '" + siblTransactionId + "'";



                                    oDbCommand.CommandText = sSql;
                                    oDbCommand.CommandType = CommandType.Text;
                                    int iCount = oDbCommand.ExecuteNonQuery();


                                }
                                catch (Exception exp)
                                {
                                    // abail reverse transaction is required here in case of any exception
                                    CLog.Logger.Write(CLog.EXCEPTION, "/ProcessOwnBankTransaction: " + exp.Message);

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
                                    CLog.Logger.Write(CLog.SUCCESS, this.ToString() + "/ProcessOwnBankTransaction: CBS Response Saved::Successfull, Tracking Number:: " + sTrackingNumber);
                                   
                                }
                                Console.WriteLine("CBS Transaction is successfull for ::" + sTrackingNumber);
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
                                    oAbabilTransactionResponse.RequestReference = sTrackingNumber;

                                oAbabilTransactionResponse.ResponseReference = response.responseReference;
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
                                                          " where SIBLTRASNSACTIONID = '" + siblTransactionId + "'";
                                    oDbCommand.CommandText = sSql;
                                    oDbCommand.CommandType = CommandType.Text;
                                    int iCount1 = oDbCommand.ExecuteNonQuery();

                                    Console.WriteLine(" Account Number does not exists");
                                    CLog.Logger.Write(CLog.EXCEPTION, "/ProcessOwnBankTransaction: Ababil Call is fail::Account Number does not exists");

                                }
                                else if (response.errorDetail.errorMessage.Trim().ToLower().Contains("AccountNotActive".Trim().ToLower()))
                                {

                                    sSql = "Update TRANSACTIONINFO set CBSSTATUS=5,CBSDATE=sysdate,FINALSTATUS='REJECTED',MGSTATUS='1406',PUSHRESPONSE=1, PUSHRESPONSEDATE=sysdate  " +
                                                           " where  SIBLTRASNSACTIONID = '" + siblTransactionId + "'";
                                    oDbCommand.CommandText = sSql;
                                    oDbCommand.CommandType = CommandType.Text;
                                    int iCount1 = oDbCommand.ExecuteNonQuery();

                                    Console.WriteLine(" Account Number is not Active");
                                    CLog.Logger.Write(CLog.EXCEPTION, "/ProcessOwnBankTransaction: Ababil Call is fail::Account Number is not Active");

                                }
                                else if (response.errorDetail.errorMessage.ToString().ToLower().Trim().Equals("TransactionNotAllowed".ToLower()))
                                {
                                    #region TransactionIsNotAllowed

                                    sSql = "Update TRANSACTIONINFO set CBSSTATUS=3,CBSDATE=sysdate " +
                                                         " where  SIBLTRASNSACTIONID = '" + siblTransactionId + "'";


                                    oDbCommand.CommandText = sSql;
                                    oDbCommand.CommandType = CommandType.Text;
                                    int iCount1 = oDbCommand.ExecuteNonQuery();


                                    #region PendingList for CBS Transaction


                                    sSql = " INSERT INTO INSTRUCTION_PENDING  (INSTRUCTIONID,ENTRYDATE,TRANSACTIONSTATUS) " +
                                           " VALUES  ( " + siblTransactionId + ",sysdate,2)";

                                    oDbCommand.CommandText = sSql;
                                    oDbCommand.CommandType = CommandType.Text;
                                    iCount1 = oDbCommand.ExecuteNonQuery();

                                    #endregion PendingList for CBS Transaction

                                    #endregion TransactionIsNotAllowed
                                    Console.WriteLine(" Transaction is Not Allowed!!!");
                                    CLog.Logger.Write(CLog.EXCEPTION, "/ProcessOwnBankTransaction: Ababil Call is fail::TransactionNotAllowed");
                                }
                                else if (response.errorDetail.errorMessage.ToString().ToLower().Trim().Equals("Insufficient Balanace".ToLower()))
                                {
                                    #region Insufficient Fund
                                    // this region will be used to check insufficient balance
                                    // information. this option will be developed later.
                                    //  CBSSTATUS 4: INSUFFICIENT BALANCE
                                    //

                                    sSql = "Update TRANSACTIONINFO set CBSSTATUS=4,CBSDATE=sysdate " +
                                           " where SIBLTRASNSACTIONID = '" + siblTransactionId + "'";
                                    oDbCommand.CommandText = sSql;
                                    oDbCommand.CommandType = CommandType.Text;
                                    int iCount1 = oDbCommand.ExecuteNonQuery();


                                    CLog.Logger.Write(CLog.EXCEPTION, "/ProcessOwnBankTransaction: Ababil Call is fail::MG insufficient Balanace");

                                    #endregion Insufficient Fund
                                }
                                Console.WriteLine(" CBS Transaction status::" + response.transactionStatus.ToString());
                                CLog.Logger.Write(CLog.EXCEPTION, "/ProcessOwnBankTransaction: Ababil Call is fail" + oResult.Message);
                                #endregion FailErrorMessageTreatment
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

                                CLog.Logger.Write(CLog.EXCEPTION, "/ProcessOwnBankTransaction: Ababil Call is fail-" + oResult.Message);


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

                            CLog.Logger.Write(CLog.EXCEPTION, "/ProcessOwnBankTransaction: Ababil Call is fail-" + exp.Message);
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

                        CLog.Logger.Write(CLog.ERROR, "/ProcessOwnBankTransaction:" + oResult.Message);
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



        public AccountInformationResponse isValidAccount(string sAccountNumber, string mgiTransactionNumber)
        {
            CResult oResult = new CResult();
            CResult oResultCBS = new CResult();
            
            AccountInformationResponse response = new AccountInformationResponse();
            try
            {
                #region doAbabilTransaction

              
                string sTrackingNumber = mgiTransactionNumber;
                string requestID = DateTime.Now.ToString("yyyyMMdd") + DateTime.Now.Ticks.ToString();
                
                SIBLATSSoapClient tt = new SIBLATSSoapClient();
               
               
                try
                {
                    response = tt.GetAccountDetail(sAccountNumber, sTrackingNumber, requestID);
                   
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
            return response;
        }

    
        public CResult ProcessTransactions()
        {
            #region VariableDeclaration
            CResult oResult = new CResult();
            string sSql = "";
            string instructionId = "";
           // string sAccountName = "";
            string sAccountNumber = "";
            string sAmount = "";
           // string sTrackingNumber = "";
            //string sBankName = "";
           // string sBranchCode = "";
            decimal dBalance = 0;
            string sCurrency = "";

            string siblTransactionId = "";
            string mgiTransactionId = "";
            string toAccountNumber = "";
            string fromAccountNumber = "";
            string sCharge = "";
            string transactionType = string.Empty;
            string receiverAccNum = "";
            CMGAPIDA oDA = new CMGAPIDA();
          //  CBalanceCheck oVal = new CBalanceCheck();
            string ForeignRemitAccount = Convert.ToString(ConfigurationManager.AppSettings["ForeignRemitAccount"]);
            string MoneyGramAccountGLID = Convert.ToString(ConfigurationManager.AppSettings["MGGLID"]);
            string NPSBAccountGLID = Convert.ToString(ConfigurationManager.AppSettings["NPSBGLID"]);

            string GLtoRemitTransaction = "";
            int hasRows = 0;
            AccountInformationResponse accountResponse = new AccountInformationResponse();
            #endregion VariableDeclaration

            // end of format date
            try
            {
                using (OracleConnection oConnection = new OracleConnection(GetConnectionStrings(DB_CONNECTION)))
                {
                    oConnection.Open();
                    
                   OracleCommand oDbCommand = oConnection.CreateCommand();
                   //OracleTransaction oTranasaction = oConnection.BeginTransaction();

                    
                   //oDbCommand.Transaction = oTranasaction;

                    try
                    {
                        
                        #region Select RandomInstruction for CBS

                        // select a random instruction which is acknowladed to xoom
                        sSql = " select t.* from (SELECT ti.TRANSID, ti.SIBLTRASNSACTIONID, ti.MGITRANSACTIONID,  " +
                               " ti.TRASACTIONTYPE, ti.RECEIVERAMOUNT,ti.ACCOUNTNUMBER ReceiverAccNum,GLTOREMITCBS," +
                       " (select accountno from remi_account where companyname= ti.TRASACTIONTYPE) ACCOUNTNO," +
                       " '0' CHARGE,  (select toaccnum from remi_account where companyname= ti.TRASACTIONTYPE) TOACCNUM " +
                          " FROM TRANSACTIONINFO ti  where ti.CBSSTATUS is null " +
                          " and ti.TRASACTIONTYPE in('SIBL','NPSB' )" +
                          " order by dbms_random.value  ) t where rownum=1";

                        
                        oDbCommand.CommandText = sSql;
                        oDbCommand.CommandType = CommandType.Text;
                        OracleDataReader oReader = oDbCommand.ExecuteReader();
                        
                        while (oReader.Read())
                        {
                            siblTransactionId = oReader["SIBLTRASNSACTIONID"].ToString();
                            mgiTransactionId = oReader["MGITRANSACTIONID"].ToString();
                            sCharge = oReader["CHARGE"].ToString();
                            sAmount = oReader["RECEIVERAMOUNT"].ToString();
                            //toAccountNumber = oReader["TOACCNUM"].ToString();
                            //fromAccountNumber = oReader["ACCOUNTNO"].ToString();
                            transactionType = oReader["TRASACTIONTYPE"].ToString();
                            receiverAccNum = oReader["ReceiverAccNum"].ToString();
                            GLtoRemitTransaction = oReader["GLTOREMITCBS"].ToString();
                            hasRows = 1;

                        }

                      
                        //Transaction Start Message
                        CLog.Logger.Write(CLog.INFORMATION, "/Transaction Start:: mgiTransactionId ID:" + mgiTransactionId);
                        CLog.Logger.Write(CLog.INFORMATION, "                  :: siblTransactionId:" + siblTransactionId);

                        //Select account Number based on the Transaction Type

                        string OwnBank = sSIBL;// "SOCIAL ISLAMI BANK LTD";

                       

                        // END OF SETTING BEFTN ACCOUNT NUMBER

                        #region CheckAccountNumber
                        // new code dated on 17.09.2017
                        // iValidAccount 1: account number is valid
                        // iValidAccount 0: account number is invalid
                        int iValidAccount = 1;


                        if (transactionType.Equals(sSIBL))
                        {
                            // Check account Number 
                            toAccountNumber = receiverAccNum;
                            accountResponse = isValidAccount(toAccountNumber, mgiTransactionId);
                           

                            try {

                                if (!accountResponse.accountDetail.accountStatus.Equals("ACTIVATED"))
                                {
                                    // invalid account number for own bank account
                                    iValidAccount = 0;
                                    // Update CBS status and Disbursement Type for this instruction id
                                    sSql = " Update TRANSACTIONINFO set CBSSTATUS=2,CBSDATE=sysdate,MGSTATUS='02',PUSHRESPONSE='1',PUSHRESPONSEDATE=sysdate, FINALSTATUS='REJECTED' where SIBLTRASNSACTIONID ='" + siblTransactionId + "'";

                                    oDbCommand.CommandText = sSql;
                                    oDbCommand.CommandType = CommandType.Text;
                                    int ii = oDbCommand.ExecuteNonQuery();

                                    // Write Log For this invalid account Number
                                    CLog.Logger.Write(CLog.ERROR, "/Invalid Account number:" + sAccountNumber + " for siblTransactionId: " + siblTransactionId);
                                }
                                else
                                {
                                    CLog.Logger.Write(CLog.ERROR, "/valid Account number:" + sAccountNumber + " for instruction id: " + instructionId);
                                }
                            
                            }
                            catch (Exception)
                            {
                                try {

                                    if (accountResponse.transactionStatus==transactionStatus.FAIL)
                                    {
                                        // invalid account number for own bank account
                                        iValidAccount = 0;
                                        // Update CBS status and Disbursement Type for this instruction id
                                        sSql = " Update TRANSACTIONINFO set CBSSTATUS=2,CBSDATE=sysdate,MGSTATUS='02',PUSHRESPONSE='1',PUSHRESPONSEDATE=sysdate, FINALSTATUS='REJECTED' where SIBLTRASNSACTIONID ='" + siblTransactionId + "'";

                                        oDbCommand.CommandText = sSql;
                                        oDbCommand.CommandType = CommandType.Text;
                                        int ii = oDbCommand.ExecuteNonQuery();

                                    }
                                    else
                                    {

                                    }

                                }
                                catch (Exception exp)
                                { 
                                
                                }
                            }
                           

                        }

                        // End of new code dated on 17.09.2017
                        #endregion CheckAccountNumber

                        #endregion End of Select RandomInstruction for CBS

                        if (hasRows == 1 && iValidAccount==1)
                        {


                            #region LockTable for Update Status
                            //lock table for update

                            oDbCommand.CommandText = " select * from TRANSACTIONINFO where SIBLTRASNSACTIONID='" + siblTransactionId + "'  FOR UPDATE OF CBSSTATUS";
                            oDbCommand.CommandType = CommandType.Text;
                            OracleDataReader oReaderLock = oDbCommand.ExecuteReader();
                            // end of lock table for update
                            #endregion LockTable for Update Status
                            #region Check Available Balance

                           
                            if (sAmount != "")
                                dBalance = Convert.ToDecimal(sAmount);

                            // balance is available
                            //sCurrency = "BDT";
                            accountResponse = new AccountInformationResponse();
                            //Check balance of Foreign Remit Account
                            accountResponse = isValidAccount(ForeignRemitAccount, mgiTransactionId);
                           
                            decimal iAbabilBalance = accountResponse.accountDetail.accountBalance;
                           // decimal iAbabilBalance = 0;
                            if (iAbabilBalance >= dBalance)
                            {
                                #region BalanceAvailable
                                ///Till now ID is permitting Transaction though there is not enough balance.
                                ///Once we get clearance from ID that, payment will be only done if there is
                                ///available balance, this block of code will be active 
                              
                                //Add DoAbabilTransaction Block

                                CResult oResAppId = new CResult();
                                // set BEFTN ACCOUNT NUMBER
                                if (transactionType.Equals(sSIBL))
                                {
                                    //Transaction Type : GL to Savings
                                    fromAccountNumber = MoneyGramAccountGLID;
                                    toAccountNumber = receiverAccNum;
                                    oResAppId = ProcessOwnBankTransaction(oDbCommand, ForeignRemitAccount, receiverAccNum, sAmount, mgiTransactionId, siblTransactionId);
                                }
                                if (transactionType.Equals(sNPSB))
                                {
                                    //fromAccountNumber = MoneyGramAccountGLID;

                                    //GLtoRemitTransaction:1 means Gl to Remit transaction is done 
                                    //if (GLtoRemitTransaction.Equals(""))
                                    //{
                                    //    // GL to Remit transaction is not done. Doing new Transaction
                                    //    oResAppId = TransferAmountGLToRemitAccount113(oDbCommand, fromAccountNumber, ForeignRemitAccount, sAmount, mgiTransactionId, siblTransactionId);
                                    //}
                                    // Transferring amount Remit account to NPSB
                                    TransferAmountRemitAccount113ToGL(oDbCommand, ForeignRemitAccount, NPSBAccountGLID, sAmount, mgiTransactionId, siblTransactionId);
                                    //if (oResAppId.ResponseCode != "")
                                    //    oResAppId = TransferAmountRemitAccount113ToGL(oDbCommand, ForeignRemitAccount, NPSBAccountGLID, sAmount, mgiTransactionId, siblTransactionId);

                                }
                                if (transactionType.Equals(sBEFTN))
                                {
                                    //fromAccountNumber = MoneyGramAccountGLID;
                                    //oResAppId = TransferAmountGLToRemitAccount113(oDbCommand, fromAccountNumber, ForeignRemitAccount, sAmount, mgiTransactionId, siblTransactionId);

                                    sSql = "Update TRANSACTIONINFO set CBSSTATUS='',CBSDATE='' " +
                                                            " where SIBLTRASNSACTIONID = '" + siblTransactionId + "'";


                                    oDbCommand.CommandText = sSql;
                                    oDbCommand.CommandType = CommandType.Text;
                                    int iCount = oDbCommand.ExecuteNonQuery();
                                }
                                #endregion AvailableBalance
                            }
                            else
                            {  
                                    // balance is not available
                                    // There is not enough xoom balance for a transaction
                                    #region Insufficient Fund
                                    // this region will be used to check insufficient balance
                                    // information. this option will be developed later.
                                    //  CBSSTATUS 4: INSUFFICIENT BALANCE
                                    //

                                    sSql = " Update TRANSACTIONINFO set CBSSTATUS=4,CBSDATE=sysdate  where SIBLTRASNSACTIONID ='" + siblTransactionId + "'";
                                   
                                    oDbCommand.CommandText = sSql;
                                    oDbCommand.CommandType = CommandType.Text;
                                    int iCount1 = oDbCommand.ExecuteNonQuery();

                                    Console.WriteLine("MG insufficient Balanace for#" + siblTransactionId);


                                    #endregion Insufficient Fund

                                    CLog.Logger.Write(CLog.INFORMATION, "/ProcessTransactions:Insufficient MG balance for#" + siblTransactionId);
                            }
                            
                            #endregion Check Available Balance


                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(DateTime.Now.ToShortTimeString()+"::No More Transaction is available for CBS");
                            CLog.Logger.Write(CLog.INFORMATION, "/ProcessTransactions: No More Transaction is available");
                        }
                        
                    }
                    catch (Exception exp)
                    {
                       // oTranasaction.Rollback();
                        CLog.Logger.Write(CLog.ERROR, exp.Message);
                    }
                    finally
                    {   // Added exception handling with Transaction Commit Error : A Rahim Khan
                        try
                        {
                            //oTranasaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            CLog.Logger.Write(CLog.ERROR, "ProcessTransactions::oTranasaction.Commit" + ex.Message);
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

        /// <summary>
        /// This function will return whether an account number is valid or invalid
        /// 0: Invalid account Number
        /// 1: Valid account Number
        /// </summary>
        /// <param name="sAccountNo"></param>
        /// <returns></returns>
        private int IsValidAccountNo2(string sAccountNo)
        {
            #region VariableDeclaration

            int hasRows = 0;// hasRows=0 :Invalid accountnumber
                            // hasRows=1 :Valid accountnumber


            #endregion VariableDeclaration

            // end of format date
            try
            {
                using (OracleConnection oConnection = new OracleConnection(GetConnectionStrings(DB_CONNECTION_CBS)))
                {
                    oConnection.Open();

                    OracleCommand oDbCommand = oConnection.CreateCommand();

                    try
                    {

                       string ssQl = " select count(*) isAvailable from account where ACCCODE= '" + sAccountNo + "'"; 

                        oDbCommand.CommandText = ssQl;
                        oDbCommand.CommandType = CommandType.Text;
                        OracleDataReader oReader = oDbCommand.ExecuteReader();


                        if (oReader.HasRows)
                        {
                            oReader.Read();
                            hasRows =Convert.ToInt16(oReader["isAvailable"].ToString());
                            oReader.Close();
                        }

                    }
                    catch (Exception exp)
                    {

                        CLog.Logger.Write(CLog.INFORMATION, "/CheckAccountNo" + exp.Message);

                    }
                    finally
                    {
                        oConnection.Close();

                    }
                }

            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.INFORMATION, "/CheckAccountNo" + exp.Message);
            }

            return hasRows;

        }

        private decimal AvailableBalance(string currency)
        {
            #region VariableDeclaration

            int hasRows = 0;

            string GLBDT = ConfigurationManager.AppSettings["GLBDT"].ToString();
            string GLUSD = ConfigurationManager.AppSettings["GLUSD"].ToString();
            string branchCode = ConfigurationManager.AppSettings["branchCode"].ToString();
            string sBalance = string.Empty;

            decimal dBalance = 0;

            string sGLNo = string.Empty;

            if (currency.ToUpper().Equals("BDT"))
                sGLNo = GLBDT;
            else if (currency.ToUpper().Equals("USD"))
                sGLNo = GLUSD;


            #endregion VariableDeclaration

            // end of format date
            try
            {
                using (OracleConnection oConnection = new OracleConnection(GetConnectionStrings(DB_CONNECTION_CBS)))
                {
                    oConnection.Open();

                    OracleCommand oDbCommand = oConnection.CreateCommand();

                    try
                    {

                        DateTime dtBF = DateTime.Now;
                        DateTime dtAcc = DateTime.Now;

                        string sBFDate = dtBF.Day.ToString().PadLeft(2, '0') + "-" + dtBF.Month.ToString().PadLeft(2, '0') + "-" + dtBF.Year.ToString();

                        string ssQl = "";
                        if (currency.ToUpper().Equals("USD"))
                        {
                            // for USD Balance
                            ssQl = " SELECT SUM(CASE WHEN TXNCODE=102 THEN (TRAMOUNT) END)- SUM(CASE WHEN TXNCODE=202 THEN (TRAMOUNT) END) BF_BAL " +
                                   " FROM FC_GL_ACCOUNTSTATEMENT_VW " +
                                   " WHERE ACCID='" + sGLNo + "'" +
                                   " AND TRDATE <= to_date('" + sBFDate + "','DD-MM-YYYY')" +
                                   " AND FCT_BRANCHID=" + branchCode;
                        }
                        else if (currency.ToUpper().Equals("BDT"))
                        {
                            // Update query on 21.05.2019
                            ssQl = @"Select rpt_balancing.GETPARENTBALANCE(" + sGLNo + ",to_date('" + sBFDate + "','DD-mm-YYYY')," + branchCode + ") BF_BAL from dual";
                            //ssQl = @"SELECT  GLADGLID, GLADBALANCE as BF_BAL,GLADBRID from GLACCOUNTDETAIL where GLADGLID='" + sGLNo + "'";
                        }

                        oDbCommand.CommandText = ssQl;
                        oDbCommand.CommandType = CommandType.Text;
                        OracleDataReader oReader = oDbCommand.ExecuteReader();


                        if (oReader.HasRows)
                        {
                            oReader.Read();
                            sBalance = oReader["BF_BAL"].ToString();
                            oReader.Close();
                        }

                        if (sBalance != "")
                            dBalance = Convert.ToDecimal(sBalance);


                    }
                    catch (Exception exp)
                    {
                        dBalance = -100;//Exception Occur in Convertion
                        CLog.Logger.Write(CLog.INFORMATION, "/CheckInsufficientBalance" + exp.Message);

                    }
                    finally
                    {
                        oConnection.Close();

                    }
                }

            }
            catch (Exception exp)
            {
                dBalance = -100;//Exception Occur in Connection
                CLog.Logger.Write(CLog.INFORMATION, "/CheckInsufficientBalance" + exp.Message);
            }

            return dBalance;

        }

    }
}
