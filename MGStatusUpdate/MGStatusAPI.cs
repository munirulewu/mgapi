using MGStatusUpdate.PartnerConnect;
using Newtonsoft.Json.Linq;
using RestSharp;
using SIBLCommon.Common.Util.Logger;
using SIBLCommon.SIBLCommon.Common.Entity.MGAPI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MGStatusUpdate
{
    public class MGStatusAPI
    {

        public static string UpdateStatus(CTransaction oTransaction)
        {
            string soapResult = "";            
            string sParameter = "";
            string sResult = "";

            updateStatusResponse oUpRes = new updateStatusResponse();

            string sURL = ConfigurationManager.AppSettings["mg_url"];
            string sUSER_NAME = ConfigurationManager.AppSettings["Username"];
            string sPASSWORD = ConfigurationManager.AppSettings["Password"];

            sParameter = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:par=\"http://moneygram.com/service/PartnerConnectService\">\r\n<soapenv:Header/>\r\n<soapenv:Body>\r\n <par:updateStatus>\r\n <par:status>\r\n <par:mgiTransactionID>"+ oTransaction.transaction.mgiTransactionId +"</par:mgiTransactionID>\r\n <par:partnerTransactionID>"+ oTransaction.transaction.siblTransactionId+"</par:partnerTransactionID>\r\n <par:partnerReasonCode>"+ oTransaction.transaction.TransactionResponse.responseCode +"</par:partnerReasonCode>\r\n <par:partnerReasonMessage>"+ oTransaction.transaction.TransactionResponse.message+"</par:partnerReasonMessage>\r\n </par:status>\r\n </par:updateStatus>\r\n</soapenv:Body>\r\n</soapenv:Envelope>\r\n\r\n\r\n";
            
            //For Basic Authentication
            string authInfo = sUSER_NAME + ":" + sPASSWORD;
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));


            var client = new RestClient(sURL);
            var request = new RestRequest(Method.POST);
            request.AddHeader("postman-token", "3366475a-c21c-5703-a3bd-63bb7dd20f2b");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic "+ authInfo);
            request.AddHeader("content-type", "text/xml");
            request.AddParameter("application/xml", sParameter, ParameterType.RequestBody);


            try
            {
                CLog.Logger.Write(CLog.INFORMATION, "/UpdateStatus:Request/" + sParameter);
                IRestResponse response = client.Execute(request);
                if (response.ResponseStatus == ResponseStatus.Completed)
                {
                    sResult = response.Content;
                    CLog.Logger.Write(CLog.INFORMATION, "/UpdateStatus:Response" + sResult);
                }
                else
                {
                    CLog.Logger.Write(CLog.EXCEPTION, "/BillPaymentAPI:" + response.ErrorMessage);
                }

                
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, "/BillPaymentAPI:" + exp.ToString());
            }

            return sResult;
        }


        public static CResponse GetUpdateStatusResponse(string soapResult)
        {
            CResponse oRespose = new CResponse();

            // Create xml object to store response result
            XmlDocument xmlDocDoc = new XmlDocument();
           // soapResult = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:par=\"http://moneygram.com/service/PartnerConnectService\"> <soapenv:Header/> <soapenv:Body> <par:updateStatusResponse/> </soapenv:Body> </soapenv:Envelope>";

            // for load directory file
            xmlDocDoc.LoadXml(soapResult);
            bool b = soapResult.Contains("<par:updateStatusResponse");

            if (b == true)
            {
                oRespose.ResponseCode = "SUCCESS";
            }
            else
            {

                if (xmlDocDoc.SelectSingleNode("//faultcode") != null)
                {
                    oRespose.faultCode = xmlDocDoc.SelectSingleNode("//faultcode").InnerText;
                }

                if (xmlDocDoc.SelectSingleNode("//faultstring") != null)
                {
                    oRespose.faultString = xmlDocDoc.SelectSingleNode("//faultstring").InnerText;
                }
                        
          
                if (xmlDocDoc.SelectSingleNode("//errorCode") != null)
                {
                    oRespose.ResponseCode = xmlDocDoc.SelectSingleNode("//errorCode").InnerText;
                }

                if (xmlDocDoc.SelectSingleNode("//errorMessage") != null)
                {
                    oRespose.ResponseMessage = xmlDocDoc.SelectSingleNode("//errorMessage").InnerText;
                }
            }

            
            return oRespose;


        }



    }
}
