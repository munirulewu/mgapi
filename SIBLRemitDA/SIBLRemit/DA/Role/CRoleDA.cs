/*
 * File name            : CRoleDA.cs
 * Author               : Munirul Islam
 * Date                 : November 25.2014
 * Version              : 1.0
 *
 * Description          : Role Infortmation DataAccess Class
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
using System.Data;
using Oracle.ManagedDataAccess.Client;
using SIBLCommon.Common.Entity.Result;
using SIBLRemit.DA.Common.Connections;
using SIBLCommon.Common.Entity.Role;

namespace SIBLRemit.DA.Role
{
    public class CRoleDA
    {
        
        public CResult AddRoleInformation(CRole oRole)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_ROLE.INS_ROLEINFO");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_ROLEID", DbType.String, 50, oRole.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_RoleName", DbType.String, 50, oRole.RoleName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_OPERATIONTYPE", DbType.String, 50, oRole.OperationType);
                        oDatabase.AddInOutParameter(oDbCommand, "P_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 50, DBNull.Value);

                        int i = oDatabase.ExecuteNonQuery(oDbCommand);

                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                        string success = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_success")]).Value.ToString();

                        string roleid = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_ROLEID")]).Value.ToString();

                        oRole.CN = roleid;
                        oResult.Message = sMessage;

                        oResult.Return = oRole;
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

    
        public CResult GetRoleInfo(CRole oRole)
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
                        string sqlStr = "SELECT ROLEID,ROLENAME FROM ROLEINF ORDER BY ROLENAME";
                        oDataSet = oDatabase.ExecuteSQLDataSet(oConnection, sqlStr);

                        if (oDataSet.Tables != null)
                        {
                            //oResult.Return = BuildRoleList(oDataSet.Tables[0]);
                            oResult.Return = oDataSet.Tables[0];
                        }
                        else
                            oResult.Return = null;
                    }
                    catch(Exception exp)
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


        public CResult GetRoleInfoList(CRole oRole)
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
                        string sqlStr = "SELECT ROLEID,ROLENAME FROM ROLEINF ORDER BY ROLENAME";
                        oDataSet = oDatabase.ExecuteSQLDataSet(oConnection, sqlStr);

                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = BuildRoleList(oDataSet.Tables[0]);
                            //oResult.Return = oDataSet.Tables[0];
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

        private CRoleList BuildRoleList(DataTable dt)
        {
            CRoleList oRoleList = new CRoleList();
            CRole oRole = new CRole();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                oRole = new CRole();
                oRole.CN = Convert.ToString(dt.Rows[i]["ROLEID"]);
                oRole.RoleName = Convert.ToString(dt.Rows[i]["ROLENAME"]);

                oRoleList.RoleDataList.Add(oRole);
            }

            return oRoleList;
        }


    }
}
