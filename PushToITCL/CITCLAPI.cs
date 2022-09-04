using RestSharp;
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
    public class CITCLAPI
    {
        public static string ITCL_SUCCESS="1";

        static byte[] tdesKey = Encoding.ASCII.GetBytes("JDUEOF#UDNCMKCK#MDJEHGWH");
        static byte[] tdesIV = Encoding.ASCII.GetBytes("12345678");
        public static string Encrypt(string textToEncrypt, string sKey1, string sKey2)
        {
            byte[] tdesKey = Encoding.ASCII.GetBytes(sKey1);
            byte[] tdesIV = Encoding.ASCII.GetBytes(sKey2);
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = tdesKey;
            tdes.IV = tdesIV;
            byte[] buffer = Encoding.ASCII.GetBytes(textToEncrypt);
            return Convert.ToBase64String(tdes.CreateEncryptor().TransformFinalBlock(buffer, 0,
           buffer.Length));
        }

        public static string Decrypt(string textToDecrypt)
        {
            byte[] buffer = Convert.FromBase64String(textToDecrypt);
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = tdesKey;
            des.IV = tdesIV;
            return Encoding.ASCII.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0,
           buffer.Length));
        }


       
        public  static CTokenResponse getToken()
        {

            CTokenResponse oTokenResponse = new CTokenResponse();

            // user name provided by ITCL
            string userName = ConfigurationManager.AppSettings["userName"].ToString();
            // bank Code provided by ITCL
            string bankCode = ConfigurationManager.AppSettings["bankCode"].ToString();
            // API URL provided by ITCL
            string itcl_url = ConfigurationManager.AppSettings["itcl_url"].ToString();


            string sRequestBody = "";
            try
            {
                // Creating API URL
                itcl_url = itcl_url + "/getToken";
                var client = new RestClient(itcl_url);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/xml");
                // Creating Request Body
                sRequestBody = "<GetTokenRequest>\r\n <InputParameter>\r\n <BankCode>" + bankCode + "</BankCode>\r\n <UserName>" + userName + "</UserName>\r\n </InputParameter>\r\n</GetTokenRequest>";
                request.AddParameter("application/xml", sRequestBody, ParameterType.RequestBody);

                CLog.Logger.Write(CLog.INFORMATION, "/getToken Request::" + sRequestBody);

                // Execute Request
                IRestResponse response = client.Execute(request);

                XmlDocument xmldoc = new XmlDocument();
                string sResult = "";
                string sTokenResponse = "";
                if (response.ResponseStatus == ResponseStatus.Completed)
                {

                    sResult = response.Content;
                    //Load Result in xml document
                    xmldoc.LoadXml(sResult);
                    CLog.Logger.Write(CLog.INFORMATION, "/getToken Response::" + sResult);
                    try
                    {
                        XmlNodeList xmlGetTokenResponse = xmldoc.GetElementsByTagName("GetTokenResponse");
                        sTokenResponse=xmlGetTokenResponse[0].Attributes["Response"].Value.ToString() ;

                        if (sTokenResponse == "1")
                        {
                            XmlNodeList xmlbankCode = xmldoc.GetElementsByTagName("BankCode");
                            XmlNodeList xmlKeyID = xmldoc.GetElementsByTagName("KeyID");
                            XmlNodeList xmlKey1 = xmldoc.GetElementsByTagName("Key1");
                            XmlNodeList xmlKey2 = xmldoc.GetElementsByTagName("Key2");
                            oTokenResponse.bankCode = xmlbankCode[0].InnerText;
                            oTokenResponse.keyID = xmlKeyID[0].InnerText;
                            oTokenResponse.key1 = xmlKey1[0].InnerText;
                            oTokenResponse.key2 = xmlKey2[0].InnerText;
                        }
                        else
                        {
                            //Error in Token Generation
                            if (sTokenResponse == "0")
                            {
                                XmlNodeList xmlInfo = xmldoc.GetElementsByTagName("Info");
                                if (xmlInfo != null)
                                {
                                    //CLog.Logger.Write(CLog.INFORMATION, "/ResponseError:: getToken: Token is not generated."+xmlInfo[0].InnerText);
                                    Console.WriteLine(DateTime.Now.ToShortTimeString() + " "+xmlInfo[0].InnerText);
                                }
                            }
                        }
                        //Response
                    }
                    catch (Exception exp)
                    {
                        oTokenResponse = new CTokenResponse();
                        CLog.Logger.Write(CLog.INFORMATION, "/ResponseError:: getToken: Can not parsing "+exp.ToString());
                    }

                }
                else
                {
                    //Response Error Message
                    CLog.Logger.Write(CLog.INFORMATION, "/ResponseError:: getToken:" + response.ErrorMessage);
                }

            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.INFORMATION, "/getToken Response::" + exp.ToString());
            }
            return oTokenResponse;
        }


       


        public static CTokenResponse TransactionInquery(CAccDeposit oAccDeposit, CTokenResponse oTokenResponse)
        {
            // Receiver Bank Short name
            string bankName = oAccDeposit.bankName;
            // Receiver account number
            string accountNumber = oAccDeposit.fullPan;
            // receiver amount
            string sAmount = oAccDeposit.Amount;
            CITCLDA oDA = new CITCLDA();
            // CTokenResponse oTokenResponse = new CTokenResponse();

            //Generate Token for using in password encryption and account number
            //oTokenResponse = getToken();

            //provided by ITCL
            string userName = ConfigurationManager.AppSettings["userName"].ToString();
            // Provided by ITCL
            string bankCode = ConfigurationManager.AppSettings["bankCode"].ToString();
            // Provided by ITCL
            string itcl_url = ConfigurationManager.AppSettings["itcl_url"].ToString();
            // Initial password provided by ITCL
            string password = ConfigurationManager.AppSettings["password"].ToString();
            //Terminal name
            string TermName = ConfigurationManager.AppSettings["TermName"].ToString();
            //SourceOfFund
            string SourceOfFund = ConfigurationManager.AppSettings["SourceOfFund"].ToString();
            // Name of the application
            string userID = ConfigurationManager.AppSettings["userID"].ToString();

            string sRequestBody = "";
            // encrypt password by using key1 and key2 and clear the user password for account deposit packet
            string encrypePasswordByKey1andKey2 = Encrypt(password, oTokenResponse.key1, oTokenResponse.key2);
            // encrypt receiver account number by key1 and key2
            string encrypeAccNoByKey1andKey2 = Encrypt(accountNumber, oTokenResponse.key1, oTokenResponse.key2);

            // Create url to for Transaction Inquity
            itcl_url = itcl_url + "/siblInquiry";
            //itcl_url = itcl_url + "/siblIBFTAccountCredit";

            var client = new RestClient(itcl_url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/xml");

            try
            {
                string sAppID = oAccDeposit.extId;

                //sRequestBody = "<AccountDepositRequest>\r\n <InputParameter>\r\n<KeyID>" + oTokenResponse.keyID + "</KeyID>\r\n<UserName>" + userName + "</UserName>\r\n<Password>" + encrypePasswordByKey1andKey2 + "</Password>\r\n<BankCode>" + bankCode +
                //    "</BankCode>\r\n<ExtID>" + sAppID + "</ExtID>\r\n<UserID>" + userID + "</UserID>\r\n<BankName>" + bankName + "</BankName>\r\n<AccountNo>" + encrypeAccNoByKey1andKey2 + "</AccountNo>\r\n<Provider>PAY</Provider>\r\n<Amount>" + sAmount + "</Amount>\r\n<TermName>BKASSIBL</TermName>\r\n<SourceOfFund>123456789</SourceOfFund>\r\n<Comment>AAQ/AAN</Comment>\r\n </InputParameter>\r\n</AccountDepositRequest>\r\n";
                //
                //Create request Body
                sRequestBody = "<AccountDepositRequest>\r\n <InputParameter>\r\n<KeyID>" + oTokenResponse.keyID + "</KeyID>\r\n<UserName>" + userName + "</UserName>\r\n<Password>" + encrypePasswordByKey1andKey2 + "</Password>\r\n<BankCode>" + bankCode +
                    "</BankCode>\r\n<ExtID>" + sAppID + "</ExtID>\r\n<UserID>" + userID + "</UserID>\r\n<BankName>" + bankName + "</BankName>\r\n<AccountNo>" + encrypeAccNoByKey1andKey2 + "</AccountNo>\r\n<Provider>PAY</Provider>\r\n<Amount>" + sAmount + "</Amount>\r\n<TermName>" + TermName + "</TermName>\r\n<SourceOfFund>" + SourceOfFund + "</SourceOfFund>\r\n<Comment>" + oAccDeposit.comment + "</Comment>\r\n </InputParameter>\r\n</AccountDepositRequest>\r\n";


                CLog.Logger.Write(CLog.INFORMATION, "/AccDeposit Request::" + sRequestBody);

                request.AddParameter("application/xml", sRequestBody, ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);

                XmlDocument xmldoc = new XmlDocument();
                string sResult = "";

                if (response.ResponseStatus == ResponseStatus.Completed)
                {

                    sResult = response.Content;
                    CLog.Logger.Write(CLog.INFORMATION, "/AccDeposit Response::" + sResult);
                    xmldoc.LoadXml(sResult);

                    XmlNodeList xmlAuthRespCode = xmldoc.GetElementsByTagName("AuthRespCode");

                    oTokenResponse.OutParameter.AuthResponseCode = xmlAuthRespCode[0].InnerText;

                    if (oTokenResponse.OutParameter.AuthResponseCode == ITCL_SUCCESS)
                    {
                        XmlNodeList xmlTransId = xmldoc.GetElementsByTagName("TranId");
                        oTokenResponse.OutParameter.TranId = xmlTransId[0].InnerText;
                        //Next Token KeyId and Value
                        XmlNodeList xmlAccountDepositCreateResponse = xmldoc.GetElementsByTagName("AccountDepositCreateResponse");
                        oTokenResponse.keyID = xmlAccountDepositCreateResponse[0].Attributes["keyID"].Value.ToString();
                        oTokenResponse.key1 = xmlAccountDepositCreateResponse[0].Attributes["key1"].Value.ToString();
                        oTokenResponse.key2 = xmlAccountDepositCreateResponse[0].Attributes["key2"].Value.ToString();

                    }
                    else
                    {
                        XmlNodeList xmlAccountDepositCreateResponse = xmldoc.GetElementsByTagName("AccountDepositCreateResponse");

                        XmlElement root = xmldoc.DocumentElement;
                        if (root.HasAttribute("keyID"))
                        {
                            oTokenResponse.keyID = xmlAccountDepositCreateResponse[0].Attributes["keyID"].Value.ToString();
                        }

                        if (root.HasAttribute("key1"))
                        {
                            oTokenResponse.key1 = xmlAccountDepositCreateResponse[0].Attributes["key1"].Value.ToString();
                        }

                        if (root.HasAttribute("key2"))
                        {
                            oTokenResponse.key2 = xmlAccountDepositCreateResponse[0].Attributes["key2"].Value.ToString();
                        }
                        else
                        {

                            XmlNodeList xmlInfo = xmldoc.GetElementsByTagName("Info");
                            oTokenResponse.OutParameter.Info = xmlInfo[0].InnerText;
                            oTokenResponse.keyID = "";
                            oTokenResponse.key1 = "";
                            oTokenResponse.key2 = "";
                        }
                        // fail Transaction
                    }
                }
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.INFORMATION, "/AccDeposit Response::" + exp.ToString());
            }
            return oTokenResponse;
        }

        /// <summary>
        /// Function used to transfer fund in third party banks
        /// </summary>
        /// <param name="oAccDeposit"></param>
        /// <returns></returns>
        public static CTokenResponse accountDeposit(CAccDeposit oAccDeposit, CTokenResponse oTokenResponse)
        {
            // Receiver Bank Short name
            string bankName = oAccDeposit.bankName;
            // Receiver account number
            string accountNumber = oAccDeposit.fullPan;
            // receiver amount
            string sAmount = oAccDeposit.Amount;
            CITCLDA oDA = new CITCLDA();
           // CTokenResponse oTokenResponse = new CTokenResponse();
            
            //Generate Token for using in password encryption and account number
            //oTokenResponse = getToken();
           
            //provided by ITCL
            string userName = ConfigurationManager.AppSettings["userName"].ToString();
            // Provided by ITCL
            string bankCode = ConfigurationManager.AppSettings["bankCode"].ToString();
            // Provided by ITCL
            string itcl_url = ConfigurationManager.AppSettings["itcl_url"].ToString();
            // Initial password provided by ITCL
            string password = ConfigurationManager.AppSettings["password"].ToString();
            //Terminal name
            string TermName = ConfigurationManager.AppSettings["TermName"].ToString();
            //SourceOfFund
            string SourceOfFund = ConfigurationManager.AppSettings["SourceOfFund"].ToString();
            // Name of the application
            string userID = ConfigurationManager.AppSettings["userID"].ToString();

            string sRequestBody = "";
            // encrypt password by using key1 and key2 and clear the user password for account deposit packet
            string encrypePasswordByKey1andKey2 = Encrypt(password, oTokenResponse.key1, oTokenResponse.key2);
            // encrypt receiver account number by key1 and key2
            string encrypeAccNoByKey1andKey2 = Encrypt(accountNumber, oTokenResponse.key1, oTokenResponse.key2);
            
            // Create url to push transaction for other account deposit
            itcl_url = itcl_url + "/siblAccountBaseDeposit";
            //itcl_url = itcl_url + "/siblIBFTAccountCredit";
            
            var client = new RestClient(itcl_url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/xml");

            try
            {
                string sAppID = oAccDeposit.extId;
                
                //sRequestBody = "<AccountDepositRequest>\r\n <InputParameter>\r\n<KeyID>" + oTokenResponse.keyID + "</KeyID>\r\n<UserName>" + userName + "</UserName>\r\n<Password>" + encrypePasswordByKey1andKey2 + "</Password>\r\n<BankCode>" + bankCode +
                //    "</BankCode>\r\n<ExtID>" + sAppID + "</ExtID>\r\n<UserID>" + userID + "</UserID>\r\n<BankName>" + bankName + "</BankName>\r\n<AccountNo>" + encrypeAccNoByKey1andKey2 + "</AccountNo>\r\n<Provider>PAY</Provider>\r\n<Amount>" + sAmount + "</Amount>\r\n<TermName>BKASSIBL</TermName>\r\n<SourceOfFund>123456789</SourceOfFund>\r\n<Comment>AAQ/AAN</Comment>\r\n </InputParameter>\r\n</AccountDepositRequest>\r\n";
                //
                //Create request Body
                sRequestBody = "<AccountDepositRequest>\r\n <InputParameter>\r\n<KeyID>" + oTokenResponse.keyID + "</KeyID>\r\n<UserName>" + userName + "</UserName>\r\n<Password>" + encrypePasswordByKey1andKey2 + "</Password>\r\n<BankCode>" + bankCode +
                    "</BankCode>\r\n<ExtID>" + sAppID + "</ExtID>\r\n<UserID>" + userID + "</UserID>\r\n<BankName>" + bankName + "</BankName>\r\n<AccountNo>" + encrypeAccNoByKey1andKey2 + "</AccountNo>\r\n<Provider>PAY</Provider>\r\n<Amount>" + sAmount + "</Amount>\r\n<TermName>" + TermName + "</TermName>\r\n<SourceOfFund>"+SourceOfFund+"</SourceOfFund>\r\n<Comment>" + oAccDeposit.comment + "</Comment>\r\n </InputParameter>\r\n</AccountDepositRequest>\r\n";


                CLog.Logger.Write(CLog.INFORMATION, "/AccDeposit Request::" + sRequestBody);

                request.AddParameter("application/xml", sRequestBody, ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);

                XmlDocument xmldoc = new XmlDocument();
                string sResult = "";

                if (response.ResponseStatus == ResponseStatus.Completed)
                {

                    sResult = response.Content;
                    CLog.Logger.Write(CLog.INFORMATION, "/AccDeposit Response::" + sResult);
                    xmldoc.LoadXml(sResult);

                    XmlNodeList xmlAuthRespCode = xmldoc.GetElementsByTagName("AuthRespCode");
                  
                    oTokenResponse.OutParameter.AuthResponseCode = xmlAuthRespCode[0].InnerText;
                   
                    if (oTokenResponse.OutParameter.AuthResponseCode == ITCL_SUCCESS)
                    {
                        XmlNodeList xmlTransId = xmldoc.GetElementsByTagName("TranId");
                        oTokenResponse.OutParameter.TranId = xmlTransId[0].InnerText;
                        //Next Token KeyId and Value
                       XmlNodeList xmlAccountDepositCreateResponse = xmldoc.GetElementsByTagName("AccountDepositCreateResponse");
                       oTokenResponse.keyID= xmlAccountDepositCreateResponse[0].Attributes["keyID"].Value.ToString();
                       oTokenResponse.key1= xmlAccountDepositCreateResponse[0].Attributes["key1"].Value.ToString();
                       oTokenResponse.key2=xmlAccountDepositCreateResponse[0].Attributes["key2"].Value.ToString();
                        
                    }
                    else
                    {
                        XmlNodeList xmlAccountDepositCreateResponse = xmldoc.GetElementsByTagName("AccountDepositCreateResponse");

                        XmlElement root = xmldoc.DocumentElement;
                        if (root.HasAttribute("keyID"))
                        {
                            oTokenResponse.keyID = xmlAccountDepositCreateResponse[0].Attributes["keyID"].Value.ToString();
                        }

                        if (root.HasAttribute("key1"))
                        {
                            oTokenResponse.key1 = xmlAccountDepositCreateResponse[0].Attributes["key1"].Value.ToString();
                        }

                        if (root.HasAttribute("key2"))
                        {
                            oTokenResponse.key2 = xmlAccountDepositCreateResponse[0].Attributes["key2"].Value.ToString();
                        }
                        else
                        {
                            
                            XmlNodeList xmlInfo = xmldoc.GetElementsByTagName("Info");
                            oTokenResponse.OutParameter.Info = xmlInfo[0].InnerText;
                            oTokenResponse.keyID = "";
                            oTokenResponse.key1 = "";
                            oTokenResponse.key2 = "";
                        }
                        // fail Transaction
                    }
                }
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.INFORMATION, "/AccDeposit Response::" + exp.ToString());
            }
            return oTokenResponse;
        }
    }
}
