/*
 * File name            : CLovDA.cs
 * Author               : Munirul Islam
 * Date                 : April 07, 2014
 * Version              : 1.0
 *
 * Description          : LOV Infortmation DataAccess Class
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
using System.Data.Common;
using SIBLCommon.Common.Entity.Result;
using SIBLCommon.Common.Entity.District;
using SIBLCommon.Common.Entity.Bank;



namespace SIBLRemit.DA.LOV
{
   public class CLovDA
    {
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
                            oResult.Return = BuildDistrictEntity( oDataSet.Tables[0]);
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

       public CResult GetDistrictListByBank(CBank oBank)
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
                       string sql = "SELECT DISTID, dist_name from district where distid in( select  distinct DISTRICTID  from branch_info where bank_id="+ oBank.CN +") order by dist_name";
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
               }
               return oResult;
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
                           oResult.Return = BuildBankEntity( oDataSet.Tables[0]);
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
                       string sql = "SELECT BRANCH_ID, BRANCH_NAME,  ROUTING_NO,  DISTRICTID,  BANK_ID FROM BRANCH_INFO where BANK_ID="+ oBank.CN +" order by BRANCH_NAME";
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


       public CResult GetDistrictBranchList(CDistrict oDistrict)
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
                       string sql = "SELECT BRANCH_ID, BRANCH_NAME,  ROUTING_NO,  DISTRICTID,  BANK_ID FROM BRANCH_INFO where BANK_ID =" + oDistrict.Bank.CN + " and DISTRICTID=" + oDistrict.CN + " order by BRANCH_NAME ";
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
    }
}
