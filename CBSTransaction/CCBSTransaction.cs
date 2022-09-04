using SIBLCommon.Common.Entity.Disbursement;
using SIBLCommon.Common.Entity.Result;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;

namespace CBSTransaction
{
    class CCBSTransaction
    {
        static void Main(string[] args)
        {

            CResult oResult = new CResult();            
            Int32 interVal = Convert.ToInt32(ConfigurationManager.AppSettings["interval"]);
            //interVal = 5000 * interVal;     // 60,000 for 1 minute
            // 10,000 for 10 seconds

            CBSTransactionDA oCBSDA = new CBSTransactionDA();

            try
            {
                #region Loop
                while (1 == 1)
                {
                    oCBSDA.ProcessTransactions();
                    Thread.Sleep(interVal);
                }
                #endregion EndLoop

            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                
                // system is crashed. SMS notofication will send to user
                //string MobileNoString = SMSUtility.GetMobileNos();
                //string sMessage = SMSUtility.GetMessage();
                //string[] MobileNos = MobileNoString.Split(',');
                //string s = "";
                //foreach (String sNo in MobileNos)
                //{
                //    s = SMSUtility.SendSMS(sNo, "XOOM CBSTransaction " + sMessage);
                //}
            }
        }
    }
}
