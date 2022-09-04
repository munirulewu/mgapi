using Oracle.ManagedDataAccess.Client;
using SIBLCommon.Common.Entity.Result;
using SIBLCommon.Common.Util.Attributes;
using SIBLCommon.Common.Util.Logger;
using SIBLCommon.SIBLCommon.Common.Entity.MGAPI;
using SIBLRemit.DA.Common.Connections;
using SIBLXoomCommon.SIBLXoom.Common.Entity.CPU;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SIBLRemitDA.SIBLRemit.DA.MGAPIDA
{
    public class CMGAPIDA
    {
        const string DB_CONNECTION_Bkash = "ConStringBkash";
        const string DB_CONNECTION_MGDB = "ConStringAPP";

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



        [Author("Md. Aminul Islam", "28-05-2022", "Save Transaction Information")]
        public CResult CRUD_Transaction(CTransaction oTransaction)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();

             
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection(DB_CONNECTION_MGDB))
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_MGSend_API.CRUD_TransactionInfo");
                    try
                    {

                        oDatabase.AddInOutParameter(oDbCommand, "P_TRANSID", DbType.String, 50, oTransaction.transaction.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SIBLTRASNSACTIONID", DbType.String, 50, oTransaction.transaction.siblTransactionId);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SOURCEOFFUND", DbType.String, 200, oTransaction.transaction.SouceofFund);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ACCOUNTCODE", DbType.String, 200, oTransaction.accountCode);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ACCOUNTNUMBER", DbType.String, 200, oTransaction.accountNumber);
                        oDatabase.AddInOutParameter(oDbCommand, "P_RECEIVERCOUNTRYCODE", DbType.String, 200, oTransaction.transaction.receiveCountryCode);
                        oDatabase.AddInOutParameter(oDbCommand, "P_RECEIVERAMOUNT", DbType.String, 200, oTransaction.transaction.receiveAmount.value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_RECEIVERCURRENCY", DbType.String, 200, oTransaction.transaction.receiveAmount.currencyCode);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SENDCOUNTRYCODE", DbType.String, 200, oTransaction.transaction.sendCountryCode);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SENDAMOUNT", DbType.String, 200, oTransaction.transaction.sendAmount.value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SENDCURRENCY", DbType.String, 200, oTransaction.transaction.sendAmount.currencyCode);

                        oDatabase.AddInOutParameter(oDbCommand, "P_SENDERPHONE", DbType.String, 200, oTransaction.transaction.sender.person.mobileNo);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SENDERADDRESS", DbType.String, 200, oTransaction.transaction.sender.person.address);


                        oDatabase.AddInOutParameter(oDbCommand, "P_TRASACTIONTYPE", DbType.String, 200, oTransaction.transaction.TransactionType);
                        oDatabase.AddInOutParameter(oDbCommand, "P_MGITRANSACTIONID", DbType.String, 200, oTransaction.transaction.mgiTransactionId);
                        oDatabase.AddInOutParameter(oDbCommand, "P_S_FIRSTNAME", DbType.String, 200, oTransaction.transaction.sender.person.firstName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_S_MIDDLENAME", DbType.String, 200, oTransaction.transaction.sender.person.middleName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_S_LASTNAME", DbType.String, 200, oTransaction.transaction.sender.person.lastName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_R_FIRSTNAME", DbType.String, 200, oTransaction.transaction.receiver.person.firstName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_R_MIDDLENAME", DbType.String, 200, oTransaction.transaction.receiver.person.middleName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_R_LASTNAME", DbType.String, 200, oTransaction.transaction.receiver.person.lastName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_OperationType", DbType.String, 200, oTransaction.transaction.operationType);
                        oDatabase.AddInOutParameter(oDbCommand, "P_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_statusCode", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_statusMsg", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_return_msg", DbType.String, 500, DBNull.Value);
                        int i = oDatabase.ExecuteNonQuery(oDbCommand);
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                        string success = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_success")]).Value.ToString();

                        string statusCode = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_statusCode")]).Value.ToString();
                        string statusMsg = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_statusMsg")]).Value.ToString();
                        string siblTransactionId = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_SIBLTRASNSACTIONID")]).Value.ToString();
                       
                        if (success == "1")
                        {
                            oResult.Result = true;
                            CSuccessResponse oResponse = new CSuccessResponse();
                            oResponse.partnerTransactionId = siblTransactionId;
                            oResponse.response.message = statusMsg;
                            oResponse.response.responseCode = statusCode;
                            oResult.Return = oResponse;
                        }
                        else
                        {
                            //CMGErrorResponse oResponse = new CMGErrorResponse();
                            oResult.Result = false;
                            //oResponse.message = statusMsg;
                            //oResponse.code = statusCode;
                            //oResponse.target=

                            CSuccessResponse oResponse = new CSuccessResponse();
                            oResponse.partnerTransactionId = siblTransactionId;
                            oResponse.response.message = statusMsg;
                            oResponse.response.responseCode = statusCode;
                            oResult.Return = oResponse;
                        }

                        oResult.Status = success;
                        oResult.Message = sMessage;
                        CLog.Logger.Write(CLog.INFORMATION, "/CRUD_Transaction: " + sMessage);
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/CRUD_Transaction: " + exp.Message);
                        oResult.Exception = exp;
                        oResult.Message = exp.Message;
                        oResult.Result = false;
                        oResult.Return = null;
                    }
                    finally
                    {
                        oConnection.Close();
                    }
                }
                return oResult;
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/CRUD_Transaction: " + exp.Message);
                return oResult;
            }

        }

        [Author("Md. Munirul Islam", "04-06-2022", "Save Transaction to bKash DB for Validation")]
        public CResult CRUD_bKashValidation(CbKashValidationRequest oTransaction)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            string responseCode ="";
            CSuccessResponse oResponse = new CSuccessResponse();
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection(DB_CONNECTION_Bkash))
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_MGBKASH.ADD_TransactionInfo");
                    try
                    {

                        oDatabase.AddInOutParameter(oDbCommand, "P_TID", DbType.String, 50, oTransaction.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_PINNO", DbType.String, 50, oTransaction.mgiTransactionId);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SIBLTRANSACTIONO", DbType.String, 200, oTransaction.siblTransactionId);
                        oDatabase.AddInOutParameter(oDbCommand, "P_COUNTRYCODE", DbType.String, 50, oTransaction.receiveCountryCode);
                        oDatabase.AddInOutParameter(oDbCommand, "P_MSISDN", DbType.String, 50, oTransaction.accountNumber);
                        oDatabase.AddInOutParameter(oDbCommand, "P_FIRSTNAME", DbType.String, 200, oTransaction.receiver.person.firstName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_LASTNAME", DbType.String, 200, oTransaction.receiver.person.lastName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_FULLNAME", DbType.String, 200, oTransaction.receiver.person.fullName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_CONVERSATIONID", DbType.String, 200, oTransaction.convertionId);
                        oDatabase.AddInOutParameter(oDbCommand, "P_CONVERSATIONDATE", DbType.String, 200, oTransaction.convertionDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_AMOUNT", DbType.String, 200, oTransaction.receiveAmount.value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_CORRIDOR", DbType.String, 200, oTransaction.sendCountryCode);
                        oDatabase.AddInOutParameter(oDbCommand, "P_OperationType", DbType.String, 200, oTransaction.operationType);
                        oDatabase.AddInOutParameter(oDbCommand, "P_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_return_msg", DbType.String, 500, DBNull.Value);

                        int i = oDatabase.ExecuteNonQuery(oDbCommand);
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                        string success = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_success")]).Value.ToString();
                        string siblTransactionId = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_SIBLTRANSACTIONO")]).Value.ToString();
                        string sTID = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_TID")]).Value.ToString();

                        if (success == "1")
                        {
                            oResult.Result = true;
                            oTransaction.siblTransactionId = siblTransactionId;
                            oResult.Return = oTransaction;
                        }
                        else
                        {
                            oResult.Result = false;
                        }
                        
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/CRUD_bKashValidation: " + exp.Message);
                        oResult.Exception = exp;
                        oResult.Message = exp.Message;
                        oResult.Result = false;
                        oResult.Return = null;
                    }
                    finally
                    {
                        oConnection.Close();
                    }
                }
                return oResult;
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/CRUD_bKashValidation: " + exp.Message);
                return oResult;
            }

        }
        [Author("Md. Munirul Islam", "04-06-2022", "Save Transaction to bKash DB for Validation")]
        public CResult GetbKashCalBackResult(CbKashValidationRequest oTransaction)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            string responseCode = "";
            int iCount = 1;
            CSuccessResponse oResponse = new CSuccessResponse();
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection(DB_CONNECTION_Bkash))
                {
                    IDbCommand oDbCommand1 = oDatabase.GetStoredProcICommand("PKG_MGBKASH.Get_ValidationResult");

                    try
                    {

                        string siblTransactionId = oTransaction.siblTransactionId;
                        string responseMsg = "";
                        string success = "";
                         

                        if (oTransaction.siblTransactionId != "")
                        {

                            while (responseCode == "")
                            {
                                CLog.Logger.Write(CLog.INFORMATION, "Try:" + iCount.ToString() + " Start: P_PINNO:" + oTransaction.siblTransactionId);
                                CLog.Logger.Write(CLog.INFORMATION, "/CRUD_bKashValidation: P_CONVERSATIONID:" + oTransaction.convertionId);

                                oDatabase.AddInOutParameter(oDbCommand1, "P_PINNO", DbType.String, 50, siblTransactionId);
                                oDatabase.AddInOutParameter(oDbCommand1, "P_CONVERSATIONID", DbType.String, 50, oTransaction.convertionId);
                                oDatabase.AddInOutParameter(oDbCommand1, "p_responseCode", DbType.String, 20, DBNull.Value);

                                oDatabase.AddInOutParameter(oDbCommand1, "p_success", DbType.String, 50, DBNull.Value);
                                oDatabase.AddInOutParameter(oDbCommand1, "p_return_msg", DbType.String, 300, DBNull.Value);
                                int i = oDatabase.ExecuteNonQuery(oDbCommand1);

                                success = ((OracleParameter)oDbCommand1.Parameters[oDatabase.BuildParameterName("p_success")]).Value.ToString();
                                responseCode = ((OracleParameter)oDbCommand1.Parameters[oDatabase.BuildParameterName("p_responseCode")]).Value.ToString();
                                responseMsg = ((OracleParameter)oDbCommand1.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();


                                

                                oDbCommand1.Parameters.Clear();
                                CLog.Logger.Write(CLog.INFORMATION, "Try:" + iCount.ToString() + " END: P_PINNO:" + oTransaction.siblTransactionId);
                                iCount++;
                            }
                            CMgBkashSuccess oResponsesuccess = new CMgBkashSuccess();
                            CErrorResponse oResponError = new CErrorResponse();
                            if (success == "1")
                            {
                                oResult.Result = true;
                                oResponsesuccess = new CMgBkashSuccess();
                                oResponsesuccess.message = responseMsg;
                                oResult.Return = oResponsesuccess;
                            }
                            else
                            {
                                oResult.Result = false;
                                oResponError.error.code = responseCode;
                                oResponError.error.message = responseMsg;
                                oResult.Return = oResponError;
                            }

                        }
                        else
                        {
                            CLog.Logger.Write(CLog.INFORMATION, "/GetbKashCalBackResult: SIBLtransaction ID not supplied. ");
                        }
                        
                         
                            CLog.Logger.Write(CLog.INFORMATION, "/GetbKashCalBackResult: " + responseCode + ":" + responseMsg);
                        //}
                        
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetbKashCalBackResult: " + exp.Message);
                        oResult.Exception = exp;
                        oResult.Message = exp.Message;
                        oResult.Result = false;
                        oResult.Return = null;
                    }
                    finally
                    {
                        oConnection.Close();
                    }
                }
                return oResult;
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetbKashCalBackResult: " + exp.Message);
                return oResult;
            }

        }
        [Author("Md. Munirul Islam", "04-06-2022", "Save Transaction to bKash DB for Validation")]
        public CResult bKashAccValidation(CTransaction oTransaction)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection(DB_CONNECTION_Bkash))
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_MGBKASH.ADD_TransactionInfo");
                    try
                    {

                        oDatabase.AddInOutParameter(oDbCommand, "P_TID", DbType.String, 50, oTransaction.transaction.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_PINNO", DbType.String, 50, oTransaction.transaction.mgiTransactionId);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SIBLTRANSACTIONO", DbType.String, 200, oTransaction.transaction.siblTransactionId);
                        oDatabase.AddInOutParameter(oDbCommand, "P_COUNTRYCODE", DbType.String, 50, oTransaction.transaction.receiveCountryCode);
                        oDatabase.AddInOutParameter(oDbCommand, "P_MSISDN", DbType.String, 50, oTransaction.accountNumber);
                        oDatabase.AddInOutParameter(oDbCommand, "P_FIRSTNAME", DbType.String, 200, oTransaction.transaction.receiver.person.firstName);

                        oDatabase.AddInOutParameter(oDbCommand, "P_LASTNAME", DbType.String, 200, oTransaction.transaction.receiver.person.lastName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_FULLNAME", DbType.String, 200, oTransaction.transaction.receiver.person.fullName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_CONVERSATIONID", DbType.String, 200, oTransaction.transaction.convertionId);
                        oDatabase.AddInOutParameter(oDbCommand, "P_CONVERSATIONDATE", DbType.String, 200, oTransaction.transaction.convertionDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_AMOUNT", DbType.String, 200, oTransaction.transaction.receiveAmount.value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_CORRIDOR", DbType.String, 200, oTransaction.transaction.sendCountryCode);

                        oDatabase.AddInOutParameter(oDbCommand, "P_OperationType", DbType.String, 200, oTransaction.transaction.operationType);
                        oDatabase.AddInOutParameter(oDbCommand, "P_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_return_msg", DbType.String, 500, DBNull.Value);
                        int i = oDatabase.ExecuteNonQuery(oDbCommand);
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                        string success = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_success")]).Value.ToString();
                        string siblTransactionId = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_SIBLTRANSACTIONO")]).Value.ToString();
                        string sTID = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_TID")]).Value.ToString();

                        Thread.Sleep(1000);
                        IDbCommand oDbCommand1 = oDatabase.GetStoredProcICommand("PKG_MGBKASH.Get_ValidationResult");


                        oDatabase.AddInOutParameter(oDbCommand1, "P_PINNO", DbType.String, 50, siblTransactionId);
                        oDatabase.AddInOutParameter(oDbCommand1, "p_responseCode", DbType.String, 20, DBNull.Value);

                        oDatabase.AddInOutParameter(oDbCommand1, "p_success", DbType.String, 50, oTransaction.transaction.receiveCountryCode);
                        oDatabase.AddInOutParameter(oDbCommand1, "p_return_msg", DbType.String, 300, oTransaction.accountNumber);
                        i = oDatabase.ExecuteNonQuery(oDbCommand1);

                        success = ((OracleParameter)oDbCommand1.Parameters[oDatabase.BuildParameterName("p_success")]).Value.ToString();
                        string responseCode = ((OracleParameter)oDbCommand1.Parameters[oDatabase.BuildParameterName("p_responseCode")]).Value.ToString();
                        string responseMsg = ((OracleParameter)oDbCommand1.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();



                        oResult.Result = true;
                        CSuccessResponse oResponse = new CSuccessResponse();
                        oResponse.partnerTransactionId = "";
                        oResponse.response.responseCode = responseCode;
                        oResponse.response.message = responseMsg;
                        oResult.Return = oResponse;

                        CLog.Logger.Write(CLog.INFORMATION, "/CRUD_bKashValidation: " + responseCode + ":" + responseMsg);
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/CRUD_bKashValidation: " + exp.Message);
                        oResult.Exception = exp;
                        oResult.Message = exp.Message;
                        oResult.Result = false;
                        oResult.Return = null;
                    }
                    finally
                    {
                        oConnection.Close();
                    }
                }
                return oResult;
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/CRUD_bKashValidation: " + exp.Message);
                return oResult;
            }

        }


        public CResult SaveDisbursementAbabilResponse(CAbabilTransactionResponse oAbabilTransactionResponse)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection(DB_CONNECTION_MGDB))
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKGCBS.SAVECBS_RESPONSE");
                    try
                    {

                        oDatabase.AddInOutParameter(oDbCommand, "P_RESID", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_DESCRTRIPTION", DbType.String, 200, oAbabilTransactionResponse.Description);
                        oDatabase.AddInOutParameter(oDbCommand, "P_RESPONSERREF", DbType.String, 100, oAbabilTransactionResponse.ResponseReference);
                        oDatabase.AddInOutParameter(oDbCommand, "P_REQUESTREF", DbType.String, 100, oAbabilTransactionResponse.RequestReference);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ISREVERSE", DbType.String, 50, '0');
                        oDatabase.AddInOutParameter(oDbCommand, "P_REVERSEBY", DbType.String, 200, oAbabilTransactionResponse.User.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_CREATEBY", DbType.String, 50, oAbabilTransactionResponse.User.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_APPREQUESTID", DbType.String, 50, oAbabilTransactionResponse.AppRequestId);
                        oDatabase.AddInOutParameter(oDbCommand, "P_OperationType", DbType.String, 50, oAbabilTransactionResponse.OperationType);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);
                        int i = oDatabase.ExecuteNonQuery(oDbCommand);
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                        string success = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_success")]).Value.ToString();


                        if (success == "1")
                        {
                            oResult.Result = true;
                            oResult.Message = sMessage;
                        }
                        else
                        {
                            oResult.Result = false;
                            oResult.Message = sMessage;
                        }

                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/UnDisbursementInfo: " + exp.Message);
                        oResult.Exception = exp;
                        oResult.Message = exp.Message;
                        oResult.Result = false;
                        oResult.Return = null;
                    }
                    finally
                    {
                        oConnection.Close();
                    }
                }
                return oResult;
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/UnDisbursementInfo: " + exp.Message);
                return oResult;
            }

        }
    }
}
