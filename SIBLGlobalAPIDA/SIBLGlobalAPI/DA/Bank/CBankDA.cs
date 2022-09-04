/*
 * File name            : CBankDA.cs
 * Author               : Md. Munirul Islam
 * Date                 : 12 June 2019
 * Version              : 1.0
 *
 * Description          : This is the Data Access Object Class
 *
 * Modification history :
 * Name                         Date                            Desc
 * 
 * 
 * Copyright (c) 2019: SOCIAL ISLAMI BANK LIMITED
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bank;
using SIBLCommon.Common.Entity.Result;
 
using System.Data;
using Oracle.DataAccess.Client;
using SIBLGlobalAPI.DA.Common.Connections;

namespace SIBLGlobalAPI.DA.Bank
{
    public class CBankDA
    {
        public CResult AddBankInformation(CBank oBank)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
           

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_BANK.CRUD_BANKINF");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_BANK_ID", DbType.Int16, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BANK_NAME", DbType.String, 50, oBank.BankName);                       
                        oDatabase.AddInOutParameter(oDbCommand, "P_BANK_CODE", DbType.Int32, 50, oBank.BankCode);
                        oDatabase.AddInOutParameter(oDbCommand, "P_operatuinType", DbType.String, 50, oBank.Operatiom_Type);                        
                        oDatabase.AddInOutParameter(oDbCommand, "P_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 50, DBNull.Value);
                        int i = oDatabase.ExecuteNonQuery(oDbCommand);
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                        string success = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_success")]).Value.ToString();
                        oDbCommand.Parameters.Clear();
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

        /// <summary>
        /// Author: Md. Ali Ahsan
        /// Description: Get Bank Informations
        /// Date: April 08, 2014
        /// </summary>
        /// <param name="oBank"></param>
        /// <returns></returns>


        public CResult GetBankInfo(CBank oBank)
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
                        string sqlStr = "SELECT BANK_ID,BANK_CODE,BANK_NAME FROM BANK_INFO  ORDER BY BANK_NAME";
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



        public CResult UpdateBankInfo(CBank oBank)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_BANK.CRUD_BANKINF");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_BANK_ID", DbType.Int32, 50, oBank.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BANK_NAME", DbType.String, 50, oBank.BankName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BANK_CODE", DbType.String, 50, oBank.BankCode);
                        oDatabase.AddInOutParameter(oDbCommand, "P_operatuinType", DbType.String, 50, oBank.Operatiom_Type);
                        oDatabase.AddInOutParameter(oDbCommand, "P_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 50, DBNull.Value);
                        //oDataSet = oDatabase.ExecuteDataSet(oDbCommand);
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
                        oResult.Exception = exp;
                        oResult.Message = exp.Message;
                        oResult.Result = false;
                        oResult.Return = null;
                    }
                    finally
                    {
                        // oDataSet.Clear();
                        //oDataSet.Dispose();
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

        public CResult GetBankInfoIndividual(CBank cBank)
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
                        string sqlStr = "SELECT BANK_ID,BANK_CODE,BANK_NAME FROM BANK_INFO where BANK_ID="+cBank.CN;
                        oDataSet = oDatabase.ExecuteSQLDataSet(oConnection, sqlStr);

                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = BuildBankEntity(oDataSet.Tables[0]);
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


        private CBankList BuildBankEntity(DataTable dt)
        {
            CBank oBank = new CBank();
            CBankList oBankList = new CBankList();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                oBank.CN = Convert.ToString(dt.Rows[i]["BANK_ID"]);
                oBank.BankCode = Convert.ToString(dt.Rows[i]["BANK_CODE"]);
                oBank.BankName = Convert.ToString(dt.Rows[i]["BANK_NAME"]);
                oBankList.BankList.Add(oBank);
            }
            return oBankList;
        }

        


    }
}
