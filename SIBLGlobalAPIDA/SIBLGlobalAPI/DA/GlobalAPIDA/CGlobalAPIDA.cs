/*
 * File name            : CUserDA.cs
 * Author               : Md. Munirul Ahsan
 * Date                 : December 12.2020
 * Version              : 1.0
 *
 * Description          : This is the Data Access Object Class
 *
 * Modification history :
 * Name                         Date                            Desc
 * 
 * 
 * Copyright (c) 2020: SOCIAL ISLAMI BANK LIMITED
 */

using Oracle.DataAccess.Client;
using SIBLCommon.Common;
using SIBLCommon.Common.Entity.Bank;
using SIBLCommon.Common.Entity.District;
using SIBLCommon.Common.Entity.Result;
using SIBLCommon.Common.Util.Attributes;
using SIBLCommon.Common.Util.Logger;
using SIBLCommon.SIBLCommon.Common;
using SIBLCommon.SIBLCommon.Common.Entity.Country;
using SIBLCommon.SIBLCommon.Common.Entity.RemitGlobalAPI;
using SIBLGlobalAPI.DA.Common.Connections;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIBLGlobalAPI.DA.GlobalAPIDA
{
   
    public class CGlobalAPIDA
    {        
        const string DB_CONNECTION = "ConString";
        const string DB_CONNECTION_SIBLSGCL = "ConStringSIBLBKASHAPI";
        CAppSettingConstant oAppSettingConstant = new CAppSettingConstant();

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


        public CResult CheckTransactionStatus(CTransactionStatus oTransactionStatus)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            CResponse oResponse = new CResponse();
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_REMITGLOBALAPI.CheckTransstatus");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_REFTXNID", DbType.String, 50, oTransactionStatus.RefTxnId);
                        oDatabase.AddInOutParameter(oDbCommand, "P_TXNIDSIBL", DbType.String, 50, oTransactionStatus.TxnIdSIBL);
                        oDatabase.AddInOutParameter(oDbCommand, "P_COMPANYCODE", DbType.String, 50, oTransactionStatus.SystemInfo.RemitCompanyCode);
                        oDatabase.AddInOutParameter(oDbCommand, "P_OPERATIONTYPE", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SUCCESS", DbType.String, 20, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_RETURN_MSG", DbType.String, 500, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_MSG_CODE", DbType.String, 500, DBNull.Value);
                        int i = oDatabase.ExecuteNonQuery(oDbCommand);
                        string success = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_SUCCESS")]).Value.ToString();                        
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_RETURN_MSG")]).Value.ToString();
                        string sMessageCode = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_MSG_CODE")]).Value.ToString();
                        if (success == "4")
                        {
                            oResult.Result = false;
                            oResponse.ResponseCode = oAppSettingConstant.responseCode8888;
                            oResponse.ResponseMessage =oAppSettingConstant.messageforCode8888 + sMessage;
                        }
                        else
                        {
                            oResult.Result = true;
                            oResponse.ResponseCode = sMessageCode;
                            oResponse.ResponseMessage = sMessage;
                        }
                        oResponse.RefTxnId = oTransactionStatus.RefTxnId;
                        oResponse.TxnIdSIBL = oTransactionStatus.TxnIdSIBL;
                        oResult.Status = success;
                        oResult.Message = sMessage;                        
                        oResult.Return = oResponse;

                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/CheckTransactionStatus: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/CheckTransactionStatus: " + exp.Message);
                return oResult;
            }

        }

        public CResult CheckUserInfo(CSystemInfo oSystemInfo)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            CResponse oResponse = new CResponse();
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_REMITGLOBALAPI.CheckUserInfo");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_USERNAME", DbType.String, 50, oSystemInfo.UserName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_UPassword", DbType.String, 50, oSystemInfo.Password);
                        oDatabase.AddInOutParameter(oDbCommand, "P_COMPANYCODE", DbType.String, 10, oSystemInfo.RemitCompanyCode);
                        oDatabase.AddInOutParameter(oDbCommand, "P_OPERATIONTYPE", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SUCCESS", DbType.String, 20, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_RETURN_MSG", DbType.String, 500, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_MSG_CODE", DbType.String, 500, DBNull.Value);
                        int i = oDatabase.ExecuteNonQuery(oDbCommand);
                        string success = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_SUCCESS")]).Value.ToString();
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_RETURN_MSG")]).Value.ToString();
                        string sMessageCode = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_MSG_CODE")]).Value.ToString();
                        if (success == "5")
                        {
                            oResult.Result = false;
                            oResponse.ResponseCode = oAppSettingConstant.responseCode9999;
                            oResponse.ResponseMessage = oAppSettingConstant.messageforCode9999;
                        }
                        else
                        {
                            oResult.Result = true;
                            oResponse.ResponseCode = sMessageCode;
                            oResponse.ResponseMessage = sMessage;
                        }
                        oResult.Status = success;
                        oResult.Message = sMessage;
                        oResult.Return = oResponse;
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/CheckUserInfo: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/CheckUserInfo: " + exp.Message);
                return oResult;
            }

        }

        public CResult SaveTransaction(CRemitInfo oRemitInfo)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            CResponse oResponse = new CResponse();
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_REMITGLOBALAPI.INSREMITINFO");
                    try
                    {
                        //TRANSACTION INFORMATION
                        oDatabase.AddInOutParameter(oDbCommand, "P_TID", DbType.Int32, 10, 0);
                        oDatabase.AddInOutParameter(oDbCommand, "P_REFTXNID", DbType.String, 200, oRemitInfo.TransactionInfo.RefTxnId);
                        oDatabase.AddInOutParameter(oDbCommand, "P_AMOUNT", DbType.String, 100, oRemitInfo.TransactionInfo.Amount);
                        oDatabase.AddInOutParameter(oDbCommand, "P_TXNIDSIBL", DbType.String, 100, oRemitInfo.TransactionInfo.TxnIdSIBL);
                        oDatabase.AddInOutParameter(oDbCommand, "P_COUNTRY", DbType.String, 100, oRemitInfo.TransactionInfo.Country);
                        oDatabase.AddInOutParameter(oDbCommand, "P_CURRENCY", DbType.String, 200, oRemitInfo.TransactionInfo.Currency);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BANKNAME", DbType.String, 100, oRemitInfo.TransactionInfo.BankName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BRANCHNAME", DbType.String, 150, oRemitInfo.TransactionInfo.BranchName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_DISTRICT", DbType.String, 100, oRemitInfo.TransactionInfo.District);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ROUTINGNUMBER", DbType.String, 50, oRemitInfo.TransactionInfo.RoutingNumber);
                        oDatabase.AddInOutParameter(oDbCommand, "P_TRANSACTIONTYPE", DbType.String, 200, oRemitInfo.TransactionInfo.TransactionType);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ENTRYDATE", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_COMPANYCODE", DbType.String, 50, oRemitInfo.SystemInfo.RemitCompanyCode);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BANKACNO", DbType.String, 50, oRemitInfo.TransactionInfo.AccountNo);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BANKCODE", DbType.String, 50, oRemitInfo.TransactionInfo.BankCode);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BRANCHCODE", DbType.String, 50, oRemitInfo.TransactionInfo.BranchCode);
                        oDatabase.AddInOutParameter(oDbCommand, "P_DISTRICTCODE", DbType.String, 50, oRemitInfo.TransactionInfo.DistrictCode);

                        //SENDER INFORMATION
                        oDatabase.AddInOutParameter(oDbCommand, "P_SFIRSTNAME", DbType.String, 200, oRemitInfo.SenderInfo.FirstName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SLASTNAME", DbType.String, 100, oRemitInfo.SenderInfo.LastName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_CONTACTNO", DbType.String, 100, oRemitInfo.SenderInfo.ContactNo);
                        oDatabase.AddInOutParameter(oDbCommand, "P_NATIONALITY", DbType.String, 50, oRemitInfo.SenderInfo.Nationality);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SLOCATION", DbType.String, 200, oRemitInfo.SenderInfo.Location);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SPOB", DbType.String, 50, oRemitInfo.SenderInfo.Pob);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SDOB", DbType.String, 50, oRemitInfo.SenderInfo.Dob);
                        oDatabase.AddInOutParameter(oDbCommand, "P_DOCUMENTTYPE", DbType.String, 200, oRemitInfo.SenderInfo.DocumentType);
                        oDatabase.AddInOutParameter(oDbCommand, "P_DOCUMENTNUMBER", DbType.String, 50, oRemitInfo.SenderInfo.DocumentNumber);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SIDISSUEDATE", DbType.String, 200, oRemitInfo.SenderInfo.IdIssueDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SIDEXPORYDATE", DbType.String, 50, oRemitInfo.SenderInfo.IdExpiryDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SADDRESS", DbType.String, 200, oRemitInfo.SenderInfo.Address);
                        oDatabase.AddInOutParameter(oDbCommand, "P_KYCSOURCEOFFUNDS", DbType.String, 200, oRemitInfo.SenderInfo.KycSourceOfFund);
                        oDatabase.AddInOutParameter(oDbCommand, "P_KYCPURPOSE", DbType.String, 200,  oRemitInfo.SenderInfo.KycPurpose);
                        
                        // RECEIVER INFORMATION
                        oDatabase.AddInOutParameter(oDbCommand, "P_RCOUNTRYCODE", DbType.String, 200, oRemitInfo.RecipientInfo.CountryCode);
                        oDatabase.AddInOutParameter(oDbCommand, "P_RCONTACTNO", DbType.String, 100, oRemitInfo.RecipientInfo.ContactNo);
                        oDatabase.AddInOutParameter(oDbCommand, "P_RFIRSTNAME", DbType.String, 100, oRemitInfo.RecipientInfo.FirstName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_RLASTNAME", DbType.String, 100, oRemitInfo.RecipientInfo.LastName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_RFULLNAME", DbType.String, 200, oRemitInfo.RecipientInfo.FullName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_RLOCATION", DbType.String, 200, oRemitInfo.RecipientInfo.Location);
                        oDatabase.AddInOutParameter(oDbCommand, "P_RIDNO", DbType.String, 50, oRemitInfo.RecipientInfo.IdNo);
                        oDatabase.AddInOutParameter(oDbCommand, "P_RIDTYPE", DbType.String, 200, oRemitInfo.RecipientInfo.IdType);
                        oDatabase.AddInOutParameter(oDbCommand, "P_RIDISSUEDATE", DbType.String, 100, oRemitInfo.RecipientInfo.IdIssueDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_RIDEXPIRTDATE", DbType.String, 100, oRemitInfo.RecipientInfo.IdExpiryDate);

                        oDatabase.AddInOutParameter(oDbCommand, "P_OPERATIONTYPE", DbType.String, 50, oRemitInfo.OperationType);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SUCCESS", DbType.String, 20, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_RETURN_MSG", DbType.String, 500, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_MSG_CODE", DbType.String, 500, DBNull.Value);

                        int i = oDatabase.ExecuteNonQuery(oDbCommand);
                        string success = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_SUCCESS")]).Value.ToString();
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_RETURN_MSG")]).Value.ToString();
                        string sMessageCode = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_MSG_CODE")]).Value.ToString();                        
                        string TXNIDSIBL = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_TXNIDSIBL")]).Value.ToString();
                        if (success == "4")
                        {
                            oResult.Result = false;
                            oResponse.ResponseCode = oAppSettingConstant.responseCode8888;
                            oResponse.ResponseMessage = oAppSettingConstant.messageforCode8888 + sMessage;
                        }
                        else
                        {
                            oResult.Result = true;
                            oResponse.ResponseCode = sMessageCode;
                            oResponse.ResponseMessage = sMessage;
                        }
                        oResult.Status = success;
                        oResult.Message = sMessage;
                        oResponse.TxnIdSIBL = TXNIDSIBL;
                        oResponse.RefTxnId = oRemitInfo.TransactionInfo.RefTxnId;
                        oResult.Return = oResponse;
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/SaveTransaction: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/SaveTransaction: " + exp.Message);
                return oResult;
            }

        }

        public CResult GetRemitInfo(CRemitInfo oRemitInfo)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            string success = "";
            string sMessage = "";
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_REMITGLOBALAPI.GetRemitInfo");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_TID", DbType.String, 50, oRemitInfo.TransactionInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_REFTXNID", DbType.String, 50, oRemitInfo.TransactionInfo.RefTxnId);
                        oDatabase.AddInOutParameter(oDbCommand, "P_TXNIDSIBL", DbType.String, 50, oRemitInfo.TransactionInfo.TxnIdSIBL);
                        oDatabase.AddInOutParameter(oDbCommand, "P_COMPANYCODE", DbType.String, 50, oRemitInfo.SystemInfo.RemitCompanyCode);
                        oDatabase.AddInOutParameter(oDbCommand, "P_FROM_DATE", DbType.String, 50, oRemitInfo.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_TO_DATE", DbType.String, 50, oRemitInfo.ToDate);
                        oDatabase.AddOutParameter(oDbCommand, "RC_LIST", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SUCCESS", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_RETURN_MSG", DbType.String, 500, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_MSG_CODE", DbType.String, 500, DBNull.Value);
                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);
                        success = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_SUCCESS")]).Value.ToString();
                        sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_RETURN_MSG")]).Value.ToString();
                        oResult.ResponseCode = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_MSG_CODE")]).Value.ToString();
                        oResult.Return = oDataSet.Tables[0];
                        oResult.Status = success;
                        oResult.Message = sMessage;
                        oResult.Result = true;
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetRemitInfo: " + exp.Message);
                        oResult.Exception = exp;
                        oResult.Message = exp.Message;
                        oResult.Result = false;
                        oResult.Return = null;
                    }
                    finally
                    {
                        oDbCommand.Dispose();
                        oConnection.Close();
                    }
                }
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetRemitInfo: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
            return oResult;
        }

        private CBankList BuildBankList(DataTable dt)
        {           
            CBankList oBankList = new CBankList();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CBank oBank = new CBank();
                oBank.BankCode = dt.Rows[i]["bank_id"].ToString();
                oBank.BankName = dt.Rows[i]["bank_name"].ToString();
                oBank.RecordNumber = dt.Rows[i]["RN"].ToString();
                oBankList.BankList.Add(oBank);                
            }
            return oBankList;
        }

        private CBranchList BuildBankBranchList(DataTable dt)
        {
            
            CBranchList oBranchList = new CBranchList();



            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CBranch oBranch = new CBranch();
                oBranch.BranchCode = dt.Rows[i]["branch_id"].ToString();
                oBranch.BranchName = dt.Rows[i]["BRANCH_NAME"].ToString();
                oBranch.RoutingNumber = dt.Rows[i]["routing_no"].ToString();
                oBranch.BankId = dt.Rows[i]["bank_id"].ToString();
                oBranch.RecordNumber= dt.Rows[i]["RN"].ToString();
                oBranchList.BranchList.Add(oBranch);
            }

            return oBranchList;
        }


        private CCountryList BuildCountryList(DataTable dt)
        {
            CCountryList oCountrList = new CCountryList();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CCountry oCountry = new CCountry();
                oCountry.CID = dt.Rows[i]["CID"].ToString();
                oCountry.CountryName = dt.Rows[i]["COUNTRY_NAME"].ToString();
                oCountry.CountryCode = dt.Rows[i]["COUNTRY_CODE"].ToString();
                oCountry.RecordNumber = dt.Rows[i]["RN"].ToString();
                oCountrList.CountryList.Add(oCountry);                
            }
            return oCountrList;
        }


        private CDistrictList BuildDistrictList(DataTable dt)
        {

            CDistrictList oDistrictList = new CDistrictList();



            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CDistrict oDistrict = new CDistrict();
                oDistrict.DistrictCode = dt.Rows[i]["distid"].ToString();
                oDistrict.DistrictName = dt.Rows[i]["dist_name"].ToString();
                
                oDistrict.RecordNumber = dt.Rows[i]["RN"].ToString();
                oDistrictList.DistrictList.Add(oDistrict);

               
            }

            return oDistrictList;
        }

        public CResult GetDistrictInfo(CDAS oDAS)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            string success = "";
            string sMessage = "";
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_REMITGLOBALAPI.GetDistrictInfo");
                    try
                    {


                        oDatabase.AddInOutParameter(oDbCommand, "P_LastRecordId", DbType.String, 50, oDAS.LastRecordId);
                        oDatabase.AddOutParameter(oDbCommand, "RC_LIST", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SUCCESS", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_RETURN_MSG", DbType.String, 500, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_MSG_CODE", DbType.String, 500, DBNull.Value);

                        oDatabase.AddInOutParameter(oDbCommand, "P_MoreRecord", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_TotalRecord", DbType.String, 500, DBNull.Value);

                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);
                        success = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_SUCCESS")]).Value.ToString();
                        sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_RETURN_MSG")]).Value.ToString();
                        oResult.ResponseCode = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_MSG_CODE")]).Value.ToString();
                        oResult.MoreRecord = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_MoreRecord")]).Value.ToString();
                        string sTotal = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_TotalRecord")]).Value.ToString();

                        oResult.TotalRecord = Convert.ToInt16(sTotal == "" ? "0" : sTotal);
                        if (oDataSet.Tables != null)
                            oResult.Return = BuildDistrictList(oDataSet.Tables[0]);
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetDistrictInfo: " + exp.Message);
                    }
                    finally
                    {
                        oResult.Status = success;
                        oResult.Message = sMessage;
                        oResult.Result = true;
                        oDbCommand.Dispose();
                        oConnection.Close();
                    }
                }

            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetCountryInfo: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
            return oResult;
        }

        public CResult GetCountryInfo(CDAS oDAS)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            string success = "";
            string sMessage = "";
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_REMITGLOBALAPI.GetCountryInfo");
                    try
                    {
                             

                        oDatabase.AddInOutParameter(oDbCommand, "P_LastRecordId", DbType.String, 50, oDAS.LastRecordId);
                         oDatabase.AddOutParameter(oDbCommand, "RC_LIST", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SUCCESS", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_RETURN_MSG", DbType.String, 500, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_MSG_CODE", DbType.String, 500, DBNull.Value);
                        
                        oDatabase.AddInOutParameter(oDbCommand, "P_MoreRecord", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_TotalRecord", DbType.String, 500, DBNull.Value);
                       
                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);
                        success = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_SUCCESS")]).Value.ToString();
                        sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_RETURN_MSG")]).Value.ToString();
                        oResult.ResponseCode= ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_MSG_CODE")]).Value.ToString();
                        oResult.MoreRecord = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_MoreRecord")]).Value.ToString();
                        string sTotal = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_TotalRecord")]).Value.ToString();
                        oResult.TotalRecord = Convert.ToInt16(sTotal==""?"0":sTotal);
                        if(oDataSet.Tables!=null)
                            oResult.Return = BuildCountryList(oDataSet.Tables[0]);
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetCountryInfo: " + exp.Message);
                    }
                    finally
                    {
                        oResult.Status = success;
                        oResult.Message = sMessage;
                        oResult.Result = true;
                        oDbCommand.Dispose();
                        oConnection.Close();
                    }
                }

            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetCountryInfo: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
            return oResult;
        }

        public CResult GetBankInfo(CDAS oDAS)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            string success = "";
            string sMessage = "";
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_REMITGLOBALAPI.GetBankInfo");
                    try
                    {


                        oDatabase.AddInOutParameter(oDbCommand, "P_LastRecordId", DbType.String, 50, oDAS.LastRecordId);
                        oDatabase.AddOutParameter(oDbCommand, "RC_LIST", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SUCCESS", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_RETURN_MSG", DbType.String, 500, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_MSG_CODE", DbType.String, 500, DBNull.Value);

                        oDatabase.AddInOutParameter(oDbCommand, "P_MoreRecord", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_TotalRecord", DbType.String, 500, DBNull.Value);

                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);
                        success = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_SUCCESS")]).Value.ToString();
                        sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_RETURN_MSG")]).Value.ToString();
                        oResult.ResponseCode = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_MSG_CODE")]).Value.ToString();
                        oResult.MoreRecord = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_MoreRecord")]).Value.ToString();
                        string sTotal = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_TotalRecord")]).Value.ToString();

                        oResult.TotalRecord = Convert.ToInt16(sTotal == "" ? "0" : sTotal);
                        if (oDataSet.Tables != null)
                            oResult.Return = BuildBankList(oDataSet.Tables[0]);
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetBankInfo: " + exp.Message);
                    }
                    finally
                    {
                        oResult.Status = success;
                        oResult.Message = sMessage;
                        oResult.Result = true;
                        oDbCommand.Dispose();
                        oConnection.Close();
                    }
                }

            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetBankInfo: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
            return oResult;
        }

        public CResult GetBankBranchInfo(CDAS oDAS)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            string success = "";
            string sMessage = "";
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_REMITGLOBALAPI.GetBankBranchInfo");
                    try
                    {
                        
                        oDatabase.AddInOutParameter(oDbCommand, "P_LastRecordId", DbType.String, 50, oDAS.LastRecordId);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BankID", DbType.String, 50, oDAS.BankId);
                        oDatabase.AddOutParameter(oDbCommand, "RC_LIST", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SUCCESS", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_RETURN_MSG", DbType.String, 500, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_MSG_CODE", DbType.String, 500, DBNull.Value);

                        oDatabase.AddInOutParameter(oDbCommand, "P_MoreRecord", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_TotalRecord", DbType.String, 500, DBNull.Value);

                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);
                        success = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_SUCCESS")]).Value.ToString();
                        sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_RETURN_MSG")]).Value.ToString();
                        oResult.ResponseCode = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_MSG_CODE")]).Value.ToString();
                        oResult.MoreRecord = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_MoreRecord")]).Value.ToString();
                        string sTotal = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_TotalRecord")]).Value.ToString();

                        oResult.TotalRecord = Convert.ToInt16(sTotal == "" ? "0" : sTotal);
                        if (oDataSet.Tables != null)
                            oResult.Return = BuildBankBranchList(oDataSet.Tables[0]);
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetBankBranchInfo: " + exp.Message);
                    }
                    finally
                    {
                        oResult.Status = success;
                        oResult.Message = sMessage;
                        oResult.Result = true;
                        oDbCommand.Dispose();
                        oConnection.Close();
                    }
                }

            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetBankBranchInfo: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
            return oResult;
        }

        
    }
}
