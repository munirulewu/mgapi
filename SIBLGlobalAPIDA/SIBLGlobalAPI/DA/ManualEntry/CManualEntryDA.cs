
/*
 * File name            : CManualEntryDA.cs
 * Author               : Md. Ali Ahsan
 * Date                 : April 24, 2014
 * Version              : 1.0
 *
 * Description          : MAnual Entry DataAccess Class
 *
 * Modification history :
 * Name                         Date                            Desc
 * 
 * 
 * Copyright (c) 2014: SOCIAL ISLAMI BANK LIMITED
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DPDC.Common.Entity.Result;
using DPDC.Common.Entity.ManualEntry;
using DPDC.DA.Common.Connections;
using System.Data;
using Oracle.DataAccess.Client;
using DPDC.Common;

namespace DPDC.DA.ManualEntry
{
    public class CManualEntryDA
    {
        public CResult ManualEntryInfo(CManualEntry oManualEntry)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;


            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_MANUAL_ENTRY.INS_MANUAL_ENTRY");
                    try
                    {
                        if (oManualEntry.OperationType != CConstants.DB_ADD)
                        {
                            oDatabase.AddInOutParameter(oDbCommand, "P_MANUAL_ID", DbType.String, 50, oManualEntry.CN); // used for data Update
                        }
                        else
                        {
                            oDatabase.AddInOutParameter(oDbCommand, "P_MANUAL_ID", DbType.String, 50, DBNull.Value); // used for data Insert
                        }

                        //oDatabase.AddInOutParameter(oDbCommand, "P_MANUAL_ID", DbType.String, 50, oManualEntry.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_REF_NO", DbType.String, 50, oManualEntry.RefNo);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ACCOUNT_NO", DbType.String, 50, oManualEntry.AccountNo);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BENEFICIARY", DbType.String, 50, oManualEntry.Beneficiary);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BANK_ID", DbType.Int32, 50, oManualEntry.BankName.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BRANCH_ID", DbType.Int32, 50, oManualEntry.BranchName.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SENDER_NAME", DbType.String, 50, oManualEntry.SenderName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_AMOUNT", DbType.Double, 50, oManualEntry.Amount);
                        oDatabase.AddInOutParameter(oDbCommand, "P_COMPANYID", DbType.Int32, 50, oManualEntry.CompanyInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_OPERATIONTYPE", DbType.String, 50, oManualEntry.OperationType);                        
                        oDatabase.AddInOutParameter(oDbCommand, "P_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 50, DBNull.Value);

                        int i = oDatabase.ExecuteNonQuery(oDbCommand);
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                        string Success = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_success")]).Value.ToString();

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
                    return oResult;
                }
            }
            catch (Exception exp)
            {
                return oResult;
            }

        }

        /// <summary>
        /// Author: Md. Ali Ahsan
        /// Description: Get Exchange Manual Entry Informations.
        /// Date: April 24, 2014
        /// </summary>
        /// <param name="oManualEntry"></param>
        /// <returns></returns>
        public CResult GetManualEntryList(CManualEntry oManualEntry)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    try
                    {
                        string sql2 = "SELECT ME.COMPANYID,ME.MANUAL_ID,ME.REF_NO,ME.ACCOUNT_NO,ME.BENEFICIARY,ME.BANK_ID,BI.BANK_NAME,ME.BRANCH_ID,BRI.BRANCH_NAME,ME.SENDER_NAME,ME.AMOUNT,ALLL.TYPE_NAME AS COMPANY_NAME FROM MANUAL_ENTRY ME,BANK_INFO BI,BRANCH_INFO BRI,ALLLOOKUP ALLL";
                        string sql3 = " WHERE ME.BANK_ID = BI.BANK_ID AND ME.BRANCH_ID = BRI.BRANCH_ID AND ME.COMPANYID=ALLL.ID";
                        string sqlStr = sql2 + sql3;
                        oDataSet = oDatabase.ExecuteSQLDataSet(oConnection, sqlStr);

                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = oDataSet.Tables[0];
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






    }
}
