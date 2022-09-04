/*
 * File name            : CBranchDA.cs
 * Author               : Md. Ali Ahsan
 * Date                 : April 08, 2014
 * Version              : 1.0
 *
 * Description          : Branch Infortmation DataAccess Class
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
using SIBLRemit.DA.Common.Connections;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using SIBLCommon.Common.Entity.Bank;
using SIBLCommon.Common.Entity.Result;
using SIBLCommon.Common.Entity.District;
using SIBLCommon.Common.Util.Logger;

namespace SIBLRemit.DA.Branch
{
    public class CBranchDA
    {

        public CResult AddBranchInformation(CBranch oBranch)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_BRANCH.INS_BRANCHINF");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand,"P_BRANCH_ID",DbType.Int32,50,DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BRANCH_NAME",DbType.String,50,oBranch.BranchName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ROUTING_NO", DbType.Int32, 50, oBranch.RoutingNumber);
                        oDatabase.AddInOutParameter(oDbCommand, "P_DISTRICTID", DbType.Int32, 50, oBranch.District.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BANK_ID", DbType.Int32, 50, oBranch.Bank.CN);                        
                        oDatabase.AddInOutParameter(oDbCommand, "P_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 50, DBNull.Value);
                        int i = oDatabase.ExecuteNonQuery(oDbCommand);

                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                        string sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_success")]).Value.ToString();

                        if (sSuccess == "1")
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
        /// Description: This Method used to generate report for Barnch with data
        /// Date: 10 August,2014
        /// </summary>
        /// <param name="oBank"></param>
        /// <returns></returns>

        public CResult GetBranchListbyData(CBank oBank)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_BANK.GetBranchListByData");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_BANK_ID", DbType.Int32, 50, oBank.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_COMPANY_ID", DbType.Int32, 50, oBank.Company);
                        oDatabase.AddInOutParameter(oDbCommand, "p_FromDate", DbType.String, 50, oBank.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_ToDate", DbType.String, 50, oBank.ToDate);
                        oDatabase.AddOutParameter(oDbCommand, "rc_BranchList", "RefCursor", 100);
                        //oDatabase.AddInOutParameter(oDbCommand, "P_operatuinType", DbType.String, 50, oBank.Operatiom_Type);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 50, DBNull.Value);

                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = BuildBranchEntity(oDataSet.Tables[0]);

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


        /// <summary>
        /// Author: Md. Ali Ahsan
        /// Description: Get District List 
        /// Date: April 08, 2014
        /// </summary>
        /// <param name="oDistList"></param>
        /// <returns></returns>
        public CResult GetDistrictList(CDistrictList oDistList)
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
                        string sql = "SELECT DISTID, dist_name from district order by dist_name";
                        oDataSet = oDatabase.ExecuteSQLDataSet(oConnection, sql);
                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = BuildDistrictEntity(oDataSet.Tables[0]);
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
                    return oResult;
                }
            }
            catch (Exception exp)
            {

                return oResult;
            }
        }

        private CDistrictList BuildDistrictEntity(DataTable dt)
        {
            CDistrictList oDistrictList = new CDistrictList();
            CDistrict oDistrict = new CDistrict();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                oDistrict = new CDistrict();
                oDistrict.CN = Convert.ToString(dt.Rows[i]["DISTID"]);
                oDistrict.DistrictName = Convert.ToString(dt.Rows[i]["dist_name"]);

                oDistrictList.DistrictList.Add(oDistrict);
            }

            return oDistrictList;
        }


        public CResult GetBankList(CDistrict oDistrict)
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
                        string sql = "SELECT BANK_NAME, BANK_ID FROM BANK_INFO  order by BANK_NAME";
                        oDataSet = oDatabase.ExecuteSQLDataSet(oConnection, sql);
                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = BuildBankEntity(oDataSet.Tables[0]);
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

        private CBankList BuildBankEntity(DataTable dt)
        {
            CBankList oBankList = new CBankList();
            CBank oBank = new CBank();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                oBank = new CBank();
                oBank.CN = Convert.ToString(dt.Rows[i]["BANK_ID"]);
                oBank.BankName = Convert.ToString(dt.Rows[i]["BANK_NAME"]);

                oBankList.BankList.Add(oBank);
            }

            return oBankList;
        }

        /// <summary>
        /// Name:  Md. Aminul Islam
        /// Date : May 28, 2014
        /// </summary> Description : Get branch List
        /// <param name="oBank"></param>
        /// <returns></returns>
        public CResult GetBankBranchList(CBank oBank)
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
                        string sql = "SELECT BRANCH_ID, BRANCH_NAME,  ROUTING_NO,  DISTRICTID,  BANK_ID FROM BRANCH_INFO where BANK_ID=" + oBank.CN + " order by BRANCH_NAME";
                        oDataSet = oDatabase.ExecuteSQLDataSet(oConnection, sql);
                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = BuildBranchEntity(oDataSet.Tables[0]);
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

        private CBranchList BuildBranchEntity(DataTable dt)
        {
            CBranchList oBranchList = new CBranchList();
            CBranch oBranch = new CBranch();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                oBranch = new CBranch();
                oBranch.CN = Convert.ToString(dt.Rows[i]["BRANCH_ID"]);
                oBranch.BranchName = Convert.ToString(dt.Rows[i]["BRANCH_NAME"]);
                oBranch.Bank.CN = Convert.ToString(dt.Rows[i]["BANK_ID"]);
                oBranch.District.CN = Convert.ToString(dt.Rows[i]["DISTRICTID"]);
                oBranch.RoutingNumber = Convert.ToString(dt.Rows[i]["ROUTING_NO"]);

                oBranchList.BranchList.Add(oBranch);
            
            }
            return oBranchList;
        }


        public CResult GetBranchList(CBranch oBranch)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_CASHDATA_PROCESS.GET_BRANCHINF");
                    try
                    {
                        //oDatabase.AddInOutParameter(oDbCommand, "P_BRANCH_ID", DbType.Int32, 50, DBNull.Value);
                        oDatabase.AddOutParameter(oDbCommand, "rc_BranchList", "RefCursor", 100);                        
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);

                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = BuildGetBranchList(oDataSet.Tables[0]);
                            CLog.Logger.Write(CLog.INFORMATION, "/GetBranchList: Information Retrieve successfully ");

                        }
                        else
                        {
                            oResult.Return = null;
                            CLog.Logger.Write(CLog.INFORMATION, "/GetBranchList: Information is not Retrieve");
                        }

                        

                    }
                    catch (Exception exp)
                    {
                        oResult.Exception = exp;
                        oResult.Message = exp.Message;
                        oResult.Result = false;
                        oResult.Return = null;
                        CLog.Logger.Write(CLog.INFORMATION, "/GetBranchList: "+exp.ToString());
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

        private CBranchList BuildGetBranchList(DataTable dt)
        {
            CBranchList oBranchList = new CBranchList();
            CBranch oBranch = new CBranch();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                oBranch = new CBranch();
                oBranch.CN = Convert.ToString(dt.Rows[i]["BRANCH_ID"]);
                oBranch.BranchName = Convert.ToString(dt.Rows[i]["BRANCH_NAME"]);
                oBranchList.BranchList.Add(oBranch);

            }
            return oBranchList;
        }

        //[Author("Md. Ali Ahsan", "17-05-2015", "This Method used to get Branch List in Dropdown List")]
        //public CResult GetBranchList(CBranch oBranch)
        //{
        //    CResult oResult = new CResult();
        //    Database oDatabase = new Database();
        //    DataSet oDataSet = null;
        //    CBranchList oBranchList = new CBranchList();
        //    try
        //    {
        //        using (IDbConnection oConnection = oDatabase.CreateConnection())
        //        {
        //            IDbCommand oDbCommand = oConnection.CreateCommand();
        //            try
        //            {
        //                oDbCommand.CommandText = " SELECT BRANCH_ID,INITCAP(BRANCH_NAME) BRANCH_NAME,ROUTING_NO,DISTRICTID,BANK_ID,CREATE_DATE  FROM BRANCH_INFO WHERE BANK_ID=453 ORDER BY  BRANCH_NAME ";
        //                oDbCommand.CommandType = CommandType.Text;

        //                //oDataSet=oDatabase.ExecuteDataSet(oDbCommand);
        //                IDataReader oReader = oDatabase.ExecuteDataReader(oDbCommand);

        //                while (oReader.Read())
        //                {
        //                    oBranch = new CBranch();
        //                    oBranch.CN = oReader["BRANCH_ID"].ToString();
        //                    oBranch.BranchName = oReader["BRANCH_NAME"].ToString();
        //                    oBranchList.BranchList.Add(oBranch);
        //                }

        //                oResult.Result = true;
        //                oResult.Return = oBranchList;

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

        //                //oDataSet.Clear();
        //                //oDataSet.Dispose();

        //                oConnection.Close();
        //            }
        //        }
        //        return oResult;
        //    }
        //    catch (Exception exp)
        //    {

        //        return oResult;
        //    }
        //}



        /// <summary>
        /// Md. Aminul Islam
        /// Date: 25.05.2014
        /// </summary> Search Brach Information
        /// <param name="oBranch"></param>
        /// <returns></returns>

        public CResult SearchBrachInformation(CBranch oBranch)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;


            try
            {
              
                   using (IDbConnection oConnection = oDatabase.CreateConnection())
                    {
                        IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_SEARCH.SEARCH_BRANCH");
                        try
                        {
                            oDatabase.AddInOutParameter(oDbCommand, "P_BRANCH_ID", DbType.Int16, 50, DBNull.Value);
                            oDatabase.AddInOutParameter(oDbCommand, "P_BRANCH_NAME", DbType.String, 50, oBranch.BranchName);
                            oDatabase.AddInOutParameter(oDbCommand, "P_ROUTING_NO", DbType.String, 50, oBranch.RoutingNumber);
                            oDatabase.AddInOutParameter(oDbCommand, "P_DISTRICTID", DbType.String, 50, oBranch.District.CN);
                            oDatabase.AddInOutParameter(oDbCommand, "P_BANK_ID", DbType.String, 50, oBranch.Bank.CN);
                            oDatabase.AddOutParameter(oDbCommand, "rc_branchlist", "RefCursor", 100);
                            oDatabase.AddInOutParameter(oDbCommand, "P_success", DbType.String, 50, DBNull.Value);
                            oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 50, DBNull.Value);                           
                            oDataSet = oDatabase.ExecuteDataSet(oDbCommand);                            
                            if (oDataSet.Tables != null)
                            {
                                oResult.Return = BuildBranchEntity(oDataSet.Tables[0]);

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


        /// <summary>
        /// Md. Aminul Islam
        /// Date: 25.05.2014
        /// </summary> Get Brach Information
        /// <param name="oBranch"></param>
        /// <returns></returns>
        public CResult GetBranchInfo(CBranch oBranch)
        {


            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;


            try
            {

                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_SEARCH.SEARCH_BRANCH");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_BRANCH_ID", DbType.Int32, 50, oBranch.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BRANCH_NAME", DbType.String, 50, oBranch.BranchName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ROUTING_NO", DbType.String, 50, oBranch.RoutingNumber);
                        oDatabase.AddInOutParameter(oDbCommand, "P_DISTRICTID", DbType.String, 50, oBranch.District.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BANK_ID", DbType.String, 50, oBranch.Bank.CN);
                        oDatabase.AddOutParameter(oDbCommand, "rc_branchlist", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "P_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 50, DBNull.Value);
                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);
                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = BuildBranchEntity(oDataSet.Tables[0]);

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


        /// <summary>
        /// Md. Aminul Islam
        /// Date: May 28, 2014
        /// </summary> Description : Update Branch Information
        /// <param name="oBranch"></param>
        /// <returns></returns>

        public CResult UpdateBranchInfo(CBranch oBranch)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {

                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_BANK.CRUD_BBRANCHNF");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_BRANCH_ID", DbType.Int32, 50, oBranch.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BRANCH_NAME", DbType.String, 50, oBranch.BranchName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ROUTING_NO", DbType.String, 50, oBranch.RoutingNumber);
                        oDatabase.AddInOutParameter(oDbCommand, "P_DISTRICTID", DbType.String, 50, oBranch.District.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BANK_ID", DbType.String, 50, oBranch.Bank.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_operatuinType", DbType.String, 50, oBranch.Operatiom_Type);
                        oDatabase.AddInOutParameter(oDbCommand, "P_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 50, DBNull.Value);                        
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

    }
}
