/**
 * File name            : CSIBLSGCLDA.cs
 * Author               : Md. Aminul Islam 
 * Date                 : Feb 25 ,2016
 * Version              : 1.0
 *
 * Description          : This is the Business Object manager
 *
 * Modification history :
 * Name                         Date                            Desc
 *                          
 * 
 * Copyright (c) 2016: Social Islami Bank Limited
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Result; 
using SIBLGlobalAPI.DA.Common.Connections;
using System.Configuration;
using System.Data;
using Oracle.DataAccess.Client;
using SIBLCommon.Common.Util.Logger; 
using SIBLCommon.Common.Util.Attributes;
using SIBLCommon.Common.Entity.Titas;
using SIBLCommon.SIBLCommon.Common.Entity.Titas;
using SIBLCommon.Common.Entity.Bank;
using SIBLXoomCommon.SIBLXoom.Common.Entity.CPU;
using SIBLCommon.Common.Entity.Bank;
using SIBLCommon.SIBLCommon.Common.Entity.SGCL;
 
 
namespace SIBLGlobalAPI.DA.SIBLSGCL
{
    public class CSIBLSGCLDA
    {
        const string DB_CONNECTION = "ConString";
        const string DB_CONNECTION_SIBLSGCL = "ConStringSIBLSGCL";

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
 
        [Author("Munirul Islam", "12-07-2017", "Get AccessToken")]
        public CResult GetAPIAccessToken(CAccessToken oAccessToken)
 
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
                        oDatabase.AddInOutParameter(oDbCommand, "p_sTokenDate", DbType.String, 50, oAccessToken.TokenDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_sTokenValue", DbType.String, 500, oAccessToken.access_token);
                        oDatabase.AddInOutParameter(oDbCommand, "p_sCreator", DbType.String, 50, oAccessToken.Creator);
                        oDatabase.AddInOutParameter(oDbCommand, "p_sExpiresIn", DbType.String, 50, oAccessToken.expires_in);
                        oDatabase.AddInOutParameter(oDbCommand, "p_sTokenType", DbType.String, 50, oAccessToken.token_type);
                        oDatabase.AddInOutParameter(oDbCommand, "p_sMeterType", DbType.String, 50, oAccessToken.MeterType);
                        oDatabase.AddInOutParameter(oDbCommand, "p_Success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_Message", DbType.String, 50, DBNull.Value);
                        oDatabase.ExecuteNonQuery(oDbCommand);
                        string sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Success")]).Value.ToString();
                        sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Message")]).Value.ToString();
                        sTokenValue = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_sTokenValue")]).Value.ToString();

                        

                        if (sTokenValue != "")
                        {
                            oAccessToken.access_token = sTokenValue;
                            oAccessToken.Status = sSuccess;
                            oResult.Return = oAccessToken;
                            oResult.Message = sMessage;
                            oResult.Result = true;


                        }
                        else
                        {
                            oResult.Result = false;
                            oResult.Message = sMessage;
                        }
                        

                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetAPIAccessToken: " + exp.Message);
                        oResult.Exception = exp;
                        oResult.Message = exp.Message;
                        oResult.Result = false;
                        oResult.Return = null;
                    }
                    finally
                    {
                        oConnection.Close();
                        //oConnection.Dispose();
                        CLog.Logger.Write(CLog.SUCCESS, this.ToString() + "Function Name : GetAPIAccessToken:" + PrintNewLine() + " Token :" + sTokenValue + PrintNewLine() + "DBMessage:" + sMessage);
                    }
                }
                return oResult;
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetAPIAccessToken: " + exp.Message);
                return oResult;
            }

        }

        private void AddInOutParameter(string sParam, OracleCommand oDbCommand, string []sValue, int iSize)
        {
            OracleParameter Param = new OracleParameter(sParam, OracleDbType.Varchar2);
            Param.Direction = ParameterDirection.InputOutput;
            Param.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
            Param.Value = sValue;
            Param.Size =iSize ;
        }

        [Author("Md. Munirul Islam", "18-01-2018", "Bill Post/Receive for Reconciliation")]
        public CResult PostHistoryMB(CBillInfoList oList)
        {


            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            int iCount = 0;
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {


                    // delete data 

                    CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "=============================History Table Insertion==========================");
                    CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "/PostHistoryMB::Delete History Data dated on: " + oList.BillInfoList[0].TransactionDate);
                    IDbCommand oDbCommand = oConnection.CreateCommand();
                    oDbCommand.CommandType = CommandType.Text;
                    oDbCommand.CommandText = "delete from HISTORY_BILLINFO_TITAS where TRANSACTIONDATE='" + oList.BillInfoList[0].TransactionDate + "' and BRANCHID=" + oList.BillInfoList[0].ChalanBranch;// chalanbranch contains branch id
                    oDbCommand.ExecuteNonQuery();
                    oDbCommand.Parameters.Clear();
                    // end of deletion


                    oDbCommand = oDatabase.GetStoredProcICommand("PKG_HISTORY.ADDPAYMENT_HISTORY");
                    try
                    {
                        CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "/PostHistoryMB:: Data Insertion Start on History Table");
                        CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "/PostHistoryMB::Transaction Date: " + oList.BillInfoList[0].TransactionDate);
                    
                        foreach (CBillInfo oBillInfo in oList.BillInfoList)
                        {
                            oDatabase.AddInOutParameter(oDbCommand, "P_TID", DbType.String, 50, oBillInfo.CN);
                            oDatabase.AddInOutParameter(oDbCommand, "P_PAYMENTID", DbType.String, 50, oBillInfo.PaymentId);
                            oDatabase.AddInOutParameter(oDbCommand, "P_TRANSACTIONDATE", DbType.String, 150, oBillInfo.TransactionDate);
                            oDatabase.AddInOutParameter(oDbCommand, "P_CUSTOMERCODE", DbType.String, 50, oBillInfo.customerCode);
                            oDatabase.AddInOutParameter(oDbCommand, "P_TRANSACTIONSTATUS", DbType.String, 50, oBillInfo.Status);
                            oDatabase.AddInOutParameter(oDbCommand, "P_INVOICENO", DbType.String, 50, oBillInfo.invoiceNo);
                            oDatabase.AddInOutParameter(oDbCommand, "P_INVOICEAMOUNT", DbType.String, 10, oBillInfo.invoiceAmount);
                            oDatabase.AddInOutParameter(oDbCommand, "P_PAIDAMOUNT", DbType.String, 50, oBillInfo.PaidAmount);
                            oDatabase.AddInOutParameter(oDbCommand, "P_SOURCETAXAMOUNT", DbType.String, 50, oBillInfo.SourceTaxAmount);
                            oDatabase.AddInOutParameter(oDbCommand, "P_REVENUESTAMP", DbType.String, 20, oBillInfo.RevenueStamp);
                            oDatabase.AddInOutParameter(oDbCommand, "P_SZONE", DbType.String, 50, oBillInfo.zone);
                            oDatabase.AddInOutParameter(oDbCommand, "P_BRANCHID", DbType.String, 50, oBillInfo.ChalanBranch);//contains branch id
                            oDatabase.AddInOutParameter(oDbCommand, "P_BRANCHCODE", DbType.String, 50, oBillInfo.Branch.BranchCode);
                            oDatabase.AddInOutParameter(oDbCommand, "P_BRANCHROUTINGNO", DbType.String, 50, oBillInfo.BranchRoutingNo);
                            oDatabase.AddInOutParameter(oDbCommand, "P_SOPERATOR", DbType.String, 50, oBillInfo.Operator);
                            oDatabase.AddInOutParameter(oDbCommand, "P_REFNO", DbType.String, 50, oBillInfo.RefNo);
                            oDatabase.AddInOutParameter(oDbCommand, "P_REASON", DbType.String, 50, oBillInfo.Reason);
                            oDatabase.AddInOutParameter(oDbCommand, "P_CHALANNO", DbType.String, 50, oBillInfo.ChalanNo);
                            oDatabase.AddInOutParameter(oDbCommand, "P_CHALANDATE", DbType.String, 50, oBillInfo.ChalanDate);
                            oDatabase.AddInOutParameter(oDbCommand, "P_CHALANBANK", DbType.String, 50, oBillInfo.ChalanBank);
                            oDatabase.AddInOutParameter(oDbCommand, "P_CHALANBRANCH", DbType.String, 50, oBillInfo.ChalanBranch);
                            oDatabase.AddInOutParameter(oDbCommand, "P_OPERATIONTYPE", DbType.String, 50, oBillInfo.OperationType);
                            oDatabase.AddInOutParameter(oDbCommand, "P_USERID", DbType.String, 50, oBillInfo.CreateBy);
                            oDatabase.AddInOutParameter(oDbCommand, "p_Success", DbType.String, 50, DBNull.Value);
                            oDatabase.AddInOutParameter(oDbCommand, "p_Message", DbType.String, 500, DBNull.Value);
                            oDatabase.ExecuteNonQuery(oDbCommand);

                            oDbCommand.Parameters.Clear();
                            iCount++;
                        }

                        if (iCount == oList.BillInfoList.Count)
                        {
                            oResult.Result = true;
                            CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "/Data Insertion completed on History Table");
                            CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "/Number of Record Inserted:" + iCount.ToString());
                            CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "/Transaction Date: " + oList.BillInfoList[0].TransactionDate);

                        }
                        else
                        {
                            oResult.Result = false;
                            CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "/Data Insertion is not completed on History Table");
                            CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "/Transaction Date: " + oList.BillInfoList[0].TransactionDate);
                        }
                        CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "=============================History Table Insertion End==========================");
                       
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/PostHistoryMB: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/PostHistoryMB: " + exp.Message);
                return oResult;
            }



        }

        [Author("Md. Munirul Islam", "18-01-2018", "Bill Post/Receive for Reconciliation")]
        public CResult PostHistoryNMB(CBillInfoList oList)
        {


            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            int iCount = 0;
            string sTransactionDate = "";
            string BranchId = "";
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {


                    // delete data 

                    sTransactionDate = oList.BillInfoList[0].TransactionDate;
                    BranchId=oList.BillInfoList[0].ChalanBranch;
                    CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "=============================History Table Insertion(Non metered)==========================");
                    CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "/PostHistoryNMB::Delete History Data dated on: " + sTransactionDate);
                    IDbCommand oDbCommand = oConnection.CreateCommand();
                    oDbCommand.CommandType = CommandType.Text;
                    oDbCommand.CommandText = "delete from HISTORY_NM_BILLINFO_TITAS where TRANSACTIONDATE='" + sTransactionDate + "' and BRANCHID=" + BranchId;// chalanbranch contains branch id
                    oDbCommand.ExecuteNonQuery();
                    oDbCommand.Parameters.Clear();
                    // end of deletion


                    oDbCommand = oDatabase.GetStoredProcICommand("PKG_HISTORY.ADDNMPAYMENT_HISTORY");
                    try
                    {
                        CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "/PostHistoryNMB:: Data Insertion Start on History Table");
                        CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "/PostHistoryNMB::Transaction Date: " + sTransactionDate);


                        foreach (CBillInfo oBillInfo in oList.BillInfoList)
                        {
                            oDatabase.AddInOutParameter(oDbCommand, "P_NMBILLID", DbType.String, 50, DBNull.Value);
                            oDatabase.AddInOutParameter(oDbCommand, "P_AMOUNT", DbType.String, 50, oBillInfo.invoiceAmount);
                            oDatabase.AddInOutParameter(oDbCommand, "P_SURCHARGE", DbType.String, 50, oBillInfo.SurchargeAmount);
                            oDatabase.AddInOutParameter(oDbCommand, "P_PARTICULARS", DbType.String, 50, oBillInfo.issueMonth);
                            oDatabase.AddInOutParameter(oDbCommand, "P_CREATEBY", DbType.String, 50, DBNull.Value);
                            oDatabase.AddInOutParameter(oDbCommand, "P_CREATEDATE", DbType.String, 50, DBNull.Value);
                            oDatabase.AddInOutParameter(oDbCommand, "P_BATCHNO", DbType.String, 50, oBillInfo.invoiceNo);
                            oDatabase.AddInOutParameter(oDbCommand, "P_VOUCHERDATE", DbType.String, 50, oBillInfo.DueDate);
                            oDatabase.AddInOutParameter(oDbCommand, "P_BILLTYPE", DbType.String, 50, DBNull.Value);
                            oDatabase.AddInOutParameter(oDbCommand, "P_INVOICENO", DbType.String, 50, oBillInfo.invoiceNo);
                            oDatabase.AddInOutParameter(oDbCommand, "P_PAYMENTID", DbType.String, 50, oBillInfo.PaymentId);
                            oDatabase.AddInOutParameter(oDbCommand, "P_BRANCHID", DbType.String, 50, oBillInfo.ChalanBranch);
                            oDatabase.AddInOutParameter(oDbCommand, "P_TRANSACTIONDATE", DbType.String, 50, oBillInfo.TransactionDate);
                            oDatabase.AddInOutParameter(oDbCommand, "P_BANKTRANSID", DbType.String, 50, oBillInfo.TrackerNo);
                            
                            oDatabase.AddInOutParameter(oDbCommand, "P_OPERATIONTYPE", DbType.String, 50, oBillInfo.OperationType);
                            oDatabase.AddInOutParameter(oDbCommand, "p_Success", DbType.String, 50, DBNull.Value);
                            oDatabase.AddInOutParameter(oDbCommand, "p_Message", DbType.String, 500, DBNull.Value);
                            oDatabase.ExecuteNonQuery(oDbCommand);

                            string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Message")]).Value.ToString();

                            CLog.Logger.Write(CLog.INFORMATION, sMessage);

                            oDbCommand.Parameters.Clear();
                            iCount++;
                        }

                        if (iCount == oList.BillInfoList.Count)
                        {
                            oResult.Result = true;
                            CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "/Number of Record Inserted(NonMetered):" + iCount.ToString());
                            CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "/Data Insertion completed(NonMetered) on History Table");
                            
                            //CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "/Transaction Date(NonMetered): " + sTransactionDate);

                        }
                        else
                        {
                            oResult.Result = false;
                            CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "/Data Insertion is not completed(NonMetered) on History Table");
                            CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "/Transaction Date(NonMetered): " + sTransactionDate);
                        }
                        CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "=============================History Table Insertion end(Non metered)==========================");

                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/PostHistoryNMB: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/PostHistoryNMB: " + exp.Message);
                return oResult;
            }



        }
    

        [Author("Md. Munirul Islam", "18-01-2018", "Bill Post/Receive for Reconciliation")]
        public CResult PostHistoryMB_old(CBillInfoList oList)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            DataTable dt = new DataTable();
            try
            {
                CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "=============================History Table Insertion==========================");
                using (OracleConnection oConnection = new OracleConnection(GetConnectionStrings(DB_CONNECTION_SIBLSGCL)))
                {
                    oConnection.Open();
                    OracleCommand oDbCommand = oConnection.CreateCommand();
                    
                    // delete data 


                    CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "/PostHistoryMB::Delete History Data dated on: " + oList.BillInfoList[0].TransactionDate);
                    oDbCommand.CommandType = CommandType.Text;
                    oDbCommand.CommandText = "delete from HISTORY_BILLINFO_TITAS where TRANSACTIONDATE='" + oList.BillInfoList[0].TransactionDate + "' and BRANCHID=" + oList.BillInfoList[0].ChalanBranch;// chalanbranch contains branch id
                    oDbCommand.ExecuteNonQuery();
                    // end of deletion
                    
                    // insert new data to table
                    oDbCommand = oConnection.CreateCommand();
                    oDbCommand.CommandType = CommandType.StoredProcedure;
                    oDbCommand.CommandText = "PKG_HISTORY.ADDPAYMENT_HISTORY";
                    oDbCommand.BindByName = true;


                    int iCount = oList.BillInfoList.Count;
                    CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "/Total Number of Record :" + oList.BillInfoList.Count.ToString());

                    //OracleDataAdapter oda = new OracleDataAdapter();
                    //oda.InsertCommand = oDbCommand;
                    //oda.Update(dt);
                    //OracleParameter opram = new OracleParameter();
                    
                    try
                    {
                      
                        CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "/PostHistoryMB:: Data Insertion Start on History Table");
                        CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "/PostHistoryMB::Transaction Date: " + oList.BillInfoList[0].TransactionDate);

                        AddInOutParameter("P_TID", oDbCommand, oList.BillInfoList.Select(c => c.CN).ToArray(), iCount);
                        AddInOutParameter("P_PAYMENTID", oDbCommand, oList.BillInfoList.Select(c => c.PaymentId).ToArray(), iCount);
                        AddInOutParameter("P_TRANSACTIONDATE", oDbCommand, oList.BillInfoList.Select(c => c.TransactionDate).ToArray(), iCount);
                        AddInOutParameter("P_CUSTOMERCODE", oDbCommand, oList.BillInfoList.Select(c => c.customerCode).ToArray(), iCount);
                        AddInOutParameter("P_TRANSACTIONSTATUS", oDbCommand, oList.BillInfoList.Select(c => c.Status).ToArray(), iCount);
                        AddInOutParameter("P_INVOICENO", oDbCommand, oList.BillInfoList.Select(c => c.invoiceNo).ToArray(), iCount);
                        AddInOutParameter("P_INVOICEAMOUNT", oDbCommand, oList.BillInfoList.Select(c => c.invoiceAmount).ToArray(), iCount);

                        AddInOutParameter("P_PAIDAMOUNT", oDbCommand, oList.BillInfoList.Select(c => c.PaidAmount).ToArray(), iCount);
                        AddInOutParameter("P_SOURCETAXAMOUNT", oDbCommand, oList.BillInfoList.Select(c => c.SourceTaxAmount).ToArray(), iCount);
                        AddInOutParameter("P_REVENUESTAMP", oDbCommand, oList.BillInfoList.Select(c => c.RevenueStamp).ToArray(), iCount);
                        AddInOutParameter("P_SZONE", oDbCommand, oList.BillInfoList.Select(c => c.zone).ToArray(), iCount);

                        AddInOutParameter("P_BRANCHID", oDbCommand, oList.BillInfoList.Select(c => c.ChalanBranch).ToArray(), iCount);
                        AddInOutParameter("P_BRANCHCODE", oDbCommand, oList.BillInfoList.Select(c => c.Branch.BranchCode).ToArray(), iCount);
                        AddInOutParameter("P_BRANCHROUTINGNO", oDbCommand, oList.BillInfoList.Select(c => c.BranchRoutingNo).ToArray(), iCount);
                        AddInOutParameter("P_SOPERATOR", oDbCommand, oList.BillInfoList.Select(c => c.Operator).ToArray(), iCount);
                        AddInOutParameter("P_REFNO", oDbCommand, oList.BillInfoList.Select(c => c.RefNo).ToArray(), iCount);
                        AddInOutParameter("P_REASON", oDbCommand, oList.BillInfoList.Select(c => c.Reason).ToArray(), iCount);

                        AddInOutParameter("P_CHALANNO", oDbCommand, oList.BillInfoList.Select(c => c.ChalanNo).ToArray(), iCount);
                        AddInOutParameter("P_CHALANDATE", oDbCommand, oList.BillInfoList.Select(c => c.ChalanDate).ToArray(), iCount);
                        AddInOutParameter("P_CHALANBANK", oDbCommand, oList.BillInfoList.Select(c => c.ChalanBank).ToArray(), iCount);
                        AddInOutParameter("P_CHALANBRANCH", oDbCommand, oList.BillInfoList.Select(c => c.ChalanBranch).ToArray(), iCount);
                        AddInOutParameter("P_OPERATIONTYPE", oDbCommand, oList.BillInfoList.Select(c => c.OperationType).ToArray(), iCount);

                        AddInOutParameter("P_USERID", oDbCommand, oList.BillInfoList.Select(c => c.UserInfo.CN).ToArray(), iCount);
                        AddInOutParameter("p_Success", oDbCommand, oList.BillInfoList.Select(c => c.OperationType).ToArray(), iCount);
                        AddInOutParameter("p_Message", oDbCommand, oList.BillInfoList.Select(c => c.OperationType).ToArray(), iCount);
                         
                        
                         /*
                         oDbCommand.Parameters.Add("P_TID",OracleDbType.Varchar2,oList.BillInfoList.Select(c => c.CN).ToArray(),ParameterDirection.InputOutput);
                         oDbCommand.Parameters.Add("P_PAYMENTID", OracleDbType.Varchar2, oList.BillInfoList.Select(c => c.PaymentId).ToArray(), ParameterDirection.InputOutput);
                         oDbCommand.Parameters.Add("P_TRANSACTIONDATE", OracleDbType.Varchar2, oList.BillInfoList.Select(c => c.TransactionDate).ToArray(), ParameterDirection.InputOutput);
                         oDbCommand.Parameters.Add("P_CUSTOMERCODE", OracleDbType.Varchar2, oList.BillInfoList.Select(c => c.customerCode).ToArray(), ParameterDirection.InputOutput);
                         oDbCommand.Parameters.Add("P_TRANSACTIONSTATUS", OracleDbType.Varchar2, oList.BillInfoList.Select(c => c.Status).ToArray(), ParameterDirection.InputOutput);
                         oDbCommand.Parameters.Add("P_INVOICENO", OracleDbType.Varchar2, oList.BillInfoList.Select(c => c.invoiceNo).ToArray(), ParameterDirection.InputOutput);
                         oDbCommand.Parameters.Add("P_INVOICEAMOUNT", OracleDbType.Varchar2, oList.BillInfoList.Select(c => c.invoiceAmount).ToArray(), ParameterDirection.InputOutput);

                         oDbCommand.Parameters.Add("P_PAIDAMOUNT", OracleDbType.Varchar2, oList.BillInfoList.Select(c => c.PaidAmount).ToArray(), ParameterDirection.InputOutput);
                         oDbCommand.Parameters.Add("P_SOURCETAXAMOUNT", OracleDbType.Varchar2, oList.BillInfoList.Select(c => c.SourceTaxAmount).ToArray(), ParameterDirection.InputOutput);
                         oDbCommand.Parameters.Add("P_REVENUESTAMP", OracleDbType.Varchar2, oList.BillInfoList.Select(c => c.RevenueStamp).ToArray(), ParameterDirection.InputOutput);
                         oDbCommand.Parameters.Add("P_SZONE", OracleDbType.Varchar2, oList.BillInfoList.Select(c => c.zone).ToArray(), ParameterDirection.InputOutput);

                         oDbCommand.Parameters.Add("P_BRANCHID", OracleDbType.Varchar2, oList.BillInfoList.Select(c => c.ChalanBranch).ToArray(), ParameterDirection.InputOutput);
                         oDbCommand.Parameters.Add("P_BRANCHCODE", OracleDbType.Varchar2, oList.BillInfoList.Select(c => c.Branch.BranchCode).ToArray(), ParameterDirection.InputOutput);
                         oDbCommand.Parameters.Add("P_BRANCHROUTINGNO", OracleDbType.Varchar2, oList.BillInfoList.Select(c => c.BranchRoutingNo).ToArray(), ParameterDirection.InputOutput);
                         oDbCommand.Parameters.Add("P_SOPERATOR", OracleDbType.Varchar2, oList.BillInfoList.Select(c => c.Operator).ToArray(), ParameterDirection.InputOutput);
                         oDbCommand.Parameters.Add("P_REFNO", OracleDbType.Varchar2, oList.BillInfoList.Select(c => c.RefNo).ToArray(), ParameterDirection.InputOutput);
                         oDbCommand.Parameters.Add("P_REASON", OracleDbType.Varchar2, oList.BillInfoList.Select(c => c.Reason).ToArray(), ParameterDirection.InputOutput);

                         oDbCommand.Parameters.Add("P_CHALANNO", OracleDbType.Varchar2, oList.BillInfoList.Select(c => c.ChalanNo).ToArray(), ParameterDirection.InputOutput);
                         oDbCommand.Parameters.Add("P_CHALANDATE", OracleDbType.Varchar2, oList.BillInfoList.Select(c => c.ChalanDate).ToArray(), ParameterDirection.InputOutput);
                         oDbCommand.Parameters.Add("P_CHALANBANK", OracleDbType.Varchar2, oList.BillInfoList.Select(c => c.ChalanBank).ToArray(), ParameterDirection.InputOutput);
                         oDbCommand.Parameters.Add("P_CHALANBRANCH", OracleDbType.Varchar2, oList.BillInfoList.Select(c => c.ChalanBranch).ToArray(), ParameterDirection.InputOutput);
                         oDbCommand.Parameters.Add("P_OPERATIONTYPE", OracleDbType.Varchar2, oList.BillInfoList.Select(c => c.OperationType).ToArray(), ParameterDirection.InputOutput);

                         oDbCommand.Parameters.Add("P_USERID", OracleDbType.Varchar2, oList.BillInfoList.Select(c => c.UserInfo.CN).ToArray(), ParameterDirection.InputOutput);
                         oDbCommand.Parameters.Add("p_Success", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.InputOutput);
                         oDbCommand.Parameters.Add("p_Message", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.InputOutput);
                         */
                         oDbCommand.ArrayBindCount = oList.BillInfoList.Count;
                        
                         int result=oDbCommand.ExecuteNonQuery();
                         if (result == oList.BillInfoList.Count)
                         {
                             oResult.Result = true;
                             CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "/Data Insertion completed on History Table");
                             CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "/Number of Record Inserted:" + result.ToString());
                             CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "/Transaction Date: " + oList.BillInfoList[0].TransactionDate);

                         }
                         else
                         {
                             oResult.Result = false;
                             CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "/Data Insertion is not completed on History Table");
                             CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "/Transaction Date: " + oList.BillInfoList[0].TransactionDate);
                         }
                         
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Message")]).Value.ToString();
                        string sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Success")]).Value.ToString();
                        
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/PostHistoryMB: " + exp.Message);
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
                CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "/=============================End of History Table Insertion==========================");
                return oResult;
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/PostBill: " + exp.Message);
                return oResult;
            }

        }
     


        [Author("Md. Aminul Islam", "18-09-2017", "Bill Post/Receive")]
        public CResult PostMB(CBillInfo oBillInfo)
        {            
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;            
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {                   
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_TITAS.add_BillInfo");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "p_billid", DbType.Int32, 50, oBillInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_customerCode", DbType.String, 50, oBillInfo.customerCode);
                        oDatabase.AddInOutParameter(oDbCommand, "p_customerName", DbType.String, 150, oBillInfo.customerName);
                        oDatabase.AddInOutParameter(oDbCommand, "p_invoiceNo ", DbType.String, 50, oBillInfo.invoiceNo);
                        oDatabase.AddInOutParameter(oDbCommand, "p_invoiceAmount", DbType.Double, 50, oBillInfo.invoiceAmount);
                        oDatabase.AddInOutParameter(oDbCommand, "p_issueMonth", DbType.String, 50, oBillInfo.issueMonth);
                        oDatabase.AddInOutParameter(oDbCommand, "p_settleDate ", DbType.String, 10, oBillInfo.settleDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_EntryDate", DbType.String, 50, oBillInfo.EntryDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_CreateBy", DbType.Int32, 50, oBillInfo.UserInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_BranchId", DbType.Int32, 20, oBillInfo.Branch.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_BillStatus ", DbType.String, 50, oBillInfo.BillStatus);
                        oDatabase.AddInOutParameter(oDbCommand, "p_statuscode", DbType.String, 50, oBillInfo.statuscode);
                        oDatabase.AddInOutParameter(oDbCommand, "P_zone", DbType.String, 50, oBillInfo.zone);
                        oDatabase.AddInOutParameter(oDbCommand, "P_trackerno", DbType.String, 50, oBillInfo.TrackerNo);
                        oDatabase.AddInOutParameter(oDbCommand, "P_stampamount", DbType.String, 50, oBillInfo.RevenueStamp);
                        oDatabase.AddInOutParameter(oDbCommand, "P_sourceTaxAmount", DbType.Double, 50, oBillInfo.SourceTaxAmount);
                        oDatabase.AddInOutParameter(oDbCommand, "P_TitaspaymentId", DbType.String, 50, oBillInfo.PaymentId);
                        oDatabase.AddInOutParameter(oDbCommand, "P_chalanNo", DbType.String, 50, oBillInfo.ChalanNo);
                        oDatabase.AddInOutParameter(oDbCommand, "P_chalanDate", DbType.String, 50, oBillInfo.ChalanDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_chalanBank", DbType.String, 50, oBillInfo.ChalanBank);
                        oDatabase.AddInOutParameter(oDbCommand, "P_chalanBranch", DbType.String, 50, oBillInfo.ChalanBranch);
                        oDatabase.AddInOutParameter(oDbCommand, "p_OperationType", DbType.String, 50, oBillInfo.OperationType);
                        oDatabase.AddInOutParameter(oDbCommand, "p_transactionType", DbType.String, 50, oBillInfo.TransactionType);
                        oDatabase.AddInOutParameter(oDbCommand, "p_Success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_Message", DbType.String, 500, DBNull.Value);
                        oDatabase.ExecuteNonQuery(oDbCommand);
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Message")]).Value.ToString();
                        string sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Success")]).Value.ToString();
                        string sTrackerno = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_trackerno")]).Value.ToString();
                        string sBillId = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_billid")]).Value.ToString();
                        //string sRoutingNumber = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_BrRoutingNo")]).Value.ToString();
                        //oDbCommand.Parameters.Clear();
                        if (sSuccess == "1")
                        {
                            oBillInfo.Status = sSuccess;
                            oBillInfo.RefNo = sTrackerno;
                            //oBillInfo.BranchRoutingNo = sRoutingNumber;
                            oBillInfo.CN = sBillId;
                            oResult.Return = oBillInfo;
                            oResult.Message = sMessage;
                            oResult.Result = true;
                            CLog.Logger.Write(CLog.SUCCESS, this.ToString() + "/PostBill: DBMessage:" + sMessage);
                        }
                        else
                        {
                            oResult.Return = null;
                            oResult.Message = sMessage;
                            oResult.Result = false;
                        }
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/PostBill: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/PostBill: " + exp.Message);
                return oResult;
            }

        }
        
        private string PrintNewLine()
        {
            return Environment.NewLine;
        }

        [Author("Md. Aminul Islam", "20-09-2017", "Bill Post/Receive")]
        public CResult GetMBillDetails(CBillInfo oBillInfo)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet ds = new DataSet();

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_TITAS.Get_BillDetails");
                    try
                    {

                        oDatabase.AddInOutParameter(oDbCommand, "P_paymentId", DbType.String, 50, oBillInfo.PaymentId);
                        oDatabase.AddInOutParameter(oDbCommand, "p_OperationType", DbType.String, 50, oBillInfo.OperationType);
                        oDatabase.AddOutParameter(oDbCommand, "rcBillList", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_Success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_Message", DbType.String, 50, DBNull.Value);
                        ds = oDatabase.ExecuteDataSet(oDbCommand);
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Message")]).Value.ToString();
                        string sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Success")]).Value.ToString();
                        if (ds.Tables.Count != 0)
                        {
                            oResult.Return = ds;
                            oResult.Result = true;
                            oResult.Message = sMessage;
                        }
                        else
                        {
                            oResult.Return = null;
                            oResult.Result = false;
                            oResult.Message = sMessage;
                        }

                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetMBillDetails: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetMBillDetails: " + exp.Message);
                return oResult;
            }
        }


        public CResult SaveSearchInfo(CSGCL oSGCL)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_KGCL.SaveSearchBillInfo");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "p_BILLID", DbType.String, 50, oSGCL.CN == "" ? "0" : oSGCL.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_CUSTOMERCODE", DbType.String, 50, oSGCL.CUSTOMERCODE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_MOBILENO", DbType.String, 20, oSGCL.MOBILENO);
                        oDatabase.AddInOutParameter(oDbCommand, "p_CUSTOMERNAME", DbType.String, 150, oSGCL.CUSTOMERNAME);
                        oDatabase.AddInOutParameter(oDbCommand, "p_BILLAMOUNT", DbType.String, 20, oSGCL.BILLAMOUNT == "" ? "0" : oSGCL.BILLAMOUNT);
                        oDatabase.AddInOutParameter(oDbCommand, "p_SURCHARGE", DbType.String, 40, oSGCL.SURCHARGE == "" ? "0" : oSGCL.SURCHARGE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_METERCHARGE", DbType.String, 40, oSGCL.METERCHARGE == "" ? "0" : oSGCL.METERCHARGE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_TOTALBILL", DbType.String, 50, oSGCL.TOTALAMOUNT == "" ? "0" : oSGCL.TOTALAMOUNT);
                        oDatabase.AddInOutParameter(oDbCommand, "p_AITAMOUNT", DbType.String, 40, oSGCL.AITAMOUNT == "" ? "0" : oSGCL.AITAMOUNT);
                        oDatabase.AddInOutParameter(oDbCommand, "p_BILLMONTH", DbType.String, 70, oSGCL.BILLMONTH);
                        oDatabase.AddInOutParameter(oDbCommand, "p_BILLYEAR", DbType.String, 10, oSGCL.BILLYEAR);
                        oDatabase.AddInOutParameter(oDbCommand, "p_LASTPAYMENTDATE", DbType.String, 20, oSGCL.LASTPAYMENTDATE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_ISGOVT", DbType.String, 10, oSGCL.ISGOVT);
                        oDatabase.AddInOutParameter(oDbCommand, "p_POCBRANCHNAME", DbType.String, 100, oSGCL.POCBRANCHNAME);
                        oDatabase.AddInOutParameter(oDbCommand, "p_POCBANKNAME", DbType.String, 100, oSGCL.POCBANKNAME);
                        oDatabase.AddInOutParameter(oDbCommand, "p_POCDATE", DbType.String, 20, oSGCL.POCDATE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_POCNO", DbType.String, 50, oSGCL.POCNO);
                        oDatabase.AddInOutParameter(oDbCommand, "p_POAUTHORIZEDDATE", DbType.String, 50, oSGCL.POAUTHORIZEDDATE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_BPODATE", DbType.String, 50, oSGCL.BPODATE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_BPODETAILS", DbType.String, 50, oSGCL.BPODetails);
                        oDatabase.AddInOutParameter(oDbCommand, "p_INSTALLMENTNO", DbType.String, 50, oSGCL.INSTALLMENTNO == "" ? "0" : oSGCL.INSTALLMENTNO);
                        oDatabase.AddInOutParameter(oDbCommand, "p_INSTALLMENTAMOUNT", DbType.String, 50, oSGCL.INSTALLMENTAMOUNT == "" ? "0" : oSGCL.INSTALLMENTAMOUNT);
                        oDatabase.AddInOutParameter(oDbCommand, "p_TXID", DbType.String, 50, oSGCL.TXID);
                        oDatabase.AddInOutParameter(oDbCommand, "p_ENTRYDATE", DbType.String, 20, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_CREATEBY", DbType.String, 20, oSGCL.UserInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_BRANCHID", DbType.String, 10, oSGCL.Branch.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_APITYPE", DbType.String, 100, oSGCL.APITYPE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_BILLSTATUS", DbType.String, 50, oSGCL.BILLSTATUS);
                        oDatabase.AddInOutParameter(oDbCommand, "p_DNCODE", DbType.String, 50, oSGCL.DNCODE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_DNDETAILS", DbType.String, 200, oSGCL.DNDETAILS);
                        oDatabase.AddInOutParameter(oDbCommand, "p_DNBATYPE", DbType.String, 50, oSGCL.DNBATYPE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_TRANSACTIONNO", DbType.String, 50, oSGCL.TRANSACTIONNO);
                        oDatabase.AddInOutParameter(oDbCommand, "p_STAMPAMOUNT", DbType.String, 50, oSGCL.STAMPAMOUNT == "" ? "0" : oSGCL.STAMPAMOUNT);
                        oDatabase.AddInOutParameter(oDbCommand, "p_COMMENTS", DbType.String, 200, oSGCL.COMMENTS);
                        oDatabase.AddInOutParameter(oDbCommand, "p_BILLTYPE", DbType.String, 200, oSGCL.BillType);
                        oDatabase.AddInOutParameter(oDbCommand, "p_ZONE", DbType.String, 200, oSGCL.ZONE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_CATEGORY", DbType.String, 200, oSGCL.CATEGORY);
                        oDatabase.AddInOutParameter(oDbCommand, "p_OperationType", DbType.String, 40, oSGCL.OperationType);
                        oDatabase.AddInOutParameter(oDbCommand, "p_transactionType", DbType.String, 50, oSGCL.TransactionType);
                        oDatabase.AddInOutParameter(oDbCommand, "p_Success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_Message", DbType.String, 500, DBNull.Value);


                        oDatabase.ExecuteNonQuery(oDbCommand);
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Message")]).Value.ToString();
                        string sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Success")]).Value.ToString();
                        //string sTrackerno = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_trackingnumber")]).Value.ToString();
                        string sBillId = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_BILLID")]).Value.ToString();

                        if (sSuccess == "1")
                        {
                            oSGCL.Status = sSuccess;
                            //oSGCL.Trackerno = sTrackerno;
                            oSGCL.CN = sBillId;
                            oResult.Return = oSGCL;
                            oResult.Message = sMessage;
                            oResult.Result = true;
                        }
                        else
                        {
                            oResult.Return = null;
                            oResult.Message = sMessage;
                            oResult.Result = false;
                        }
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/SaveSearchBillInfo: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/SaveSearchBillInfo: " + exp.Message);
                return oResult;
            }

        }

        public CResult GetBillInfoByBranchForCBS(CSGCL oSEBPaymDmp)
        {
            //GET_SUMMARY_REPORT

            CResult oResult = new CResult();
            Database oDatabase = new Database();
            

            DataSet oDataSet = null;
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_KGCL.GET_SUMMARY_REPORT");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_PAY_FROM_DATE", DbType.String, 50, oSEBPaymDmp.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_PAY_TO_DATE", DbType.String, 100, oSEBPaymDmp.ToDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BILL_TYPE", DbType.String, 100, oSEBPaymDmp.BillType);
                        oDatabase.AddInOutParameter(oDbCommand, "P_CATEGORY", DbType.String, 100, oSEBPaymDmp.CATEGORY);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ISGOVT", DbType.String, 100, oSEBPaymDmp.ISGOVT);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BRANCHID", DbType.String, 100, oSEBPaymDmp.Branch.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_USERID", DbType.String, 100, oSEBPaymDmp.UserInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_IAPPID", DbType.String, 100, "");
                        oDatabase.AddOutParameter(oDbCommand, "rc_list", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);

                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);
                        if (oDataSet.Tables != null)
                        {

                            DataTable dtPending = new DataTable();
                            dtPending = oDataSet.Tables[0];
                            // Build Object
                            for (int i = 0; i < dtPending.Rows.Count; i++)
                            {
                                oSEBPaymDmp.TOTALAMOUNT = dtPending.Rows[i]["cbsTOTALAMOUNT"].ToString();
                                oSEBPaymDmp.SURCHARGE = dtPending.Rows[i]["SURCHARGE"].ToString();
                                oSEBPaymDmp.METERCHARGE = dtPending.Rows[i]["METERCHARGE"].ToString();
                                oSEBPaymDmp.AITAMOUNT = dtPending.Rows[i]["AITAMOUNT"].ToString();
                                oSEBPaymDmp.STAMPAMOUNT = dtPending.Rows[i]["STAMPAMOUNT"].ToString();
                                oSEBPaymDmp.BILLAMOUNT = dtPending.Rows[i]["BILLAMOUNT"].ToString();
                                oSEBPaymDmp.Status = dtPending.Rows[i]["totalbill"].ToString();// total number of bills
                            }
                            oResult.Return = oSEBPaymDmp;
                            oResult.Result = true;
                        }
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetBillInfoByBranchForCBS" + exp.Message);
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
                return oResult;
            }

        }


        public CResult GetBPODate(CSGCL oSGCL)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_KGCL.CalculateBPODate");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "p_CUSTOMERCODE", DbType.String, 50, oSGCL.CUSTOMERCODE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_MOBILENO", DbType.String, 20, oSGCL.MOBILENO);
                        
                        oDatabase.AddInOutParameter(oDbCommand, "p_TOTALBILL", DbType.String, 50, oSGCL.TOTALAMOUNT == "" ? "0" : oSGCL.TOTALAMOUNT);
                        
                        oDatabase.AddInOutParameter(oDbCommand, "p_BILLMONTH", DbType.String, 70, oSGCL.BILLMONTH);
                        oDatabase.AddInOutParameter(oDbCommand, "p_BILLYEAR", DbType.String, 10, oSGCL.BILLYEAR);
                        oDatabase.AddInOutParameter(oDbCommand, "p_LASTPAYMENTDATE", DbType.String, 20, oSGCL.LASTPAYMENTDATE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_INSTALLMENTNO", DbType.String, 50, oSGCL.INSTALLMENTNO == "" ? "0" : oSGCL.INSTALLMENTNO);
                        oDatabase.AddInOutParameter(oDbCommand, "p_INSTALLMENTAMOUNT", DbType.String, 50, oSGCL.INSTALLMENTAMOUNT == "" ? "0" : oSGCL.INSTALLMENTAMOUNT);
                        oDatabase.AddInOutParameter(oDbCommand, "p_ENTRYDATE", DbType.String, 20, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_CREATEBY", DbType.String, 20, oSGCL.UserInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_BRANCHID", DbType.String, 10, oSGCL.Branch.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_DNCODE", DbType.String, 50, oSGCL.DNCODE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_BILLTYPE", DbType.String, 200, oSGCL.BillType);
                        oDatabase.AddInOutParameter(oDbCommand, "p_ZONE", DbType.String, 200, oSGCL.ZONE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_CATEGORY", DbType.String, 200, oSGCL.CATEGORY);
                        oDatabase.AddInOutParameter(oDbCommand, "p_OperationType", DbType.String, 40, oSGCL.OperationType);
                        oDatabase.AddInOutParameter(oDbCommand, "p_transactionType", DbType.String, 50, oSGCL.TransactionType);
                        oDatabase.AddInOutParameter(oDbCommand, "p_Success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_Message", DbType.String, 500, DBNull.Value);
                        oDatabase.ExecuteNonQuery(oDbCommand);
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Message")]).Value.ToString();
                        string sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Success")]).Value.ToString();
                       
                       oSGCL.POCDATE= ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_ENTRYDATE")]).Value.ToString();

                        if (sSuccess == "1")
                        {
                            oSGCL.Status = sSuccess;
                          
                            oResult.Return = oSGCL;
                            oResult.Message = sMessage;
                            oResult.Result = true;
                        }
                        else
                        {
                            oResult.Return = null;
                            oResult.Message = sMessage;
                            oResult.Result = false;
                        }
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetBPODate: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetBPODate: " + exp.Message);
                return oResult;
            }

        }


        [Author("Md. Munirul Islam", "14-11-2018", "Bill NM Post/Receive")]
        public CResult PostNM(CSGCL oSGCL)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_KGCL.add_BillInfo");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "p_BILLID", DbType.String, 50, oSGCL.CN == "" ? "0" : oSGCL.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_CUSTOMERCODE", DbType.String, 50, oSGCL.CUSTOMERCODE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_MOBILENO", DbType.String, 20, oSGCL.MOBILENO);
                        oDatabase.AddInOutParameter(oDbCommand, "p_CUSTOMERNAME", DbType.String, 150, oSGCL.CUSTOMERNAME);
                        oDatabase.AddInOutParameter(oDbCommand, "p_BILLAMOUNT", DbType.String, 20, oSGCL.BILLAMOUNT == "" ? "0" : oSGCL.BILLAMOUNT);  // Gas Bill
                        oDatabase.AddInOutParameter(oDbCommand, "p_SURCHARGE", DbType.String, 40, oSGCL.SURCHARGE == "" ? "0" : oSGCL.SURCHARGE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_METERCHARGE", DbType.String, 40, oSGCL.METERCHARGE == "" ? "0" : oSGCL.METERCHARGE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_TOTALBILL", DbType.String, 50, oSGCL.TOTALAMOUNT == "" ? "0" : oSGCL.TOTALAMOUNT);
                        oDatabase.AddInOutParameter(oDbCommand, "p_AITAMOUNT", DbType.String, 40, oSGCL.AITAMOUNT == "" ? "0" : oSGCL.AITAMOUNT);
                        oDatabase.AddInOutParameter(oDbCommand, "p_BILLMONTH", DbType.String, 70, oSGCL.BILLMONTH);
                        oDatabase.AddInOutParameter(oDbCommand, "p_BILLYEAR", DbType.String, 10, oSGCL.BILLYEAR);
                        oDatabase.AddInOutParameter(oDbCommand, "p_LASTPAYMENTDATE", DbType.String, 20, oSGCL.LASTPAYMENTDATE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_ISGOVT", DbType.String, 10, oSGCL.ISGOVT);
                        oDatabase.AddInOutParameter(oDbCommand, "p_POCBRANCHNAME", DbType.String, 100, oSGCL.POCBRANCHNAME);
                        oDatabase.AddInOutParameter(oDbCommand, "p_POCBANKNAME", DbType.String, 100, oSGCL.POCBANKNAME);
                        oDatabase.AddInOutParameter(oDbCommand, "p_POCDATE", DbType.String, 20, oSGCL.POCDATE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_POCNO", DbType.String, 50, oSGCL.POCNO);
                        oDatabase.AddInOutParameter(oDbCommand, "p_POAUTHORIZEDDATE", DbType.String, 50, oSGCL.POAUTHORIZEDDATE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_BPODATE", DbType.String, 50, oSGCL.BPODATE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_BPODETAILS", DbType.String, 50, oSGCL.BPODetails);
                        oDatabase.AddInOutParameter(oDbCommand, "p_INSTALLMENTNO", DbType.String, 50, oSGCL.INSTALLMENTNO == "" ? "0" : oSGCL.INSTALLMENTNO);
                        oDatabase.AddInOutParameter(oDbCommand, "p_INSTALLMENTAMOUNT", DbType.String, 50, oSGCL.INSTALLMENTAMOUNT == "" ? "0" : oSGCL.INSTALLMENTAMOUNT);
                        oDatabase.AddInOutParameter(oDbCommand, "p_TXID", DbType.String, 50, oSGCL.TXID);
                        oDatabase.AddInOutParameter(oDbCommand, "p_ENTRYDATE", DbType.String, 20, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_CREATEBY", DbType.String, 20, oSGCL.UserInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_BRANCHID", DbType.String, 10, oSGCL.Branch.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_APITYPE", DbType.String, 100, oSGCL.APITYPE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_BILLSTATUS", DbType.String, 50, oSGCL.BILLSTATUS);
                        oDatabase.AddInOutParameter(oDbCommand, "p_DNCODE", DbType.String, 50, oSGCL.DNCODE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_DNDETAILS", DbType.String, 200, oSGCL.DNDETAILS);
                        oDatabase.AddInOutParameter(oDbCommand, "p_DNBATYPE", DbType.String, 50, oSGCL.DNBATYPE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_TRANSACTIONNO", DbType.String, 50, oSGCL.TRANSACTIONNO);
                        oDatabase.AddInOutParameter(oDbCommand, "p_STAMPAMOUNT", DbType.String, 50, oSGCL.STAMPAMOUNT == "" ? "0" : oSGCL.STAMPAMOUNT); 
                        oDatabase.AddInOutParameter(oDbCommand, "p_COMMENTS", DbType.String, 200, oSGCL.COMMENTS);
                        oDatabase.AddInOutParameter(oDbCommand, "p_BILLTYPE", DbType.String, 200, oSGCL.BillType);
                        oDatabase.AddInOutParameter(oDbCommand, "p_ZONE", DbType.String, 200, oSGCL.ZONE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_CATEGORY", DbType.String, 200, oSGCL.CATEGORY);
                        oDatabase.AddInOutParameter(oDbCommand, "p_OperationType", DbType.String, 40, oSGCL.OperationType);
                        oDatabase.AddInOutParameter(oDbCommand, "p_transactionType", DbType.String, 50, oSGCL.TransactionType);
                        oDatabase.AddInOutParameter(oDbCommand, "p_Success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_Message", DbType.String, 500, DBNull.Value);
                        
                        
                        oDatabase.ExecuteNonQuery(oDbCommand);
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Message")]).Value.ToString();
                        string sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Success")]).Value.ToString();
                        //string sTrackerno = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_trackingnumber")]).Value.ToString();
                        string sBillId = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_BILLID")]).Value.ToString();

                        if (sSuccess == "1")
                        {
                            oSGCL.Status = sSuccess;
                            //oSGCL.Trackerno = sTrackerno;
                            oSGCL.CN = sBillId;
                            oResult.Return = oSGCL;
                            oResult.Message = sMessage;
                            oResult.Result = true;
                        }
                        else
                        {
                            oResult.Return = null;
                            oResult.Message = sMessage;
                            oResult.Result = false;
                        }
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/PostNM: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/PostNM: " + exp.Message);
                return oResult;
            }

        }


        
        [Author("Md. Aminul Islam", "21-09-2017", "Get NM/NMI bill details")]
        public CResult GetPaidBillDetailsNM(CPayment oPayment)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet ds = new DataSet();

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_TITAS.Get_NMBillDetails");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_paymentId", DbType.String, 50, oPayment.PaymentId);
                        oDatabase.AddInOutParameter(oDbCommand, "p_OperationType", DbType.String, 50, oPayment.OperationType);
                        oDatabase.AddOutParameter(oDbCommand, "rcBillList", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_Success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_Message", DbType.String, 50, DBNull.Value);
                        ds = oDatabase.ExecuteDataSet(oDbCommand);
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Message")]).Value.ToString();
                        string sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Success")]).Value.ToString();
                        if (ds.Tables.Count != 0)
                        {
                            oResult.Return = ds;
                            oResult.Result = true;
                            oResult.Message = sMessage;
                        }
                        else
                        {
                            oResult.Return = null;
                            oResult.Result = false;
                            oResult.Message = sMessage;
                        }

                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetPaidBillDetailsNM: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetPaidBillDetailsNM: " + exp.Message);
                return oResult;
            }
        }
               
       
        
        
        [Author("Md. Aminul Islam", "25-11-2018", "Get Report Data")]
        public CResult GetReport(CSGCL oSGCL)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_REPORTS.Get_BillReports");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_IAPPID", DbType.Int32, 50, oSGCL.UserInfo.IAPPID);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BranchId", DbType.Int32, 50, oSGCL.Branch.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_IsSelected", DbType.String, 50, oSGCL.Branch.Status);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ReportName", DbType.String, 50, oSGCL.OperationType);
                        oDatabase.AddInOutParameter(oDbCommand, "p_FromDate", DbType.String, 50, oSGCL.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_ToDate", DbType.String, 50, oSGCL.ToDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_OperationType", DbType.String, 50, oSGCL.OperationType);

                        oDatabase.AddInOutParameter(oDbCommand, "p_BillCategory", DbType.String, 50, oSGCL.BillType);
                        oDatabase.AddInOutParameter(oDbCommand, "p_IsGovt", DbType.String, 50, oSGCL.ISGOVT);

                        oDatabase.AddInOutParameter(oDbCommand, "p_UserCode", DbType.String, 50, oSGCL.UserInfo.CN);
                        oDatabase.AddOutParameter(oDbCommand, "rcBillDetails", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_Success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_Message", DbType.String, 50, DBNull.Value);
                        ds = oDatabase.ExecuteDataSet(oDbCommand);
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Message")]).Value.ToString();
                        string sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Success")]).Value.ToString();
                        if (ds.Tables[0] != null)
                        {
                            dt = ds.Tables[0];
                            oResult.Return = dt;
                            oResult.Result = true;
                            oResult.Message = sMessage;
                        }
                        else
                        {
                            oResult.Return = null;
                            oResult.Result = false;
                            oResult.Message = sMessage;
                        }

                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetReport: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetReport: " + exp.Message);
                return oResult;
            }
        }

        
        [Author("Md. Aminul Islam", "08-07-2018", "Get Reconcile Report Data")]
        public CResult GetReconcileData(CSGCL oSGCL)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_REPORTS.Get_ReconcileData");
                    try
                    {  
                        oDatabase.AddInOutParameter(oDbCommand, "p_FromDate", DbType.String, 50, oSGCL.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_ToDate", DbType.String, 50, oSGCL.ToDate);
                        oDatabase.AddOutParameter(oDbCommand, "rcBillDetails", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_Success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_Message", DbType.String, 50, DBNull.Value);
                        ds = oDatabase.ExecuteDataSet(oDbCommand);
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Message")]).Value.ToString();
                        string sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Success")]).Value.ToString();
                        if (ds.Tables[0] != null)
                        {
                            //dt = ds.Tables[0];
                            oResult.Return = ds;
                            oResult.Result = true;
                            oResult.Message = sMessage;
                        }
                        else
                        {
                            oResult.Return = null;
                            oResult.Result = false;
                            oResult.Message = sMessage;
                        }

                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetReconcileData: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetReconcileData: " + exp.Message);
                return oResult;
            }
        }
        
        
        [Author("Md. Aminul Islam", "26-09-2017", "Get Audit Report Data")]
        public CResult GetAuditReport(CBillInfo oBillInfo)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_TITAS.Get_BillDetails");
                    try
                    {

                        oDatabase.AddInOutParameter(oDbCommand, "P_BranchId", DbType.Int32, 50, oBillInfo.Branch.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ReportName", DbType.String, 50, oBillInfo.OperationType);
                        oDatabase.AddInOutParameter(oDbCommand, "p_FromDate", DbType.String, 50, oBillInfo.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_ToDate", DbType.String, 50, oBillInfo.ToDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_OperationType", DbType.String, 50, oBillInfo.OperationType);
                        oDatabase.AddInOutParameter(oDbCommand, "p_InvoiceNo", DbType.String, 50, oBillInfo.invoiceNo);
                        oDatabase.AddInOutParameter(oDbCommand, "p_CustomerCode", DbType.String, 50, oBillInfo.customerCode);
                        //oDatabase.AddInOutParameter(oDbCommand, "p_UserCode", DbType.String, 50, oBillInfo.UserInfo.UserCode);
                        oDatabase.AddInOutParameter(oDbCommand, "p_Success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_Message", DbType.String, 50, DBNull.Value);
                        ds = oDatabase.ExecuteDataSet(oDbCommand);
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Message")]).Value.ToString();
                        string sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Success")]).Value.ToString();
                        if (ds.Tables[0] != null)
                        {
                            dt = ds.Tables[0];
                            oResult.Return = dt;
                            oResult.Result = true;
                            oResult.Message = sMessage;
                        }
                        else
                        {
                            oResult.Return = null;
                            oResult.Result = false;
                            oResult.Message = sMessage;
                        }

                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetAuditReport: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetAuditReport: " + exp.Message);
                return oResult;
            }
        }
        
        [Author("Md. Aminul Islam", "04-10-2017", "Metered Zone List")]
        public CResult GetZoneList(CZone oZone)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_REPORTS.Get_zoneList");
                    try
                    {
                        oDatabase.AddOutParameter(oDbCommand, "rc_list", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 50, DBNull.Value);
                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        if (oDataSet.Tables != null)
                        {
                           oResult.Return = BuildZoneEntity(oDataSet.Tables[0]);

                        }
                        else
                            oResult.Return = null;

                    }
                    catch (Exception exp)
                    {
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
                return oResult;
            }
        }

        private CZoneList BuildZoneEntity(DataTable dt)
        {
            CZoneList oZoneList = new CZoneList();
            CZone oZone = new CZone();
            CZone oZoneAdd = new CZone();
            oZoneAdd.Zone_Name = "Select Zone";
            oZoneAdd.CN = "";
            oZoneList.ZoneList.Add(oZoneAdd);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                oZone = new CZone();
                oZone.CN= Convert.ToString(dt.Rows[i]["SL"]);
                oZone.Zone_Name = Convert.ToString(dt.Rows[i]["ZONE"]);
                oZoneList.ZoneList.Add(oZone);
            }
            return oZoneList;
        }
               
        [Author("Md. Aminul Islam", "04-10-2017", "Metered Zone List")]
        public CResult GetNMZoneList(CZone cZone)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_REPORTS.Get_NMzoneList");
                    try
                    {
                        oDatabase.AddOutParameter(oDbCommand, "rc_list", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 50, DBNull.Value);
                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = BuildNMZoneEntity(oDataSet.Tables[0]);

                        }
                        else
                            oResult.Return = null;

                    }
                    catch (Exception exp)
                    {
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
                return oResult;
            }
        }

        private CZoneList BuildNMZoneEntity(DataTable dt)
        {
            CZoneList oZoneList = new CZoneList();
            CZone oZone = new CZone();
            CZone oZoneAdd = new CZone();
            oZoneAdd.Zone_Name = "Select Zone";
            oZoneAdd.CN = "";
            oZoneList.ZoneList.Add(oZoneAdd);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                oZone = new CZone();
                oZone.CN = Convert.ToString(dt.Rows[i]["SL"]);
                oZone.Zone_Name = Convert.ToString(dt.Rows[i]["ZONENAME"]);
                oZoneList.ZoneList.Add(oZone);
            }
            return oZoneList;
        }
        
        public CResult GetPaymentDetailsAbabilPosting(CBillInfo oBillInfo)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet ds = new DataSet();
            CBillInfo oBillInfoData = new CBillInfo();
            string sTotalAmount = string.Empty;
            string sSurchargeAmount = string.Empty;
            string sStamp = string.Empty;
            string sInvoice = string.Empty;
            string sTotalNoOfBills = string.Empty;
            
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_TITAS.Get_AMOUNTDETAILS");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "p_BRANCHID", DbType.String, 50, oBillInfo.Branch.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_TotalAmount", DbType.String, 50, oBillInfo.PaidAmount);
                        oDatabase.AddInOutParameter(oDbCommand, "p_TotalSurcharge", DbType.String, 50, oBillInfo.SurchargeAmount);
                        oDatabase.AddInOutParameter(oDbCommand, "p_TotalStamp", DbType.String, 50, oBillInfo.RevenueStamp);
                        oDatabase.AddInOutParameter(oDbCommand, "p_TransactionDate", DbType.String, 50, oBillInfo.ToDate); //ToDate used as Voucher
                        oDatabase.AddInOutParameter(oDbCommand, "p_OperationType", DbType.String, 50, oBillInfo.OperationType);
                        oDatabase.AddOutParameter(oDbCommand, "rcAmount", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_Success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_Message", DbType.String, 50, DBNull.Value);
                        ds = oDatabase.ExecuteDataSet(oDbCommand);
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Message")]).Value.ToString();
                        string sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Success")]).Value.ToString();

                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                sTotalAmount = ds.Tables[0].Rows[0]["paidamount"].ToString();  // to be Paid
                                sSurchargeAmount = ds.Tables[0].Rows[0]["totalsurcharge"].ToString();
                                sStamp = ds.Tables[0].Rows[0]["totalstampamount"].ToString();
                                sInvoice = ds.Tables[0].Rows[0]["totalamount"].ToString(); // Invoice Amount
                                sTotalNoOfBills = ds.Tables[0].Rows[0]["ttlno"].ToString(); // Number of Bill
                            }
                            oBillInfoData.PaidAmount = sTotalAmount;
                            oBillInfoData.SurchargeAmount = sSurchargeAmount;
                            oBillInfoData.RevenueStamp = sStamp;
                            oBillInfoData.invoiceAmount = sInvoice;
                            oBillInfoData.TotalNumberOfBills = sTotalNoOfBills;
                            oResult.Return = oBillInfoData;
                          
                        }

                        if (sSuccess == "1")
                            oResult.Result = true;
                        else
                            oResult.Result = false;

                        oResult.Message = sMessage;
                        oResult.Status = sSuccess;


                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetMBillDetails: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetMBillDetails: " + exp.Message);
                return oResult;
            }
        }

        public CBranch getRoutingNo(string branchid)
        {
           
            Database oDatabase = new Database();
            DataSet ds = new DataSet();
             
            string sBrRoutingNo = string.Empty;
            string sBrCode = string.Empty;
            CBranch oBranch = new CBranch();
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_KGCL.GetBrRoutingByID");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "p_BRANCHID", DbType.String, 50, branchid);
                        oDatabase.AddInOutParameter(oDbCommand, "p_BRANCHCode", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_BrRoutingNo", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_Success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_Message", DbType.String, 50, DBNull.Value);
                        IDataReader oReader = oDatabase.ExecuteDataReader(oDbCommand);
                        oBranch.RoutingNumber = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_BrRoutingNo")]).Value.ToString();
                        oBranch.BranchCode = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_BRANCHCode")]).Value.ToString();
                        oBranch.BranchName = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Message")]).Value.ToString(); // branch name
                        
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/getRoutingNo: BranchId: " + branchid+ " Br RoutingNo:"+sBrRoutingNo);

                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/getRoutingNo: " + exp.Message);
                         
                    }
                    finally
                    {
                        oConnection.Close();
                    }
                }
                
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/getRoutingNo: " + exp.Message);
                 
            }
            return oBranch;
        }

        [Author("Md. Aminul Islam", "25-10-2017", "Ababil Request Response Save")]
        public CResult AbabilReqResSave(CAbabilTransactionResponse oAbabilTransactionResponse)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_KGCL.SAVERFL_REQ_RESPONSE");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_RESID", DbType.String, 50, oAbabilTransactionResponse.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_DESCRTRIPTION", DbType.String, 200, oAbabilTransactionResponse.Description);
                        oDatabase.AddInOutParameter(oDbCommand, "P_RESPONSERREF", DbType.String, 200, oAbabilTransactionResponse.ResponseReference);
                        oDatabase.AddInOutParameter(oDbCommand, "P_REQUESTREF ", DbType.String, 200, oAbabilTransactionResponse.RequestReference);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ISREVERSE", DbType.String, 5, "");
                        oDatabase.AddInOutParameter(oDbCommand, "P_REVERSEBY ", DbType.String, 10, oAbabilTransactionResponse.User.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_CREATEBY", DbType.String, 10, oAbabilTransactionResponse.User.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_APPREQUESTID", DbType.String, 50, oAbabilTransactionResponse.AppRequestId);
                        oDatabase.AddInOutParameter(oDbCommand, "P_OperationType", DbType.String, 50, oAbabilTransactionResponse.OperationType);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);
                        oDatabase.ExecuteNonQuery(oDbCommand);
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                        string sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_success")]).Value.ToString();
                      
                        if (sSuccess == "1")
                        {                                                   
                            oResult.Message = sMessage;
                            oResult.Status = sSuccess;    
                            oResult.Result = true;
                        }
                        else
                        {
                            oResult.Return = null;
                            oResult.Message = sMessage;
                            oResult.Status = sSuccess;
                            oResult.Result = false;
                        }
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/AbabilReqResSave: " + sMessage);
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/AbabilReqResSave: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/AbabilReqResSave: " + exp.Message);
                return oResult;
            }

        }
        
        [Author("Md. Aminul Islam", "25-10-2017", "Save CBS Transaction")]
        public CResult SaveCBSTransaction(CSGCL oBillInfo)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_KGCL.DOCBSTRANSACTION");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_CBSID", DbType.String, 50, oBillInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_TRANSTYPE", DbType.String, 50, oBillInfo.TransactionType);
                        oDatabase.AddInOutParameter(oDbCommand, "P_TRANSDATE", DbType.String, 150, oBillInfo.TransactionDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_AMOUNT ", DbType.String, 50, oBillInfo.AbabilTransactionResponse.Description);
                        oDatabase.AddInOutParameter(oDbCommand, "P_REQUESTID", DbType.String, 50, oBillInfo.AbabilTransactionResponse.AppRequestId);
                        oDatabase.AddInOutParameter(oDbCommand, "P_REUESTREFNO", DbType.String, 50, oBillInfo.AbabilTransactionResponse.RequestReference);
                        oDatabase.AddInOutParameter(oDbCommand, "P_RESPONSEID ", DbType.String, 20, oBillInfo.AbabilTransactionResponse.ResponseReference);
                        oDatabase.AddInOutParameter(oDbCommand, "P_STATUS ", DbType.String, 10, oBillInfo.Status);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BRANCHID", DbType.String, 50, oBillInfo.Branch.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_CREATEBY", DbType.String, 50, oBillInfo.UserInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_FROMDATE", DbType.String, 50, oBillInfo.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_TODATE", DbType.String, 50, oBillInfo.ToDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_OperationType", DbType.String, 50, oBillInfo.OperationType);
                        oDatabase.AddInOutParameter(oDbCommand, "P_Comments", DbType.String, 50, oBillInfo.COMMENTS);                        
                        oDatabase.AddInOutParameter(oDbCommand, "P_Success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_Message", DbType.String, 500, DBNull.Value);
                        oDatabase.ExecuteNonQuery(oDbCommand);
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_Message")]).Value.ToString();
                        string sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_Success")]).Value.ToString();
                        string sCBSTransactionID = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_CBSID")]).Value.ToString();
                        if (sSuccess == "1")
                        {
                            oResult.Message = sMessage;
                            oResult.Status = sSuccess;
                            oResult.Return = sCBSTransactionID;
                            oResult.Result = true;
                        }
                        else
                        {
                            oResult.Return = null;
                            oResult.Message = sMessage;
                            oResult.Status = sSuccess;
                            oResult.Result = false;
                        }
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/SaveCBSTransaction: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/SaveCBSTransaction: " + exp.Message);
                return oResult;
            }

        }

        public CResult GetCBSTransDtlByBranch(CSGCL oBillInfo)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet ds = new DataSet();
            CSGCL oBillInfoData = new CSGCL();
            CSGCLList oBillInfoDataList = new CSGCLList();
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_KGCL.GetCBSTransDtlByBranch");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_BRANCHID", DbType.String, 50, oBillInfo.Branch.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_TRANSACTIONDATE", DbType.String, 50, oBillInfo.TransactionDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_TRANSTYPE", DbType.String, 50, oBillInfo.TransactionType);
                        oDatabase.AddOutParameter(oDbCommand, "rcDetail", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "P_OperationType", DbType.String, 50, oBillInfo.OperationType);                        
                        oDatabase.AddInOutParameter(oDbCommand, "p_Success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 50, DBNull.Value);
                        ds = oDatabase.ExecuteDataSet(oDbCommand);
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                        string sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Success")]).Value.ToString();

                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                oBillInfoData = new CSGCL();
                                oBillInfoData.CN = ds.Tables[0].Rows[0]["CBSID"].ToString();
                                oBillInfoData.TransactionType = ds.Tables[0].Rows[0]["TRANSTYPE"].ToString();
                                oBillInfoData.TransactionDate = ds.Tables[0].Rows[0]["TRANSDATE"].ToString();
                                oBillInfoData.AbabilTransactionResponse.Description = ds.Tables[0].Rows[0]["AMOUNT"].ToString();
                                oBillInfoData.AbabilTransactionResponse.AppRequestId = ds.Tables[0].Rows[0]["REQUESTID"].ToString();
                                oBillInfoData.AbabilTransactionResponse.RequestReference = ds.Tables[0].Rows[0]["REUESTREFNO"].ToString();
                                oBillInfoData.AbabilTransactionResponse.ResponseReference = ds.Tables[0].Rows[0]["RESPONSEID"].ToString();
                                oBillInfoData.Status = ds.Tables[0].Rows[0]["STATUS"].ToString();
                                oBillInfoData.Branch.CN = ds.Tables[0].Rows[0]["BRANCHID"].ToString();
                                oBillInfoData.UserInfo.CN = ds.Tables[0].Rows[0]["CREATEBY"].ToString();
                                oBillInfoData.AbabilTransactionResponse.Status = ds.Tables[0].Rows[0]["CBSSTATUS"].ToString();
                                oBillInfoData.FromDate = ds.Tables[0].Rows[0]["CBSTRANSDATE"].ToString();
                                oBillInfoDataList.BillInfoList.Add(oBillInfoData);
                            }

                            oResult.Return = oBillInfoDataList;
                            oResult.Result = true;
                        }

                        oResult.Message = sMessage;
                        oResult.Status = sSuccess;
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetCBSTransDtlByBranch: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetCBSTransDtlByBranch: " + exp.Message);
                return oResult;
            }
        }


        public CResult GetMismatchList(CBillInfo oBillInfo)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet ds = new DataSet();
            CBillInfo oBillInfoData = new CBillInfo();
            CBillInfoList oBillInfoDataList = new CBillInfoList();
            
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_HISTORY.GetRconcileList");
                    try
                    {


                        oDatabase.AddInOutParameter(oDbCommand, "P_BRANCHID", DbType.String, 50, oBillInfo.Branch.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ToDate", DbType.String, 50, oBillInfo.ToDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_FromDate", DbType.String, 50, oBillInfo.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_TRANSACTIONDATE", DbType.String, 50, oBillInfo.TransactionDate);
                        oDatabase.AddOutParameter(oDbCommand, "rcBillList", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "P_OPERATIONTYPE", DbType.String, 50, oBillInfo.OperationType);
                        oDatabase.AddInOutParameter(oDbCommand, "p_Success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_Message", DbType.String, 50, DBNull.Value);
                        ds = oDatabase.ExecuteDataSet(oDbCommand);
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Message")]).Value.ToString();
                        string sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Success")]).Value.ToString();

                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                oBillInfoDataList.BillInfoList = 
                                    (from DataRow row in ds.Tables[0].Rows
                                                                  select new CBillInfo()
                                                                  {
                                                                      PaymentId = row["PAYMENTID"].ToString(),
                                                                      TransactionDate = row["TRANSACTIONDATE"].ToString(),
                                                                      customerCode = row["CUSTOMERCODE"].ToString(),

                                                                      Status = row["TRANSACTIONSTATUS"].ToString(),
                                                                      invoiceNo = row["INVOICENO"].ToString(),
                                                                      invoiceAmount = row["INVOICEAMOUNT"].ToString(),

                                                                      PaidAmount = row["PAIDAMOUNT"].ToString(),
                                                                      SourceTaxAmount = row["SOURCETAXAMOUNT"].ToString(),
                                                                      RevenueStamp = row["REVENUESTAMP"].ToString(),
                                                                      BranchRoutingNo = row["branchRoutingNo"].ToString(),
                                                                      RefNo = row["REFNO"].ToString(),
                                                                      Reason = row["REASON"].ToString()

                                                                  }).ToList();
                            }

                            oResult.Return = oBillInfoDataList;
                            oResult.Result = true;
                        }

                        oResult.Message = sMessage;
                        oResult.Status = sSuccess;
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetMismatchList: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetMismatchList: " + exp.Message);
                return oResult;
            }
        }

        public CResult ReconcileBill(CBillInfo oBillInfo)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet ds = new DataSet();
            CBillInfo oBillInfoData = new CBillInfo();
            CBillInfoList oBillInfoDataList = new CBillInfoList();

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_HISTORY.RECONCILEBILL");
                    try
                    {

                      
                        oDatabase.AddInOutParameter(oDbCommand, "P_BRANCHID", DbType.String, 50, oBillInfo.Branch.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_CREATEBY", DbType.String, 50, oBillInfo.UserInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_PAYMENTID", DbType.String, 50, oBillInfo.PaymentId);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BANKTRANSID", DbType.String, 50, oBillInfo.TrackerNo);
                        
                        oDatabase.AddInOutParameter(oDbCommand, "P_OPERATIONTYPE", DbType.String, 50, oBillInfo.OperationType);
                        oDatabase.AddInOutParameter(oDbCommand, "p_Success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_Message", DbType.String, 500, DBNull.Value);
                        ds = oDatabase.ExecuteDataSet(oDbCommand);
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Message")]).Value.ToString();
                        string sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_Success")]).Value.ToString();

                        oResult.Message = sMessage;
                        oResult.Status = sSuccess;
                        if (sSuccess == "1")
                            oResult.Result = true;
                        else
                            oResult.Result = false;

                        CLog.Logger.Write(CLog.INFORMATION,  "/ReconcileBill: " + sMessage);

                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/ReconcileBill: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/ReconcileBill: " + exp.Message);
                return oResult;
            }
        }
    }
}
