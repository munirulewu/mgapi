/*
 * File name            : CUserDA.cs
 * Author               : Munirul Islam
 * Date                 : November 10,2014
 * Version              : 1.0
 *
 * Description          : This is the Data Access Object Class
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
using System.Data.SqlClient;
using SIBLCommon.Common.Entity.Result;
using SIBLRemit.DA.Common.Connections;
using SIBLCommon.Common.Util.Logger;
using SIBLCommon.Common.Entity.User;
using SIBLCommon.Common;
using SIBLCommon.Common.Entity.Role;
using SIBLCommon.Common.Util.Attributes;
using SIBLCommon.Common.Entity.Account;


namespace SIBLRemit.DA.User
{
   
    public class CUserDA
    {


        public CResult SignupUser(CUser oUser)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;


           try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_LOGIN.signup");


                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_USERID", DbType.Int16, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_USERNAME", DbType.String, 50, oUser.USERNAME);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ACCNO", DbType.String, 50, oUser.ACCNO);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ACCNAME", DbType.String, 50, oUser.ACCNAME);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SPASSWORD", DbType.String, 50, oUser.PASSWORD);
                        oDatabase.AddInOutParameter(oDbCommand, "P_EMAIL", DbType.String, 50, oUser.EMAIL);
                        oDatabase.AddInOutParameter(oDbCommand, "P_MOBILE_NO", DbType.String, 50, oUser.MOBILE_NO);
                         oDatabase.AddInOutParameter(oDbCommand, "P_DEVICEID", DbType.String, 50, oUser.DEVICEID);
                        oDatabase.AddInOutParameter(oDbCommand, "P_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 50, DBNull.Value);


                        int i = oDatabase.ExecuteNonQuery(oDbCommand);


                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                        string userId = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_USERID")]).Value.ToString();

                        oUser.CN= userId;
                        oUser.Status = sMessage;
                        oResult.Result = true;
                        oResult.Return = oUser;
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/SignupUser" + exp.Message);
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


        public CResult CheckAccNo(CUser oUser)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;


            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_IBANKING.CHECK_ACCNO");


                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_ACCNO", DbType.String, 50, oUser.ACCNO);
                        oDatabase.AddInOutParameter(oDbCommand, "P_CUSTID", DbType.String, 50, oUser.CUST_ID);
                       ;
                        oDatabase.AddInOutParameter(oDbCommand, "P_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 50, DBNull.Value);


                        int i = oDatabase.ExecuteNonQuery(oDbCommand);


                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                        string custId = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_CUSTID")]).Value.ToString();

                        oUser.CUST_ID = custId;
                        oUser.Status = sMessage;
                        oResult.Result = true;
                        oResult.Return = oUser;
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


        public CResult AddUserInformation(CUser oUser)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {

                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_LOGIN.INS_USERINF");


                    IDbTransaction oTranasaction = oConnection.BeginTransaction();
                    oDbCommand.Transaction = oTranasaction;

                    try
                    {
                        if (oUser.OperationType != CConstants.DB_ADD)
                        {
                            oDatabase.AddInOutParameter(oDbCommand, "P_USERID", DbType.Int16, 50, oUser.CN); // used for data Update
                        }
                        else
                        {
                            oDatabase.AddInOutParameter(oDbCommand, "P_USERID", DbType.Int16, 50, DBNull.Value); // used for data Insert
                        }

                        oDatabase.AddInOutParameter(oDbCommand, "P_USERNAME", DbType.String, 50, oUser.USERNAME);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SPASSWORD", DbType.String, 50, oUser.PASSWORD);
                        oDatabase.AddInOutParameter(oDbCommand, "P_EMAIL", DbType.String, 50, oUser.EMAIL);
                        oDatabase.AddInOutParameter(oDbCommand, "P_FULL_NAME", DbType.String, 50, oUser.FULL_NAME);
                        oDatabase.AddInOutParameter(oDbCommand, "P_DESIGNATION", DbType.String, 50, oUser.DESIGNATION);
                        oDatabase.AddInOutParameter(oDbCommand, "P_USERLOCKED", DbType.String, 50, oUser.UserLocked);
                        oDatabase.AddInOutParameter(oDbCommand, "P_EMPLOYEEID", DbType.String, 50, oUser.EmployeeId);
                        //oDatabase.AddInOutParameter(oDbCommand, "P_ACCNO", DbType.String, 50, oUser.ACCNO);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BRANCHID", DbType.String, 50, oUser.BranchInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_DIVID", DbType.String, 50, oUser.DivInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_OPERATIONTYPE", DbType.String, 50, oUser.OperationType);
                        oDatabase.AddInOutParameter(oDbCommand, "P_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);

                        int i = oDatabase.ExecuteNonQuery(oDbCommand);

                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                        string sUserId = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_USERID")]).Value.ToString();
                        oResult.Message = sMessage;

                        // updated by Munir
                        // Date: 21.04.2014
                        // add role information for user
                        if (sUserId != "")
                        {
                            IDbCommand oDbCommandRole = oDatabase.GetStoredProcICommand("PKG_INS_USER_ROLE.INS_USER_ROLE");
                            foreach (CRole oRole in oUser.RoleList.RoleDataList)
                            {
                                if (oUser.OperationType != CConstants.DB_ADD)
                                {
                                    oDatabase.AddInOutParameter(oDbCommandRole, "P_URID", DbType.Int32, 50, oUser.CN);
                                }
                                else
                                {
                                    oDatabase.AddInOutParameter(oDbCommandRole, "P_URID", DbType.Int32, 50, DBNull.Value);
                                }
                                //oDatabase.AddInOutParameter(oDbCommandRole, "P_URID", DbType.Int32, 50, oUser.CN);  // main
                                oDatabase.AddInOutParameter(oDbCommandRole, "P_USER_ID", DbType.Int32, 50, sUserId);
                                oDatabase.AddInOutParameter(oDbCommandRole, "P_ROLE_ID", DbType.Int32, 50, oRole.CN);
                                oDatabase.AddInOutParameter(oDbCommandRole, "P_OPERATIONTYPE", DbType.String, 50, oUser.OperationType);
                                oDatabase.AddInOutParameter(oDbCommandRole, "P_success", DbType.String, 50, DBNull.Value);
                                oDatabase.AddInOutParameter(oDbCommandRole, "p_return_msg", DbType.String, 500, DBNull.Value);

                                i = oDatabase.ExecuteNonQuery(oDbCommandRole);
                                oDbCommandRole.Parameters.Clear();
                            }

                        }
                        // end of update

                        //addUserRole(oUser);

                    }
                    catch (Exception exp)
                    {
                        oResult.Exception = exp;
                        oResult.Message = exp.Message;
                        oResult.Result = false;
                        oResult.Return = null;
                        oTranasaction.Rollback();

                    }
                    finally
                    {
                        oTranasaction.Commit();
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
        /// Description: Used to show data in GridView
        /// Date: May 21, 2014
        /// </summary>
        /// <param name="oUser"></param>
        /// <returns></returns>
        public CResult GetUserInformation(CUser oUser)
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

                        string sql = " SELECT URI.URID,U.USERID,U.USERNAME,U.FULL_NAME,U.DESIGNATION,u.SPASSWORD,u.EMAIL,u.USERLOCKED,R.ROLENAME, U.BRANCHID, (SELECT BRANCH_NAME FROM BRANCH_INFO WHERE BRANCH_ID =U.BRANCHID) BRANCH_NAME,";
                        sql = sql + " case when U.Useractive='False' then 'Inactive' when U.Useractive='True' then 'Active' end status  FROM USERINF U,USER_ROLE_INFO URI,ROLEINF R WHERE U.USERID = URI.USER_ID AND URI.ROLE_ID = R.ROLEID";
                

                        //string sql = " SELECT URI.URID,U.USERID,U.USERNAME,U.FULL_NAME,U.DESIGNATION,u.SPASSWORD,u.EMAIL,u.USERLOCKED,R.ROLENAME,";
                        //sql = sql + " case when U.Useractive='False' then 'Inactive' when U.Useractive='True' then 'Active' end status  FROM USERINF U,USER_ROLE_INFO URI,ROLEINF R WHERE U.USERID = URI.USER_ID AND URI.ROLE_ID = R.ROLEID";
                

                        oDataSet = oDatabase.ExecuteSQLDataSet(oConnection, sql);

                        if (oDataSet.Tables != null)
                        {
                            //oResult.Return = oDataSet.Tables[0]; // Before
                            oResult.Return = BuildUserListInfo(oDataSet.Tables[0]);
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


        private CUserList BuildUserListInfo(DataTable dt)
        {
            CUser oUser = new CUser();
            CUserList oUserList = new CUserList();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                oUser = new CUser();
                oUser.CN = Convert.ToString(dt.Rows[i]["USERID"]);
                oUser.USERNAME = Convert.ToString(dt.Rows[i]["USERNAME"]);
                oUser.FULL_NAME = Convert.ToString(dt.Rows[i]["FULL_NAME"]);
                oUser.DESIGNATION = Convert.ToString(dt.Rows[i]["DESIGNATION"]);
                oUser.PASSWORD = Convert.ToString(dt.Rows[i]["SPASSWORD"]);
                oUser.EMAIL = Convert.ToString(dt.Rows[i]["EMAIL"]);
                oUser.UserRole = Convert.ToString(dt.Rows[i]["ROLENAME"]);
                oUser.Status = Convert.ToString(dt.Rows[i]["status"]);
                oUser.BranchInfo.TYPE_NAME = Convert.ToString(dt.Rows[i]["BRANCH_NAME"]);               
                oUserList.UserDataList.Add(oUser);
            }
            return oUserList;
        }




        public CResult GetUserDetails(CUser oUser)
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
                        string sql = " SELECT URI.URID,U.USERID,U.USERNAME,U.FULL_NAME,U.DESIGNATION,u.SPASSWORD,u.EMAIL,u.USERLOCKED,R.ROLENAME";
                        sql = sql + "  FROM USERINF U,USER_ROLE_INFO URI,ROLEINF R WHERE U.USERID = URI.USER_ID AND URI.ROLE_ID = R.ROLEID";

                        oDataSet = oDatabase.ExecuteSQLDataSet(oConnection, sql);

                        if (oDataSet.Tables != null)
                        {
                            //oResult.Return = oDataSet.Tables[0]; // Before
                            oResult.Return = BuildUserListInfo(oDataSet.Tables[0]);
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


        public CResult GetUserList(CUser oUser)
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
                        string sql = "SELECT USERID,USERNAME FROM Userinf";
                        oDataSet = oDatabase.ExecuteSQLDataSet(oConnection, sql);
                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = BuildEntityUser(oDataSet.Tables[0]);
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


        private CUserList BuildEntityUser(DataTable dtUser)
        {
            CUser oUser = new CUser();
            CUserList oUserList = new CUserList();
            for (int i = 0; i < dtUser.Rows.Count; i++)
            {
                oUser = new CUser();
                oUser.CN = Convert.ToString(dtUser.Rows[i]["USERID"]);
                oUser.USERNAME = Convert.ToString(dtUser.Rows[i]["USERNAME"]);
                //oUser.PASSWORD = Convert.ToString(dtUser.Rows[i]["SPASSWORD"]);
                //oUser.EMAIL = Convert.ToString(dtUser.Rows[i]["EMAIL"]);
                //oUser.FULL_NAME = Convert.ToString(dtUser.Rows[i]["FULL_NAME"]);
                //oUser.DESIGNATION = Convert.ToString(dtUser.Rows[i]["DESIGNATION"]);                
                //oUser.CN = Convert.ToString(dtUser.Rows[i]["CREATE_DATE"]);
                //oUser.USERNAME = Convert.ToString(dtUser.Rows[i]["UPDATE_DATE"]);
                //oUser.PASSWORD = Convert.ToString(dtUser.Rows[i]["USERACTIVE"]);
                //oUser.EMAIL = Convert.ToString(dtUser.Rows[i]["USERLOCKED"]);
                //oUser.FULL_NAME = Convert.ToString(dtUser.Rows[i]["PASSWORDCHANGE_DATE"]);
                //oUser.DESIGNATION = Convert.ToString(dtUser.Rows[i]["USERALTERFLAG"]);

                oUserList.UserDataList.Add(oUser);
            }
            return oUserList ;
        }

        /// <summary>
        /// Author: Md. Ali Ahsan
        /// Description: Used to Get Role Name List into the ListBox
        /// Date: April 16, 2014
        /// </summary>
        /// <param name="oUser"></param>
        /// <returns></returns>
       public CResult GetUserInfo(CUser oUser)
        {
           CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_LOGIN.INS_USERINF");

                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_USERID", DbType.Int16, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_USERNAME", DbType.String, 50, oUser.USERNAME);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SPASSWORD", DbType.String, 50,oUser.PASSWORD);
                        oDatabase.AddInOutParameter(oDbCommand, "P_EMAIL", DbType.String, 50, oUser.EMAIL);
                        oDatabase.AddInOutParameter(oDbCommand, "P_FULL_NAME", DbType.String, 50, oUser.FULL_NAME);
                        oDatabase.AddInOutParameter(oDbCommand, "P_DESIGNATION", DbType.String, 50, oUser.DESIGNATION);
                        oDatabase.AddInOutParameter(oDbCommand, "P_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);

                        int i = oDatabase.ExecuteNonQuery(oDbCommand);

                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();

                       
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
        /// Description: 
        /// Date: 17 - 04 - 2014
        /// </summary>
        /// <param name="oUser"></param>
        /// <returns></returns>
        /// 



   

       public CResult AddUserRoleInformation(CUser oUser)
       {
           CResult oResult = new CResult();
           Database oDatabase = new Database();
           DataSet oDataSet = null;


           try
           {
               using (IDbConnection oConnection = oDatabase.CreateConnection())
               {
                   IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_LOGIN.INS_USERINF");


                   try
                   {
                       oDatabase.AddInOutParameter(oDbCommand, "P_USERID", DbType.Int16, 50, DBNull.Value);
                       oDatabase.AddInOutParameter(oDbCommand, "P_USERNAME", DbType.String, 50, oUser.USERNAME);
                       oDatabase.AddInOutParameter(oDbCommand, "P_SPASSWORD", DbType.String, 50, oUser.PASSWORD);
                       oDatabase.AddInOutParameter(oDbCommand, "P_EMAIL", DbType.String, 50, oUser.EMAIL);
                       oDatabase.AddInOutParameter(oDbCommand, "P_FULL_NAME", DbType.String, 50, oUser.FULL_NAME);
                       oDatabase.AddInOutParameter(oDbCommand, "P_DESIGNATION", DbType.String, 50, oUser.DESIGNATION);
                       oDatabase.AddInOutParameter(oDbCommand, "P_success", DbType.String, 50, DBNull.Value);
                       oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 50, DBNull.Value);


                       int i = oDatabase.ExecuteNonQuery(oDbCommand);


                       string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                       string userId = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_USERID")]).Value.ToString();
                       // addUserRole(oUser, oConnection);

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
        /// Author: Munirul Islam
        /// Date: 20.04.2014
        /// </summary>
        /// <param name="oUser"></param>
        /// <param name="oCon"></param>
        //private void addUserRole(CUser oUser)
        //{
        //    Database oDatabase = new Database();
        //    try
        //    {
        //        IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_INS_USER_ROLE.INS_USER_ROLE");
        //        foreach (CRole oRole in oUser.RoleList.RoleDataList)
        //        {
        //            oDatabase.AddInOutParameter(oDbCommand, "P_URID", DbType.Int16, 50, DBNull.Value);
        //            oDatabase.AddInOutParameter(oDbCommand, "P_USER_ID", DbType.Int16, 50, oUser.USERNAME);
        //            oDatabase.AddInOutParameter(oDbCommand, "P_ROLE_ID", DbType.Int16, 50, oUser.RoleList);
        //            oDatabase.AddInOutParameter(oDbCommand, "P_success", DbType.String, 50, DBNull.Value);
        //            oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 50, DBNull.Value);
        //        }
               


        //        int i = oDatabase.ExecuteNonQuery(oDbCommand);

        //        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
        //        string userId = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_USER_ID")]).Value.ToString();


        //    }
        //    catch (Exception exp)
        //    {
               
        //    }
        //}



    /// <summary>
    /// Author:
    /// Description
    /// Date:
    /// </summary>
    /// <param name="oUser"></param>
    /// <returns></returns>
        
        public CResult LoginUserInfo_old(CUser oUser)
        {

            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            string sPassword = "";
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    //IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_LOGIN.Login");
                    try
                    {
                        //oDatabase.AddInOutParameter(oDbCommand, "P_USERID", DbType.Int16, 50, oUser.CN);
                        //oDatabase.AddInOutParameter(oDbCommand, "P_USERNAME", DbType.String, 50, oUser.USERNAME);
                        //oDatabase.AddInOutParameter(oDbCommand, "P_SPASSWORD", DbType.String, 50, oUser.PASSWORD);
                        //oDatabase.AddInOutParameter(oDbCommand, "P_EMAIL", DbType.String, 50, oUser.EMAIL);
                        //oDatabase.AddInOutParameter(oDbCommand, "P_FULL_NAME", DbType.String, 50, oUser.FULL_NAME);
                        //oDatabase.AddInOutParameter(oDbCommand, "P_success", DbType.String, 50, DBNull.Value);
                        //oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 50, DBNull.Value);

                        //int i = oDatabase.ExecuteNonQuery(oDbCommand);

                        //string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                        //string sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_success")]).Value.ToString();

                        //if (sSuccess == "1")
                        //{
                        //    oResult.Result = true;
                        //}
                        //else
                        //{
                        //    oResult.Result = false;
                        //}

                         //string sql = "SELECT * from userinf where USERNAME=" + oUser.USERNAME + " and SPASSWORD ="+ oUser.PASSWORD+"";
                        string sql = "SELECT spassword from userinf where USERNAME= '" + oUser.USERNAME + "'";
                        oDataSet = oDatabase.ExecuteSQLDataSet(oConnection, sql);
                        if (oDataSet.Tables != null)
                        {
                            if (oDataSet.Tables.Count > 0)
                            {
                                if (oDataSet.Tables[0].Rows.Count > 0)
                                {
                                    sPassword=oDataSet.Tables[0].Rows[0]["spassword"].ToString();
                                }
                                if (sPassword == oUser.PASSWORD)
                                {
                                    oResult.Return = oUser;
                                    oResult.Result = true;
                                }
                                else
                                {
                                    oResult.Return = null;
                                    oResult.Result = false;
                                }
                            }
                        }

                        else
                            oResult.Result = false;
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


        public CResult LoginUserInfo(CUser oUser)
        {

            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            string sPassword = "";
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_LOGIN.Login");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_USERID", DbType.String, 50, oUser.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_USERNAME", DbType.String, 50, oUser.USERNAME);                       
                        oDatabase.AddInOutParameter(oDbCommand, "P_SPASSWORD", DbType.String, 50, oUser.PASSWORD);
                        oDatabase.AddInOutParameter(oDbCommand, "P_LOGIN_SESSION_KEY", DbType.String, 50, oUser.SessionKey);
                        oDatabase.AddInOutParameter(oDbCommand, "P_USERACTIVE", DbType.String, 50, oUser.SessionKey);
                        oDatabase.AddInOutParameter(oDbCommand, "P_USERLOCKED", DbType.String, 50, oUser.SessionKey);
                        oDatabase.AddInOutParameter(oDbCommand, "P_USERALTERFLAG", DbType.String, 50, oUser.SessionKey);
                        //oDatabase.AddInOutParameter(oDbCommand, "P_branchid", DbType.String, 50, oUser.BranchInfo.CN);
                        oDatabase.AddOutParameter(oDbCommand, "rc_roleList", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "P_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);


                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        string USERID = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_USERID")]).Value.ToString();
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                        string sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_success")]).Value.ToString();
                        string LOGIN_SESSION_KEY = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_LOGIN_SESSION_KEY")]).Value.ToString();
                        oUser.USERACTIVE = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_USERACTIVE")]).Value.ToString();
                        oUser.UserLocked = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_USERLOCKED")]).Value.ToString();
                        oUser.UserAlterFlag = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_USERALTERFLAG")]).Value.ToString();
                        //oUser.BranchInfo.CN = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_branchid")]).Value.ToString();

                        if (sSuccess == "1")
                        {
                            oUser.CN = USERID;

                            oUser.LoginKey = LOGIN_SESSION_KEY;


                            CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/LoginUserInfo: " + sMessage);

                            if (oDataSet.Tables != null)
                            {
                                 oUser.RoleListdt = oDataSet.Tables[0];
                                 GetDistinctUserRoleForLoginUser(oUser, oDataSet.Tables[0]);
                            }

                            oResult.Return = oUser;
                            oResult.Result = true;
                        }
                        else
                        {
                            oResult.Message = sMessage;
                            oResult.Result = false;
                            CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/LoginUserInfo: User id is null - user is inactive " + sMessage);
                        }


                    }
                    catch (Exception exp)
                    {
                        oResult.Exception = exp;
                        oResult.Message = exp.Message;
                        oResult.Result = false;
                        oResult.Return = null;
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/LoginUserInfo: " + exp.Message);
                    }
                    finally
                    {

                        //oDataSet.Clear();
                        //oDataSet.Dispose();

                        oConnection.Close();
                    }
                }
                return oResult;
            }
            catch (Exception exp)
            {

                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/LoginUserInfo: " + exp.Message);
            }
            return oResult;
        }


        public CResult GetAccInfo(CUser oUser)
        {

            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            string sPassword = "";
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_IBANKING.ACC_INFO");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_USERID", DbType.String, 50, oUser.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_LOGIN_KEY", DbType.String, 50, oUser.LoginKey);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SESSION_KEY", DbType.String, 50, oUser.SessionKey);
                       
                        oDatabase.AddOutParameter(oDbCommand, "rc_ACCINFO", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "P_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);

                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);
                        //StringBuilder json = new StringBuilder();
                        //IDataReader reader = oDatabase.ExecuteDataReader(oDbCommand);
                      
                        //    while (reader.Read())
                        //    {
                        //        json.AppendFormat("{{\"name\": \"{0}\"}}", reader["name"]);
                        //    }
                       

                        oResult.Return = oDataSet.Tables[0];
                        oResult.Result = true;


                    }
                    catch (Exception exp)
                    {
                        oResult.Exception = exp;
                        oResult.Message = exp.Message;
                        oResult.Result = false;
                        oResult.Return = null;
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetAccInfo: " + exp.Message);
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

                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/LoginUserInfo: " + exp.Message);
            }
            return oResult;
        }

        private void GetDistinctUserRoleForLoginUser(CUser oUser, DataTable DtRole)
        {
            CRole oRole = new CRole();
            CRoleList oRoleList = new CRoleList();
            string sRoleName = "";
            DataTable uniqueCols = DtRole.DefaultView.ToTable(true, "ROLENAME");
            for (int i = 0; i < uniqueCols.Rows.Count; i++)
            {
                oRole = new CRole();
                oRole.RoleName = uniqueCols.Rows[i]["ROLENAME"].ToString();
                oUser.RoleList.RoleDataList.Add(oRole);
                
            }
        }

        [Author("Md. Ali Ahsan","09-02-2015","Get User Information")]

        public CResult Get_UserInforByID(CUser oUser)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_ROLE.Get_UserInforByID");

                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_USER_ID", DbType.Int16, 50,oUser.CN);
                        oDatabase.AddOutParameter(oDbCommand, "rc_roleList", "RefCursor", 100);
                        oDatabase.AddOutParameter(oDbCommand, "rc_userinfo", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "P_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 50, DBNull.Value);

                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        if (oDataSet.Tables != null)
                        {
                            oUser.RoleList = BuildRoleList(oDataSet.Tables[0]);
                            BuildUserEntity(oUser, oDataSet.Tables[1]);

                            oResult.Result = true;
                            oResult.Return = oUser;
                        }
                        else
                        {
                            oResult.Return = null;
                        }

                       
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                        string success = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_USER_ID")]).Value.ToString();                   


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
        [ModifiedAuthor("Md. Ali Ahsan","09-02-2015"," Build user data")]
        private void BuildUserEntity(CUser oUser,DataTable dtUser)
        {
            for (int i = 0; i < dtUser.Rows.Count; i++)
            {
                oUser.USERNAME = Convert.ToString(dtUser.Rows[i]["USERNAME"]);
                oUser.PASSWORD = Convert.ToString(dtUser.Rows[i]["SPASSWORD"]);
                oUser.EMAIL = Convert.ToString(dtUser.Rows[i]["EMAIL"]);
                oUser.FULL_NAME = Convert.ToString(dtUser.Rows[i]["FULL_NAME"]);
                oUser.DESIGNATION = Convert.ToString(dtUser.Rows[i]["DESIGNATION"]);
                oUser.USERACTIVE = Convert.ToString(dtUser.Rows[i]["USERACTIVE"]);
                oUser.UserLocked = Convert.ToString(dtUser.Rows[i]["USERLOCKED"]);
                oUser.EmpId = Convert.ToString(dtUser.Rows[i]["EMPID"]); // Added by Ali
                oUser.BranchInfo.CN = Convert.ToString(dtUser.Rows[i]["BRANCHID"]); // Added by Ali
                oUser.DivInfo.CN = Convert.ToString(dtUser.Rows[i]["DIVID"]); // Added by Ali
            }

        }
        

        private CRoleList BuildRoleList(DataTable dtRole)
        {
            CRoleList oRoleList = new CRoleList();
            CRole oRole = new CRole();
            for (int i = 0; i < dtRole.Rows.Count; i++)
            {
                oRole = new CRole();
                oRole.CN = Convert.ToString(dtRole.Rows[i]["ROLEID"]);
                oRole.RoleName = Convert.ToString(dtRole.Rows[i]["ROLENAME"]);

                oRoleList.RoleDataList.Add(oRole);
            }
            return oRoleList;

        }

        public CResult ChangePassword(CUser oUser)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_LOGIN.CHANGEPASSWORD");

                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_USERID", DbType.Int32, 50, oUser.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SPASSWORD", DbType.String, 50, oUser.PASSWORD);
                        oDatabase.AddInOutParameter(oDbCommand, "P_NEWSPASSWORD", DbType.String, 50, oUser.NEWPASSWORD);                   
                        oDatabase.AddInOutParameter(oDbCommand, "P_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);

                        int i = oDatabase.ExecuteNonQuery(oDbCommand);

                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                        string success = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_success")]).Value.ToString();
                        if (success == "1")
                        {
                            oResult.Result = true;
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
                    return oResult;
                }
            }
            catch (Exception exp)
            {
                return oResult;
            }
        }
          //------------NEWLY ADDED BY ALI---------------//
        public CResult UserLockedTest(CUser oUser)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_LOGIN.LOCKEDUSER");

                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_USERID", DbType.Int32, 50, 581);
                        oDatabase.AddInOutParameter(oDbCommand, "P_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);

                        int i = oDatabase.ExecuteNonQuery(oDbCommand);

                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                        string success = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_success")]).Value.ToString();


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



        public CResult InactiveUser(CUser oUser)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oConnection.CreateCommand();
                    try
                    {
                        //string sQuery = "";


                        string sQuery = "UPDATE USERINF SET  USERACTIVE = 'False' where USERNAME ='" + oUser.USERNAME + "'";
                        //string sQuery = "UPDATE USERINF SET  USERLOCKED ='True' where USERID ='" + oUser.CN + "'";


                        oDbCommand.CommandText = sQuery;
                        oDbCommand.CommandType = CommandType.Text;

                        int i = oDbCommand.ExecuteNonQuery();

                        if (i == 1)
                            oResult.Result = true;

                        //oDataSet = oDatabase.ExecuteSQLDataSet(oConnection, sQuery);

                        //if (oDataSet.Tables != null)
                        //{
                        //    oResult.Return = oDataSet.Tables[0];
                        //}
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





        public CResult LockUser(CUser oUser)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oConnection.CreateCommand();
                    try
                    {
                        //string sQuery = "";


                        string sQuery = "UPDATE USERINF SET  USERLOCKED = 'True' where USERNAME ='" + oUser.USERNAME + "'";
                        //string sQuery = "UPDATE USERINF SET  USERLOCKED ='True' where USERID ='" + oUser.CN + "'";


                        oDbCommand.CommandText = sQuery;
                        oDbCommand.CommandType = CommandType.Text;

                        int i = oDbCommand.ExecuteNonQuery();

                        if (i == 1)
                            oResult.Result = true;

                        //oDataSet = oDatabase.ExecuteSQLDataSet(oConnection, sQuery);

                        //if (oDataSet.Tables != null)
                        //{
                        //    oResult.Return = oDataSet.Tables[0];
                        //}
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




        public CResult IPTracking(CUser oUser)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oConnection.CreateCommand();
                    try
                    {
                        //string sQuery = "";


                        string sQuery = "insert into USERIPTRACKING (IP, ACCESS_DATE) VALUES ('"+oUser.USERIP+"', sysdate)";
                        //string sQuery = "UPDATE USERINF SET  USERLOCKED ='True' where USERID ='" + oUser.CN + "'";
                        //(IP, ACCESS_DATE) VALUES (oUser.USERIP, sysdate);

                        oDbCommand.CommandText = sQuery;
                        oDbCommand.CommandType = CommandType.Text;

                        int i = oDbCommand.ExecuteNonQuery();

                        if (i == 1)
                            oResult.Result = true;

                        //oDataSet = oDatabase.ExecuteSQLDataSet(oConnection, sQuery);

                        //if (oDataSet.Tables != null)
                        //{
                        //    oResult.Return = oDataSet.Tables[0];
                        //}
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




        public CResult ChecckPasswordExpiryDays(CUser oUser)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            bool validateLockUser = false;
            int days = 0;
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oConnection.CreateCommand();
                    try
                    {
                        string sQuery = "";

                        sQuery = "select TRUNC(SYSDATE-PASSWORDCHANGE_DATE) from USERINF WHERE USERID="+oUser.CN;

                        oDbCommand.CommandText = sQuery;
                        oDbCommand.CommandType = CommandType.Text;
                        OracleDataReader userDataReader = (OracleDataReader)oDbCommand.ExecuteReader();
                       
                        if (userDataReader.HasRows == true)
                        {
                            userDataReader.Read();
                            days = Convert.ToInt32(userDataReader[0].ToString());
                        }

                        oResult.TotalRecord = days;

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

        public CResult CheckUserPasswordReuse(CUser oUser)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oConnection.CreateCommand();
                    try
                    {
                        string sQuery = "";

                        sQuery = "select * from USERINF where USERID ='" + oUser.USERNAME + "'";

                        oDataSet = oDatabase.ExecuteSQLDataSet(oConnection, sQuery);

                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = oDataSet.Tables[0];
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

      

        public CResult UnlockUser(CUser oUser)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oConnection.CreateCommand();
                    try
                    {
                        string sQuery = "";
                        if (oUser.UserLocked.Equals("1") && oUser.USERACTIVE.Equals("1"))
                            sQuery = "UPDATE USERINF SET USERLOCKED = 'False', USERACTIVE = 'True'  where USERID ='" + oUser.CN + "'";
                        else if (oUser.UserLocked.Equals("1") && oUser.USERACTIVE != "1")
                            sQuery = "UPDATE USERINF SET USERLOCKED ='False' where USERID ='" + oUser.CN + "'";
                        else if (oUser.USERACTIVE.Equals("1") && oUser.UserLocked != "1")
                            sQuery = "UPDATE USERINF SET USERACTIVE='True' where USERID ='" + oUser.CN + "'";                      
                        
                        oDbCommand.CommandText = sQuery;
                        oDbCommand.CommandType = CommandType.Text;
                        int i = oDbCommand.ExecuteNonQuery();
                        if (i == 1)
                            oResult.Result = true;
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

        public CResult UserActive(CUser oUser)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oConnection.CreateCommand();
                    try
                    {
                        string sQuery = "";
                       
                            sQuery = "UPDATE USERINF SET USERACTIVE = '"+oUser.USERACTIVE+"'  where USERID ='" + oUser.CN + "'";
                        oDbCommand.CommandText = sQuery;
                        oDbCommand.CommandType = CommandType.Text;
                        int i = oDbCommand.ExecuteNonQuery();
                        if (i == 1)
                        {
                            oResult.Result = true;
                            oResult.Message = "User status is changed successfully.";
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


        public CResult ChequeUserLock(CUser oUser)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            //bool validateLockUser = false;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oConnection.CreateCommand();
                    try
                    {
                        string sQuery = "";

                        sQuery = "select userid,nvl(USERLOCKED,0) from USERINF where USERNAME = '" + oUser.CN + "'";
                        oDataSet = oDatabase.ExecuteSQLDataSet(oConnection, sQuery);

                        if (oDataSet.Tables != null)
                        {
                            oResult.Return =oDataSet.Tables[0];
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
        public CResult UserLogging(CUser oUser)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oConnection.CreateCommand();
                    try
                    {
                        string sQuery = "";

                        sQuery = "INSERT INTO USERLOG(USERID,LOGOUT_TIME) VALUES('" + oUser.CN + "' ,SYSDATE)";
                    
                        oDataSet = oDatabase.ExecuteSQLDataSet(oConnection, sQuery);

                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = oDataSet.Tables[0];
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
        public CResult IsUserAuthenticated(CUser userObject)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oConnection.CreateCommand();
                    try
                    {
                        string sQuery = "";

                        sQuery = "select USERID,USERALTERFLAG,USERNAME from USERINF where USERID ='" + userObject.USERNAME + "' and USERACTIVE =1";

                        oDataSet = oDatabase.ExecuteSQLDataSet(oConnection, sQuery);

                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = oDataSet.Tables[0];
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
       
        public CResult ValidateIPAddress(CUser oUser)
        {

            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            string dbIPAddress = "";
            bool validateIPAddress = false;


            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oConnection.CreateCommand();
                    try
                    {
                        string sQuery = "";

                        sQuery = "select UserIpAddress from t_User where UserCode = '" + oUser.CN + "'";


                        oDbCommand.CommandText = sQuery;
                        oDbCommand.CommandType = CommandType.Text;

                        // IDataReader userDataReader = oDbCommand.ExecuteReader();
                        SqlDataReader userDataReader = (SqlDataReader)oDbCommand.ExecuteReader();
                        if (userDataReader.HasRows == true)
                        {
                            userDataReader.Read();
                            dbIPAddress = userDataReader[0].ToString();
                        }
                        if (dbIPAddress == "")
                        {
                            //this.UpdateUserNewIP(oUser.UserCode, oUser.UserIPAdress);
                            validateIPAddress = true;
                        }
                        //else if (dbIPAddress == oUser.UserIPAdress)
                        //{
                        //    validateIPAddress = true;
                        //}
                        else
                        {
                            validateIPAddress = false;
                        }

                        oResult.Result = validateIPAddress;

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

                throw exp;
            }

        }



        public CResult LogOut(CUser oUser)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oConnection.CreateCommand();
                    try
                    {
                        string sQuery = "";
                        sQuery = "UPDATE USERLOG SET LOGOUT_TIME = SYSDATE where SESSIONKEY ='" + oUser.SessionKey + "'";
                      
                        oDbCommand.CommandText = sQuery;
                        oDbCommand.CommandType = CommandType.Text;
                        int i = oDbCommand.ExecuteNonQuery();
                        if (i == 1)
                            oResult.Result = true;

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
        /// Description: This Method used to get user List
        /// Date: June 18, 2014
        /// </summary>
        /// <param name="oUser"></param>
        /// <returns></returns>
        public CResult UserSearchByUserName(CUser oUser)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_SEARCH.SEARCH_USERINF");
                    try
                    {
                        //oDatabase.AddInOutParameter(oDbCommand, "P_USERID", DbType.Int32, 100, oUser.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_USERNAME", DbType.String, 100, oUser.USERNAME);
                        oDatabase.AddInOutParameter(oDbCommand, "P_EMAIL", DbType.String, 100, oUser.EMAIL);
                        oDatabase.AddInOutParameter(oDbCommand, "P_FULL_NAME", DbType.String, 10, oUser.FULL_NAME);
                        oDatabase.AddInOutParameter(oDbCommand, "P_DESIGNATION", DbType.String, 100, oUser.DESIGNATION);
                        oDatabase.AddOutParameter(oDbCommand, "rc_userinf", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);


                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();



                        oResult.Message = sMessage;

                        if (oDataSet.Tables != null)
                        {
                            //oResult.Return = oDataSet.Tables[0];
                            oResult.Return = BuildUserName(oDataSet.Tables[0]);

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


        private CUserList BuildUserName(DataTable dtUser)
        {
            CUser oUser = new CUser();
            CUserList oUserList = new CUserList();
            for (int i = 0; i < dtUser.Rows.Count; i++)
            {
                oUser = new CUser();
                oUser.CN = Convert.ToString(dtUser.Rows[i]["USERID"]);
                oUser.USERNAME = Convert.ToString(dtUser.Rows[i]["USERNAME"]);
                oUser.PASSWORD = Convert.ToString(dtUser.Rows[i]["SPASSWORD"]);
                oUser.EMAIL = Convert.ToString(dtUser.Rows[i]["EMAIL"]);
                oUser.FULL_NAME = Convert.ToString(dtUser.Rows[i]["FULL_NAME"]);
                oUser.DESIGNATION = Convert.ToString(dtUser.Rows[i]["DESIGNATION"]);
                oUser.UserRole = Convert.ToString(dtUser.Rows[i]["ROLENAME"]);
                oUser.Status = Convert.ToString(dtUser.Rows[i]["status"]);
                oUserList.UserDataList.Add(oUser);
            }
            return oUserList;
        }

        private CUser BuildAccount(DataTable dtUser, CUser oUser)
        {
            CAccInfo oAccInfo = new CAccInfo();
            CAccList oAccList = new CAccList();
            
            for (int i = 0; i < dtUser.Rows.Count; i++)
            {
                oAccInfo = new CAccInfo();
                oAccInfo.AccountName = Convert.ToString(dtUser.Rows[i]["AccountName"]);
                oAccInfo.AccountNumber = Convert.ToString(dtUser.Rows[i]["AccountsCode"]);
                oUser.AccountList.AccInfoList.Add(oAccInfo);
            }

            return oUser;

           

        }
        private CUser BuildUserDetails(DataTable dtUser)
        {
            CUser oUser = new CUser();
            CUserList oUserList = new CUserList();

      
            for (int i = 0; i < dtUser.Rows.Count; i++)
            {
                oUser = new CUser();
                oUser.CN = Convert.ToString(dtUser.Rows[i]["USERID"]);
                oUser.USERNAME = Convert.ToString(dtUser.Rows[i]["USERNAME"]);
                oUser.PASSWORD = Convert.ToString(dtUser.Rows[i]["SPASSWORD"]);
                oUser.EMAIL = Convert.ToString(dtUser.Rows[i]["EMAIL"]);
                oUser.FULL_NAME = Convert.ToString(dtUser.Rows[i]["FULL_NAME"]);
                oUser.DESIGNATION = Convert.ToString(dtUser.Rows[i]["DESIGNATION"]);
                oUser.ACCNAME = Convert.ToString(dtUser.Rows[i]["ACCNAME"]);
                oUser.ACCNO = Convert.ToString(dtUser.Rows[i]["ACCNO"]);
                oUser.EmployeeId = Convert.ToString(dtUser.Rows[i]["EMPID"]);

                oUser.ReferenceNumber = Convert.ToString(dtUser.Rows[i]["REFNO"]);
                oUser.DEVICEID = Convert.ToString(dtUser.Rows[i]["DEVICEID"]);
                oUser.CUST_ID = Convert.ToString(dtUser.Rows[i]["CUST_ID"]);
                oUser.MOBILE_NO = Convert.ToString(dtUser.Rows[i]["MOBILE_NO"]);

                oUser.USERACTIVE = Convert.ToString(dtUser.Rows[i]["USERACTIVE"]);
                oUser.UserLocked = Convert.ToString(dtUser.Rows[i]["USERLOCKED"]);
                oUser.UserAlterFlag = Convert.ToString(dtUser.Rows[i]["USERALTERFLAG"]);
                oUser.PASSWORDCHANGE_DATE = Convert.ToString(dtUser.Rows[i]["PASSWORDCHANGE_DATE"]);
                
                //oUserList.UserDataList.Add(oUser);
            }
            return oUser;
        }


        public CResult GetDetailsUserinfo(CUser oUser)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oConnection.CreateCommand();
                    try
                    {
                        string sQuery = "";

                        //sQuery = "select USERNAME,EMAIL,FULL_NAME,DESIGNATION,CREATE_DATE,USERACTIVE,USERLOCKED from USERINF where USERID ='" + oUser.CN + "'";

                        sQuery = "SELECT USERNAME,EMAIL,FULL_NAME AS \"FULL NAME\",DESIGNATION,CREATE_DATE as \"CREATE DATE\",CASE WHEN USERACTIVE='True' THEN 'YES' ELSE 'NO' END USERACTIVE,CASE WHEN USERLOCKED='False' THEN 'NO' ELSE 'YES' END USERLOCKED";
                        sQuery = sQuery + " FROM USERINF WHERE USERID ='" + oUser.CN + "'" ;

                        oDataSet = oDatabase.ExecuteSQLDataSet(oConnection, sQuery);

                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = oDataSet.Tables[0];
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


        public CResult GetUserDetailbyRefNo(CUser oUser)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_IBANKING.getinfo_by_refno");
                    try
                    {
                        //oDatabase.AddInOutParameter(oDbCommand, "P_USERID", DbType.Int32, 100, oUser.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_refNo", DbType.String, 100, oUser.ReferenceNumber);
                        oDatabase.AddOutParameter(oDbCommand, "rc_ACCINFO", "RefCursor", 100);
                        oDatabase.AddOutParameter(oDbCommand, "rc_ACCList", "RefCursor", 100);
                        
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);


                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();



                        oResult.Message = sMessage;

                        if (oDataSet.Tables != null)
                        {
                            //oResult.Return = oDataSet.Tables[0];
                            oUser = BuildUserDetails(oDataSet.Tables[0]);
                            oResult.Return = BuildAccount(oDataSet.Tables[1], oUser);

                            oResult.Result = true;

                        }
                        else
                        {
                            oResult.Return = null;
                            oResult.Result = false;
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

        public CResult GetAccInfoByAccNo(CAccInfo oAccInfo)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_IBANKING.ACC_INFO_BY_ACCNO");
                    try
                    {
                      
                        oDatabase.AddInOutParameter(oDbCommand, "P_ACCNO", DbType.String, 100, oAccInfo.AccountNumber);
                        oDatabase.AddOutParameter(oDbCommand, "rc_ACCINFO", "RefCursor", 100);
                      
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);


                        IDataReader oReader = oDatabase.ExecuteDataReader(oDbCommand);

                        while (oReader.Read())
                        {
                            oAccInfo.AccountName = oReader["AccountName"].ToString();
                            oAccInfo.AccountNumber = oReader["AccountsCode"].ToString();
                            oAccInfo.AccountType = oReader["TYPE"].ToString();
                            oAccInfo.CustomerID = oReader["CustomerId"].ToString();
                            oAccInfo.UserId = oReader["USERID"].ToString();
                        }
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();

                        oResult.Return = oAccInfo;
                        oResult.Result = true;


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


        public CResult AddAccMapInfo(CUser oUser)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;


            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_LOGIN.AddAccMapInfo");


                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_USERID", DbType.Int16, 50, oUser.AccInfo.UserId);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ACCNO", DbType.String, 50, oUser.AccInfo.AccountNumber);
                        oDatabase.AddInOutParameter(oDbCommand, "P_CREATEDBY", DbType.String, 50, oUser.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 50, DBNull.Value);


                        int i = oDatabase.ExecuteNonQuery(oDbCommand);


                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                        string userId = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_USERID")]).Value.ToString();

                        oUser.CN = userId;
                        oUser.Status = sMessage;
                        oResult.Result = true;
                        oResult.Return = oUser;
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

      



    }
}
