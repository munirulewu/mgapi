/*
 * File name            : CRolePermissionDA.cs
 * Author               : Munirul Islam
 * Date                 : November 25,2014
 * Version              : 1.0
 *
 * Description          : CRolePermissionDA Infortmation DataAccess Class
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
using SIBLCommon.Common.Entity.Permission;
using SIBLCommon.Common.Entity.RolePermission;

namespace SIBLRemit.DA.RolePermission
{
    public class CRolePermissionDA
    {
       
        public CResult GetRolePermissionList(CRoleList oRoleList)
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
                        string sql = "SELECT ROLEID, ROLENAME FROM ROLEINF  order by ROLENAME";
                        oDataSet = oDatabase.ExecuteSQLDataSet(oConnection, sql);
                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = BuildRolePerEntity(oDataSet.Tables[0]);
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
            catch (Exception exp)
            {

                return oResult;
            }
        }

        private CRoleList BuildRolePerEntity(DataTable dt)   
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

       
        public CResult GetPermissionList(CPermissionList oPermissionList)
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
                        string sql = "SELECT PERM_ID, PERM_NAME FROM PERMISSION  order by PERM_NAME";
                        oDataSet = oDatabase.ExecuteSQLDataSet(oConnection, sql);
                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = BuildPermissionEntity(oDataSet.Tables[0]);
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
            catch (Exception exp)
            {

                return oResult;
            }
        }
        private CPermissionList BuildPermissionEntity(DataTable dt)
        {
            CPermissionList oPermissionList = new CPermissionList();
            CPermission oPermission = new CPermission();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                oPermission = new CPermission();
                oPermission.CN = Convert.ToString(dt.Rows[i]["PERM_ID"]);
                oPermission.PermissionName = Convert.ToString(dt.Rows[i]["PERM_NAME"]);
                oPermissionList.PermissionList.Add(oPermission);
            }

            return oPermissionList;
        }


        public CResult AddUserWiseRolePermission(CRolePermission ORolePermission)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

           // string ss = ORolePermission.CN == "" ? "0" : ORolePermission.CN;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_INS_ROLE_PERMISSION.INS_ROLE_PERMISSION");
                    try
                    {

                        oDatabase.AddInOutParameter(oDbCommand, "P_RPID", DbType.String, 50, ORolePermission.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ROLE_ID", DbType.String, 50, ORolePermission.Role.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_PERM_ID", DbType.String, 50, ORolePermission.Permission.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_READ_ONLY", DbType.String, 50, ORolePermission.Read_Only);
                        oDatabase.AddInOutParameter(oDbCommand, "P_WRITE_ONLY", DbType.String, 50, ORolePermission.Write_Only);
                        oDatabase.AddInOutParameter(oDbCommand, "P_EDIT_ONLY", DbType.String, 50, ORolePermission.Edit_Only);
                        oDatabase.AddInOutParameter(oDbCommand, "P_DELETE_ONLY", DbType.String, 50, ORolePermission.Delete_Only);
                        oDatabase.AddInOutParameter(oDbCommand, "P_OPERATIONTYPE", DbType.String,50, ORolePermission.OperationType);

                        oDatabase.AddInOutParameter(oDbCommand, "P_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 150, DBNull.Value);



                        int i = oDatabase.ExecuteNonQuery(oDbCommand);

                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                        string success = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_success")]).Value.ToString();
                        oResult.Message = sMessage;


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


        public CResult GetUserWiseRolePermission(CRolePermission oRolePermission)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
          
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())  // For Create connection
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_INS_ROLE_PERMISSION.GetRolePermList"); // Used to Call Procedure
                    try
                    {
                        oDatabase.AddOutParameter(oDbCommand, "rc_rolePermList", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);

                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        if (oDataSet.Tables != null)                     
                        {
                            oResult.Return = BuildRolePermissionList(oDataSet.Tables[0]);                                                    
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
                return oResult;
            }
        }
   
        public CResult GetUserWiseRolePermission_old(CRolePermission oRolePermission)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            string sql = "";
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    try
                    {

                        sql = " SELECT rp.RPID, rp.ROLE_ID, (select rolename from roleinf WHERE roleid=rp.ROLE_ID) as ROLENAME, rp.PERM_ID, ";
                        sql += " (SELECT perm_name FROM PERMISSION where perm_id=rp.PERM_ID) as PERM_NAME, rp.CREATE_DATE,case when rp.READ_ONLY='1' then 'YES' else 'NO' end READ_ONLY,";
                        sql += " case when rp.WRITE_ONLY='1' then 'YES' else 'NO' end WRITE_ONLY, case when rp.EDIT_ONLY='1' then 'YES' else 'NO' end EDIT_ONLY, case when rp.DELETE_ONLY='1' then 'YES' else 'NO' end DELETE_ONLY ";
                        sql += " FROM ROLE_PERMISSION rp";


                        //sql = "SELECT RP.RPID RPID,URI.ROLE_ID ROLE_ID,RP.PERM_ID PERM_ID, R.ROLENAME ROLENAME,P.PERM_NAME PERM_NAME,U.USERNAME USERNAME,(CASE WHEN RP.READ_ONLY=1 THEN 'YES' ELSE 'NO' END) READ_ONLY,(CASE WHEN RP.WRITE_ONLY=1 THEN 'YES' ELSE 'NO' END) WRITE_ONLY, ";
                        //sql = sql + " (case when RP.EDIT_ONLY=1 THEN 'YES' ELSE 'NO' END) EDIT_ONLY,(CASE WHEN RP.DELETE_ONLY=1 THEN 'YES' ELSE 'NO' END) DELETE_ONLY FROM USERINF U,USER_ROLE_INFO URI,ROLE_PERMISSION RP,ROLEINF R,PERMISSION P";
                        //sql = sql + " WHERE U.USERID = URI.USER_ID AND URI.ROLE_ID = RP.ROLE_ID AND URI.ROLE_ID = R.ROLEID   AND RP.PERM_ID = P.PERM_ID";

                        oDataSet = oDatabase.ExecuteSQLDataSet(oConnection, sql);

                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = BuildRolePermissionList(oDataSet.Tables[0]);
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


        private CRolePermissionList BuildRolePermissionList(DataTable dt)
        {
            CRolePermissionList oRolePermissionList = new CRolePermissionList();
            CRolePermission oRolePermission = new CRolePermission();
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                oRolePermission = new CRolePermission();
          
                oRolePermission.CN = Convert.ToString(dt.Rows[i]["RPID"]);
                oRolePermission.Role.CN = Convert.ToString(dt.Rows[i]["ROLE_ID"]);
                oRolePermission.Permission.CN = Convert.ToString(dt.Rows[i]["PERM_ID"]);
                oRolePermission.Role.RoleName = Convert.ToString(dt.Rows[i]["ROLENAME"]);
                oRolePermission.Permission.PermissionName = Convert.ToString(dt.Rows[i]["PERM_NAME"]);
                oRolePermission.Read_Only = Convert.ToString(dt.Rows[i]["READ_ONLY"]);
                oRolePermission.Write_Only = Convert.ToString(dt.Rows[i]["WRITE_ONLY"]);
                oRolePermission.Edit_Only = Convert.ToString(dt.Rows[i]["EDIT_ONLY"]);
                oRolePermission.Delete_Only = Convert.ToString(dt.Rows[i]["DELETE_ONLY"]);
                oRolePermissionList.RolePermissionList.Add(oRolePermission);
            }
            return oRolePermissionList;
        }
       


        //public CResult DeleteUserWiseRolePermission(CRolePermission ORolePermission)
        //{
        //    CResult oResult = new CResult();
        //    Database oDatabase = new Database();
        //    DataSet oDataSet = null;


        //    try
        //    {
        //        using (IDbConnection oConnection = oDatabase.CreateConnection())
        //        {
        //            IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_INS_ROLE_PERMISSION.INS_ROLE_PERMISSION");
        //            try
        //            {

        //                oDatabase.AddInOutParameter(oDbCommand, "P_RPID", DbType.Int32, 50, ORolePermission.CN);
        //                oDatabase.AddInOutParameter(oDbCommand, "P_OPERATIONTYPE", DbType.String, 50, ORolePermission.OperationType);
                        
        //                oDatabase.AddInOutParameter(oDbCommand, "P_success", DbType.String, 50, DBNull.Value);
        //                oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 50, DBNull.Value);



        //                int i = oDatabase.ExecuteNonQuery(oDbCommand);

        //                string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
        //                string sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_success")]).Value.ToString();


        //            }
        //            catch (Exception exp)
        //            {
        //                oResult.Exception = exp;
        //                oResult.Message = exp.Message;
        //                oResult.Result = false;
        //                oResult.Return = null;
        //            }
        //            finally
        //            {
        //                oConnection.Close();
        //            }
        //            return oResult;
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        return oResult;
        //    }
        //}



        public CResult SearchRolePermission(CRolePermission ORolePermission)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_SEARCH.SEARCH_ROLEPERMISSION");
                    try
                    {
                        //oDatabase.AddInOutParameter(oDbCommand, "P_RPID", DbType.String, 50, ORolePermission.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ROLE_ID", DbType.String, 50, ORolePermission.Role.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_PERM_ID", DbType.String, 50, ORolePermission.Permission.CN);
                        //oDatabase.AddInOutParameter(oDbCommand, "P_READ_ONLY", DbType.String, 50, ORolePermission.Read_Only);
                        //oDatabase.AddInOutParameter(oDbCommand, "P_WRITE_ONLY", DbType.String, 50, ORolePermission.Write_Only);
                        //oDatabase.AddInOutParameter(oDbCommand, "P_EDIT_ONLY", DbType.String, 50, ORolePermission.Edit_Only);
                        //oDatabase.AddInOutParameter(oDbCommand, "P_DELETE_ONLY", DbType.String, 50, ORolePermission.Delete_Only);
                        oDatabase.AddOutParameter(oDbCommand, "rc_roleinf", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 50, DBNull.Value);


                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();


                        oResult.Message = sMessage;

                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = BuildUserWiseRolePermission(oDataSet.Tables[0]);

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

                        oDbCommand.Dispose();
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


        private CRolePermissionList BuildUserWiseRolePermission(DataTable dt)
        {
            CRolePermission oRolePermission = new CRolePermission();
            CRolePermissionList oRolePermissionList = new CRolePermissionList();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                oRolePermission = new CRolePermission();
                oRolePermission.CN = Convert.ToString(dt.Rows[i]["RPID"]);
                oRolePermission.Role.CN = Convert.ToString(dt.Rows[i]["ROLE_ID"]);
                oRolePermission.Permission.CN = Convert.ToString(dt.Rows[i]["PERM_ID"]);
                
                oRolePermission.Permission.PermissionName = Convert.ToString(dt.Rows[i]["PERM_NAME"]);
                oRolePermission.Role.RoleName = Convert.ToString(dt.Rows[i]["ROLENAME"]);
                oRolePermission.Read_Only = Convert.ToString(dt.Rows[i]["READ_ONLY"]);
                oRolePermission.Write_Only = Convert.ToString(dt.Rows[i]["WRITE_ONLY"]);
                oRolePermission.Edit_Only = Convert.ToString(dt.Rows[i]["EDIT_ONLY"]);
                oRolePermission.Delete_Only = Convert.ToString(dt.Rows[i]["DELETE_ONLY"]);
                oRolePermissionList.RolePermissionList.Add(oRolePermission);
            }

            return oRolePermissionList;
        }

    }
}
