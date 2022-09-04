
using Oracle.ManagedDataAccess.Client;
using SIBLCommon.Common.Util.Logger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace CBSBalanceCheck
{
    public static class CBalanceCheck
    {
        #region DB Connection AND CONSTANTS
        const string DB_CONNECTION = "ACCConString";
        const string DB_CONNECTION_CBS = "ACCConStringCBS";

        const string DB_OWN_BANK = "OWN_BANK";
        const string DB_OTHER_BANK = "OTHER_BANK";

        /// <summary>
        /// Author: Munirul Islam
        /// Description: This function is used to connect to with database
        /// Date:05.05.2015
        /// </summary>
        /// <param name="sConnectionName"></param>
        /// <returns></returns>
        /// 
        
        static string GetConnectionStrings(string sConnectionName)
        {
            ConnectionStringSettingsCollection settings =
                ConfigurationManager.ConnectionStrings;

            //string conName = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.22.101.171)(PORT=1530))(CONNECT_DATA=(SERVICE_NAME=ABABILCBS)));User Id=sibl;Password=city_2018;";
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


  
        /// <summary>
        /// This function will return whether an account number is valid or invalid
        /// 0: Invalid account Number
        /// 1: Valid account Number
        /// </summary>
        /// <param name="sAccountNo"></param>
        /// <returns></returns>
        public static int IsValidAccountNo(string sAccountNo)
        {
            #region VariableDeclaration

            int hasRows = 0;// hasRows=0 :Invalid accountnumber
            // hasRows=1 :Valid accountnumber


            #endregion VariableDeclaration

            // end of format date
            try
            {
                using (OracleConnection oConnection = new OracleConnection(GetConnectionStrings(DB_CONNECTION_CBS)))
                {
                    oConnection.Open();

                    OracleCommand oDbCommand = oConnection.CreateCommand();

                    try
                    {

                        string ssQl = " select count(*) isAvailable from account where ACCCODE= '" + sAccountNo + "'";

                        oDbCommand.CommandText = ssQl;
                        oDbCommand.CommandType = CommandType.Text;
                        OracleDataReader oReader = oDbCommand.ExecuteReader();


                        if (oReader.HasRows)
                        {
                            oReader.Read();
                            hasRows = Convert.ToInt16(oReader["isAvailable"].ToString());
                            oReader.Close();
                        }

                    }
                    catch (Exception exp)
                    {

                        CLog.Logger.Write(CLog.INFORMATION, "/CheckAccountNo" + exp.Message);

                    }
                    finally
                    {
                        oConnection.Close();

                    }
                }

            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.INFORMATION, "/CheckAccountNo" + exp.Message);
            }

            return hasRows;

        }



        public static decimal AvailableBalance(string currency)
        {
            #region VariableDeclaration

            int hasRows = 0;

            string GLBDT = ConfigurationManager.AppSettings["MGGLID"].ToString();
            //string GLUSD = ConfigurationManager.AppSettings["GLUSD"].ToString();
            string branchCode = ConfigurationManager.AppSettings["branchCode"].ToString();
            string sBalance = string.Empty;

            decimal dBalance = 0;

            string sGLNo = string.Empty;

            if (currency.ToUpper().Equals("BDT"))
                sGLNo = GLBDT;
            //else if (currency.ToUpper().Equals("USD"))
            //    sGLNo = GLUSD;


            #endregion VariableDeclaration
            CLog.Logger.Write(CLog.INFORMATION, "/CheckBalance:: Balance Check Function called...");

            // end of format date
            try
            {
                using (OracleConnection oConnection = new OracleConnection(GetConnectionStrings(DB_CONNECTION_CBS)))
                {
                    oConnection.Open();

                    OracleCommand oDbCommand = oConnection.CreateCommand();

                    try
                    {

                        DateTime dtBF = DateTime.Now;
                        DateTime dtAcc = DateTime.Now;

                        //string sBFDate = dtBF.Day.ToString().PadLeft(2, '0') + "-" + dtBF.Month.ToString().PadLeft(2, '0') + "-" + dtBF.Year.ToString();
                        string ssQl = "";
                        //Get Current Date from CBS
                        //Update on: 10.03.2019

                        ssQl = "select get_current_date from dual";
                        oDbCommand.CommandText = ssQl;
                        oDbCommand.CommandType = CommandType.Text;
                        object oDate = oDbCommand.ExecuteScalar();

                        //End of Update on: 10.03.2019

                        dtBF = Convert.ToDateTime(oDate);
                        string sBFDate = dtBF.Day.ToString().PadLeft(2, '0') + "-" + dtBF.Month.ToString().PadLeft(2, '0') + "-" + dtBF.Year.ToString();

                        CLog.Logger.Write(CLog.INFORMATION, "/Request:AvailableBalance:: DB PROC: rpt_balancing.GETPARENTBALANCE");
                        CLog.Logger.Write(CLog.INFORMATION, "                         :: PARAM DATE:" + dtBF.ToString());
                        CLog.Logger.Write(CLog.INFORMATION, "                         :: BRCODE:" + branchCode);
                        CLog.Logger.Write(CLog.INFORMATION, "                         :: GL:" + sGLNo);

                        if (currency.ToUpper().Equals("USD"))
                        {
                            // for USD Balance
                            ssQl = " SELECT SUM(CASE WHEN TXNCODE=102 THEN (TRAMOUNT) END)- SUM(CASE WHEN TXNCODE=202 THEN (TRAMOUNT) END) BF_BAL " +
                                   " FROM FC_GL_ACCOUNTSTATEMENT_VW " +
                                   " WHERE ACCID='" + sGLNo + "'" +
                                   " AND TRDATE <= to_date('" + sBFDate + "','DD-MM-YYYY')" +
                                   " AND FCT_BRANCHID=" + branchCode;
                        }
                        else if (currency.ToUpper().Equals("BDT"))
                        {
                            // Update query on 21.05.2019
                            ssQl = @"Select rpt_balancing.GETPARENTBALANCE(" + sGLNo + ",to_date('" + sBFDate + "','DD-mm-YYYY')," + branchCode + ") BF_BAL from dual";
                            //ssQl = @"SELECT  GLADGLID, GLADBALANCE as BF_BAL,GLADBRID from GLACCOUNTDETAIL where GLADGLID='" + sGLNo + "'";
                        }

                        oDbCommand.CommandText = ssQl;
                        oDbCommand.CommandType = CommandType.Text;
                        OracleDataReader oReader = oDbCommand.ExecuteReader();


                        if (oReader.HasRows)
                        {
                            oReader.Read();
                            sBalance = oReader["BF_BAL"].ToString();
                            oReader.Close();
                        }

                        if (sBalance != "")
                            dBalance = Convert.ToDecimal(sBalance);

                        CLog.Logger.Write(CLog.INFORMATION, "/Response:AvailableBalance:: CBS Balance:" + sBalance.ToString());

                    }
                    catch (Exception exp)
                    {
                        dBalance = -100;//Exception Occur in Convertion
                        CLog.Logger.Write(CLog.INFORMATION, "/AvailableBalance" + exp.Message);

                    }
                    finally
                    {
                        oConnection.Close();

                    }
                }

            }
            catch (Exception exp)
            {
                dBalance = -100;//Exception Occur in Connection
                CLog.Logger.Write(CLog.INFORMATION, "/AvailableBalance" + exp.Message);
            }

            return dBalance;

        }

       
    }
}
