/*
 * File name            : CPlacidDA.cs
 * Author               : Munirul Islam
 * Date                 : March 30, 2014
 * Version              : 1.0
 *
 * Description          : Placid Infortmation DataAccess Class
 *
 * Modification history :
 * Name                         Date                      Desc
 * Munirul Islam                04.02.2015                change data type of the function updateBankBrDistInf  
 * 
 * Copyright (c) 2014: SOCIAL ISLAMI BANK LIMITED
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DPDC.DA.Common.Connections;
using DPDC.Common.Entity.Placid;
using DPDC.Common.Entity.AllLookup;
using DPDC.Common.Entity.Result;
using System.Data.Common;
using DPDC.Common;
using System.Data ;
using System.Collections;
using Oracle.DataAccess.Client;
using DPDC.Common.Entity.CashPayment;
using DPDC.Common.Entity.RAWData;
using DPDC.Common.Entity.File;
using DPDC.Common.Util.Logger;
using DPDC.Common.Util.Attributes;
namespace DPDC.DA.Placid
{
    public class CPlacidDA
    {
        public void LogInUser()
        {
            //CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    DbCommand oDbCommand = oDatabase.GetStoredProcCommand("PKG_LOGIN.INS_USERINF");

                    try
                    {

                        oDatabase.AddInOutParameter(oDbCommand, "P_USERID", DbType.String, 50, "234");
                        oDatabase.AddInOutParameter(oDbCommand, "P_USERNAME", DbType.String, 50, "asd");
                        oDatabase.AddInOutParameter(oDbCommand, "P_SPASSWORD", DbType.String, 50, "asd");
                        // oDatabase.AddInOutParameter(oDbCommand, "P_MD5", DbType.String, 50, "Test");



                        int i = oDatabase.ExecuteNonQuery(oDbCommand);

                    }
                    catch (Exception exp)
                    {
                         CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/LogInUser: " + exp.Message);

                        //oResult.Exception = exp;
                        //oResult.Message = exp.Message;
                        //oResult.Result = false;
                        //oResult.Return = null;
                    }
                    finally
                    {
                        oDbCommand.Dispose();
                        oConnection.Close();
                    }
                }
                // return oResult;
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/LoginUser: " + exp.Message);
                //oResult.Exception = exp;
                //oResult.Message = exp.Message;
                //oResult.Result = false;
                //oResult.Return = null;
                //return oResult;
            }
        }


        public CResult ApprovedData(CPlacidList oPlacidList)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            int iTotalRecord = 0;
            // CPlacidList  oPlacidList= new CPlacidList();
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {

                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_PLACID.approved_data");

                    try
                    {
                        #region Add Placid Information
                        //oDatabase.BeginTransaction(oConnection);  
                        foreach (CPlacid oPlacid in oPlacidList.PlacidDataList)
                        {
                            #region AddInfo


                            oDatabase.AddInOutParameter(oDbCommand, "p_id", DbType.Int32, 50, oPlacid.CN);

                            oDatabase.AddInOutParameter(oDbCommand, "p_approved", DbType.String, 2, oPlacid.IsApproved);


                            oDatabase.AddInOutParameter(oDbCommand, "P_SUCCESS", DbType.String, 50, DBNull.Value);
                            oDatabase.AddInOutParameter(oDbCommand, "P_RETURN_MSG", DbType.String, 500, DBNull.Value);

                            int i = oDatabase.ExecuteNonQuery(oDbCommand);
                            //string st = oDbCommand.Parameters["p_success"]ToString();
                            string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();


                            oDbCommand.Parameters.Clear();

                            iTotalRecord++;
                            #endregion AddInfo
                        }
                        //oDbCommand.Transaction.Commit();
                        #endregion Add Placid Information
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
                        //oDbCommand.Transaction.Commit();
                        //oDbCommand.Dispose();
                        // oDatabase.CommitTransaction(oDbCommand);

                        oConnection.Close();
                        oResult.Result = true;
                        if (iTotalRecord > 1)
                            oResult.Message = iTotalRecord.ToString() + " Records are Approved Successfully.";
                        else if (iTotalRecord == 1)
                            oResult.Message = iTotalRecord.ToString() + " Record is Approved Successfully.";
                        else
                        {
                            oResult.Message = " No Data is Approved";
                            oResult.Result = false;
                        }
                    }
                }
                return oResult;
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/ApprovedData: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
        }
        /// <summary>
        /// Author: Munirul Islam
        /// Date:27.03.2014
        /// Description: Add Placid Information into database
        /// </summary>
        /// <param name="oPlacidList"></param>
        /// <returns></returns>
        public CResult AddPlacidInfo(CPlacidList oPlacidList)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            int iTotalRecord = 0;
            // CPlacidList  oPlacidList= new CPlacidList();
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {

                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_PLACID.INS_PLACIDINFO");

                    try
                    {
                        #region Add Placid Information
                        //oDatabase.BeginTransaction(oConnection);  
                        foreach (CPlacid oPlacid in oPlacidList.PlacidDataList)
                        {
                            #region AddInfo


                            oDatabase.AddInOutParameter(oDbCommand, "P_PLACID_ID", DbType.Int32, 50, oPlacid.CN);
                            oDatabase.AddInOutParameter(oDbCommand, "P_ID_BRANCH", DbType.String, 10, oPlacid.IdBranch);
                            oDatabase.AddInOutParameter(oDbCommand, "P_ID_RECEIVER", DbType.String, 20, oPlacid.IdReceiver);
                            oDatabase.AddInOutParameter(oDbCommand, "P_TOTAL_PAY_RECEIVER", DbType.Double, 20, oPlacid.TotalPayReceiver);

                            oDatabase.AddInOutParameter(oDbCommand, "P_NAME_RECEIVER", DbType.String, 100, oPlacid.NameReceiver);
                            oDatabase.AddInOutParameter(oDbCommand, "P_ADDRESS_RECEIVER", DbType.String, 150, oPlacid.AddressReceiver);
                            oDatabase.AddInOutParameter(oDbCommand, "P_PHONE1_RECEIVER", DbType.String, 50, oPlacid.Phone1Receiver);

                            oDatabase.AddInOutParameter(oDbCommand, "P_PHONE2_RECEIVER", DbType.String, 50, oPlacid.Phone2Receiver);
                            oDatabase.AddInOutParameter(oDbCommand, "P_NAME_BRANCH", DbType.String, 150, oPlacid.NameBranch);
                            oDatabase.AddInOutParameter(oDbCommand, "P_NAME_SENDER", DbType.String, 150, oPlacid.NameSender);
                            oDatabase.AddInOutParameter(oDbCommand, "P_NAME_MAIN_BRANCH", DbType.String, 150, oPlacid.NameMainBranch);
                            oDatabase.AddInOutParameter(oDbCommand, "P_ID_COUNTRY_RECEIVER", DbType.String, 150, oPlacid.IdCountryReceiver);
                            oDatabase.AddInOutParameter(oDbCommand, "P_ID_CITY_RECEIVER", DbType.String, 150, oPlacid.IdCityReceiver);

                            oDatabase.AddInOutParameter(oDbCommand, "P_DATE_RECEIVER", DbType.String, 150, oPlacid.DateReceiver);
                            oDatabase.AddInOutParameter(oDbCommand, "P_REF_RECEIVER", DbType.String, 150, oPlacid.RefReceiver);
                            oDatabase.AddInOutParameter(oDbCommand, "P_ACC_RECEIVER", DbType.String, 150, oPlacid.AccReceiver);
                            oDatabase.AddInOutParameter(oDbCommand, "P_RECEIVER_BANK_RECEIVER", DbType.String, 150, oPlacid.ReceiverBankReceiver);
                            oDatabase.AddInOutParameter(oDbCommand, "P_NOTES_RECEIVER", DbType.String, 200, oPlacid.NotesReceiver);
                            oDatabase.AddInOutParameter(oDbCommand, "P_MODE_PAY_RECEIVER", DbType.String, 100, oPlacid.ModePayReceiver);

                            oDatabase.AddInOutParameter(oDbCommand, "P_ID_MAIN_BRANCH", DbType.String, 100, oPlacid.IdMainBranch);
                            oDatabase.AddInOutParameter(oDbCommand, "P_MOD_PAY_CURRENCY", DbType.String, 50, oPlacid.ModePayReceiver);
                            oDatabase.AddInOutParameter(oDbCommand, "P_EXCHANGE_RECEIVER", DbType.String, 200, oPlacid.ExchangeReceiver);
                            oDatabase.AddInOutParameter(oDbCommand, "P_NAME_BRANCH_BANK", DbType.String, 200, oPlacid.NameBanchBank);
                            oDatabase.AddInOutParameter(oDbCommand, "P_NOM_MODO_PAGO", DbType.String, 200, oPlacid.NomModoPago);
                            oDatabase.AddInOutParameter(oDbCommand, "P_TRACKINGNUMBER", DbType.String, 50, oPlacid.TrackingNumber);

                            oDatabase.AddInOutParameter(oDbCommand, "P_TIME_RECEIVER", DbType.String, 10, oPlacid.TimeReceiver);
                            oDatabase.AddInOutParameter(oDbCommand, "P_SET_PAYMENT_MODE", DbType.String, 50, oPlacid.PaymentMODE.CN);
                            oDatabase.AddInOutParameter(oDbCommand, "P_OPERATIONTYPE", DbType.String, 50, oPlacid.OperationType);
                            oDatabase.AddInOutParameter(oDbCommand, "p_isapproved", DbType.String, 2, oPlacid.IsApproved);
                            oDatabase.AddInOutParameter(oDbCommand, "p_CREATE_BY", DbType.String, 50, oPlacid.User.CN);

                            oDatabase.AddInOutParameter(oDbCommand, "P_SUCCESS", DbType.String, 50, DBNull.Value);
                            oDatabase.AddInOutParameter(oDbCommand, "P_RETURN_MSG", DbType.String, 500, DBNull.Value);

                            int i = oDatabase.ExecuteNonQuery(oDbCommand);
                            //string st = oDbCommand.Parameters["p_success"]ToString();
                            string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();


                            oDbCommand.Parameters.Clear();

                            iTotalRecord++;
                            #endregion AddInfo
                        }
                        //oDbCommand.Transaction.Commit();
                        #endregion Add Placid Information
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
                        //oDbCommand.Transaction.Commit();
                        //oDbCommand.Dispose();
                        // oDatabase.CommitTransaction(oDbCommand);

                        oConnection.Close();
                        oResult.Result = true;
                        if (iTotalRecord > 1)
                            oResult.Message = iTotalRecord.ToString() + " Records are inserted to database";
                        else if (iTotalRecord == 1)
                            oResult.Message = iTotalRecord.ToString() + " Record is inserted to database";
                        else
                        {
                            oResult.Message = " No Record is inserted to database";
                            oResult.Result = false;
                        }
                    }
                }
                return oResult;
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/AddPlacidInfo: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oPlacid"></param>
        /// <returns></returns>
        //public CResult GetPlacidInfo(CPlacid oPlacid)
        //{
        //    CResult oResult = new CResult();
        //    Database oDatabase = new Database();
        //    DataSet oDataSet = null;

        //    try
        //    {
        //        using (IDbConnection oConnection = oDatabase.CreateConnection())
        //        {                  
        //            try
        //            {                      
        //               string sql = "SELECT * from placidinfo";
        //               oDataSet= oDatabase.ExecuteSQLDataSet(oConnection,sql);
        //                if(oDataSet.Tables!=null){
        //                    oResult.Return = BuildPlacidListEntity(oDataSet.Tables[0]);
        //                }
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
        /// 
        /// </summary>
        /// <param name="oPlacid"></param>
        /// <returns></returns>

        public CResult GetPlacidInfo(CPlacid oPlacid)
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
                        string sql = "SELECT * from placidinfo";
                        oDataSet = oDatabase.ExecuteSQLDataSet(oConnection, sql);
                        if (oDataSet.Tables != null)
                        {
                            // oResult.Return = BuildPlacidListEntity(oDataSet.Tables[0]);
                            //oResult.Return = oDataSet;
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

                        oDataSet.Clear();
                        oDataSet.Dispose();

                        oConnection.Close();
                    }
                }
                return oResult;
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetPlacidInfo: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
               
            }
        }

        /// <summary>
        /// Author: Md. Ali Ahsan
        /// Date: 2014-04-01
        /// This Code for Payment Mode
        /// </summary>
        /// <param name="oPlacid"></param>
        /// <returns></returns>
        public CResult GetTypeListByKey(CAllLookup oAlllookUp)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())  // For Create connection
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_PLACID.GET_TYPE_LIST_BY_KEY"); // Used to Call Procedure
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
        /// <summary>
        /// Author: Munirul Islam
        /// Date: 02.04.2014
        /// Description: Get Table Coulmns name in a Hash Table
        /// </summary>
        /// <param name="oAlllookUp"></param>
        /// <returns></returns>
        public CResult GetTypeListHashTableByKey(CAllLookup oAlllookUp)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            Hashtable htColumnsHeaderList = new Hashtable();
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())  // For Create connection
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_PLACID.GET_TYPE_LIST_BY_KEY"); // Used to Call Procedure
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "p_typekey", DbType.String, 50, oAlllookUp.TYPE_KEY);
                        oDatabase.AddOutParameter(oDbCommand, "rc_lookuplist", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        if (oDataSet.Tables != null)
                        {

                            DataTable dt = new DataTable();
                            dt = oDataSet.Tables[0];
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                htColumnsHeaderList.Add(Convert.ToString(dt.Rows[i]["TYPE_NAME"]), Convert.ToString(dt.Rows[i]["TYPE_NAME"]));
                            }


                            oResult.Return = htColumnsHeaderList;


                        }
                        else
                        {
                            oResult.Return = null;
                        }

                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetTypeListHashTableByKey: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetTypeListHashTableByKey: " + ex.Message);
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

        public CResult GetMismatchDataByDate(CPlacid oPlacid)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_PLACID.get_mismatchdata_by_date");
                    try
                    {

                        oDatabase.AddInOutParameter(oDbCommand, "p_companyid", DbType.String, 100, oPlacid.CompanyInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_bank_name", DbType.String, 100, oPlacid.ReceiverBankReceiver);
                        oDatabase.AddInOutParameter(oDbCommand, "p_district_name", DbType.String, 100, oPlacid.AddressReceiver);
                        oDatabase.AddInOutParameter(oDbCommand, "p_fromDate", DbType.String, 100, oPlacid.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_toDate", DbType.String, 100, oPlacid.ToDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_paymentMode", DbType.String, 100, oPlacid.PaymentMODE.CN);
                        oDatabase.AddOutParameter(oDbCommand, "rc_placid", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);

                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);
                        string sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_success")]).Value.ToString();
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();


                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = BuildPlacidListEntity(oDataSet.Tables[0]);
                            oResult.Message = sMessage;
                        }
                        else
                        {
                            oResult.Return = null;
                            oResult.Message = sMessage;
                        }
                            
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetMismatchDataByDate: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetMismatchDataByDate: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
               
            }
        }
        /// <summary>
        /// Get Placid List By date
        /// </summary>
        /// <param name="oPlacid"></param>
        /// <returns></returns>
        public CResult GetPlacidListByDate(CPlacid oPlacid)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_PLACID.GET_PLACIDLIST_BY_DATE");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "p_COMPANYID", DbType.String, 100, oPlacid.CompanyInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_dist_name", DbType.String, 100, oPlacid.AddressReceiver);
                        oDatabase.AddInOutParameter(oDbCommand, "p_bank_name", DbType.String, 100, oPlacid.ReceiverBankReceiver);
                        oDatabase.AddInOutParameter(oDbCommand, "P_fromDate", DbType.String, 100, oPlacid.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_toDate", DbType.String, 100, oPlacid.ToDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_getALL", DbType.String, 10, oPlacid.DataRetrivalType);
                        oDatabase.AddInOutParameter(oDbCommand, "p_fileName", DbType.String, 10, oPlacid.FileName);

                        oDatabase.AddOutParameter(oDbCommand, "rc_placid", "RefCursor", 100);
                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);
                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = BuildPlacidListEntity(oDataSet.Tables[0]);
                            //oResult.Return = oDataSet.Tables[0];
                            //oResult.Return = oDataSet;
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

                        oDataSet.Clear();
                        oDataSet.Dispose();

                        oConnection.Close();
                    }
                }
                return oResult;
            }
            catch (Exception exp)
            {

                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetPlacidListByDate: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
            }
            return oResult;
        }
        /// <summary>
        /// Author: Munirul Islam
        /// 
        /// </summary>
        /// <param name="oPlacid"></param>
        /// <returns></returns>

        public CResult SearchByCompany(CPlacid oPlacid)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_SEARCH.SEARCH_RAWDATA");
                    try
                    {


                        oDatabase.AddInOutParameter(oDbCommand, "P_COMPANYID", DbType.String, 100, oPlacid.CompanyInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BRANCH_ID", DbType.String, 100, oPlacid.BranchInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BRANCH_NAME", DbType.String, 100, oPlacid.BranchInfo.BranchName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ROUTING_NO", DbType.String, 100, oPlacid.RoutingNumber);
                        oDatabase.AddInOutParameter(oDbCommand, "P_DISTRICTID", DbType.String, 100, oPlacid.DistrictInfo.DistrictName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BANK_ID", DbType.String, 200, oPlacid.BankInfo.BankName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_IMPORTED_DATA", DbType.String, 10, oPlacid.ImportedALLData);
                        oDatabase.AddInOutParameter(oDbCommand, "P_fromDate", DbType.String, 100, oPlacid.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_toDate", DbType.String, 100, oPlacid.ToDate);

                        oDatabase.AddInOutParameter(oDbCommand, "P_PinNO", DbType.String, 100, oPlacid.PinNo);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BeneficiaryName", DbType.String, 100, oPlacid.NameReceiver);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SenderName", DbType.String, 100, oPlacid.NameSender);
                        oDatabase.AddInOutParameter(oDbCommand, "P_AccountNo", DbType.String, 100, oPlacid.AccReceiver);
                        oDatabase.AddInOutParameter(oDbCommand, "P_Amount", DbType.Double, 50, oPlacid.TotalPayReceiver);

                        oDatabase.AddOutParameter(oDbCommand, "rc_RAWDATA", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 10, oPlacid.DataRetrivalType);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, oPlacid.DataRetrivalType);



                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);
                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = BuildCommonEntity(oDataSet.Tables[0]);
                            //oResult.Return = oDataSet.Tables[0];
                            //oResult.Return = oDataSet;
                        }
                        else
                            oResult.Return = null;
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/SearchByCompany: " + exp.Message);
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

                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/SearchByCompany: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
            }
            return oResult;
        }
        public CResult GetPlacidListDtByDate(CPlacid oPlacid)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_PLACID.GET_PLACIDLIST_BY_DATE");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_fromDate", DbType.String, 100, oPlacid.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_toDate", DbType.String, 100, oPlacid.ToDate);
                        oDatabase.AddOutParameter(oDbCommand, "rc_placid", "RefCursor", 100);
                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);
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
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetPlacidListDtByDate: " + exp.Message);
               
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetPlacidListDtByDate: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                return oResult;
            }
        }

        /// <summary>
        /// 
        ///  Author: Munirul Islam
        /// Date: 31.03.2014
        /// Get Placid Header Information
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>

        private CPlacidList BuildCommonEntity(DataTable dt)
        {
            CPlacidList oPlacidList = new CPlacidList();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CPlacid oPlacid = new CPlacid();
                oPlacid.CN = Convert.ToString(dt.Rows[i]["cn"]);
                //oPlacid.IdBranch = Convert.ToString(dt.Rows[i]["ID_BRANCH"]);
                //oPlacid.IdReceiver = Convert.ToString(dt.Rows[i]["ID_RECEIVER"]);
                oPlacid.NameReceiver = Convert.ToString(dt.Rows[i]["NameReceiver"]);
                oPlacid.AddressReceiver = Convert.ToString(dt.Rows[i]["AddressReceiver"]);
                oPlacid.Phone1Receiver = Convert.ToString(dt.Rows[i]["Phone1Receiver"]);
                //oPlacid.Phone2Receiver = Convert.ToString(dt.Rows[i]["PHONE2_RECEIVER"]);
                //oPlacid.NameBranch = Convert.ToString(dt.Rows[i]["NAME_BRANCH"]);
                oPlacid.NameSender = Convert.ToString(dt.Rows[i]["NameSender"]);
                //oPlacid.NameMainBranch = Convert.ToString(dt.Rows[i]["NAME_MAIN_BRANCH"]);
                //oPlacid.IdCountryReceiver = Convert.ToString(dt.Rows[i]["ID_COUNTRY_RECEIVER"]);
                //oPlacid.IdCityReceiver = Convert.ToString(dt.Rows[i]["ID_CITY_RECEIVER"]);
                //oPlacid.DateReceiver = Convert.ToString(dt.Rows[i]["DATE_RECEIVER"]);
                //oPlacid.RefReceiver = Convert.ToString(dt.Rows[i]["REF_RECEIVER"]);
                oPlacid.AccReceiver = Convert.ToString(dt.Rows[i]["AccReceiver"]);
                oPlacid.ReceiverBankReceiver = Convert.ToString(dt.Rows[i]["ReceiverBankReceiver"]);
                //oPlacid.NotesReceiver = Convert.ToString(dt.Rows[i]["NOTES_RECEIVER"]);
                //oPlacid.ModePayReceiver = Convert.ToString(dt.Rows[i]["MODE_PAY_RECEIVER"]);
                //oPlacid.IdMainBranch = Convert.ToString(dt.Rows[i]["ID_MAIN_BRANCH"]);
                //oPlacid.ModPayCurrency = Convert.ToString(dt.Rows[i]["MOD_PAY_CURRENCY"]);
                oPlacid.CheckNo = Convert.ToString(dt.Rows[i]["checkNO"]);
                oPlacid.NameBanchBank = Convert.ToString(dt.Rows[i]["NameBanchBank"]);
                //oPlacid.NomModoPago = Convert.ToString(dt.Rows[i]["NOM_MODO_PAGO"]);
                oPlacid.TrackingNumber = Convert.ToString(dt.Rows[i]["TrackingNumber"]);
                oPlacid.TotalPayReceiver = Convert.ToDouble(dt.Rows[i]["Amount"]);
                oPlacid.Comments = Convert.ToString(dt.Rows[i]["Comments"]);
                oPlacid.PinNo = Convert.ToString(dt.Rows[i]["PinNo"]);
                //oPlacid.TimeReceiver = Convert.ToString(dt.Rows[i]["TIME_RECEIVER"]);
                // oPlacid.PaymentMODE.TYPE_NAME = Convert.ToString(dt.Rows[i]["PAYMENT_MODE_NAME"]);
               
                oPlacidList.PlacidDataList.Add(oPlacid);
            }
            return oPlacidList;
        }
        public CResult GetMatchDataListRpt(CPlacid oPlacid)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_PLACID.rpt_matchdata_by_date");

                    try
                    {

                        oDatabase.AddInOutParameter(oDbCommand, "p_COMPANYID", DbType.String, 50, oPlacid.CompanyInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_fromDate", DbType.String, 50, oPlacid.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_toDate", DbType.String, 50, oPlacid.ToDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_DataType", DbType.String, 50, oPlacid.DataRetrivalType);
                        oDatabase.AddInOutParameter(oDbCommand, "p_district_name", DbType.String, 50, oPlacid.DistrictInfo.DistrictName);
                        oDatabase.AddInOutParameter(oDbCommand, "p_bank_name", DbType.String, 50, oPlacid.BankInfo.BankName);
                        oDatabase.AddOutParameter(oDbCommand, "rc_placid", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);


                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        if (oDataSet.Tables != null)
                        {
                            //oResult.Return = BuildPlacidListEntity(oDataSet.Tables[0]);
                            oResult.Return = oDataSet.Tables[0];
                        }

                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetMatchDataListRpt: " + exp.Message);
             
                    }
                    finally
                    {
                        //oDataSet.Dispose();
                        oDbCommand.Dispose();
                        oConnection.Close();
                    }
                }
                return oResult;
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetMatchDataListRpt: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                return oResult;
            }
            return oResult;
        }
       
        private CPlacidList BuildPlacidListEntity(DataTable dt)
        {

            CPlacidList oPlacidList = new CPlacidList();

            try {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CPlacid oPlacid = new CPlacid();
                    oPlacid.CN = Convert.ToString(dt.Rows[i]["PLACID_ID"]);
                    oPlacid.IdBranch = Convert.ToString(dt.Rows[i]["ID_BRANCH"]);
                    oPlacid.IdReceiver = Convert.ToString(dt.Rows[i]["ID_RECEIVER"]);
                    oPlacid.NameReceiver = Convert.ToString(dt.Rows[i]["NAME_RECEIVER"]);
                    oPlacid.AddressReceiver = Convert.ToString(dt.Rows[i]["ADDRESS_RECEIVER"]);
                    oPlacid.Phone1Receiver = Convert.ToString(dt.Rows[i]["PHONE1_RECEIVER"]);
                    oPlacid.Phone2Receiver = Convert.ToString(dt.Rows[i]["PHONE2_RECEIVER"]);
                    oPlacid.NameBranch = Convert.ToString(dt.Rows[i]["NAME_BRANCH"]);
                    oPlacid.NameSender = Convert.ToString(dt.Rows[i]["NAME_SENDER"]);
                    oPlacid.NameMainBranch = Convert.ToString(dt.Rows[i]["NAME_MAIN_BRANCH"]);
                    oPlacid.IdCountryReceiver = Convert.ToString(dt.Rows[i]["ID_COUNTRY_RECEIVER"]);
                    oPlacid.IdCityReceiver = Convert.ToString(dt.Rows[i]["ID_CITY_RECEIVER"]);
                    oPlacid.DateReceiver = Convert.ToString(dt.Rows[i]["DATE_RECEIVER"]);
                    oPlacid.RefReceiver = Convert.ToString(dt.Rows[i]["REF_RECEIVER"]);
                    oPlacid.AccReceiver = Convert.ToString(dt.Rows[i]["ACC_RECEIVER"]);
                    oPlacid.ReceiverBankReceiver = Convert.ToString(dt.Rows[i]["RECEIVER_BANK_RECEIVER"]);
                    oPlacid.NotesReceiver = Convert.ToString(dt.Rows[i]["NOTES_RECEIVER"]);
                    oPlacid.ModePayReceiver = Convert.ToString(dt.Rows[i]["MODE_PAY_RECEIVER"]);
                    oPlacid.IdMainBranch = Convert.ToString(dt.Rows[i]["ID_MAIN_BRANCH"]);
                    oPlacid.ModPayCurrency = Convert.ToString(dt.Rows[i]["MOD_PAY_CURRENCY"]);
                    oPlacid.ExchangeReceiver = Convert.ToString(dt.Rows[i]["EXCHANGE_RECEIVER"]);
                    oPlacid.NameBanchBank = Convert.ToString(dt.Rows[i]["NAME_BRANCH_BANK"]);
                    oPlacid.NomModoPago = Convert.ToString(dt.Rows[i]["NOM_MODO_PAGO"]);
                    oPlacid.TrackingNumber = Convert.ToString(dt.Rows[i]["TRACKINGNUMBER"]);
                    oPlacid.TimeReceiver = Convert.ToString(dt.Rows[i]["TIME_RECEIVER"]);
                    oPlacid.TotalPayReceiver = Convert.ToDouble(dt.Rows[i]["TOTAL_PAY_RECEIVER"]);
                    oPlacid.PaymentMODE.TYPE_NAME = Convert.ToString(dt.Rows[i]["PAYMENT_MODE_NAME"]);
                    oPlacid.OrderNo = Convert.ToString(dt.Rows[i]["ORDERNO"]);
                    oPlacid.PinNo = Convert.ToString(dt.Rows[i]["SECRETNO"]);
                    oPlacid.CheckNo = Convert.ToString(dt.Rows[i]["CHECKNO"]);
                    oPlacidList.PlacidDataList.Add(oPlacid);
                }
            
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/BuildPlacidListEntity: " + exp.Message);
            }
            
           
            return oPlacidList;
        }




        public CResult GetHeaderByID(CAllLookupList oAllLookupList)
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
                        Hashtable htColumnsHeaderList = new Hashtable();
                        //CAllLookupList oAllLookupList = new CAllLookupList();

                        CAllLookup oAllLookup = new CAllLookup();
                        string sql = "SELECT ID,TYPE_NAME FROM ALLLOOKUP WHERE PARENT_COMPANY_ID=" + oAllLookupList.TableKey + " ORDER BY ID asc";
                        oDataSet = oDatabase.ExecuteSQLDataSet(oConnection, sql);
                        if (oDataSet.Tables != null)
                        {
                            DataTable dt = new DataTable();
                            dt = oDataSet.Tables[0];
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                //oAllLookup = new CAllLookup();
                                //oAllLookup.TYPE_NAME = Convert.ToString(dt.Rows[i]["TYPE_NAME"]);
                                //oAllLookupList.AllLookupList.Add(oAllLookup);
                                htColumnsHeaderList.Add(i, Convert.ToString(dt.Rows[i]["TYPE_NAME"]));
                                //htColumnsHeaderList.Add(Convert.ToString(dt.Rows[i]["TYPE_NAME"]), Convert.ToString(dt.Rows[i]["TYPE_NAME"]));
                            }

                            //oResult.Return = oAllLookupList;
                            oResult.Return = htColumnsHeaderList;

                        }


                    }
                    catch (Exception exp)
                    {

                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetHeaderByID: " + exp.Message);
             
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetHeaderByID: " + exp.Message);

                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
        }


        /// <summary>
        /// Author: Munirul Islam
        /// Date: 31.03.2014
        /// Get Placid Header Information
        /// </summary>
        /// <param name="cPlacid"></param>
        /// <returns></returns>
        public CResult GetPlacidCSVHeader(CAllLookupList oAllLookupList)
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
                        Hashtable htColumnsHeaderList = new Hashtable();
                        //CAllLookupList oAllLookupList = new CAllLookupList();

                        CAllLookup oAllLookup = new CAllLookup();
                        string sql = "SELECT ID,TYPE_NAME FROM ALLLOOKUP WHERE PARENT_ID IN(SELECT ID from Alllookup where TYPE_KEY='" + oAllLookupList.TableKey + "') ORDER BY ID asc";
                        oDataSet = oDatabase.ExecuteSQLDataSet(oConnection, sql);
                        if (oDataSet.Tables != null)
                        {
                            DataTable dt = new DataTable();
                            dt = oDataSet.Tables[0];
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                //oAllLookup = new CAllLookup();
                                //oAllLookup.TYPE_NAME = Convert.ToString(dt.Rows[i]["TYPE_NAME"]);
                                //oAllLookupList.AllLookupList.Add(oAllLookup);
                                htColumnsHeaderList.Add(i, Convert.ToString(dt.Rows[i]["TYPE_NAME"]));
                                //htColumnsHeaderList.Add(Convert.ToString(dt.Rows[i]["TYPE_NAME"]), Convert.ToString(dt.Rows[i]["TYPE_NAME"]));
                            }

                            //oResult.Return = oAllLookupList;
                            oResult.Return = htColumnsHeaderList;

                        }


                    }
                    catch (Exception exp)
                    {

                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetPlacidCSVHeader: " + exp.Message);
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

                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetPlacidCSVHeader: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
        }



        public CResult GetPlacidListByDateold(CPlacid cPlacid)
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
                        string sql = "SELECT * from placidinfo";
                        oDataSet = oDatabase.ExecuteSQLDataSet(oConnection, sql);
                        if (oDataSet.Tables != null)
                        {
                            // oResult.Return = BuildPlacidListEntity(oDataSet.Tables[0]);
                            //oResult.Return = oDataSet;
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetPlacidListByDateold: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
        }


        public CResult updateBankBrDistInf(CPlacidList oPlacidList)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            string success = "";
            string sMessage = "";
            #region Proc
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {

                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_PLACID.updateBankBrDistInf");
                    try
                    {

                        #region Add Placid Information
                        //oDatabase.BeginTransaction(oConnection);  
                        foreach (CPlacid oPlacid in oPlacidList.PlacidDataList)
                        {
                            #region AddInfo

                            oDatabase.AddInOutParameter(oDbCommand, "p_placid_id", DbType.Int32, 50, oPlacid.CN);// change data type from Int16 to Int32 dated on 04.02.2015 by munirul islam

                            oDatabase.AddInOutParameter(oDbCommand, "p_bank_cn", DbType.String, 50, oPlacid.BankInfo.CN);
                            oDatabase.AddInOutParameter(oDbCommand, "p_branch_cn", DbType.String, 150, oPlacid.BranchInfo.CN);
                            oDatabase.AddInOutParameter(oDbCommand, "p_dist_cn", DbType.String, 50, oPlacid.DistrictInfo.CN);
                            oDatabase.AddInOutParameter(oDbCommand, "p_acc_no", DbType.String, 50, oPlacid.AccReceiver);
                            oDatabase.AddInOutParameter(oDbCommand, "p_receiver_name", DbType.String, 50, oPlacid.NameReceiver);
                            oDatabase.AddInOutParameter(oDbCommand, "p_ChequeNO", DbType.String, 50, oPlacid.CheckNo); 

                            oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                            oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);

                            int i = oDatabase.ExecuteNonQuery(oDbCommand);
                            success = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_success")]).Value.ToString();
                            sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();

                            oDbCommand.Parameters.Clear();
                            #endregion AddInfo
                        }
                        oResult.Message = sMessage;
                        if (success == "1")
                            oResult.Result = true;

                        #endregion Add Cash Payment Information


                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/updateBankBrDistInf: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/updateBankBrDistInf: " + exp.Message);
              
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
            #endregion

            //#region Query

            //try
            //{
            //    string sQuery = "";
            //    using (IDbConnection oConnection = oDatabase.CreateConnection())
            //    {
            //        try
            //        {
            //            foreach (CPlacid oPlacid in oPlacidList.PlacidDataList)
            //            {
            //                sQuery = "";
            //            }

            //        }
            //        catch (Exception exp)
            //        {
            //            oResult.Exception = exp;
            //            oResult.Message = exp.Message;
            //            oResult.Result = false;
            //            oResult.Return = null;
            //        }
            //        finally
            //        {

            //            oDataSet.Clear();
            //            oDataSet.Dispose();

            //            oConnection.Close();
            //        }
            //    }
            //    return oResult;
            //}
            //catch (Exception exp)
            //{

            //    return oResult;
            //}
            //#endregion 
        }

        /// <summary>
        /// Author: Munirul Islam
        /// Date: 02.04.2014
        /// Description: Add Cash Payment Information
        /// </summary>
        /// <param name="oCashPaymentList"></param>
        /// <returns></returns>


        public CResult CRUDCashPayment(CCashPaymentList oCashPaymentList)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {

                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_PLACID.CRUD_CASHPAYMENT");
                    try
                    {
                        #region Add Cash Payment Information
                        //oDatabase.BeginTransaction(oConnection);  
                        foreach (CCashPayment oCashPayment in oCashPaymentList.CashPaymentList)
                        {
                            #region AddInfo

                            oDatabase.AddInOutParameter(oDbCommand, "P_CPID", DbType.String, 50, oCashPayment.CashPaymentId);
                            oDatabase.AddInOutParameter(oDbCommand, "P_PAYMENTDATE", DbType.String, 50, oCashPayment.PayDate);
                            oDatabase.AddInOutParameter(oDbCommand, "P_BENIFICIARYNAME", DbType.String, 150, oCashPayment.BeneficiaryName);
                            oDatabase.AddInOutParameter(oDbCommand, "P_PAYOUTLOCATION", DbType.String, 50, oCashPayment.PayoutLocation);
                            oDatabase.AddInOutParameter(oDbCommand, "P_AMOUNT", DbType.String, 50, oCashPayment.Amount);
                            oDatabase.AddInOutParameter(oDbCommand, "P_FXRATE", DbType.String, 50, oCashPayment.FxRate);
                            oDatabase.AddInOutParameter(oDbCommand, "P_GAIN", DbType.String, 50, oCashPayment.Gain);
                            oDatabase.AddInOutParameter(oDbCommand, "P_LIQ", DbType.String, 50, oCashPayment.LIQ_USD);
                            oDatabase.AddInOutParameter(oDbCommand, "P_USERS", DbType.String, 50, oCashPayment.User);
                            oDatabase.AddInOutParameter(oDbCommand, "P_COMPANYID", DbType.String, 50, oCashPayment.CompanyID);
                            oDatabase.AddInOutParameter(oDbCommand, "P_TRACKING_NUMBER", DbType.String, 50, oCashPayment.TrackingNumber);
                            oDatabase.AddInOutParameter(oDbCommand, "P_REF_NUMBER", DbType.String, 50, oCashPayment.RefNum);
                            oDatabase.AddInOutParameter(oDbCommand, "P_SUCCESS", DbType.String, 50, DBNull.Value);
                            oDatabase.AddInOutParameter(oDbCommand, "P_RETURN_MSG", DbType.String, 500, DBNull.Value);
                            oDatabase.AddInOutParameter(oDbCommand, "P_OPERATION_TYPE", DbType.String, 50, oCashPayment.OperationType);
                            
                            int i = oDatabase.ExecuteNonQuery(oDbCommand);
                            string sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_SUCCESS")]).Value.ToString();
                            string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_RETURN_MSG")]).Value.ToString();
                            oDbCommand.Parameters.Clear();

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

                            #endregion AddInfo
                        }
                        #endregion Add Cash Payment Information


                    }
                    catch (Exception exp)
                    {

                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/CRUDCashPayment: " + exp.Message);
              
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/CRUDCashPayment: " + exp.Message);
              
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
        }

        public CResult GetInfoByPaymentMode(CPlacid oPlacid)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_PLACID.get_placid_info_by_paymode");

                    try
                    {

                        oDatabase.AddInOutParameter(oDbCommand, "P_COMPANYID", DbType.Int32, 50, oPlacid.CompanyInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SET_PAYMENT_MODE", DbType.String, 50, oPlacid.PaymentMODE.CN);
                        oDatabase.AddOutParameter(oDbCommand, "rc_summary", "RefCursor", 100);
                        oDatabase.AddOutParameter(oDbCommand, "rc_placidinfo", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "P_FROM_DATE", DbType.String, 50, oPlacid.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_TO_DATE", DbType.String, 50, oPlacid.ToDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_fileId", DbType.String, 50, oPlacid.FileName);// file id
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);

                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);
                        if (oDataSet.Tables != null)
                        {
                            if (oDataSet.Tables.Count > 1)
                            {
                                oDataSet.Tables[0].TableName = "TransInfo";
                                oDataSet.Tables[1].TableName = "ItemDetails";

                            }

                            oResult.Return = oDataSet;

                        }

                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetInfoByPaymentMode: " + exp.Message);
              
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetInfoByPaymentMode: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
            }
            return oResult;
        }


        /// <summary>
        /// Author: Md. Ali Ahsan
        /// Date: 09-04-2014
        /// Description: This Method use to Generate Bangladesh Bank Report
        /// </summary>
        /// <param name="oPlacid"></param>
        /// <returns></returns>

        public CResult GetBBankReport(CPlacid oPlacid)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_REPORT.GET_BBANK_REPORT_DATA");

                    try
                    {

                        oDatabase.AddInOutParameter(oDbCommand, "P_FROM_DATE", DbType.String, 50, oPlacid.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_TO_DATE", DbType.String, 50, oPlacid.ToDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_COMPANYID", DbType.Int32, 50, oPlacid.CompanyInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_fileId", DbType.String, 50, oPlacid.FileName);// file id
                        oDatabase.AddOutParameter(oDbCommand, "rc_datalist", "RefCursor", 100);

                        //oDatabase.AddOutParameter(oDbCommand, "rc_summary", "RefCursor", 100);
                        //oDatabase.AddOutParameter(oDbCommand, "rc_placidinfo", "RefCursor", 100);                        
                        
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);

                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);
                        if (oDataSet.Tables != null)
                        {
                            if (oDataSet.Tables.Count > 0)
                            {
                                oDataSet.Tables[0].TableName = "INPUT_TEMPLATE";
                            }

                            oResult.Return = oDataSet;

                        }

                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetBBankReport: " + exp.Message);

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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetBBankReport: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
            }
            return oResult;
        }

        public CResult GetMatchDataList(CPlacid oPlacid)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_PLACID.get_matchdata_by_date");

                    try
                    {

                        oDatabase.AddInOutParameter(oDbCommand, "p_COMPANYID", DbType.Int32, 50, oPlacid.CompanyInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_fromDate", DbType.String, 50, oPlacid.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_toDate", DbType.String, 50, oPlacid.ToDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_DataType", DbType.String, 50, oPlacid.DataRetrivalType);
                        oDatabase.AddInOutParameter(oDbCommand, "p_district_name", DbType.String, 50, oPlacid.DistrictInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_bank_name", DbType.String, 50, oPlacid.BankInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_paymentMode", DbType.String, 50, oPlacid.PaymentMODE.CN);                        
                        oDatabase.AddOutParameter(oDbCommand, "rc_placid", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);


                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = BuildPlacidListEntity(oDataSet.Tables[0]);
                            //oResult.Return =  oDataSet.Tables[0];
                        }

                    }
                    catch (Exception exp)
                    {

                    }
                    finally
                    {
                        oDataSet.Dispose();
                        oDbCommand.Dispose();
                        oConnection.Close();
                    }
                }
                return oResult;
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetMatchDataList: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
            }
            return oResult;
        }

        public CResult GetImportedInfoWthNoficationByDate(CPlacid oPlacid)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_PLACID.getAll_by_date");
                    try
                    {

                        oDatabase.AddInOutParameter(oDbCommand, "p_company_id", DbType.String, 50, oPlacid.CompanyInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_bank_name", DbType.String, 50, oPlacid.ReceiverBankReceiver);
                        oDatabase.AddInOutParameter(oDbCommand, "p_district_name", DbType.String, 50, oPlacid.AddressReceiver);
                        oDatabase.AddInOutParameter(oDbCommand, "p_tracking_no", DbType.String, 50, oPlacid.TrackingNumber);
                        oDatabase.AddInOutParameter(oDbCommand, "p_fromDate", DbType.String, 50, oPlacid.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_toDate", DbType.String, 50, oPlacid.ToDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_paymentMode", DbType.String, 50, oPlacid.PaymentMODE.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_fileid", DbType.String, 50, oPlacid.FileName);// FILE ID
                        oDatabase.AddOutParameter(oDbCommand, "rc_placid", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);


                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = BuildPlacidListEntity(oDataSet.Tables[0]);
                            //oResult.Return =  oDataSet.Tables[0];
                        }

                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetImportedInfoWthNoficationByDate: " + exp.Message);
                        oResult.Exception = exp;
                        oResult.Message = exp.Message;
                        oResult.Result = false;
                        oResult.Return = null;
                    }
                    finally
                    {
                        oDataSet.Dispose();
                        oDbCommand.Dispose();
                        oConnection.Close();
                    }
                }
                return oResult;
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetImportedInfoWthNoficationByDate: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
            }
            return oResult;
        }

        public CResult GetCashPaymentReport(CPlacid oPlacid)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_PLACID.get_cashpayment");

                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "p_company_id", DbType.String, 50, oPlacid.CompanyInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_fromDate", DbType.String, 50, oPlacid.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_toDate", DbType.String, 50, oPlacid.ToDate);
                        oDatabase.AddOutParameter(oDbCommand, "rc_cashinfo", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);


                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = BuildCashListEntity(oDataSet.Tables[0]);
                            //oResult.Return =  oDataSet.Tables[0];
                        }

                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetCashPaymentReport: " + exp.Message);
                        oResult.Exception = exp;
                        oResult.Message = exp.Message;
                        oResult.Result = false;
                        oResult.Return = null;
                    }
                    finally
                    {
                        oDataSet.Clear();
                        oDataSet.Dispose();
                        oDbCommand.Dispose();
                        oConnection.Close();
                    }
                }
                return oResult;
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetCashPaymentReport: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }

        }

        private CCashPaymentList BuildCashListEntity(DataTable dt)
        {
            CCashPayment oCashpayment = new CCashPayment();
            CCashPaymentList cCashpaymentList = new CCashPaymentList();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                oCashpayment = new CCashPayment();
                oCashpayment.CN = Convert.ToString(dt.Rows[i]["CPID"]);
                oCashpayment.PayDate = Convert.ToString(dt.Rows[i]["PAYMENTDATE"]);
                oCashpayment.BeneficiaryName = Convert.ToString(dt.Rows[i]["BENIFICIARYNAME"]);
                oCashpayment.PayoutLocation = Convert.ToString(dt.Rows[i]["PAYOUTLOCATION"]);
                oCashpayment.Amount = Convert.ToString(dt.Rows[i]["AMOUNT"]);
                oCashpayment.FxRate = Convert.ToString(dt.Rows[i]["FXRATE"]);
                oCashpayment.Gain = Convert.ToString(dt.Rows[i]["GAIN"]);
                oCashpayment.LIQ_USD = Convert.ToString(dt.Rows[i]["LIQ"]);
                oCashpayment.User = Convert.ToString(dt.Rows[i]["USERS"]);
                oCashpayment.CompanyID = Convert.ToString(dt.Rows[i]["COMPANYID"]);
                oCashpayment.CreateDate = Convert.ToDateTime(dt.Rows[i]["CREATEDATE"]);
                oCashpayment.TrackingNumber = Convert.ToString(dt.Rows[i]["TRACKING_NUMBER"]);
                oCashpayment.RefNum = Convert.ToString(dt.Rows[i]["REF_NUMBER"]);
                oCashpayment.User = Convert.ToString(dt.Rows[i]["CREATE_BY"]);
                cCashpaymentList.CashPaymentList.Add(oCashpayment);
            }
            return cCashpaymentList;
        }


        /// <summary>
        /// Md. Aminul Islam
        /// Date: 
        /// </summary> Description: Get online all data 
        /// <param name="oPlacid"></param>
        /// <returns></returns>
        public CResult GetOnlinePayment(CPlacid oPlacid)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_PLACID.get_online_payment");

                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "p_companyName", DbType.String, 100, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_paymentMode", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_bankName", DbType.String, 20, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_BranchName", DbType.String, 20, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_fromDate", DbType.String, 50, oPlacid.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_toDate", DbType.String, 50, oPlacid.ToDate);
                        oDatabase.AddOutParameter(oDbCommand, "rc_placid", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);

                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);
                        if (oDataSet.Tables != null)
                        {
                            //oResult.Return = BuildOnlinePaymentEntity(oDataSet.Tables[0]);
                            //oResult.Return =  oDataSet;
                        }

                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetOnlinePayment: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetOnlinePayment: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
            }
            return oResult;
        }


        /// <summary>
        /// Md. Aminul Islam
        /// Date: 
        /// </summary> Description: Get imported all data 
        /// <param name="oPlacid"></param>
        /// <returns></returns> 
        public CResult GetImportdata(CPlacid oPlacid)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_PLACID.get_importeddata_by_date");

                    try
                    {

                        oDatabase.AddInOutParameter(oDbCommand, "p_fromdate", DbType.String, 50, oPlacid.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_todate", DbType.String, 50, oPlacid.ToDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_district_id", DbType.Int16, 50, oPlacid.DistrictInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_bank_id", DbType.Int16, 50, oPlacid.BankInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_company_id", DbType.Int16, 50, oPlacid.CompanyInfo.CN);
                        oDatabase.AddOutParameter(oDbCommand, "rc_datalist", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);

                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = BuildPlacidListEntity(oDataSet.Tables[0]);
                            // oResult.Return =  oDataSet.Tables[0];
                        }

                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetImportdata: " + exp.Message);
                        oResult.Exception = exp;
                        oResult.Message = exp.Message;
                        oResult.Result = false;
                        oResult.Return = null;
                    }
                    finally
                    {
                        oDataSet.Dispose();
                        oDbCommand.Dispose();
                        oConnection.Close();
                    }
                }
                return oResult;
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetImportdata: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
            }
            return oResult;
        }

        /// <summary>
        /// Author: Md. Ali Ahsan
        /// Description: This Method used to get Checker List 
        /// Date:11 May, 2014
        /// Update By: Munir
        /// Date:12 May, 2014
        /// </summary>
        /// <param name="oPlacid"></param>
        /// <returns></returns>

        public CResult CheckerSearchByCompany(CPlacid oPlacid)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_SEARCH.SEARCH_DATA_FOR_CHECKER");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_COMPANYID", DbType.String, 100, oPlacid.CompanyInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ROUTING_NO", DbType.String, 100, oPlacid.RoutingNumber);
                        oDatabase.AddInOutParameter(oDbCommand, "P_DISTRICTID", DbType.String, 100, oPlacid.DistrictInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BANK_ID", DbType.String, 10, oPlacid.BankInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_FromDate", DbType.String, 100, oPlacid.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ToDate", DbType.String, 100, oPlacid.ToDate);
                        oDatabase.AddOutParameter(oDbCommand, "rc_RAWDATA", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);


                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();


                        oResult.Message = sMessage;

                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = BuildCommonEntity(oDataSet.Tables[0]);
                            //oResult.Return = oDataSet.Tables[0];
                            //oResult.Return = oDataSet;
                        }
                        else
                            oResult.Return = null;
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/CheckerSearchByCompany: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/CheckerSearchByCompany: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
        }



        public CResult SearchImportdata(CPlacid oPlacid)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_PLACID.get_importeddata_by_date");

                    try
                    {

                        oDatabase.AddInOutParameter(oDbCommand, "p_fromdate", DbType.String, 50, oPlacid.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_todate", DbType.String, 50, oPlacid.ToDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_bank_id", DbType.String, 50, oPlacid.BankInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_district_id", DbType.String, 50, oPlacid.DistrictInfo.CN);

                        oDatabase.AddInOutParameter(oDbCommand, "p_company_id", DbType.String, 50, oPlacid.CompanyInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_tracking_no", DbType.String, 50, oPlacid.TrackingNumber);
                        oDatabase.AddOutParameter(oDbCommand, "rc_datalist", "RefCursor", 100);

                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);


                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        if (oDataSet.Tables != null)
                        {
                            oResult.Return = BuildPlacidListEntity(oDataSet.Tables[0]);

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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/SearchImportdata: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
            return oResult;
        }



        public CResult AddRawDataNew(RawDataList oRawDataList)
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
            string sQuery = "";
            string sQueryString = "insert into raw_data(";
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oConnection.CreateCommand();

                    try
                    {

                        int pp = 0;
                        foreach (CRawData oRawData in oRawDataList.RawdataList)
                        {

                            if (pp == 0)
                            {
                                sQuery = "select 'COL'||colno as colname ,'oRawData.Col'||ROWNUM colobj from map_table where companyid=" + oRawData.CompanyId + " order by ORDERBY";
                                oDbCommand.CommandText = sQuery;
                                oDbCommand.CommandType = CommandType.Text;
                                
                                IDataReader oReader = oDbCommand.ExecuteReader();
                                int iCount = oReader.Depth;
                                int i = 0;
                                while (oReader.Read())
                                {

                                    sQueryString = sQueryString + oReader["colname"].ToString();
                                    i++;
                                    if (i < iCount)
                                        sQueryString = sQueryString + ",";
                                }
                            }

                            oDbCommand.Parameters.Clear();
                            sPaymentMode = oRawData.OperationType;
                            sToDate = oRawData.ToDate;
                            sFromDate = oRawData.FromDate;
                            sCompanyId = oRawData.CompanyId;
                        }

                        if (sSuccess == "5")
                        {
                            CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/AddRawData: " + sMessage);
                            oResult.Message = "Your are trying to Insert Duplicate Record.!!!";
                            oResult.Result = false;

                        }
                        else if (sSuccess == "1")
                        {
                            oResult.Message = sMessage;
                            oResult.Result = true;
                        }
                        else
                        {
                            CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/AddRawData: " + sMessage);
                            oResult.Result = false;
                            oResult.Message = "DB Exception Occured!!!";
                        }
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/AddRawData: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/AddRawData: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
            return oResult;

        }


        public CResult AddRawData(RawDataList oRawDataList)
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
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_RAWDATA.crud_rawData");

                    try
                    {
                        foreach (CRawData cRawData in oRawDataList.RawdataList)
                        {
                            oDatabase.AddInOutParameter(oDbCommand, "p_RID", DbType.String, 50, cRawData.CN);
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL1", DbType.String, 200, cRawData.Col1.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL2", DbType.String, 200, cRawData.Col2.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL3", DbType.String, 200, cRawData.Col3.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL4", DbType.String, 200, cRawData.Col4.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL5", DbType.String, 200, cRawData.Col5.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL6", DbType.String, 200, cRawData.Col6.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL7", DbType.String, 200, cRawData.Col7.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL8", DbType.String, 200, cRawData.Col8.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL9", DbType.String, 200, cRawData.Col9.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL10", DbType.String, 200, cRawData.Col10.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL11", DbType.String, 200, cRawData.Col11.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL12", DbType.String, 200, cRawData.Col12.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL13", DbType.String, 200, cRawData.Col13.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL14", DbType.String, 200, cRawData.Col14.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL15", DbType.String, 200, cRawData.Col15.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL16", DbType.String, 200, cRawData.Col16.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL17", DbType.String, 200, cRawData.Col17.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL18", DbType.String, 200, cRawData.Col18.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL19", DbType.String, 200, cRawData.Col19.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL20", DbType.String, 200, cRawData.Col20.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL21", DbType.String, 200, cRawData.Col21.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL22", DbType.String, 200, cRawData.Col22.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL23", DbType.String, 200, cRawData.Col23.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL24", DbType.String, 200, cRawData.Col24.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL25", DbType.String, 200, cRawData.Col25.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL26", DbType.String, 200, cRawData.Col26.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL27", DbType.String, 200, cRawData.Col27.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL28", DbType.String, 200, cRawData.Col28.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL29", DbType.String, 200, cRawData.Col29.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL30", DbType.String, 200, cRawData.Col30.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL31", DbType.String, 200, cRawData.Col31.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL32", DbType.String, 200, cRawData.Col32.Trim());
                            //oDatabase.AddInOutParameter(oDbCommand, "p_COL33", DbType.String, 200, cRawData.Col33);
                            //oDatabase.AddInOutParameter(oDbCommand, "p_COL34", DbType.String, 200, cRawData.Col34);
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL35", DbType.String, 200, cRawData.Col35.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL36", DbType.String, 200, cRawData.Col36.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL37", DbType.String, 200, cRawData.Col37.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL38", DbType.String, 200, cRawData.Col38.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL39", DbType.String, 200, cRawData.Col39.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL40", DbType.String, 200, cRawData.Col40.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL41", DbType.String, 200, cRawData.Col41.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL42", DbType.String, 200, cRawData.Col42.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL43", DbType.String, 200, cRawData.Col43.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL44", DbType.String, 200, cRawData.Col44.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL45", DbType.String, 200, cRawData.Col45.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL46", DbType.String, 200, cRawData.Col46.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL47", DbType.String, 200, cRawData.Col47.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL48", DbType.String, 200, cRawData.Col48.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL49", DbType.String, 200, cRawData.Col49.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL50", DbType.String, 200, cRawData.Col50);
                            //oDatabase.AddInOutParameter(oDbCommand, "p_COL51", DbType.String, 200, cRawData.ToDate);
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL52", DbType.String, 200, cRawData.Col52.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL53", DbType.String, 200, cRawData.Col53.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_COL54", DbType.String, 200, cRawData.Col54.Trim());
                            oDatabase.AddInOutParameter(oDbCommand, "p_paymentMode", DbType.String, 200, cRawData.PaymentMode);
                            oDatabase.AddInOutParameter(oDbCommand, "p_CREATE_DATE", DbType.String, 50, DBNull.Value);
                            oDatabase.AddInOutParameter(oDbCommand, "p_CREATE_BY", DbType.String, 50, cRawData.CreateBy);
                            oDatabase.AddInOutParameter(oDbCommand, "p_UPDATE_DATE", DbType.String, 50, DBNull.Value);
                            oDatabase.AddInOutParameter(oDbCommand, "p_UPDATE_BY", DbType.String, 50, cRawData.UpdateBy);
                            oDatabase.AddInOutParameter(oDbCommand, "p_COMPANYID", DbType.String, 50, cRawData.CompanyId);
                            oDatabase.AddInOutParameter(oDbCommand, "p_OPERATIONTYPE", DbType.String, 50, cRawData.OperationType);
                            oDatabase.AddInOutParameter(oDbCommand, "P_UPLOAD_DATE", DbType.String, 50, cRawData.Uploaddate);
                            oDatabase.AddInOutParameter(oDbCommand, "P_RETURN_MESSAGE", DbType.String, 2000, DBNull.Value);
                            oDatabase.AddInOutParameter(oDbCommand, "P_SUCCESS", DbType.String, 50, DBNull.Value);
                            int i = oDatabase.ExecuteNonQuery(oDbCommand);
                            //string st = oDbCommand.Parameters["p_success"]ToString();
                            sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_success")]).Value.ToString();
                            sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_RETURN_MESSAGE")]).Value.ToString();
                           
                            
                            oDbCommand.Parameters.Clear();
                            sPaymentMode=cRawData.OperationType;
                            sToDate = cRawData.ToDate;
                            sFromDate = cRawData.FromDate;
                            sCompanyId = cRawData.CompanyId;
                        }

                        if (sSuccess == "5")
                        {
                            CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/AddRawData: " + sMessage);
                            oResult.Message = "Your are trying to Insert Duplicate Record.!!!";
                            oResult.Result = false;
                           
                        }
                        else if (sSuccess == "1")
                        {
                            oResult.Message = sMessage;
                            oResult.Result = true;
                        }
                        else
                        {
                            CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/AddRawData: " + sMessage);
                            oResult.Result = false;
                            oResult.Message = "DB Exception Occured!!!";
                        }
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/AddRawData: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/AddRawData: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
            return oResult;

        }

        public CResult ProcessData(CPlacid oPlacid)
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
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_PROCESS_DATA.process_data");

                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_from_date", DbType.String, 50, oPlacid.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_to_date", DbType.String, 50, oPlacid.ToDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_companyid", DbType.Int32, 50, oPlacid.CompanyInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_message", DbType.String, 500, DBNull.Value);

                        int i = oDatabase.ExecuteNonQuery(oDbCommand);

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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/ProcessData: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
            return oResult;
        }

        

        public CResult GetCompanyFileName(CPlacid oPlacid)
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
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_RAWDATA.GetCompanyFileName");

                    try
                    {
                     
                        oDatabase.AddInOutParameter(oDbCommand, "p_companyid", DbType.Int32, 50, oPlacid.CompanyInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_fromDate", DbType.String, 50, oPlacid.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_toDate", DbType.String, 50, oPlacid.ToDate);
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



        public CResult updateSearchImportData(CPlacidList oPlacidList)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            string sSuccess = "";
            string sMessage = "";
            string rid = "";
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_RAWDATA.updateSearchImportData");

                    try
                    {
                        foreach (CPlacid oPlacid in oPlacidList.PlacidDataList)
                        {

                            oDatabase.AddInOutParameter(oDbCommand, "p_rid", DbType.Int32, 50, oPlacid.CN);
                            oDatabase.AddInOutParameter(oDbCommand, "p_comments", DbType.String, 50, oPlacid.Comments);

                            oDatabase.AddInOutParameter(oDbCommand, "P_RETURN_MESSAGE", DbType.String, 500, DBNull.Value);
                            oDatabase.AddInOutParameter(oDbCommand, "P_SUCCESS", DbType.String, 50, DBNull.Value);
                            int i = oDatabase.ExecuteNonQuery(oDbCommand);

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
                            oDbCommand.Parameters.Clear();
                        }

                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/updateSearchImportData: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/updateSearchImportData: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
            return oResult;
        }


        public CResult AddFileName(CPlacid oPlacid)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            string sSuccess = "";
            string sMessage = "";
            string rid = "";
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_RAWDATA.addFileName");

                    try
                    {
                        oPlacid.CN = "1";
                        oDatabase.AddInOutParameter(oDbCommand, "p_RID", DbType.Int32, 50, oPlacid.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_filename", DbType.String, 50, oPlacid.FileName);
                        oDatabase.AddInOutParameter(oDbCommand, "p_uploadby", DbType.Int32, 50, oPlacid.User.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_companyid", DbType.Int32, 50, oPlacid.CompanyInfo.CN);
                        
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


        public CResult DataDeleteByFileWise(CPlacid oPlacid)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;



            string sSuccess = String.Empty;
            string sMessage = String.Empty;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_RAWDATA.DelDataByFileId");

                    try
                    {

                        oDatabase.AddInOutParameter(oDbCommand, "p_fileId", DbType.Int32, 50, oPlacid.FileName);
                        oDatabase.AddInOutParameter(oDbCommand, "P_RETURN_MESSAGE", DbType.String, 500, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_SUCCESS", DbType.String, 50, DBNull.Value);
                        int i = oDatabase.ExecuteNonQuery(oDbCommand);

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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/DataDeleteByFileWise: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
            return oResult;
        }


        public CResult GetCashReportData(CPlacid oPlacid)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_REPORT.GET_CASH_REPORT_DATA");

                    try
                    {

                        oDatabase.AddInOutParameter(oDbCommand, "p_from_date", DbType.String, 50, oPlacid.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_to_date", DbType.String, 50, oPlacid.ToDate);
                        oDatabase.AddInOutParameter(oDbCommand, "p_bank_id", DbType.Int32, 50, oPlacid.BankInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_branch_id", DbType.Int32, 50, oPlacid.BranchInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_company_id", DbType.Int32, 50, oPlacid.CompanyInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_file_id", DbType.Int32, 50, oPlacid.FileName);
                        oDatabase.AddOutParameter(oDbCommand, "rc_datalist", "RefCursor", 100);

                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);


                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        if (oDataSet.Tables != null)
                        {
                            oResult.Return =  oDataSet;

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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetCashReportData: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
            return oResult;
        }

        public CResult AddCheckNo(CChequeList oChequeList)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;


            string sSuccess = "";
            string sMessage ="";


            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_PAYOUT.CRUDCHEQUEINFO");

                    try
                    {
                        foreach (CCheque oCheque in oChequeList.ChequeList)
                        {
                            oDatabase.AddInOutParameter(oDbCommand, "P_CHKID", DbType.String, 50, oCheque.CN);
                            oDatabase.AddInOutParameter(oDbCommand, "P_CHEQUENO", DbType.String, 50, oCheque.CHEQUENO);
                            oDatabase.AddInOutParameter(oDbCommand, "P_ISACTIVE", DbType.String, 50, oCheque.ISACTIVE);
                            oDatabase.AddInOutParameter(oDbCommand, "P_ENTRYDATE", DbType.String, 50, DBNull.Value);
                            oDatabase.AddInOutParameter(oDbCommand, "P_CREATEBY", DbType.String, 50, oCheque.User.CN);
                            oDatabase.AddInOutParameter(oDbCommand, "P_COMPANYID", DbType.String, 50, oCheque.COMPANYID);
                            oDatabase.AddInOutParameter(oDbCommand, "P_BENEFICIARY_NAME", DbType.String, 50, oCheque.BENEFICIARY_NAME);
                            oDatabase.AddInOutParameter(oDbCommand, "P_PINNO", DbType.String, 50, oCheque.PINNO);
                            oDatabase.AddInOutParameter(oDbCommand, "P_AMOUNT", DbType.Double, 50, oCheque.AMOUNT == "" ? "0" : oCheque.AMOUNT);
                            oDatabase.AddInOutParameter(oDbCommand, "P_REMARKS", DbType.String, 50, oCheque.REMARKS);
                            oDatabase.AddInOutParameter(oDbCommand, "P_NIDNO", DbType.String, 50, oCheque.NIDNO);
                            oDatabase.AddInOutParameter(oDbCommand, "P_NIDTYPE ", DbType.String, 50, oCheque.Id_Type);
                            oDatabase.AddInOutParameter(oDbCommand, "P_NIDISSUEDATE", DbType.String, 50, oCheque.NIDISSUEDATE);
                            oDatabase.AddInOutParameter(oDbCommand, "P_NIDEXPIRYDATE", DbType.String, 50, oCheque.NIDEXPIRYDATE);
                            oDatabase.AddInOutParameter(oDbCommand, "P_USEDDATE", DbType.String, 50, oCheque.dUSEDDATE);
                            oDatabase.AddInOutParameter(oDbCommand, "P_USEDBY", DbType.String, 50, oCheque.User.CN);
                            oDatabase.AddInOutParameter(oDbCommand, "P_oPerationType", DbType.String, 50, oCheque.OperationType);
                       
                            oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                            oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);

                     
                           int i = oDatabase.ExecuteNonQuery(oDbCommand);
                          

                            sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_success")]).Value.ToString();
                            sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();

                           oDbCommand.Parameters.Clear();
                        }

                      


                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/AddCheckNo: " + exp.Message);
                    }
                    finally
                    {
                        oResult.Message = sMessage;
                        oResult.Status = sSuccess;
                        if (sSuccess == "1")
                            oResult.Result = true;
                        else
                            oResult.Result = false;

                        oDbCommand.Dispose();
                        oConnection.Close();
                    }
                }
                return oResult;
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/AddCheckNo: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
            return oResult;
        }     

        [Author("Md. Ali Ahsan", "13-05-2015", "Add or Update Balance Informations")]
        public CResult AddBalanceInfo(CBalance oBalance)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_PAYOUT.CRUDCASHBALANCE");
                    try
                    {

                        
                        oDatabase.AddInOutParameter(oDbCommand, "P_CBID", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_COMPANYID", DbType.Int32, 50, oBalance.COMPANYID.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_AMOUNT", DbType.Double, 50, oBalance.AMOUNT == "" ? "0" : oBalance.AMOUNT);
                        oDatabase.AddInOutParameter(oDbCommand, "P_DrAMOUNT", DbType.Double, 50, oBalance.DrAmount == "" ? "0" : oBalance.DrAmount);
                        oDatabase.AddInOutParameter(oDbCommand, "P_USERID", DbType.String, 80, oBalance.USERID.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_UPDATEDATE", DbType.String, 80, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "P_REMARKS", DbType.String, 80, oBalance.REMARKS);
                        oDatabase.AddInOutParameter(oDbCommand, "p_OPerationType", DbType.String, 50, oBalance.OperationType);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);

                        int i = oDatabase.ExecuteNonQuery(oDbCommand);

                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                        string success = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_success")]).Value.ToString();

                        
                        oResult.Result = true;
                        oResult.Message = sMessage;

                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/AddBalanceInfo" + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/AddBalanceInfo" + exp.Message);
                return oResult;
            }

        }


        [Author("Md. Ali Ahsan", "11-12-2014", "To Get Vendor Details Informations")]
        public CResult GetBalanceListDetails(CBalance oBalance)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            CBalanceList oBalanceList = new CBalanceList();
            DataSet oDataSet = null;
            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_PAYOUT.GetBalanceInfo");
                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_COMPANYID", DbType.String, 100, oBalance.COMPANYID.CN);
                        oDatabase.AddOutParameter(oDbCommand, "rc_balanceInfo", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_oPerationType", DbType.String, 50, oBalance.OperationType);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);


                        IDataReader oReader = oDatabase.ExecuteDataReader(oDbCommand);

                        while (oReader.Read())
                        {
                            oBalance = new CBalance();
                            oBalance.COMPANYID.CN = oReader["companyName"].ToString();
                            oBalance.AMOUNT = oReader["amount"].ToString();
                            oBalanceList.BalanceDataList.Add(oBalance);
                        }
                        string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();

                        oResult.Return = oBalanceList;
                        oResult.Result = true;
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetBalanceListDetails" + exp.Message);
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


        //[Author("Md. Ali Ahsan", "17-05-2015", "Add or Update Cheque Informations")]
        //public CResult AddChequeGenerateInfo(CCheque oCheque)
        //{
        //    CResult oResult = new CResult();
        //    Database oDatabase = new Database();
        //    DataSet oDataSet = null;

        //    try
        //    {
        //        using (IDbConnection oConnection = oDatabase.CreateConnection())
        //        {
        //            IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_PAYOUT.CRUDCHEQUEINFO");
        //            try
        //            {

        //                oDatabase.AddInOutParameter(oDbCommand, "P_CHKID", DbType.String, 50, DBNull.Value);
        //                oDatabase.AddInOutParameter(oDbCommand, "P_CHEQUENO", DbType.String, 50, oCheque.CHEQUENO);
        //                oDatabase.AddInOutParameter(oDbCommand, "P_ISACTIVE", DbType.Double, 50, oCheque.ISACTIVE);
        //                oDatabase.AddInOutParameter(oDbCommand, "P_ENTRYDATE", DbType.String, 80, DBNull.Value);
        //                oDatabase.AddInOutParameter(oDbCommand, "P_CREATEBY", DbType.String, 80, oCheque.User.CN);
        //                oDatabase.AddInOutParameter(oDbCommand, "P_COMPANYID", DbType.String, 80, oCheque.COMPANYID);
        //                oDatabase.AddInOutParameter(oDbCommand, "P_BENEFICIARY_NAME", DbType.String, 80, oCheque.BENEFICIARY_NAME);
        //                oDatabase.AddInOutParameter(oDbCommand, "P_PINNO", DbType.String, 80, oCheque.PINNO);
        //                oDatabase.AddInOutParameter(oDbCommand, "P_AMOUNT", DbType.Int32, 80, oCheque.AMOUNT);
        //                oDatabase.AddInOutParameter(oDbCommand, "P_REMARKS", DbType.String, 80, oCheque.REMARKS);
        //                oDatabase.AddInOutParameter(oDbCommand, "P_NIDNO", DbType.String, 80, oCheque.NIDNO);
        //                oDatabase.AddInOutParameter(oDbCommand, "P_NIDTYPE", DbType.String, 80, oCheque.Id_Type);
        //                oDatabase.AddInOutParameter(oDbCommand, "P_NIDISSUEDATE", DbType.String, 80, oCheque.NIDISSUEDATE);
        //                oDatabase.AddInOutParameter(oDbCommand, "P_NIDEXPIRYDATE", DbType.String, 80, oCheque.NIDEXPIRYDATE);
        //                oDatabase.AddInOutParameter(oDbCommand, "P_USEDDATE", DbType.String, 80, DBNull.Value);
        //                oDatabase.AddInOutParameter(oDbCommand, "P_USEDBY", DbType.String, 80, oCheque.USEDBY);
        //                oDatabase.AddInOutParameter(oDbCommand, "P_oPerationType", DbType.String, 50, oCheque.OperationType);
        //                oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
        //                oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);

        //                int i = oDatabase.ExecuteNonQuery(oDbCommand);

        //                string sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
        //                string success = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("P_success")]).Value.ToString();

        //                oResult.Message = sMessage;

        //            }
        //            catch (Exception exp)
        //            {
        //                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/AddChequeGenerateInfo" + exp.Message);
        //                oResult.Exception = exp;
        //                oResult.Message = exp.Message;
        //                oResult.Result = false;
        //                oResult.Return = null;
        //            }
        //            finally
        //            {
        //                oConnection.Close();
        //            }
        //        }
        //        return oResult;
        //    }
        //    catch (Exception exp)
        //    {
        //        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/AddBalanceInfo" + exp.Message);
        //        return oResult;
        //    }

        //}       


        public CResult GetChequeInfo(CCheque oCheque)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            string sMessage = "";
            CChequeList oChequeList = new CChequeList();  

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_PAYOUT.GetChequeList");

                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_CHKID", DbType.String, 50, oCheque.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_USERID", DbType.String, 50, oCheque.User.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_COMPANYID", DbType.String, 50, oCheque.COMPANYID);
                        oDatabase.AddInOutParameter(oDbCommand, "P_FromDate", DbType.String, 50, oCheque.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ToDate", DbType.String, 50, oCheque.ToDate);
                        oDatabase.AddOutParameter(oDbCommand, "rc_chequeList", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_oPerationType", DbType.String, 50, oCheque.OperationType);                       
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);

                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        if (oCheque.Id_Type == CConstants.DB_DATASET)
                        {
                            oResult.Return = oDataSet; // for  xls export
                        }
                        else
                        {
                            if (oDataSet.Tables != null)
                            {
                                DataTable dtChequeInfo = new DataTable();
                                dtChequeInfo = oDataSet.Tables[0];
                                for (int i = 0; i < dtChequeInfo.Rows.Count; i++)
                                {
                                    oCheque = new CCheque();
                                    oCheque.CN = dtChequeInfo.Rows[i]["CHKID"].ToString();
                                    oCheque.CHEQUENO = dtChequeInfo.Rows[i]["CHEQUENO"].ToString();
                                    oCheque.ISACTIVE = dtChequeInfo.Rows[i]["ISACTIVE"].ToString();
                                    oCheque.CREATEBY = dtChequeInfo.Rows[i]["CREATEBY"].ToString();
                                    oCheque.COMPANYID = dtChequeInfo.Rows[i]["COMPANYID"].ToString();
                                    oCheque.COMPANYNAME = dtChequeInfo.Rows[i]["companyname"].ToString();

                                    oCheque.BENEFICIARY_NAME = dtChequeInfo.Rows[i]["BENEFICIARY_NAME"].ToString();
                                    oCheque.PINNO = dtChequeInfo.Rows[i]["PINNO"].ToString();
                                    oCheque.AMOUNT = dtChequeInfo.Rows[i]["AMOUNT"].ToString();
                                    oCheque.REMARKS = dtChequeInfo.Rows[i]["REMARKS"].ToString();
                                    oCheque.NIDNO = dtChequeInfo.Rows[i]["NIDNO"].ToString();
                                    oCheque.NIDISSUEDATE = dtChequeInfo.Rows[i]["NIDISSUEDATE"].ToString();
                                    oCheque.dUSEDDATE = dtChequeInfo.Rows[i]["USEDDATE"].ToString();
                                    oCheque.USEDBY = dtChequeInfo.Rows[i]["USEDBY"].ToString();
                                    oCheque.User.USERNAME = dtChequeInfo.Rows[i]["USEDBYName"].ToString();
                                    oCheque.User.CN = dtChequeInfo.Rows[i]["USEDBY"].ToString();
                                    oCheque.Id_Type = dtChequeInfo.Rows[i]["NIDTYPE"].ToString();
                                    oCheque.Status = dtChequeInfo.Rows[i]["ApproveStatus"].ToString();
                                    
                                    oChequeList.ChequeList.Add(oCheque);



                                }

                               
                                oResult.Return = oChequeList;
                            }
                        }
                        sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                        oResult.Message = sMessage;
                        oResult.Result = true;
                      
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetChequeInfo: " + exp.Message);

                    }
                    finally
                    {
                        //oDataSet.Dispose();
                        if (oCheque.Id_Type == CConstants.DB_DATASET)
                        {
                            //oResult.Return = oDataSet;
                        }
                        else
                        {
                            oDbCommand.Dispose();
                        }
                        
                        
                        oConnection.Close();

                        
                    }
                }
                return oResult;
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetMatchDataListRpt: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                return oResult;
            }
            return oResult;
        }


        public CResult DeleteCheque(CChequeList oChequeList)
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
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_PAYOUT.CRUDCHEQUEINFO");

                    try
                    {
                        foreach (CCheque oCheque in oChequeList.ChequeList)
                        {
                            //oDatabase.AddInOutParameter(oDbCommand, "P_CHKID", DbType.String, 50, DBNull.Value);
                            //oDatabase.AddInOutParameter(oDbCommand, "P_CHEQUENO", DbType.String, 50, oCheque.CHEQUENO);
                            //oDatabase.AddInOutParameter(oDbCommand, "P_ISACTIVE", DbType.String, 50, oCheque.ISACTIVE);
                            //oDatabase.AddInOutParameter(oDbCommand, "P_ENTRYDATE", DbType.String, 50, DBNull.Value);
                            //oDatabase.AddInOutParameter(oDbCommand, "P_CREATEBY", DbType.String, 50, oCheque.User.CN);
                            //oDatabase.AddInOutParameter(oDbCommand, "P_COMPANYID", DbType.String, 50, oCheque.COMPANYID);
                            //oDatabase.AddInOutParameter(oDbCommand, "P_BENEFICIARY_NAME", DbType.String, 50, oCheque.BENEFICIARY_NAME);
                            //oDatabase.AddInOutParameter(oDbCommand, "P_PINNO", DbType.String, 50, oCheque.PINNO);
                            //oDatabase.AddInOutParameter(oDbCommand, "P_AMOUNT", DbType.Int32, 50, oCheque.AMOUNT);
                            //oDatabase.AddInOutParameter(oDbCommand, "P_REMARKS", DbType.String, 50, oCheque.REMARKS);
                            //oDatabase.AddInOutParameter(oDbCommand, "P_NIDNO", DbType.String, 50, oCheque.NIDNO);
                            //oDatabase.AddInOutParameter(oDbCommand, "P_NIDTYPE ", DbType.String, 50, oCheque.Id_Type);
                            //oDatabase.AddInOutParameter(oDbCommand, "P_NIDISSUEDATE", DbType.String, 50, oCheque.NIDISSUEDATE);
                            //oDatabase.AddInOutParameter(oDbCommand, "P_NIDEXPIRYDATE", DbType.String, 50, oCheque.NIDEXPIRYDATE);
                            //oDatabase.AddInOutParameter(oDbCommand, "P_USEDDATE", DbType.String, 50, oCheque.dUSEDDATE);
                            //oDatabase.AddInOutParameter(oDbCommand, "P_USEDBY", DbType.String, 50, oCheque.USEDBY);
                            //oDatabase.AddInOutParameter(oDbCommand, "P_oPerationType", DbType.String, 50, oCheque.OperationType);
                            //oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                            //oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);
                            //int i = oDatabase.ExecuteNonQuery(oDbCommand);
                            //sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_success")]).Value.ToString();
                            //sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();
                            //oDbCommand.Parameters.Clear();
                        }
                        
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/DeleteCheque: " + exp.Message);
                    }
                    finally
                    {
                        oResult.Message = sMessage;
                        oResult.Result = true;
                        oDbCommand.Dispose();
                        oConnection.Close();
                    }
                }
                return oResult;
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/DeleteCheque: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
            return oResult;
        }

        [Author("Munirul Islam", "01-09-2015", "Get Balance Information for approval")]
        public CResult GetBalanceInformation(CBalance oBalance)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            CBalanceList oBalanceList = new CBalanceList();
           

            string sSuccess = "";
            string sMessage = "";

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_PAYOUT.GetBalanceForApproval");

                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_CBID", DbType.String, 50, oBalance.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_USERID", DbType.String, 50, oBalance.USERID.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_oPerationType", DbType.String,50, oBalance.OperationType);
                        oDatabase.AddOutParameter(oDbCommand, "rc_balanceInfo", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);
                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);
                        
                        sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_success")]).Value.ToString();
                        sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();


                        #region Build Balance Object

                        if (oDataSet.Tables.Count > 0)
                        {
   
                            DataTable dtBalance=oDataSet.Tables[0];
                            for (int i = 0; i < dtBalance.Rows.Count; i++)
                            {
                                oBalance = new CBalance();

                                oBalance.CN = Convert.ToString(dtBalance.Rows[i]["CBID"]);
                                oBalance.AMOUNT = Convert.ToString(dtBalance.Rows[i]["AMOUNT"]);
                                oBalance.DrAmount = Convert.ToString(dtBalance.Rows[i]["dr"]);
                                oBalance.REMARKS = Convert.ToString(dtBalance.Rows[i]["REMARKS"]);
                                oBalance.COMPANYID.CN = Convert.ToString(dtBalance.Rows[i]["COMPANYID"]);
                                oBalance.COMPANYID.TYPE_NAME = Convert.ToString(dtBalance.Rows[i]["companyName"]);
                                oBalance.USERID.CN = Convert.ToString(dtBalance.Rows[i]["USERID"]);
                                oBalanceList.BalanceDataList.Add(oBalance);
                            }

                            oResult.Return = oBalanceList;
                        }
                        #endregion Build Balance Object



                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetBalanceInformation: " + exp.Message);
                    }
                    finally
                    {
                        oResult.Message = sMessage;
                        oResult.Result = true;
                        oDbCommand.Dispose();
                        oConnection.Close();
                    }
                }
                return oResult;
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetBalanceInformation: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
            return oResult;
        }


        [Author("Munirul Islam", "08-09-2015", "Get Requested Cheque List")]
        public CResult GetRequestedChequeList(CCheque oCheque)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            CChequeList oChequeList = new CChequeList();


            string sSuccess = "";
            string sMessage = "";

            try
            {
                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_PAYOUT.GetRequestedChequeList");

                    try
                    {
                        oDatabase.AddInOutParameter(oDbCommand, "P_CHKID", DbType.String, 50, oCheque.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_USERID", DbType.String, 50, oCheque.User.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "p_BranchId", DbType.String, 50, oCheque.User.BranchInfo.CN);

                        oDatabase.AddInOutParameter(oDbCommand, "P_FromDate", DbType.String, 50, oCheque.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ToDate", DbType.String, 50, oCheque.ToDate);
                        oDatabase.AddOutParameter(oDbCommand, "rc_chequeList", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_oPerationType", DbType.String, 50, oCheque.OperationType);

                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);
                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_success")]).Value.ToString();
                        sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();


                        #region Build Balance Object

                        if (oDataSet.Tables.Count > 0)
                        {
                            DataTable dtBalance = oDataSet.Tables[0];
                            for (int i = 0; i < dtBalance.Rows.Count; i++)
                            {
                                oCheque = new CCheque();

                                oCheque.CN = Convert.ToString(dtBalance.Rows[i]["CHKREQID"]);
                                oCheque.AMOUNT = Convert.ToString(dtBalance.Rows[i]["AMOUNT"]);
                                oCheque.PINNO = Convert.ToString(dtBalance.Rows[i]["PINNO"]);
                                oCheque.COMPANYID = Convert.ToString(dtBalance.Rows[i]["COMPANYID"]);
                                oCheque.COMPANYNAME = Convert.ToString(dtBalance.Rows[i]["companyName"]);
                                oCheque.User.CN = Convert.ToString(dtBalance.Rows[i]["CREATEBY"]);
                                oCheque.BENEFICIARY_NAME = Convert.ToString(dtBalance.Rows[i]["BENEFICIARY_NAME"]);
                                oCheque.Id_Type = Convert.ToString(dtBalance.Rows[i]["NIDTYPE"]);
                                oCheque.NIDNO = Convert.ToString(dtBalance.Rows[i]["NIDNO"]);
                                oCheque.NIDISSUEDATE = Convert.ToString(dtBalance.Rows[i]["NIDISSUEDATE"]);
                                oCheque.NIDEXPIRYDATE = Convert.ToString(dtBalance.Rows[i]["NIDEXPIRYDATE"]);
                                oCheque.REMARKS = Convert.ToString(dtBalance.Rows[i]["REMARKS"]);

                                oChequeList.ChequeList.Add(oCheque);
                            }

                            oResult.Return = oChequeList;
                        }
                        #endregion Build Balance Object



                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetRequestedChequeList: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetRequestedChequeList: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
            return oResult;
        }

        [Author("Munirul Islam", "10-09-2015", "Get Balance Report")]
        public CResult GetBalannceReport(CBalance oBalance)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            CBalanceList oBalanceList = new CBalanceList();


            string sSuccess = "";
            string sMessage = "";

            try
            {

                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_PAYOUT.GetBalanceForRpt");

                    try
                    {

                        oDatabase.AddInOutParameter(oDbCommand, "P_USERID", DbType.String, 50, oBalance.USERID.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_FromDate", DbType.String, 50, oBalance.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ToDate", DbType.String, 50, oBalance.ToDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_Companyid", DbType.String, 50, oBalance.COMPANYID.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BranchId", DbType.String, 50, oBalance.USERID.BranchInfo.CN);
                        oDatabase.AddOutParameter(oDbCommand, "rc_balanceList", "RefCursor", 100);
                        oDatabase.AddInOutParameter(oDbCommand, "p_oPeningBalance", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_success", DbType.String, 50, DBNull.Value);
                        oDatabase.AddInOutParameter(oDbCommand, "p_return_msg", DbType.String, 500, DBNull.Value);
                        oDataSet = oDatabase.ExecuteDataSet(oDbCommand);

                        sSuccess = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_success")]).Value.ToString();
                        sMessage = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_return_msg")]).Value.ToString();

                        string sOpeningBalance = ((OracleParameter)oDbCommand.Parameters[oDatabase.BuildParameterName("p_oPeningBalance")]).Value.ToString();

                        DataTable dtBalance = new DataTable();
                        double dBalance = 0;
                        double dCr = 0;
                        double dDr = 0;

                        dBalance = Convert.ToDouble(sOpeningBalance);

                        if (oDataSet.Tables.Count > 0)
                        {
                            dtBalance = oDataSet.Tables[0].Clone();
                            DataRow dtRow1 = dtBalance.NewRow();
                            dtRow1["BRANCHNAME"] = "BF Balance";
                           
                            dtRow1["balance"] = sOpeningBalance;
                            dtBalance.Rows.Add(dtRow1);
                            
                            for (int i = 0; i < oDataSet.Tables[0].Rows.Count; i++)
                            {
                                DataRow dtRow = dtBalance.NewRow();

                                dtRow["CBID"] = Convert.ToString(oDataSet.Tables[0].Rows[i]["CBID"]);
                                dtRow["BRANCHNAME"] = Convert.ToString(oDataSet.Tables[0].Rows[i]["BRANCHNAME"]);
                                dtRow["BRANCHID"] = Convert.ToString(oDataSet.Tables[0].Rows[i]["BRANCHID"]);
                                dtRow["DIST_NAME"] = Convert.ToString(oDataSet.Tables[0].Rows[i]["DIST_NAME"]);
                                dtRow["COMPANYNAME"] = Convert.ToString(oDataSet.Tables[0].Rows[i]["COMPANYNAME"]);
                                dtRow["ENTRYUSER"] = Convert.ToString(oDataSet.Tables[0].Rows[i]["ENTRYUSER"]);
                                dtRow["VERIFYUSER"] = Convert.ToString(oDataSet.Tables[0].Rows[i]["VERIFYUSER"]);
                                dtRow["APPROVEDDATE"] = Convert.ToString(oDataSet.Tables[0].Rows[i]["APPROVEDDATE"]);
                                dtRow["COMPANYID"] = Convert.ToString(oDataSet.Tables[0].Rows[i]["COMPANYID"]);
                                dtRow["REMARKS"] = Convert.ToString(oDataSet.Tables[0].Rows[i]["REMARKS"]);
                                string sCrAmount = "";
                                string sDrAmount = "";
                                string sBalanceAmount = "";

                                sCrAmount = Convert.ToString(oDataSet.Tables[0].Rows[i]["AMOUNT"]);// cr.amount
                                sDrAmount = Convert.ToString(oDataSet.Tables[0].Rows[i]["DR"]);
                                sBalanceAmount = Convert.ToString(oDataSet.Tables[0].Rows[i]["BALANCE"]);

                                if (sCrAmount != "")
                                    dCr = Convert.ToDouble(sCrAmount);

                                if (sDrAmount != "")
                                    dDr = Convert.ToDouble(sDrAmount);

                               

                                dBalance = dBalance + dCr - dDr;

                                dtRow["amount"] = sCrAmount;
                                dtRow["DR"] = sDrAmount;
                                dtRow["balance"] = dBalance.ToString();


                                dtBalance.Rows.Add(dtRow);
                            }
                            oResult.Return = dtBalance;
                        }
                        

                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetBalannceReport: " + exp.Message);
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
                CLog.Logger.Write(CLog.EXCEPTION, this.ToString() + "/GetBalannceReport: " + exp.Message);
                oResult.Exception = exp;
                oResult.Message = exp.Message;
                oResult.Result = false;
                oResult.Return = null;
                return oResult;
            }
            return oResult;
        }

       
        public CResult GetPayoutDetailReport(CBalance oBalance)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            CBalanceList oBalanceList = new CBalanceList();


            string sSuccess = "";
            string sMessage = "";

            try
            {

                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_PAYOUT.GetPayoutDetailForRpt");

                    try
                    {

                        oDatabase.AddInOutParameter(oDbCommand, "P_USERID", DbType.String, 50, oBalance.USERID.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_COMPANYID", DbType.String, 50, oBalance.COMPANYID.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BranchId", DbType.String, 50, oBalance.USERID.BranchInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_FromDate", DbType.String, 50, oBalance.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ToDate", DbType.String, 50, oBalance.ToDate);
                        oDatabase.AddOutParameter(oDbCommand, "rc_payoutDetail", "RefCursor", 100);
                        //oDatabase.AddInOutParameter(oDbCommand, "p_oPerationType", DbType.String, 50, oCheque.OperationType);
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


        public CResult GetTransactionReport(CBalance oBalance)
        {
            CResult oResult = new CResult();
            Database oDatabase = new Database();
            DataSet oDataSet = null;
            CBalanceList oBalanceList = new CBalanceList();


            string sSuccess = "";
            string sMessage = "";

            try
            {

                using (IDbConnection oConnection = oDatabase.CreateConnection())
                {
                    IDbCommand oDbCommand = oDatabase.GetStoredProcICommand("PKG_PAYOUT.GetTransactionForRpt");

                    try
                    {

                        oDatabase.AddInOutParameter(oDbCommand, "P_USERID", DbType.String, 50, oBalance.USERID.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_COMPANYID", DbType.String, 50, oBalance.COMPANYID.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_BranchId", DbType.String, 50, oBalance.USERID.BranchInfo.CN);
                        oDatabase.AddInOutParameter(oDbCommand, "P_FromDate", DbType.String, 50, oBalance.FromDate);
                        oDatabase.AddInOutParameter(oDbCommand, "P_ToDate", DbType.String, 50, oBalance.ToDate);
                        oDatabase.AddOutParameter(oDbCommand, "rc_payoutDetail", "RefCursor", 100);
                        //oDatabase.AddInOutParameter(oDbCommand, "p_oPerationType", DbType.String, 50, oCheque.OperationType);
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
    }
}
