/*
 * File name            : BeneficiaryValidDA.cs
 * Author               : Munirul Islam
 * Date                 : June 22, 2020
 * Version              : 1.0
 *
 * Description          : This class is used to check beneficiary account validation 
 *
 * Modification history :
 * Name                         Date                            Desc
 * 
 * 
 * Copyright (c) 2020: SOCIAL ISLAMI BANK LIMITED
 */

using Oracle.ManagedDataAccess.Client;
using SIBLBKash.Common;
using SIBLCommon.Common.Entity.Result;
using SIBLCommon.Common.Util.Logger;
using SIBLCommon.SIBLCommon.Common.Entity.bKash;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeneficiaryValidation
{
    public class BeneficiaryValidDA
    {
        #region DB Connection AND CONSTANTS
        const string DB_CONNECTION_bKASH = "ACCConBkashDB";
        const string DB_CONNECTION_XOOM = "XOOMDBConString";

        
        /// <summary>
        /// Author: Munirul Islam
        /// Description: This function is used to connect to with database
        /// Date:15-06-2020
        /// </summary>
        /// <param name="sConnectionName"></param>
        /// <returns></returns>
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


        public CResult BeneficiaryValidationCheck()
        {
            #region VariableDeclaration
            CResult oResult = new CResult();
            BeneficiaryAccount oAcc = new BeneficiaryAccount();
            BeneficiaryAccount oAccResponse = new BeneficiaryAccount();
            string sSql = string.Empty;
            int iHasRow = 0;
            #endregion VariableDeclaration

            // end of format date
            try
            {
                using (OracleConnection oConnection = new OracleConnection(GetConnectionStrings(DB_CONNECTION_bKASH)))
                {
                    oConnection.Open();

                    OracleCommand oDbCommand = oConnection.CreateCommand();

                    try
                    {

                        // select a random instruction which beneficiary validation is not done
                        sSql = " select * from (select * from MGBKASHTRANSINFO  where BENEACK is null and ISAPPROVED='Yes' order by dbms_random.value) t where  rownum=1";

                        oDbCommand.CommandText = sSql;
                        oDbCommand.CommandType = CommandType.Text;
                        OracleDataReader oReader = oDbCommand.ExecuteReader();


                        while (oReader.Read())
                        {
                            oAcc.CN = oReader["SIBLTRANSACTIONO"].ToString();
                            oAcc.RecipientInfo.FullName = oReader["FULLNAME"].ToString();
                            oAcc.RecipientInfo.FirstName = oReader["FIRSTNAME"].ToString();
                            oAcc.RecipientInfo.LastName = oReader["LASTNAME"].ToString();
                            oAcc.RecipientInfo.MSISDN = oReader["MSISDN"].ToString();
                            
                            oAcc.RecipientInfo.CountryCode ="BD";
                            iHasRow = 1; // record found

                        }
                        if (iHasRow == 1)
                        {
                            #region DataFound
                            Console.WriteLine(DateTime.Now.ToShortTimeString() + ":: Beneficiary Validation start for SIBL Transaction NO: " + oAcc.CN);
                            CLog.Logger.Write(CLog.INFORMATION, "Beneficiary Validation start for SIBL Transaction NO: " + oAcc.CN);

                            try
                            {
                                // Call bKash API for beneficiary account validation
                                oAccResponse = CUIUtil2.BeneficiaryValidation(oAcc);
                            }
                            catch (Exception exp)
                            { 

                                CLog.Logger.Write(CLog.EXCEPTION, " BeneficiaryValidation API Exception: " + exp.ToString());
                            }

                            // Response 0000: means success
                            if (oAccResponse.Response.ResponseCode.Equals("0000"))
                            {
                                // Update Ack Information
                                sSql = "Update MGBKASHTRANSINFO   set BENEACK =1,BENEACKDATE=sysdate,CONVERSATIONID='" + oAccResponse.Response.ConversationID + "' where SIBLTRANSACTIONO='" + oAcc.CN + "'";

                                oDbCommand.CommandText = sSql;
                                oDbCommand.CommandType = CommandType.Text;
                                int iSuccess = oDbCommand.ExecuteNonQuery();

                                if (iSuccess == 1)
                                {
                                    
                                    
                                    try {
                                    
                                    }
                                    catch (Exception exp)
                                    {
                                        CLog.Logger.Write(CLog.EXCEPTION, exp.ToString());
                                    }
                                   
                                }
                            }
                            else
                            {
                                //Update Table by Response Code
                                sSql = "Update MGBKASHTRANSINFO   set BENEACK ='" + oAccResponse.Response.ResponseCode + "',BENEACKDATE=sysdate,CONVERSATIONID='" + oAccResponse.Response.ConversationID + "' where SIBLTRANSACTIONO='" + oAcc.CN + "'";

                                oDbCommand.CommandText = sSql;
                                oDbCommand.CommandType = CommandType.Text;
                                int iSuccess = oDbCommand.ExecuteNonQuery();

                                Console.WriteLine(DateTime.Now.ToShortTimeString() + "::API Response:" + oAccResponse.Response.ResponseMessage);
                                CLog.Logger.Write(CLog.INFORMATION, "API Response:" + oAccResponse.Response.ResponseMessage);
                            }
                            #endregion DataFound
                        }
                        else
                        {
                            #region NoDataFound
                            Console.WriteLine(DateTime.Now.ToShortTimeString() + ":: There is no data for beneficiary validation");
                            CLog.Logger.Write(CLog.INFORMATION, "There is no data for beneficiary validation");
                            #endregion NoDataFound

                        }
                    }
                    catch (Exception exp)
                    {
                       
                        CLog.Logger.Write(CLog.ERROR, exp.Message);
                    }
                    finally
                    {
                        oConnection.Close();
                        oConnection.Dispose();
                    }
                }

            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, exp.Message);
            }

            return oResult;
        }
    }
}
