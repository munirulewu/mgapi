using RestSharp;
using SIBLCommon.Common.Entity.Result;
using SIBLCommon.Common.Util.Logger;
using SIBLCommon.SIBLCommon.Common.Entity.ITCL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace PushToITCL
{
    class Program
    {
       
        
        static void Main(string[] args)
        {
            Console.WriteLine("WelCome to Data Push Service  to ITCL");
            //Console.WriteLine(Decrypt("TzOlDdhNyT8="));
            CResult oResult = new CResult();
            CITCLDA oDA = new CITCLDA();
            oResult.Result = false;
            CTokenResponse oTokenResponse = new CTokenResponse();
            try 
            {

                while (1 == 1)
                {

                    try
                    {

                        if (oDA.IsNPSBTransactionAvailable() == true)
                        {
                            if (oTokenResponse.keyID == "")
                            {

                                oTokenResponse = CITCLAPI.getToken();
                                if (oTokenResponse.keyID != "")
                                {
                                    Console.WriteLine(DateTime.Now.ToShortTimeString() + ":: New Token is Generated");
                                }
                                else
                                {
                                    // Error Message has been written in GetToken Method
                                }
                            }
                            else
                            {
                                Console.WriteLine(DateTime.Now.ToShortTimeString() + ":: New Token is not Generated.Workign with existing token");
                            }
                            // Token is Generated or Transaction is Successful
                            if (oTokenResponse.keyID != "")
                            {
                                oResult = oDA.DoNPSBTransaction(oTokenResponse);
                                oTokenResponse = (CTokenResponse)oResult.Return;
                            }
                        }
                        else
                        {
                            Console.WriteLine(DateTime.Now.ToShortTimeString() + ":: No Pending Transaction for NPSB.");
                            oTokenResponse.keyID = "";
                        }

                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + ":: Exception Inside whieloop."+exp.ToString());
                        CLog.Logger.Write(CLog.INFORMATION, ":: Exception Inside whieloop." + exp.ToString());
                    }
                    
                }

            }
            catch (Exception exp)
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString() + ":: Exit from Program." + exp.ToString());
                CLog.Logger.Write(CLog.INFORMATION, ":: Exit from Program.." + exp.ToString());
            }
        }

       
    }
}
