/*
 * File name            : BeneficiaryValidation
 * Author               : Md. Munirul Islam
 * Date                 : 22 June 2020
 * Version              : 1.0
 *
 * Description          : This is the Beneficairy Validation Procedure
 * 
 * Modification history :
 * Name                         Date                            Desc
 * 
 * 
 * Copyright (c) 2020: SOCIAL ISLAMI BANK LIMITED
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.SIBLCommon.Common.Entity;
using SIBLCommon.SIBLCommon.Common.Entity.bKash;
using SIBLBKash.Common;
using System.Threading;
using System.Globalization;
using SIBLCommon.Common.Entity.Result;
using SIBLCommon.Common.Util.Logger;
using System.Configuration;

namespace BeneficiaryValidation
{
    public class Program
    {
        static void Main()
        {

            Int32 interVal = Convert.ToInt32(ConfigurationManager.AppSettings["interval"]);
            //interVal = 100 * interVal;     // 60,000 for 1 minute
         

            BeneficiaryAccount oAcc = new BeneficiaryAccount();
            //Retrive Data from Database
            //oAcc.RecipientInfo.FullName = "Munirul Islam";
            //oAcc.RecipientInfo.First
          
            BeneficiaryValidDA oDa= new BeneficiaryValidDA ();
            CResult oResult= new CResult ();

            //string sCopyMail = "munirul.islam@sibl-bd.com, aminul.islam@sibl-bd.com, ali.ahsan@sibl-bd.com";

            Console.WriteLine(DateTime.Now.ToShortTimeString() + ":: Beneficiary Account Validation Service");

            
            try {

                while (1 == 1)
                {

                    try {

                        oResult = oDa.BeneficiaryValidationCheck();
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, "/while:{Exception-Block} " + exp.Message);
                    }

                    Thread.Sleep(interVal);
                }
            
            }
            catch (Exception exp)
            {

                CLog.Logger.Write(CLog.EXCEPTION, "/Main:{Exception-Block} " + exp.Message);
            }
            
        }
    }
}
