using Oracle.DataAccess.Client;
using SIBLGlobalAPI.DA.Common.Connections;
using SIBLCommon.Common.Entity.Result;
using SIBLCommon.Common.Entity.Titas;
using SIBLCommon.Common.Util.Attributes;
using SIBLCommon.Common.Util.Logger;
using SIBLCommon.SIBLCommon.Common.Entity.bKash;
using SIBLCommon.SIBLCommon.Common.Entity.Titas;
using SIBLXoomCommon.SIBLXoom.Common.Entity.CPU;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace SIBLBKashDA.SIBLBKash.DA.bKashAPI
{
   public  class CbKashAPIDA
    {
        const string DB_CONNECTION = "ConString";
        const string DB_CONNECTION_SIBLSGCL = "ConStringSIBLBKASHAPI";
        
       const string RESPONSE_SUCCESS = "0000";
       const string RESPONSE_ERROR = "1000";
       
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
       private void AddInOutParameter(string sParam, OracleCommand oDbCommand, string[] sValue, int iSize)
        {
            OracleParameter Param = new OracleParameter(sParam, OracleDbType.Varchar2);
            Param.Direction = ParameterDirection.InputOutput;
            Param.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
            Param.Value = sValue;
            Param.Size = iSize;
        }
       private string PrintNewLine()
       {
           return Environment.NewLine;
       }
        [Author("Munirul Islam", "18-08-2020", "UpDateConversationID")]
        public CResult UpDateConversationID(BeneficiaryAccount oAccount)
        {

            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            string sMessage = "";
            string sTokenValue = "";
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_TITAS.GetAccessToken");

                    try
                    {
                        //oDatabase.AddInOutParameter(oDbCommand, "p_sTokenDate", DbType.String, 50, oAccessToken.TokenDate);
                        //oDatabase.AddInOutParameter(oDbCommand, "p_sTokenValue", DbType.String, 500, oAccessToken.access_token);
                        //oDatabase.AddInOutParameter(oDbCommand, "p_sCreator", DbType.String, 50, oAccessToken.Creator);
                        //oDatabase.AddInOutParameter(oDbCommand, "p_sExpiresIn", DbType.String, 50, oAccessToken.expires_in);
                        //oDatabase.AddInOutParameter(oDbCommand, "p_sTokenType", DbType.String, 50, oAccessToken.token_type);
                        //oDatabase.AddInOutParameter(oDbCommand, "p_sMeterType", DbType.String, 50, oAccessToken.MeterType);
                        //oDatabase.AddInOutParameter(oDbCommand, "p_Success", DbType.String, 50, DBNull.Value);
                        //oDatabase.AddInOutParameter(oDbCommand, "p_Message", DbType.String, 50, DBNull.Value);
                        //oDatabase.ExecuteNonQuery(oDbCommand);
                        string sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Success")]).Value.ToString();
                        sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Message")]).Value.ToString();
                        sTokenValue = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_sTokenValue")]).Value.ToString();



                        if (sTokenValue != "")
                        {
                           

                        }
                        else
                        {
                            oResult.Result = false;
                            oResult.Message = sMessage;
                        }


                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/UpDateConversationID: " + exp.Message);
                        oResult.Exception = exp;
                        oResult.Message = exp.Message;
                        oResult.Result = false;
                        oResult.Return = null;
                    }
                    finally
                    {
                        oConnection.Close();
                        //oConnection.Dispose();
                        CLog.Logger.Write(CLog.SUCCESS, this.ToString() + "UpDateConversationID:" + PrintNewLine() + " Token :" + sTokenValue + PrintNewLine() + "DBMessage:" + sMessage);
                    }
                }
                return oResult;
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/UpDateConversationID: " + exp.Message);
                return oResult;
            }

        }

       [Author("Munirul Islam", "18-08-2020", "UpdateRemitTransferPendingStatus")]
       public CResult UpdateRemitTransferPendingStatus(RemitStatusCallBackReq oCallBackRequest)
        {

            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            string sMessage = "";
            CCallBackResult oCallBackResult = new CCallBackResult();
            BAVResponse oResponse = new BAVResponse();
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_REMITAPI.updateremitStatusCallBackRes");

                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_ConversationID", DbType.String, 50, oCallBackRequest.conversionID);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ResponseCode", DbType.String, 50, oCallBackRequest.ResponseCode);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ResponseMsg", DbType.String, 500, oCallBackRequest.ResponseDescription);

                       
                        oDatabase.AddInOutParameter(oDbCommand, "P_SIBLTRANSACTIONNO", DbType.String, 50, oCallBackRequest.SIBLREFNO);

                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);
                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        string sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_success")]).Value.ToString();
                        sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();


                        oResponse.ResponseDescription = sMessage;
                        oResponse.conversionID = oCallBackRequest.conversionID;

                        if (sSuccess == "1")
                        {

                            oResponse.ResponseCode = RESPONSE_SUCCESS;
                            oResult.Result = true;
                            oResult.Return = oResponse;

                        }
                        else
                        {
                            oResponse.ResponseCode = RESPONSE_ERROR;
                            oResult.Result = false;

                        }
                        oResult.Return = oResponse;


                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/UpdateRemitTransferPendingStatus: " + exp.Message);
                        oResult.Exception = exp;
                        oResult.Message = exp.Message;
                        oResult.Result = false;
                        oResult.Return = null;
                    }
                    finally
                    {
                        oConnection.Close();
                        //oConnection.Dispose();

                    }
                }
                return oResult;
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/UpdateRemitTransferPendingStatus: " + exp.Message);
                return oResult;
            }

        }

       [Author("Munirul Islam", "18-08-2020", "UpdateRemitTransferStatus")]
       public CResult UpdateRemitTransferStatus(RemitCallBack oCallBackRequest)
        {

            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            string sMessage = "";
            CCallBackResult oCallBackResult = new CCallBackResult();
            BAVResponse oResponse = new BAVResponse();
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_REMITAPI.updateremitCallBackRes");

                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_ConversationID", DbType.String, 50, oCallBackRequest.conversionID);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ResponseCode", DbType.String, 50, oCallBackRequest.ResponseCode);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ResponseMsg", DbType.String, 500, oCallBackRequest.ResponseDescription);

                        oDatabase.AddInOutParameter(oDbCommand, "P_msisdn", DbType.String, 50, oCallBackRequest.msisdn);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SIBLTRANSACTIONNO", DbType.String, 50, oCallBackRequest.SIBLREFNO);

                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);
                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        string sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_success")]).Value.ToString();
                        sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();


                        oResponse.ResponseDescription = sMessage;
                        oResponse.conversionID = oCallBackRequest.conversionID;
                        
                        if (sSuccess == "1")
                        {
                            
                            oResponse.ResponseCode =RESPONSE_SUCCESS;
                            oResult.Result = true;
                            oResult.Return = oResponse;

                        }
                        else
                        {
                            oResponse.ResponseCode = RESPONSE_ERROR;
                            oResult.Result = false;

                        }
                        oResult.Return = oResponse;


                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/BeneficiaryAccValidation: " + exp.Message);
                        oResult.Exception = exp;
                        oResult.Message = exp.Message;
                        oResult.Result = false;
                        oResult.Return = null;
                    }
                    finally
                    {
                        oConnection.Close();
                        //oConnection.Dispose();

                    }
                }
                return oResult;
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/BeneficiaryAccValidation: " + exp.Message);
                return oResult;
            }

        }

       [Author("Munirul Islam", "18-08-2020", "BeneficiaryAccValidation")]
       public CResult BeneficiaryAccValidation(CallBackRequest oCallBackRequest)
        {

            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            string sMessage = "";
            CCallBackResult oCallBackResult = new CCallBackResult();
            BAVResponse oResponse = new BAVResponse();
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_REMITAPI.beneCallBackRes");

                    try
                    {
                                   
                        oDatabase.AddInOutParameter(oDbCommand, "P_ConversationID", DbType.String, 50, oCallBackRequest.conversionID);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ResponseCode", DbType.String, 50, oCallBackRequest.ResponseCode);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ResponseMsg", DbType.String, 500, oCallBackRequest.ResponseDescription);

                        oDatabase.AddInOutParameter(oDbCommand, "P_CountryCode", DbType.String, 50, oCallBackRequest.countryCode);
                        oDatabase.AddInOutParameter(oDbCommand, "P_msisdn", DbType.String, 50, oCallBackRequest.msisdn);
                        oDatabase.AddInOutParameter(oDbCommand, "P_firstName", DbType.String, 50, oCallBackRequest.firstName);


                        oDatabase.AddInOutParameter(oDbCommand, "P_lastName", DbType.String, 50, oCallBackRequest.lastName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_fullName", DbType.String, 250, oCallBackRequest.fullName);
                        

                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);
                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);
                        string sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_success")]).Value.ToString();
                        sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();

                        oResponse.conversionID = oCallBackRequest.conversionID;
                        oResponse.ResponseDescription = sMessage;

                        if (sSuccess == "1")
                        {
                            oResponse.ResponseCode = RESPONSE_SUCCESS;
                            oResult.Result = true;
                           
                        }
                        else
                        {
                            oResponse.ResponseCode = RESPONSE_ERROR;
                            oResult.Result = false;
                           
                        }
                        oResult.Return = oResponse;
                        /*
                        if (sSuccess == "1")
                        {

                            oCallBackResult.TransactionInfo.ConversationID = oCallBackRequest.conversionID;
                            oCallBackResult.TransactionInfo.ResponseCode = oCallBackRequest.ResponseCode;
                            oCallBackResult.TransactionInfo.ResponseDescription = oCallBackRequest.ResponseDescription;
                            oCallBackResult.WalletData.FirstName = oCallBackRequest.firstName;
                            oCallBackResult.WalletData.LastName = oCallBackRequest.lastName;
                            oCallBackResult.WalletData.FullName = oCallBackRequest.fullName;
                            oCallBackResult.WalletData.MSISDN = oCallBackRequest.msisdn;
                            oCallBackResult.WalletData.CountryCode =oCallBackRequest.countryCode;
                            oResult.Return = oCallBackResult;
                            oResult.Result = true;
                        }
                        else
                        {
                            if (sSuccess == "2")
                            {
                                oCallBackResult.TransactionInfo.ResponseCode = "1005";
                                oCallBackResult.TransactionInfo.ResponseDescription = "Information is not updated";
                               
                                oResult.Message = sMessage;


                            }
                            else
                            {
                                oCallBackResult.TransactionInfo.ResponseCode = "1002";
                                oCallBackResult.TransactionInfo.ResponseDescription = sMessage;
                            
                            }

                            oResult.Result = false;
                            oResult.Return = oCallBackResult;
                           
                        }
                          */
                       
                        


                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/BeneficiaryAccValidation: " + exp.Message);
                        oResult.Exception = exp;
                        oResult.Message = exp.Message;
                        oResult.Result = false;
                        oResult.Return = null;
                    }
                    finally
                    {
                        oConnection.Close();
                        //oConnection.Dispose();

                    }
                }
                return oResult;
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/BeneficiaryAccValidation: " + exp.Message);
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
               using (IDbConnection oConnection = oDatabase.CreateConnection())
               {
                   IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_REMITAPI.SAVECBS_RESPONSE");
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
