/**
 * File name            : CDPDCDA.cs
 * Author               : Md. Aminul Islam 
 * Date                 : June 05,2018
 * Version              : 1.0
 *
 * Description          : This is the Data Access for WR Transactions
 *
 * Modification history :
 * Name                         Date                            Desc
 * Munirul Islam                06-07-2018                      Add methods                         
 * 
 * Copyright (c) 2018: Social Islami Bank Limited
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLRemit.DA.Common.Connections;
using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using SIBLCommon.Common.Util.Attributes;
using SIBLCommon.Common.Entity.Result;
using SIBLCommon.SIBLCommon.Common.Entity.SIBLRemitPay;
using SIBLCommon.Common.Util.Logger;
using SIBLCommon.Common.Entity.File;
using SIBLCommon.SIBLCommon.Common.Entity.CashPayment;
using SIBLCommon.Common.Entity.Bank;
using SIBLCommon.Common.Entity.AllLookup;
using SIBLCommon.SIBLCommon.Common.Entity.RemitGlobalAPI;
using SIBLXoomCommon.SIBLXoom.Common.Entity.CPU;



namespace SIBLRemit.DA.SIBLRemit
{
    public class CSIBLRemitDA
    {
        const string DB_CONNECTION = "ConString";
        const string DB_CONNECTION_CBS = "ConStringCBS";

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



        [Author("Md. Aminul Islam", "21-10-2018", "Update Mismatch Bill")]
        public CResult GetTypeListByKey(CAllLookup oAlllookUp)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_SETUP.GET_TYPE_LIST_ALLLOOKUP");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "p_typekey", DbType.String, 50, oAlllookUp.TYPE_KEY);
                        oDatabase.AddOutParameter(oDbCommand, "rc_lookuplist", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = BuildLookupEntity(oDataSet.Tables[0]);
                        }
                        else
                        {
                            oResult.Return = null;
                        }

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
                        oDataSet.Clear();
                        oDataSet.Dispose();
                        oConnection.Close();
                    }
                }
                return oResult;
            }

            catch (Exception ex)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetTypeListByKey: " + ex.Message);
                oResult.Exception = ex;
                oResult.Message = ex.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;

            }
        }



        private CAllLookupList BuildLookupEntity(DataTable dt)
        {

            CAllLookup oAllLookup = new CAllLookup();
            CAllLookupList oAllLookupList = new CAllLookupList();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //CPlacid oPlacid = new CPlacid();
                oAllLookup = new CAllLookup();
                oAllLookup.CN = Convert.ToString(dt.Rows[i]["ID"]);
                oAllLookup.TYPE_NAME = Convert.ToString(dt.Rows[i]["TYPE_NAME"]);
                oAllLookupList.AllLookupList.Add(oAllLookup);


            }
            return oAllLookupList;
        }


        [Author("Md. Munirul Islam", "21-01-2020", "Save Payout Information")]
        public CResult CRUDPayOut(CSIBLRemitPay oSIBLRemitPay)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_SIBLREMITPAY.CRUDPAYOUTREQUEST");
                    try
                    {

                        oDatabase.AddInOutParameter(oDbCommand, "p_PRID", DbType.String, 50, oSIBLRemitPay.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_IAPPID", DbType.String, 50, oSIBLRemitPay.IAPPID); 
                        oDatabase.AddInOutParameter(oDbCommand, "p_ENTRYDATE", DbType.String, 200, oSIBLRemitPay.ENTRYDATE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_CREATEBY", DbType.String, 100, oSIBLRemitPay.CREATEBY.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_COMPANYID", DbType.String, 100, oSIBLRemitPay.COMPANY.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_BENEFICIARY_NAME", DbType.String, 100, oSIBLRemitPay.BENEFICIARY_NAME);
                        oDatabase.AddInOutParameter(oDbCommand, "p_PINNO", DbType.String, 100, oSIBLRemitPay.PINNO);
                        oDatabase.AddInOutParameter(oDbCommand, "p_AMOUNT", DbType.String, 100, oSIBLRemitPay.AMOUNT);
                        oDatabase.AddInOutParameter(oDbCommand, "p_INCENTIVE_AMOUNT", DbType.String, 100, oSIBLRemitPay.INCENTIVE_AMOUNT);
                        oDatabase.AddInOutParameter(oDbCommand, "p_REMARKS", DbType.String, 100, oSIBLRemitPay.REMARKS);
                        oDatabase.AddInOutParameter(oDbCommand, "p_RNIDNO", DbType.String, 50, oSIBLRemitPay.RNIDNO);
                        oDatabase.AddInOutParameter(oDbCommand, "p_RNIDISSUEDATE", DbType.String, 200, oSIBLRemitPay.RNIDISSUEDATE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_RNIDEXPIRYDATE", DbType.String, 200, oSIBLRemitPay.RNIDEXPIRYDATE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_RNIDTYPE", DbType.String, 200, oSIBLRemitPay.RNIDTYPE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_PAYOUTBY", DbType.String, 200, oSIBLRemitPay.PAYOUTBY);
                        oDatabase.AddInOutParameter(oDbCommand, "p_PAYOUTDATE", DbType.String, 200, oSIBLRemitPay.PAYOUTDATE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_BENEFICIARYADDRESS", DbType.String, 200, oSIBLRemitPay.BENEFICIARYADDRESS);
                        oDatabase.AddInOutParameter(oDbCommand, "p_SENDERNAME", DbType.String, 200, oSIBLRemitPay.SENDERNAME);
                        oDatabase.AddInOutParameter(oDbCommand, "p_SENDERCOUNTRY", DbType.String, 200, oSIBLRemitPay.SENDERCOUNTRY);
                        oDatabase.AddInOutParameter(oDbCommand, "p_SENDERPASSPORTNO", DbType.String, 200, oSIBLRemitPay.SENDERPASSPORTNO);
                        oDatabase.AddInOutParameter(oDbCommand, "p_SENDERBUSINESSLICENSE", DbType.String, 200, oSIBLRemitPay.SENDERBUSINESSLICENSE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_SENDERIDTYPE", DbType.String, 200, oSIBLRemitPay.SENDERIDTYPE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_SENDERIDNO", DbType.String, 200, oSIBLRemitPay.SENDERIDTYPENO);
                        oDatabase.AddInOutParameter(oDbCommand, "p_DOCAPPROVED", DbType.String, 200, oSIBLRemitPay.DOCAPPROVED);
                        oDatabase.AddInOutParameter(oDbCommand, "p_DOCAPPROVEDATE", DbType.String, 200, oSIBLRemitPay.DOCUMENTAPPROVEDATE);
                        oDatabase.AddInOutParameter(oDbCommand, "p_ROUTINGNO", DbType.String, 200, oSIBLRemitPay.ROUTINGNO);
                        oDatabase.AddInOutParameter(oDbCommand, "p_CBSMStatus", DbType.String, 200, oSIBLRemitPay.CBSMStatus);
                        oDatabase.AddInOutParameter(oDbCommand, "p_CBSMDate", DbType.String, 200, oSIBLRemitPay.CBSMDate);

                        oDatabase.AddInOutParameter(oDbCommand, "p_CBSIStatus", DbType.String, 200, oSIBLRemitPay.CBSIStatus);
                        oDatabase.AddInOutParameter(oDbCommand, "p_CBSIDate", DbType.String, 200, oSIBLRemitPay.CBSIDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_BRANCHID", DbType.String, 200, oSIBLRemitPay.CREATEBY.Branch.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_RECEIVERCONTACTNO", DbType.String, 200, oSIBLRemitPay.ReceiverPhoneNo);
                        
                        
                        oDatabase.AddInOutParameter(oDbCommand, "p_oPerationType", DbType.String, 200, oSIBLRemitPay.OperationType);

                        oDatabase.AddInOutParameter(oDbCommand, "p_AGENTID", DbType.String, 200, oSIBLRemitPay.AgentID);
                        oDatabase.AddInOutParameter(oDbCommand, "p_TRANSTYPE", DbType.String, 200, oSIBLRemitPay.PaymentType);// BRANCH, AGENT

                        oDatabase.AddInOutParameter(oDbCommand, "p_ISDMWEB", DbType.String, 10, oSIBLRemitPay.IsMainRemitDisverse);
                        oDatabase.AddInOutParameter(oDbCommand, "p_ISFOREIGNSEBDER", DbType.String, 10, oSIBLRemitPay.ForeignSender);
                        oDatabase.AddInOutParameter(oDbCommand, "p_PAYOUTOPTION", DbType.String, 30, oSIBLRemitPay.RemitOnly);

                                                    
                                 
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);
                        int i = oDatabase.ExecuteNonQuery(oDbCommand);
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                        string success = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_success")]).Value.ToString();

                        if (success == "1")
                        {
                            oResult.Result = true;

                        }
                        else
                        {
                            oResult.Result = false;
                        }

                        oResult.Status = success;
                        oResult.Message = sMessage;
                        CLog.Logger.Write(CLog.INFORMATION, "/CRUDPayOut: " + sMessage);
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/CRUDPayOut: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/CRUDPayOut: " + exp.Message);
                return oResult;
            }

        }
        public CResult SaveAbabilResponse(CAbabilTransactionResponse oAbabilTransactionResponse)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_SIBLREMITPAY.SAVECBS_RESPONSE");
                    try
                    {

                        oDatabase.AddInOutParameter(oDbCommand, "P_RESID", DbType.String, 50, oAbabilTransactionResponse.CN);
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
                        oAbabilTransactionResponse.CN = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_RESID")]).Value.ToString();



                        if (success == "1")
                        {
                            oResult.Result = true;

                        }
                        else
                        {
                            oResult.Result = false;

                        }

                        oResult.Return = oAbabilTransactionResponse;
                        oResult.Message = sMessage;

                        CLog.Logger.Write(CLog.INFORMATION, "/SaveAbabilResponse: " + sMessage);

                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/SaveAbabilResponse: " + exp.Message);
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
        
        [Author("Md. Munirul Islam", "24-0-2020", "Get Branch Information")]
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
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_SIBLREMITPAY.GetBrRoutingByID");
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

                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/getRoutingNo: BranchId: " + branchid + " Br RoutingNo:" + sBrRoutingNo);

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

   


         [Author("Md. Munirul Islam", "14-09-2015", "Is Balance Available")]
         public CResult IsBalanceAvailable(CSIBLRemitPay oSIBLRemitPay)
         {
             CResult oResult = new CResult();
             Database oDatabase = new Database();
             decimal dValue = 0;            
             
             try
             {
                 using (OracleConnection oConnection = new OracleConnection(GetConnectionStrings(DB_CONNECTION_CBS)))
                 {

                     oConnection.Open();
                    
                     OracleCommand oDbCommand = oConnection.CreateCommand();
                     oDbCommand.CommandType = CommandType.Text;
                     oDbCommand.CommandText = "select ACCCURRENTBALANCE from account where acccode='" + oSIBLRemitPay.AccountInfo.FromAccNum+"'";
                     try
                     {

                        object oBal= oDbCommand.ExecuteScalar();
                        
                        try {
                           
                            if (oBal !=null)
                                dValue = Convert.ToDecimal(oBal);
                           
                        }
                        catch (Exception exp)
                        {
                            dValue = 0;
                            CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/IsBalanceAvailable:: Error in conversion to decimal." + exp.Message);
 
                        }

                        if (dValue >= Convert.ToDecimal(oSIBLRemitPay.AMOUNT))
                        {
                            oResult.Result = true;
                            oResult.Message = "Balance is available for payout transaction";
                        }
                        else
                        {
                            oResult.Result = false;
                            oResult.Message = "Balance is not available for payout transaction";
                        }

                     }
                     catch (Exception exp)
                     {
                         CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/IsBalanceAvailable::" + exp.Message);
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
                 CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetBillDetails::" + exp.Message);
                 return oResult;
             }

         }


         [Author("Md. Munirul Islam", "22-01-2020", "Get Requested PayOut Trans List")]
         public CResult GetRequestedPayOutTransList(CSIBLRemitPay oSIBLRemitPay)
         {
             CResult oResult = new CResult();
             Database oDatabase = new Database();
             DataSet oDataSet = null;

             try
             {
                 using (IDbConnection oConnection = oDatabase.CreateConnection())
                 {
                     IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_SIBLREMITPAY.GetRequestedTransaction");
                     try
                     {
                         oDatabase.AddInOutParameter(oDbCommand, "P_IAPPID", DbType.String, 50, oSIBLRemitPay.IAPPID);   // need to modify
                         oDatabase.AddInOutParameter(oDbCommand, "P_BRANCHID", DbType.String, 50, oSIBLRemitPay.CREATEBY.Branch.CN);
                         oDatabase.AddInOutParameter(oDbCommand, "P_USERID", DbType.String, 50, oSIBLRemitPay.CREATEBY.CN);
                         oDatabase.AddInOutParameter(oDbCommand, "P_FromDate", DbType.String, 50, oSIBLRemitPay.FromDate);
                         oDatabase.AddInOutParameter(oDbCommand, "P_ToDate", DbType.String, 50, oSIBLRemitPay.ToDate);
                         oDatabase.AddInOutParameter(oDbCommand, "P_OperationType", DbType.String, 50, oSIBLRemitPay.OperationType);
                         oDatabase.AddOutParameter(oDbCommand, "rc_TransList", "RefCursor", 100);
                         oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                         oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);
                         oDataSet = oDatabase.ExecuteDataSet(oDbCommand);
                         string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                         string success = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_success")]).Value.ToString();

                         if (oDataSet.Tables != null)
                         {
                             oResult.Return = BuildPendingList(oDataSet.Tables[0]);
                         }

                         CLog.Logger.Write(CLog.INFORMATION, "----Request--  ");
                         CLog.Logger.Write(CLog.INFORMATION, "/P_IAPPID:  " +oSIBLRemitPay.IAPPID );
                         CLog.Logger.Write(CLog.INFORMATION, "/P_BRANCHID: " + oSIBLRemitPay.CREATEBY.Branch.CN);
                         CLog.Logger.Write(CLog.INFORMATION, "P_USERID:" + oSIBLRemitPay.CREATEBY.CN);
                         CLog.Logger.Write(CLog.INFORMATION, "P_FromDate: " + oSIBLRemitPay.FromDate);
                         CLog.Logger.Write(CLog.INFORMATION, "P_ToDate: " + oSIBLRemitPay.ToDate);
                         CLog.Logger.Write(CLog.INFORMATION, "P_OperationType: " + oSIBLRemitPay.OperationType);

                         CLog.Logger.Write(CLog.INFORMATION, " DB Message: " + sMessage);
                         oResult.Result = true;
                         oResult.Message = sMessage;
                     }
                     catch (Exception exp)
                     {
                         oResult.Exception = exp;
                         CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetPendingList: " + exp.Message);
                         oResult.Message = exp.Message;
                         oResult.Result = false;
                         oResult.Return = null;
                     }
                     finally
                     {
                         oDataSet.Clear();
                         oDataSet.Dispose();

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


         [Author("MMI", "22-01-2020", "Build Pending List")]
         private CSIBLRemitPayList BuildPendingList(DataTable dataTable)
         {

             CSIBLRemitPay oSIBLRemitPay = new CSIBLRemitPay();
             CSIBLRemitPayList oSIBLRemitPayList = new CSIBLRemitPayList();

             try
             {

                 for (int i = 0; i < dataTable.Rows.Count; i++)
                 {
                      
 

                     oSIBLRemitPay = new CSIBLRemitPay();
                     oSIBLRemitPay.CN = dataTable.Rows[i]["PRID"].ToString();
                     oSIBLRemitPay.COMPANY.CN = dataTable.Rows[i]["COMPANYID"].ToString();
                     oSIBLRemitPay.CREATEBY.CN = dataTable.Rows[i]["CREATEBY"].ToString();
                     oSIBLRemitPay.COMPANY.TYPE_NAME = dataTable.Rows[i]["COMPANY_NAME"].ToString();
                     oSIBLRemitPay.BENEFICIARY_NAME = dataTable.Rows[i]["BENEFICIARY_NAME"].ToString();
                     oSIBLRemitPay.PINNO = dataTable.Rows[i]["PINNO"].ToString();
                     oSIBLRemitPay.AMOUNT = dataTable.Rows[i]["AMOUNT"].ToString();
                     oSIBLRemitPay.INCENTIVE_AMOUNT = dataTable.Rows[i]["INCENTIVE_AMOUNT"].ToString();
                     oSIBLRemitPay.SENDERNAME = dataTable.Rows[i]["SENDERNAME"].ToString();
                     oSIBLRemitPay.SENDERCOUNTRY = dataTable.Rows[i]["SENDERCOUNTRY"].ToString();

                     oSIBLRemitPay.DOCAPPROVED = dataTable.Rows[i]["DOCAPPROVED"].ToString();
                     oSIBLRemitPay.IsTransactionApproved = dataTable.Rows[i]["Approved"].ToString();
                     oSIBLRemitPay.CBSIStatus = dataTable.Rows[i]["CBSISTATUS"].ToString();
                     oSIBLRemitPay.CBSMStatus = dataTable.Rows[i]["CBSMSTATUS"].ToString();
                     oSIBLRemitPay.AgentID = dataTable.Rows[i]["AGENTID"].ToString();
                     oSIBLRemitPay.AgentName = dataTable.Rows[i]["agentname"].ToString();

                     oSIBLRemitPay.IsMainRemitDisverse = dataTable.Rows[i]["ISDMWEB"].ToString();
                     oSIBLRemitPay.ForeignSender = dataTable.Rows[i]["ISFOREIGNSEBDER"].ToString();
                     oSIBLRemitPay.RemitOnly = dataTable.Rows[i]["PAYOUTOPTION"].ToString();
                     

                     oSIBLRemitPayList.SIBLRemitPayList.Add(oSIBLRemitPay);

                 }
             }
             catch (Exception exp)
             {
                 CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/BuildPendingList: " + exp.Message);
             }

             return oSIBLRemitPayList;
         }


         [Author("Md. Munirul Islam", "22-01-2020", "Get Requested PayOut Trans List")]
         public CResult GetRemiAccNo(CRemiAccount oRemiAccount)
         {
             CResult oResult = new CResult();
             Database oDatabase = new Database();
             //DataSet oDataSet = null;

             try
             {
                 using (IDbConnection oConnection = oDatabase.CreateConnection())
                 {
                     IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_SIBLREMITPAY.GetRemiAccNOByCompanyID");
                     try
                     {

                         oDatabase.AddInOutParameter(oDbCommand, "P_IAPPID", DbType.String, 50, oRemiAccount.IAPPID);
                         oDatabase.AddInOutParameter(oDbCommand, "P_PRID", DbType.String, 50, oRemiAccount.CN);
                                     
                         oDatabase.AddInOutParameter(oDbCommand, "P_COMPANYID", DbType.String, 50, oRemiAccount.Company.CN);
                         oDatabase.AddInOutParameter(oDbCommand, "P_FROMACCNUM", DbType.String, 50, oRemiAccount.FromAccNum);
                         oDatabase.AddInOutParameter(oDbCommand, "P_TOACCNUM", DbType.String, 50, oRemiAccount.ToAccNum);
                         oDatabase.AddInOutParameter(oDbCommand, "P_CHARGE", DbType.String, 50, oRemiAccount.ToAccNum);
                         oDatabase.AddInOutParameter(oDbCommand, "P_OperationType", DbType.String, 50, oRemiAccount.OperationType);
                         oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                         oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);
                         int i = oDatabase.ExecuteNonQuery(oDbCommand);
                         string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                         string success = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_success")]).Value.ToString();


                         oRemiAccount.Company.CN = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_COMPANYID")]).Value.ToString();
                         oRemiAccount.FromAccNum = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_FROMACCNUM")]).Value.ToString();
                         oRemiAccount.ToAccNum = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_TOACCNUM")]).Value.ToString();
                         oRemiAccount.Charge = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_CHARGE")]).Value.ToString();



                         oResult.Result = true;
                         oResult.Return = oRemiAccount;
                     }
                     catch (Exception exp)
                     {
                         oResult.Exception = exp;
                         CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetRemiAccNo: " + exp.Message);
                         oResult.Message = exp.Message;
                         oResult.Result = false;
                         oResult.Return = null;
                     }
                     finally
                     {
                         //oDataSet.Clear();
                        // oDataSet.Dispose();

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


         [Author("MMI", "18-02-2020", "Build Transaction Detail Info List")]
         private CSIBLRemitPay BuildTransactionDetailInfo(DataTable dataTable)
         {

             CSIBLRemitPay oSIBLRemitPay = new CSIBLRemitPay();
            
             try
             {

                 for (int i = 0; i < dataTable.Rows.Count; i++)
                 {
                    
                     oSIBLRemitPay = new CSIBLRemitPay();
                     oSIBLRemitPay.AgentName = dataTable.Rows[i]["AGENT_NAME"].ToString();
                     oSIBLRemitPay.COMPANY.TYPE_NAME = dataTable.Rows[i]["COMPANY_NAME"].ToString();
                     oSIBLRemitPay.PINNO = dataTable.Rows[i]["PINNO"].ToString();
                     oSIBLRemitPay.SENDERNAME = dataTable.Rows[i]["SENDERNAME"].ToString();
                     oSIBLRemitPay.SENDERCOUNTRY = dataTable.Rows[i]["SENDERCOUNTRY"].ToString();
                     oSIBLRemitPay.SENDERIDTYPE = dataTable.Rows[i]["SENDERIDTYPE"].ToString();
                     oSIBLRemitPay.SENDERIDTYPENO= dataTable.Rows[i]["SENDERIDNO"].ToString();

                     oSIBLRemitPay.SENDERPASSPORTNO = dataTable.Rows[i]["SENDERPASSPORTNO"].ToString();
                     oSIBLRemitPay.SENDERBUSINESSLICENSE = dataTable.Rows[i]["SENDERBUSINESSLICENSE"].ToString();

                     oSIBLRemitPay.BENEFICIARY_NAME = dataTable.Rows[i]["BENEFICIARY_NAME"].ToString();
                     oSIBLRemitPay.BENEFICIARYADDRESS = dataTable.Rows[i]["BENEFICIARYADDRESS"].ToString();

                     oSIBLRemitPay.RNIDNO = dataTable.Rows[i]["RNIDNO"].ToString();
                     oSIBLRemitPay.RNIDTYPE = dataTable.Rows[i]["RNIDTYPE"].ToString();
                     oSIBLRemitPay.RNIDEXPIRYDATE = dataTable.Rows[i]["RNIDEXPIRYDATE"].ToString();
                     oSIBLRemitPay.RNIDISSUEDATE = dataTable.Rows[i]["RNIDISSUEDATE"].ToString();

                     oSIBLRemitPay.ReceiverPhoneNo = dataTable.Rows[i]["RECEIVERCONTACTNO"].ToString();
                     oSIBLRemitPay.REMARKS = dataTable.Rows[i]["REMARKS"].ToString();

                     oSIBLRemitPay.AMOUNT = dataTable.Rows[i]["AMOUNT"].ToString();
                     oSIBLRemitPay.INCENTIVE_AMOUNT = dataTable.Rows[i]["INCENTIVE_AMOUNT"].ToString();
                     oSIBLRemitPay.IsTransactionApproved = dataTable.Rows[i]["APPROVED"].ToString();
                     oSIBLRemitPay.CREATEBY.USERNAME = dataTable.Rows[i]["MAKER"].ToString();
                     oSIBLRemitPay.CREATEBY.Message= dataTable.Rows[i]["APPROVEDBY_NAME"].ToString();//approveby
                     oSIBLRemitPay.ENTRYDATE = dataTable.Rows[i]["ENTRYDATE"].ToString();//entry date 

   
                     oSIBLRemitPay.IsMainRemitDisverse= dataTable.Rows[i]["ISDMWEB"].ToString();//entry date 
                     oSIBLRemitPay.ForeignSender = dataTable.Rows[i]["ISFOREIGNSEBDER"].ToString();//entry date 
                     oSIBLRemitPay.RemitOnly = dataTable.Rows[i]["PAYOUTOPTION"].ToString();//entry date 
                 }
             }
             catch (Exception exp)
             {
                 CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/BuildTransactionDetailInfo: " + exp.Message);
             }

             return oSIBLRemitPay;
         }
        
        [Author("MMI", "18-02-2020", "View Transaction Detail")]
        public CResult viewDetail(CSIBLRemitPay oSIBLRemitPay)
         {
             CResult oResult = new CResult();
             Database oDatabase = new Database();
             DataSet oDataSet = null;

             try
             {
                 using (IDbConnection oConnection = oDatabase.CreateConnection())
                 {
                     IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_SIBLREMITPAY.ViewDetailByID");
                     try
                     {


                         oDatabase.AddInOutParameter(oDbCommand, "P_PRID", DbType.String, 50, oSIBLRemitPay.CN);   // need to modify
                         oDatabase.AddInOutParameter(oDbCommand, "p_BRANCHID", DbType.String, 50, oSIBLRemitPay.CREATEBY.Branch.CN);
                         oDatabase.AddInOutParameter(oDbCommand, "P_IAPPID", DbType.String, 50, oSIBLRemitPay.IAPPID);
                         oDatabase.AddInOutParameter(oDbCommand, "P_CREATEBY", DbType.String, 50, oSIBLRemitPay.CREATEBY.CN);
                         oDatabase.AddOutParameter(oDbCommand, "rc_TransList", "RefCursor", 100);
                         oDatabase.AddInOutParameter(oDbCommand, "P_OperationType", DbType.String, 50, oSIBLRemitPay.OperationType);
                         oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50,DBNull.Value);
                         oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 50, DBNull.Value);
                       
                       
                         oDataSet = oDatabase.ExecuteDataSet(oDbCommand);
                         string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                         string success = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_success")]).Value.ToString();

                         if (success == "1")
                         {
                             oResult.Result = true;
                             if (oDataSet.Tables.Count > 0)
                                 oResult.Return = BuildTransactionDetailInfo(oDataSet.Tables[0]);
                             else
                             {
                                 oResult.Return = null;
                                 oResult.Result = false;
                             }
                         }
                         else
                         {
                             oResult.Result = false;
                             oResult.Return = null;
                         }
                     }
                     catch (Exception exp)
                     {
                         oResult.Exception = exp;
                         CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/viewDetail: " + exp.Message);
                         oResult.Message = exp.Message;
                         oResult.Result = false;
                         oResult.Return = null;
                     }
                     finally
                     {
                         //oDataSet.Clear();
                         // oDataSet.Dispose();

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

        #region CashFileImport

        public CResult AddFileName(CCashTransactionInfo CashTransactionInfo)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            
            string sSuccess = "";
            string sMessage = "";
            string rid = "";
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_CASHDATA_PROCESS.addFileName");

                    try
                    {
                        CashTransactionInfo.CN = "0";
                        oDatabase.AddInOutParameter(oDbCommand, "p_RID", DbType.Int32, 50, CashTransactionInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_filename", DbType.String, 50, CashTransactionInfo.FileInfo.FileName);
                        oDatabase.AddInOutParameter(oDbCommand, "p_uploadby", DbType.Int32, 50, CashTransactionInfo.USERID.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_companyid", DbType.Int32, 50, CashTransactionInfo.CompanyInfo.CN);                        
                        oDatabase.AddInOutParameter(oDbCommand, "P_RETURN_MESSAGE", DbType.String, 500, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SUCCESS", DbType.String, 50, DBNull.Value);
                        int i = oDatabase.ExecuteNonQuery(oDbCommand);
                        sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_success")]).Value.ToString();
                        sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_message")]).Value.ToString();
                        rid = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_RID")]).Value.ToString();

                        if (sSuccess == "1")
                        {
                            oResult.CN = rid;
                            oResult.Result = true;
                            oResult.Message = sMessage;
                        }
                        else
                        {
                            oResult.Result = false;
                            oResult.Message = sMessage;

                        }
                        oResult.Message = sMessage;

                    }
                    catch (Exception exp)
                    {
                        oResult.Message = exp.ToString();
                    }
                    finally
                    {
                        oDbCommand.Dispose();
                        oConnection.Close();
                    }
                }
                return oResult;
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/AddFileName: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
            return oResult;
        }


        public CResult GetCompanyFileName(CCashTransactionInfo oCashTransactionInfo)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            CFile oFile = new CFile();
            CFileList oFileList = new CFileList();
            string sSuccess = "";
            string sMessage = "";
            string rid = "";
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_CASHDATA_PROCESS.GetCompanyFileName");

                    try
                    {

                        oDatabase.AddInOutParameter(oDbCommand, "p_companyid", DbType.Int32, 50, oCashTransactionInfo.CompanyInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_fromDate", DbType.String, 50, oCashTransactionInfo.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_toDate", DbType.String, 50, oCashTransactionInfo.ToDate);
                        oDatabase.AddOutParameter(oDbCommand, "rc_company", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "P_RETURN_MESSAGE", DbType.String, 500, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SUCCESS", DbType.String, 50, DBNull.Value);
                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        if (oDataSet.Tables != null)
                        {

                            for (int i = 0; i < oDataSet.Tables[0].Rows.Count; i++)
                            {
                                oFile = new CFile();
                                oFile.CN = Convert.ToString(oDataSet.Tables[0].Rows[i]["FID"]);
                                oFile.FileName = Convert.ToString(oDataSet.Tables[0].Rows[i]["FILE_NAME"]);
                                oFileList.FileList.Add(oFile);

                            }

                            oResult.Return = oFileList;

                        }
                        sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_success")]).Value.ToString();
                        sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_message")]).Value.ToString();

                        if (sSuccess == "1")
                        {

                            oResult.Result = true;

                        }
                        else
                        {
                            oResult.Result = false;

                        }
                        oResult.Message = sMessage;

                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetCompanyFileName: " + exp.Message);
                        oResult.Exception = exp;
                        oResult.Message = exp.Message;
                        oResult.Result = false;
                        oResult.Return = null;
                        return oResult;
                    }
                    finally
                    {
                        oDbCommand.Dispose();
                        oConnection.Close();
                    }
                }
                return oResult;
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetCompanyFileName: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
            return oResult;
        }


        public CResult SearchTransactionPINNO(CCashTransactionInfo oCashTransactionInfo)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
             


            string sSuccess = "";
            string sMessage = "";

            try
            {

                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_CASHDATA_PROCESS.SearchTransaction");

                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_USERID", DbType.Int32, 50, oCashTransactionInfo.USERID.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BRANCHID", DbType.Int32, 50, oCashTransactionInfo.USERID.BranchInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_CompanyID", DbType.Int32, 50, oCashTransactionInfo.CompanyInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_PINNO", DbType.String, 50, oCashTransactionInfo.PINNO);
                        oDatabase.AddOutParameter(oDbCommand, "rc_TransList", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "P_RETURN_MESSAGE", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SUCCESS", DbType.String, 500, DBNull.Value);

                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_SUCCESS")]).Value.ToString();
                        sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_RETURN_MESSAGE")]).Value.ToString();


                        if (oDataSet.Tables.Count > 0)
                        {
                            DataTable dt = new DataTable();
                            dt = oDataSet.Tables[0];
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {


                                oCashTransactionInfo = new CCashTransactionInfo();
                                oCashTransactionInfo.AMOUNT = Convert.ToString(dt.Rows[i]["AMOUNT"]);
                                oCashTransactionInfo.SENDERADDRESS = Convert.ToString(dt.Rows[i]["SENDERADDRESS"]);
                                oCashTransactionInfo.SENDERNAME = Convert.ToString(dt.Rows[i]["SENDERNAME"]);
                                oCashTransactionInfo.RECEIVERNAME = Convert.ToString(dt.Rows[i]["RECEIVERNAME"]);
                                oCashTransactionInfo.RECEIVERADDRESS = Convert.ToString(dt.Rows[i]["RECEIVERADDRESS"]);
                                oCashTransactionInfo.PINNO = Convert.ToString(dt.Rows[i]["PINNO"]);
                                oCashTransactionInfo.CompanyInfo.TYPE_NAME = Convert.ToString(dt.Rows[i]["company_name"]);
                            }
                        }
                        oResult.Return = oCashTransactionInfo; 

                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/SearchTransactionPINNO: " + exp.Message);
                    }
                    finally
                    {
                        oResult.Message = sMessage;
                        oResult.Result = true;
                        oDbCommand.Dispose();
                        oConnection.Close();
                    }
                }

            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/SearchTransactionPINNO: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
            return oResult;
        }

        public CResult AddCTransactionInfo(CCashTransactionInfoList oCashTransactionInfoList)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            string sSuccess = "";
            string sMessage = "";
            string sPaymentMode = "";
            string sCompanyId = "";
            string sFromDate = "";
            string sToDate = "";
            int iCount = 0;
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_CASHDATA_PROCESS.CRUDCashTransaction");

                    try
                    {
                        foreach (CCashTransactionInfo oCashTransactionInfo in oCashTransactionInfoList.CashTransactionInfoList)
                        {
                            oDatabase.AddInOutParameter(oDbCommand, "P_CID", DbType.String, 50, oCashTransactionInfo.CN);
                            oDatabase.AddInOutParameter(oDbCommand, "P_PINNO", DbType.String, 200, oCashTransactionInfo.PINNO);
                            oDatabase.AddInOutParameter(oDbCommand, "P_SENDERNAME", DbType.String, 200, oCashTransactionInfo.SENDERNAME);
                            oDatabase.AddInOutParameter(oDbCommand, "P_SENDERADDRESS", DbType.String, 200, oCashTransactionInfo.SENDERADDRESS);
                            oDatabase.AddInOutParameter(oDbCommand, "P_SENDERPHONE", DbType.String, 200, oCashTransactionInfo.SENDERPHONE);
                            oDatabase.AddInOutParameter(oDbCommand, "P_RECEIVERNAME", DbType.String, 200, oCashTransactionInfo.RECEIVERNAME);
                            oDatabase.AddInOutParameter(oDbCommand, "P_RECEIVERADDRESS", DbType.String, 200, oCashTransactionInfo.RECEIVERADDRESS);
                            oDatabase.AddInOutParameter(oDbCommand, "P_RECEIVERPHONE", DbType.String, 200, oCashTransactionInfo.RECEIVERPHONE);
                            oDatabase.AddInOutParameter(oDbCommand, "P_AMOUNT", DbType.String, 200, oCashTransactionInfo.AMOUNT);
                            oDatabase.AddInOutParameter(oDbCommand, "P_APPROVEDBY", DbType.String, 200, oCashTransactionInfo.APPROVEDBY);
                            oDatabase.AddInOutParameter(oDbCommand, "P_APPROVEDDATE", DbType.String, 200, oCashTransactionInfo.APPROVEDDATE);
                            oDatabase.AddInOutParameter(oDbCommand, "P_CREATEBY", DbType.String, 200, oCashTransactionInfo.CREATEBY);
                            oDatabase.AddInOutParameter(oDbCommand, "P_ENTRYDATE", DbType.String, 200, oCashTransactionInfo.ENTRYDATE);
                            oDatabase.AddInOutParameter(oDbCommand, "P_STATUS", DbType.String, 200, oCashTransactionInfo.STATUS);
                            oDatabase.AddInOutParameter(oDbCommand, "P_COMPANYID", DbType.String, 200, oCashTransactionInfo.CompanyInfo.CN);
                            oDatabase.AddInOutParameter(oDbCommand, "P_FILEID", DbType.String, 200, oCashTransactionInfo.FILEID);
                            oDatabase.AddInOutParameter(oDbCommand, "p_oPerationType", DbType.String, 200, oCashTransactionInfo.OperationType);
                            oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                            oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 2000, DBNull.Value);
                            int i = oDatabase.ExecuteNonQuery(oDbCommand);
                            sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_success")]).Value.ToString();
                            sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();

                            oDbCommand.Parameters.Clear();
                            sPaymentMode = oCashTransactionInfo.OperationType;
                            sCompanyId = oCashTransactionInfo.CompanyInfo.CN;

                            iCount++;
                            CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "/AddCTransactionInfo: " + sMessage);
                        }

                        if (sSuccess == "1")
                        {
                            oResult.Message = iCount.ToString() + " " + sMessage;
                            CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "/AddCTransactionInfo: " + iCount.ToString() + "Information Added Successfully.");
                            oResult.Result = true;
                        }
                        else
                        {
                            CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/AddCTransactionInfo: " + sMessage);
                            oResult.Result = false;
                            oResult.Message = "DB Exception Occured!!!";
                        }
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/AddCTransactionInfo: " + exp.Message);
                        oResult.Exception = exp;
                        oResult.Message = "Error in DA !!!";
                        oResult.Result = false;
                        oResult.Return = null;
                        return oResult;

                    }
                    finally
                    {
                        oDbCommand.Dispose();
                        oConnection.Close();
                    }
                }
                return oResult;
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/AddCTransactionInfo: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
            return oResult;

        }

        public CResult GetCashPendingTransaction(CCashTransactionInfo oCashTransactionInfo)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_CASHDATA_PROCESS.GetPendingList");

                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_CompanyID", DbType.Int32, 50, oCashTransactionInfo.CompanyInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_FILEID", DbType.Int32, 50, oCashTransactionInfo.FILEID);
                        oDatabase.AddInOutParameter(oDbCommand, "P_FromDate", DbType.String, 50, oCashTransactionInfo.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ToDate", DbType.String, 50, oCashTransactionInfo.ToDate);
                        oDatabase.AddOutParameter(oDbCommand, "rc_TransList", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "P_RETURN_MESSAGE", DbType.String, 500, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SUCCESS", DbType.String, 50, DBNull.Value);
                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);
                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = BuildCashPendingList(oDataSet.Tables[0]);
                        }
                        else
                        {
                            oResult.Return = null;
                        }
                    }
                    catch (Exception exp)
                    {

                    }
                    finally
                    {
                        oDbCommand.Dispose();
                        oConnection.Close();
                    }
                }
                return oResult;
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetCashPendingTransaction: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
            return oResult;
        }
        private CCashTransactionInfoList BuildCashPendingList(DataTable dt)
        {
            CCashTransactionInfo oCashTransactionInfo = new CCashTransactionInfo();
            CCashTransactionInfoList oCashTransactionInfoList = new CCashTransactionInfoList();

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                oCashTransactionInfo = new CCashTransactionInfo();
                oCashTransactionInfo.CN = Convert.ToString(dt.Rows[i]["CID"]);
                oCashTransactionInfo.PINNO = Convert.ToString(dt.Rows[i]["PINNO"]);
                oCashTransactionInfo.SENDERNAME = Convert.ToString(dt.Rows[i]["SENDERNAME"]);
                oCashTransactionInfo.SENDERADDRESS = Convert.ToString(dt.Rows[i]["SENDERADDRESS"]);
                oCashTransactionInfo.SENDERPHONE = Convert.ToString(dt.Rows[i]["SENDERPHONE"]);
                oCashTransactionInfo.RECEIVERNAME = Convert.ToString(dt.Rows[i]["RECEIVERNAME"]);
                oCashTransactionInfo.RECEIVERADDRESS = Convert.ToString(dt.Rows[i]["RECEIVERADDRESS"]);
                oCashTransactionInfo.RECEIVERPHONE = Convert.ToString(dt.Rows[i]["RECEIVERPHONE"]);
                oCashTransactionInfo.AMOUNT = Convert.ToString(dt.Rows[i]["AMOUNT"]);
                oCashTransactionInfo.APPROVEDBY = Convert.ToString(dt.Rows[i]["APPROVEDBY"]);
                oCashTransactionInfo.APPROVEDDATE = Convert.ToString(dt.Rows[i]["APPROVEDDATE"]);
                oCashTransactionInfo.CREATEBY = Convert.ToString(dt.Rows[i]["CREATEBY"]);
                oCashTransactionInfo.ENTRYDATE = Convert.ToString(dt.Rows[i]["ENTRYDATE"]);
                oCashTransactionInfo.STATUS = Convert.ToString(dt.Rows[i]["STATUS"]);
                oCashTransactionInfo.CompanyInfo.CN = Convert.ToString(dt.Rows[i]["COMPANYID"]);
                oCashTransactionInfo.FILEID = Convert.ToString(dt.Rows[i]["FILEID"]);
                oCashTransactionInfoList.CashTransactionInfoList.Add(oCashTransactionInfo);
            }
            return oCashTransactionInfoList;
        }


        #endregion CashFileImport

        public CResult ViewPayoutVoucher(CSIBLRemitPay oSIBLRemitPay)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataTable dt = new DataTable();
            DataSet oDataSet = null;
            string sSuccess = "";
            string sMessage = "";

            try
            {

                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_CASHDATA_PROCESS.PayoutReceipt");

                    try
                    {               

                        oDatabase.AddInOutParameter(oDbCommand, "P_USERID", DbType.String, 50, oSIBLRemitPay.CREATEBY.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_COMPANYID", DbType.String, 50, oSIBLRemitPay.COMPANY.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_PINNO", DbType.String, 50, oSIBLRemitPay.PINNO);
                        oDatabase.AddInOutParameter(oDbCommand, "P_VoucherType", DbType.String, 50, oSIBLRemitPay.PaymentType);
                        oDatabase.AddInOutParameter(oDbCommand, "P_IAPPID", DbType.String, 50, oSIBLRemitPay.IAPPID);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BRANCHID", DbType.String, 50, oSIBLRemitPay.CREATEBY.Branch.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_OpeationType", DbType.String, 50, oSIBLRemitPay.OperationType);

                        oDatabase.AddInOutParameter(oDbCommand, "P_TRANSTYPE", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_FromAccount", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_FromAccountIncentive", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ToAccountAgent", DbType.String, 50, DBNull.Value);
                        
                        oDatabase.AddOutParameter(oDbCommand, "rc_payoutDetail", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);
                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);
                        sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_success")]).Value.ToString();
                        sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();

                        oSIBLRemitPay.TransactionType = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_TRANSTYPE")]).Value.ToString();
                        oSIBLRemitPay.AccountInfo.FromAccNum = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_FromAccount")]).Value.ToString();

                        //oSIBLRemitPay.AccountInfo.ToAccNum = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_FromAccountIncentive")]).Value.ToString();
                        oSIBLRemitPay.AccountInfo.IncentiveAccountNum = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_ToAccountAgent")]).Value.ToString();

                        if (oSIBLRemitPay.TransactionType.ToUpper().Equals("AGENT"))
                            oResult.OperationType = oSIBLRemitPay.AccountInfo.IncentiveAccountNum;
                        else
                            oResult.OperationType ="";

                        if (oDataSet.Tables.Count > 0)
                        {
                            dt = oDataSet.Tables[0];
                            oResult.Return = dt;

                            CLog.Logger.Write(CLog.SUCCESS, "/ViewPayoutVoucher: Total Number record found " + dt.Rows.Count.ToString());

                        }
                        CLog.Logger.Write(CLog.SUCCESS, "/ViewPayoutVoucher: DB Message " + sMessage);                        

                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/ViewPayoutVoucher: " + exp.Message);
                    }
                    finally
                    {
                        oResult.Message = sMessage;
                        oResult.Result = true;
                        oDbCommand.Dispose();
                        oConnection.Close();
                    }
                }

            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/ViewPayoutVoucher: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
            return oResult;
        }

        public CResult GetPayoutDetailReport(CSIBLRemitPay oSIBLRemitPay)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            
            string sSuccess = "";
            string sMessage = "";

            try
            {

                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_CASHDATA_PROCESS.GetPayoutDetailForRpt");

                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_IAPPID", DbType.String, 50, oSIBLRemitPay.IAPPID);
                        oDatabase.AddInOutParameter(oDbCommand, "P_USERID", DbType.String, 50, oSIBLRemitPay.CREATEBY.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_COMPANYID", DbType.String, 50, oSIBLRemitPay.COMPANY.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BranchId", DbType.String, 50, oSIBLRemitPay.CREATEBY.Branch.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_FromDate", DbType.String, 50, oSIBLRemitPay.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ToDate", DbType.String, 50, oSIBLRemitPay.ToDate);
                        oDatabase.AddOutParameter(oDbCommand, "rc_payoutDetail", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "P_VoucherType", DbType.String, 50, oSIBLRemitPay.ReportType);// voucher type:REMITTANCE, INCENTIVE
                        oDatabase.AddInOutParameter(oDbCommand, "P_OpeationType", DbType.String, 50, oSIBLRemitPay.OperationType);// ALL Branch/Single Branch
                        
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);

                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_success")]).Value.ToString();
                        sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();


                        if (oDataSet.Tables.Count > 0)
                            oResult.Return = oDataSet.Tables[0];

                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetPayoutDetailReport: " + exp.Message);
                    }
                    finally
                    {
                        oResult.Message = sMessage;
                        oResult.Result = true;
                        oDbCommand.Dispose();
                        oConnection.Close();
                    }
                }

            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetPayoutDetailReport: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
            return oResult;
        }

        public CResult GetAgentList(CAllLookup oAllLookup)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            CAllLookupList oAllLookupList = new CAllLookupList();
            string sSuccess = "";
            string sMessage = "";

            try
            {

                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_CASHDATA_PROCESS.get_agent_list");

                    try
                    {

                        oDatabase.AddInOutParameter(oDbCommand, "p_operationType", DbType.String, 50, oAllLookup.OperationType);
                        oDatabase.AddOutParameter(oDbCommand, "rc_AgentList", "RefCursor", 100);
                        
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);

                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_success")]).Value.ToString();
                        sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();


                        if (oDataSet.Tables.Count > 0)
                        {
                            for (int i = 0; i < oDataSet.Tables[0].Rows.Count; i++)
                            {
                                oAllLookup = new CAllLookup();
                                oAllLookup.CN = oDataSet.Tables[0].Rows[i]["AID"].ToString();
                                oAllLookup.TYPE_NAME = oDataSet.Tables[0].Rows[i]["AGENT_NAME"].ToString();
                                oAllLookupList.AllLookupList.Add(oAllLookup);
                            }
                            oResult.Return = oAllLookupList;

                        }

                        CLog.Logger.Write(CLog.INFORMATION, this.ToString() + "/GetAgentList: " + sMessage);

                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetAgentList: " + exp.Message);
                    }
                    finally
                    {
                        oResult.Message = sMessage;
                        oResult.Result = true;
                        oDbCommand.Dispose();
                        oConnection.Close();
                    }
                }

            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetAgentList: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
            return oResult;
        }

        public CResult GetRemitInfoReport(CRemitInfo oRemitInfo)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            string sSuccess = "";
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
                        oDatabase.AddInOutParameter(oDbCommand, "P_FROM_DATE", DbType.String, 50, oRemitInfo.TransactionInfo.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_TO_DATE", DbType.String, 50, oRemitInfo.TransactionInfo.ToDate);
                        oDatabase.AddOutParameter(oDbCommand, "RC_LIST", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SUCCESS", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_RETURN_MSG", DbType.String, 500, DBNull.Value);
                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);
                        sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_SUCCESS")]).Value.ToString();
                        sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_RETURN_MSG")]).Value.ToString();
                        if (oDataSet.Tables != null)
                        {
                            if (oDataSet.Tables.Count > 0)
                                oResult.Return = oDataSet.Tables[0];    
                        }
                        
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetRemitInfoReport: " + exp.Message);
                    }
                    finally
                    {
                        oResult.Message = sMessage;
                        oResult.Result = true;
                        oDbCommand.Dispose();
                        oConnection.Close();
                    }
                }

            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetRemitInfoReport: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
            return oResult;
        }

        public CResult GetRemitInfo(CRemitInfo oRemitInfo)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            string sSuccess = "";
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
                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);
                        sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_SUCCESS")]).Value.ToString();
                        sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_RETURN_MSG")]).Value.ToString();
                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = BuildRemitInfoList(oDataSet.Tables[0]);
                        }
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetRemitInfo: " + exp.Message);
                    }
                    finally
                    {
                        oResult.Message = sMessage;
                        oResult.Result = true;
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

        private CRemitInfoList BuildRemitInfoList(DataTable dataTable)
        {

            CRemitInfo oRemitInfo = new CRemitInfo();
            CRemitInfoList oRemitInfoList = new CRemitInfoList();
            try
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    oRemitInfo = new CRemitInfo();
                    oRemitInfo.TransactionInfo.CN = dataTable.Rows[i]["TID"].ToString();
                    oRemitInfo.TransactionInfo.RefTxnId = dataTable.Rows[i]["REFTXNID"].ToString();
                    oRemitInfo.TransactionInfo.TxnIdSIBL = dataTable.Rows[i]["TXNIDSIBL"].ToString();
                    oRemitInfo.SystemInfo.RemitCompanyCode = dataTable.Rows[i]["COMPANYCODE"].ToString();
                    oRemitInfo.SystemInfo.RemitCompanyName = dataTable.Rows[i]["COMPANYNAME"].ToString();
                    oRemitInfo.TransactionInfo.EntryDate = dataTable.Rows[i]["ENTRYDATE"].ToString();
                    oRemitInfoList.RemitInfoList.Add(oRemitInfo);
                }
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/BuildPendingList: " + exp.Message);
            }

            return oRemitInfoList;
        }


        [Author("Rifad", "27-04-2021", "Get all Remittance company Details")]
        public CResult CheckerSearchByCompany(CAllLookup oAlllookUp)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_SETUP.");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "p_CN", DbType.String, 50, oAlllookUp.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_FromDate", DbType.String, 50, oAlllookUp.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_ToDate", DbType.String, 50, oAlllookUp.ToDate);
                        oDatabase.AddOutParameter(oDbCommand, "rc_lookuplist", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = BuildLookupEntity(oDataSet.Tables[0]);
                        }
                        else
                        {
                            oResult.Return = null;
                        }

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
                        oDataSet.Clear();
                        oDataSet.Dispose();
                        oConnection.Close();
                    }
                }
                return oResult;
            }

            catch (Exception ex)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/CheckerSearchByCompany: " + ex.Message);
                oResult.Exception = ex;
                oResult.Message = ex.Message;
                oResult.Result = false;
                oResult.Return = null;
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
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_CBS.SAVECBS_RESPONSE");
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
