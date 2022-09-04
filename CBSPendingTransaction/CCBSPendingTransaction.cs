using SIBLCommon.Common.Entity.Result;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;

namespace CBSPendingTransaction
{
    class CCBSPendingTransaction
    {
        static void Main(string[] args)
        {
            CResult oResult = new CResult();
            Int32 interVal = Convert.ToInt32(ConfigurationManager.AppSettings["interval"]);

            CCBSPendingTransactionDA oDA = new CCBSPendingTransactionDA();
            //interVal = 5000 * interVal; // 60,000 for 1 minute
            // 5000 for 5 second

            try
            {
                #region Loop
                while (1 == 1)
                {
                    oResult = oDA.ProcessOwnBankBEFTNPendingTransaction();
                    Thread.Sleep(interVal);
                    //Console.ReadKey();
                }
                #endregion Loop

            }
            catch (Exception ex)
            {
                // system is crashed. SMS notofication will send to user
                //string MobileNoString = SMSUtility.GetMobileNos();
                //string sMessage = SMSUtility.GetMessage();
                //string[] MobileNos = MobileNoString.Split(',');
                //string s = "";
                //foreach (String sNo in MobileNos)
                //{
                //    s = SMSUtility.SendSMS(sNo, "XOOM CBSPendingTransaction " + sMessage);
                //}


            }
        }
    }
}
