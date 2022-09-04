using Oracle.ManagedDataAccess.Client;
using SendingReason;
using SIBLCommon.Common.Entity.Result;
using SIBLCommon.Common.Util.Attributes;
using SIBLCommon.Common.Util.Logger;
using SIBLCommon.SIBLCommon.Common.Entity.MGAPI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGStatusUpdate
{
   public class CMGStatusUpdateDA
    {


        #region DB Connection AND CONSTANTS
       const string DB_CONNECTION_WUOB = "ConnStringApp";
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


        [Author("Md. Aminul Islam", "29.05.2022", "Get Datafor Update")]
         public CResult GetTranStatusUpdateData(string sACKData)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;            

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection(DB_CONNECTION_WUOB))
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_CALLBACK_API.GetTranStatusUpdateData");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_OperationType", DbType.String, 50, sACKData);
                        oDatabase.AddOutParameter(oDbCommand, "rc_listTransaction", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);
                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                        string success = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_success")]).Value.ToString();

                        if (oDataSet.Tables != null)
                        {
                          oResult.Return = BuilListEntity(oDataSet.Tables[0]);
                        }
                        oResult.Result = true;


                    }
                    catch (Exception exp)
                    {
                        oResult.Exception = exp;
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/ " + exp.Message);
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

        [Author("Md. Aminul Islam", "09.02.2021", "Build Reason List Entity")]
        private CTransaction BuilListEntity(DataTable dataTable)
        {
            CTransaction oTran = new CTransaction();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                oTran = new CTransaction();
                oTran.transaction.mgiTransactionId = dataTable.Rows[i]["MGITRANSACTIONID"].ToString();
                oTran.transaction.siblTransactionId = dataTable.Rows[i]["SIBLTRASNSACTIONID"].ToString();
                oTran.transaction.TransactionResponse.responseCode = dataTable.Rows[i]["partnerReasonCode"].ToString();
                oTran.transaction.TransactionResponse.message = dataTable.Rows[i]["partnerReasonMessage"].ToString();

            }
            return oTran; 

        }

          [Author("Md. Aminul Islam", "29.05.2022", "Get Datafor Update")]
        public CResult TranStatusUpdateDB(CTransaction oTransaction)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;            

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection(DB_CONNECTION_WUOB))
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_CALLBACK_API.MGStatusUpdate");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_MGITRANSACTIONID", DbType.String, 50, oTransaction.transaction.mgiTransactionId);
                        oDatabase.AddInOutParameter(oDbCommand, "P_OperationType", DbType.String, 50, oTransaction.transaction.operationType);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);
                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                        string success = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_success")]).Value.ToString();
                        if (success == "1")
                            oResult.Result = true;
                        else
                            oResult.Result = false;
                        oResult.Message = sMessage;
                    }
                    catch (Exception exp)
                    {
                        oResult.Exception = exp;
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/ " + exp.Message);
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


       


    }
}
