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
namespace ITCLTransInquary
{
    class Program
    {
       
        
        static void Main(string[] args)
        {
            Console.WriteLine("WelCome to Data Push Service  to ITCL");
            //Console.WriteLine(Decrypt("TzOlDdhNyT8="));
            CResult oResult = new CResult();
            CITCLInqDA oDA = new CITCLInqDA();
            oResult.Result = false;
            CTokenResponse oTokenResponse = new CTokenResponse();
            try {

                while (1 == 1)
                {

                    try {

                        if (oTokenResponse.keyID == "")
                        {
                           
                            oTokenResponse = CITCLAPI.getToken();
                            Console.WriteLine(DateTime.Now.ToShortTimeString() + ":: New Token is Generated");
                        }
                        else
                        {
                            Console.WriteLine(DateTime.Now.ToShortTimeString() + ":: New Token is not Generated.Workign with existing token");
                        }

                        oResult = oDA.DoNPSBTransaction(oTokenResponse);
                        oTokenResponse = (CTokenResponse)oResult.Return;

                    }
                    catch (Exception exp)
                    { 
                    
                    }
                    
                }

            }
            catch (Exception exp)
            { 
            
            }
        }

       
    }
}
