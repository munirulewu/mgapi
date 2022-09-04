
using MGStatusUpdate.PartnerConnect;
using SIBLCommon.Common.Entity.Result;
using SIBLCommon.Common.Util.Logger;
using SIBLCommon.SIBLCommon.Common.Entity.MGAPI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MGStatusUpdate
{
    class MGStatusUpdate
    {
        static void Main(string[] args)
        {          

            Int32 interVal = Convert.ToInt32(ConfigurationManager.AppSettings["interval"]);
            string Username = Convert.ToString(ConfigurationManager.AppSettings["Username"]);
            string Password = Convert.ToString(ConfigurationManager.AppSettings["Password"]);
            string mg_url = Convert.ToString(ConfigurationManager.AppSettings["mg_url"]);
            Console.WriteLine(DateTime.Now.ToShortTimeString() +  " ManoyGram Call Back Listner Service");
            try
            {
                #region Loop
                while (1 == 1)
                {
                    CallBackACK();
                    CallBackFinalStatus();
                    Thread.Sleep(interVal);
                }
                #endregion EndLoop
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, " API  Exception::" + exp.ToString());
            }
        }


        private static void CallBackACK()
        {
            string sResponseSOAP = "";
            CResult oResult = new CResult();
            CResult oResultUpdate = new CResult();
            CMGStatusUpdateDA oDA = new CMGStatusUpdateDA();
            CTransaction oTransaction = new CTransaction();
            CResponse oRespose = new CResponse();

            try
            {
                string sACKData = "ACK_DATA";
                oResult = oDA.GetTranStatusUpdateData(sACKData);
                if (oResult.Result == true)
                {
                    oTransaction = (CTransaction)oResult.Return;
                    Console.WriteLine(DateTime.Now.ToShortTimeString() + " CallBackACK/ MG Transaction found for update:: MGtransactionID: " + oTransaction.transaction.mgiTransactionId);
                    sResponseSOAP = MGStatusAPI.UpdateStatus(oTransaction);
                    CLog.Logger.Write(CLog.INFORMATION, " CallBackACK/API Response:: MGtransactionID: " + oTransaction.transaction.mgiTransactionId + " Response: " + sResponseSOAP);
                    oRespose = MGStatusAPI.GetUpdateStatusResponse(sResponseSOAP);
                    if (oRespose.ResponseCode == "SUCCESS")
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " CallBackACK/ MG Transaction found for update:: MGtransactionID: " + oTransaction.transaction.mgiTransactionId + " is updated Successfully");
                    }
                }
                else
                    Console.WriteLine(DateTime.Now.ToShortTimeString() + " CallBackACK::No More transaction is available for update to MOneyGram");

            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, " API  Exception::CallBackACK/" + exp.ToString());
            }

        }

        private static void CallBackFinalStatus()
        {
            string sResponseSOAP = "";
            CResult oResult = new CResult();
            CResult oResultUpdate = new CResult();
            CMGStatusUpdateDA oDA = new CMGStatusUpdateDA();
            CTransaction oTransaction = new CTransaction();
            CResponse oRespose = new CResponse();

            try
            {
                string sACKData = "CALLBACK_FINALSTATUS_DATA";
                oResult = oDA.GetTranStatusUpdateData(sACKData);
                if (oResult.Result == true)
                {
                    oTransaction = (CTransaction)oResult.Return;
                    Console.WriteLine(DateTime.Now.ToShortTimeString() + " CallBackFinalStatus:: MG Transaction found for update for MoneyGram/MGtransactionID: " + oTransaction.transaction.mgiTransactionId);
                    sResponseSOAP = MGStatusAPI.UpdateStatus(oTransaction);
                    CLog.Logger.Write(CLog.INFORMATION, " CallBackFinalStatus:: API Response::MGtransactionID: " + oTransaction.transaction.mgiTransactionId + " Response: " + sResponseSOAP);
                    oRespose = MGStatusAPI.GetUpdateStatusResponse(sResponseSOAP);
                    if (oRespose.ResponseCode == "SUCCESS")
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " CallBackFinalStatus:: MG Transaction" + oTransaction.transaction.mgiTransactionId + " is updated Successfully");
                    }
                }
                else
                    Console.WriteLine(DateTime.Now.ToShortTimeString() +  " CallBackFinalStatus::No More transaction is available for update to MoneyGram");

            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, " API  Exception::CallBackFinalStatus/" + exp.ToString());
            }

        }

    }
}
