/*
 * File name            : CUIUtil.cs
 * Author               : Munirul Islam
 * Date                 : May 04.05.2014
 * Version              : 1.0
 *
 * Description          : Get/ Search  Transfer Information through webservice
 *
 * Modification history :
 * Name                         Date                            Desc
 * Munirul Islam                08.10.2017                      DemandNote API                                        
 * 
 * Copyright (c) 2017: Social Islami Bank Limited
 */
 
using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
using System.Xml;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Specialized;
using Newtonsoft.Json;
using SIBLCommon.Common.Entity.User;
using SIBLCommon.Common.Entity.Titas;
using SIBLCommon.Common;
using SIBLCommon.Common.Util.Logger;
using SIBLCommon.Common.Entity.Role;

using SIBLCommon.Common.Entity.Bank;
using SIBLCommon.SIBLCommon.Common.Entity.SGCL;
using RestSharp;

using SIBLCommon.Common.Entity.Result;
using System.Globalization;
using SIBLCommon.SIBLCommon.Common.Entity.bKash;

using System.Security.Cryptography;
using SIBLCommon.SIBLCommon.Common.Entity.MGAPI;
namespace SIBLBKash.Common
{
    public class CUIUtil2
    {


        public static string CalltoBAVAPI(CTransaction transaction)
        {
            CLog.Logger.Write(CLog.EXCEPTION, " Calling BAV API...");
            string sURL = ConfigurationManager.AppSettings["bKashAPI"].ToString();
            var client = new RestClient(sURL);
            string sResult = "";
            var request = new RestRequest(Method.POST);

            try
            {

                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");

               // request.AddParameter("application/json", "{\r\n  \"transaction\": {\r\n    \"mgiTransactionId\": \"" + transaction.transaction.mgiTransactionId + "\",\r\n    \"receiveCountryCode\": \"" + transaction.transaction.receiveCountryCode + "\",\r\n    \"sendCountryCode\": \"" + transaction.transaction.sendCountryCode + "\",\r\n    \"sendAmount\": {\r\n      \"value\": \"" + transaction.transaction.sendAmount.value + "\",\r\n      \"currencyCode\": \"" + transaction.transaction.sendAmount.currencyCode + "\"\r\n    },\r\n    \"receiveAmount\": {\r\n      \"value\": \"" + transaction.transaction.receiveAmount.value + "\",\r\n      \"currencyCode\": \"" + transaction.transaction.receiveAmount.currencyCode + "\"\r\n    },\r\n    \"sender\": {\r\n      \"Person\": {\r\n        \"firstName\": \"" + transaction.transaction.sender.person.firstName + "\",\r\n        \"middleName\": \"\",\r\n        \"lastName\": \"" + transaction.transaction.sender.person.lastName + "\",\r\n        \"SecondLastName\": \"\"\r\n      }\r\n    },\r\n    \"receiver\": {\r\n      \"Person\": {\r\n        \"firstName\": \"" + transaction.transaction.receiver.person.firstName + "\",\r\n        \"middleName\": \"\",\r\n        \"lastName\": \"" + transaction.transaction.receiver.person.lastName + "\",\r\n        \"SecondLastName\": \"\"\r\n      }\r\n    },\r\n    \"accountCode\": \"" + transaction.transaction.accountCode + "\",\r\n    \"accountNumber\": \"" + transaction.transaction.accountNumber + "\",\r\n     \"AdditionalData\": [\r\n     \t{\r\n\t      \t\t\"key\":\"senderAddress\",\r\n\t      \t\t\"value\":\"" + transaction.transaction.sender.person.address + "\",\r\n\t      \t},\r\n\t      \t{\r\n\t      \t\t\"key\":\"senderPhone\",\r\n\t      \t\t\"value\":\"" + transaction.transaction.sender.person.mobileNo + "\",\r\n\t      \t}\r\n     \t]\r\n  }\r\n}\r\n", ParameterType.RequestBody);
                //request.AddParameter("application/json", sJsonData, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.ResponseStatus == ResponseStatus.Completed)
                {
                    sResult = response.Content;
                    CLog.Logger.Write(CLog.EXCEPTION, " Calling BAV API Response::" + sResult);
                }

            }
            catch (Exception exp)
            {

            }
            return sResult;
        }
        public static string CalltoBAVAPIS(string sJsonData)
        {
            CLog.Logger.Write(CLog.EXCEPTION, " Calling BAV API...");
            string sURL = ConfigurationManager.AppSettings["bKashAPI"].ToString();
            var client = new RestClient(sURL);
            string sResult = "";
            var request = new RestRequest(Method.POST);

            try
            {
                CLog.Logger.Write(CLog.EXCEPTION, " Calling BAV API Request::" + sJsonData);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");

                //request.AddParameter("application/json", "{\r\n  \"transaction\": {\r\n    \"mgiTransactionId\": \"" + transaction.transaction.mgiTransactionId + "\",\r\n    \"receiveCountryCode\": \"" + transaction.transaction.receiveCountryCode + "\",\r\n    \"sendCountryCode\": \"" + transaction.transaction.sendCountryCode + "\",\r\n    \"sendAmount\": {\r\n      \"value\": \"" + transaction.transaction.sendAmount.value + "\",\r\n      \"currencyCode\": \"" + transaction.transaction.sendAmount.currencyCode + "\"\r\n    },\r\n    \"receiveAmount\": {\r\n      \"value\": \"" + transaction.transaction.receiveAmount.value + "\",\r\n      \"currencyCode\": \"" + transaction.transaction.receiveAmount.currencyCode + "\"\r\n    },\r\n    \"sender\": {\r\n      \"Person\": {\r\n        \"firstName\": \"" + transaction.transaction.sender.person.firstName + "\",\r\n        \"middleName\": \"\",\r\n        \"lastName\": \"" + transaction.transaction.sender.person.lastName + "\",\r\n        \"SecondLastName\": \"\"\r\n      }\r\n    },\r\n    \"receiver\": {\r\n      \"Person\": {\r\n        \"firstName\": \"" + transaction.transaction.receiver.person.firstName + "\",\r\n        \"middleName\": \"\",\r\n        \"lastName\": \"" + transaction.transaction.receiver.person.lastName + "\",\r\n        \"SecondLastName\": \"\"\r\n      }\r\n    },\r\n    \"accountCode\": \"" + transaction.transaction.accountCode + "\",\r\n    \"accountNumber\": \"" + transaction.transaction.accountNumber + "\",\r\n     \"AdditionalData\": [\r\n     \t{\r\n\t      \t\t\"key\":\"senderAddress\",\r\n\t      \t\t\"value\":\"" + transaction.transaction.sender.person.address + "\",\r\n\t      \t},\r\n\t      \t{\r\n\t      \t\t\"key\":\"senderPhone\",\r\n\t      \t\t\"value\":\"" + transaction.transaction.sender.person.mobileNo + "\",\r\n\t      \t}\r\n     \t]\r\n  }\r\n}\r\n", ParameterType.RequestBody);
                request.AddParameter("application/json", sJsonData, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.ResponseStatus == ResponseStatus.Completed)
                {
                    sResult = response.Content;
                    CLog.Logger.Write(CLog.EXCEPTION, " Calling BAV API Response::" + sResult);
                }

            }
            catch (Exception exp)
            {

            }
            return sResult;
        }

        #region BKashIntegration

        public static BeneficiaryAccount BeneficiaryValidation(BeneficiaryAccount oBeneficiaryAccount)
        {
          

            //Trust all certificates
            System.Net.ServicePointManager.ServerCertificateValidationCallback =((sender, certificate, chain, sslPolicyErrors) => true);
            //Get API Credential
            string sUSER_NAME = Convert.ToString(ConfigurationManager.AppSettings["USER_NAME"]);
            string sPASSWORD = Convert.ToString(ConfigurationManager.AppSettings["PASSWORD"]);
            string InterfaceIDValue = Convert.ToString(ConfigurationManager.AppSettings["interface_id"]);
            //Get API URL
            string sURL=Convert.ToString(ConfigurationManager.AppSettings["cURLAccValidation"]);
            //Request Parameter Value
            string sRequestParamValue = string.Empty;

            //Request Parameter Value for Log
            string sRequestParamValueLog = string.Empty;
            string sUSER_NAMELog = string.Empty;
            string sPASSWORDLog = string.Empty;
            // Response Result
            string sResult = string.Empty;
            //14 digit time stamp. Do not Change the format
            string sTimeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            try

            {

                sRequestParamValue = "{\n\t\"validationRequest\": {\"systemInfo\": {\"userName\": \"" + sUSER_NAME + "\",\n\t\"password\": \"" + sPASSWORD + "\",\"timestamp\": \"" + sTimeStamp + "\"},\"recipientInfo\": {\"countryCode\": \"" + oBeneficiaryAccount.RecipientInfo.CountryCode + "\",\"msisdn\": \"" + oBeneficiaryAccount.RecipientInfo.MSISDN + "\",\"firstName\": \"" + oBeneficiaryAccount.RecipientInfo.FirstName + "\",\"lastName\": \"" + oBeneficiaryAccount.RecipientInfo.LastName + "\",\"fullName\": \"" + oBeneficiaryAccount.RecipientInfo.FullName + "\"}}}";
                sRequestParamValueLog = "{\n\t\"validationRequest\": {\"systemInfo\": {\"userName\": \"" + sUSER_NAMELog + "\",\n\t\"password\": \"" + sPASSWORDLog + "\",\"timestamp\": \"" + sTimeStamp + "\"},\"recipientInfo\": {\"countryCode\": \"" + oBeneficiaryAccount.RecipientInfo.CountryCode + "\",\"msisdn\": \"" + oBeneficiaryAccount.RecipientInfo.MSISDN + "\",\"firstName\": \"" + oBeneficiaryAccount.RecipientInfo.FirstName + "\",\"lastName\": \"" + oBeneficiaryAccount.RecipientInfo.LastName + "\",\"fullName\": \"" + oBeneficiaryAccount.RecipientInfo.FullName + "\"}}}";

                var client = new RestClient(sURL);
                var request = new RestRequest(Method.POST);
               
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("interface_id", InterfaceIDValue);
                request.AddHeader("content-type", "application/json");

                request.AddParameter("application/json", sRequestParamValue, ParameterType.RequestBody);

                //RequestLog
                CLog.Logger.Write(CLog.INFORMATION, "/Request ULR::" + sURL.ToString());
                CLog.Logger.Write(CLog.INFORMATION, "/Request:: BeneficiaryAccount:" + sRequestParamValueLog);
                
                
                IRestResponse response = client.Execute(request);

                if (response.ResponseStatus == ResponseStatus.Completed)
                {
                    sResult = response.Content;
                    //Parse Response 
                    JObject transactionsObject = JObject.Parse(sResult);
                    
                   oBeneficiaryAccount.Response.ResponseCode=transactionsObject["response"]["responseCode"].ToString();
                   oBeneficiaryAccount.Response.ResponseMessage = transactionsObject["response"]["responseMessage"].ToString();
                   //oBeneficiaryAccount.Response.Status = transactionsObject["response"]["rippleCode"].ToString();
                   oBeneficiaryAccount.Response.ConversationID = transactionsObject["response"]["conversationID"].ToString();
                   
                   
                    //Response
                    CLog.Logger.Write(CLog.INFORMATION, "/Response:: BeneficiaryAccount:" + sResult);


                }
                else
                {
                    //Response Error Message
                    CLog.Logger.Write(CLog.INFORMATION, "/ResponseError:: BeneficiaryAccount:" + response.ErrorMessage);
                }
                
                

            }
            catch (Exception exp)
            {

                CLog.Logger.Write(CLog.INFORMATION, "/BeneficiaryValidation::" + exp.ToString());
            }

            return oBeneficiaryAccount;
        }


        public static String sha256_hash(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        public static CRemitTransfer RemitTransferAccount(CRemitTransfer oRemitTransfer)
        {
            
            //Trust all certificates
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
            //Get API Credential
            string sUSER_NAME = Convert.ToString(ConfigurationManager.AppSettings["USER_NAME"]);
            string sPASSWORD = Convert.ToString(ConfigurationManager.AppSettings["PASSWORD"]);
            string InterfaceIDValue = Convert.ToString(ConfigurationManager.AppSettings["interface_id"]);
            //Get API URL
            string sURL = Convert.ToString(ConfigurationManager.AppSettings["cURLRemitTransfer"]);
            //Request Parameter Value
            string sRequestParamValue = string.Empty;
            string sRequestParamValueLog = string.Empty;
            string sUSER_NAMELog = string.Empty;
            string sPASSWORDLog = string.Empty;

            // Response Result
            string sResult = string.Empty;
            //14 digit time stamp. Do not Change the format
            string sTimeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            //SHA256
            string sCheckSum = sha256_hash(sUSER_NAME + oRemitTransfer.TransactionInfo.ConversationID);
            sCheckSum = "null"; // it should be null always


            try
            {
                //SHA256 
               // sRequestParamValue = "{\n\t\"validationRequest\": {\"systemInfo\": {\"userName\": \"" + sUSER_NAME + "\",\n\t\"password\": \"" + sPASSWORD + "\",\"timestamp\": \"" + sTimeStamp + "\"},\"recipientInfo\": {\"countryCode\": \"" + oBeneficiaryAccount.RecipientInfo.CountryCode + "\",\"msisdn\": \"" + oBeneficiaryAccount.RecipientInfo.MSISDN + "\",\"firstName\": \"" + oBeneficiaryAccount.RecipientInfo.FirstName + "\",\"lastName\": \"" + oBeneficiaryAccount.RecipientInfo.LastName + "\",\"fullName\": \"" + oBeneficiaryAccount.RecipientInfo.FullName + "\"}}}";



                sRequestParamValue = "{\r\n\t\"remitRequest\": {\r\n\t\t\"miscellaneousData\": {\r\n\t\t\t\"expiry\": null,\r\n\t\t\t\"message\": \"Message to beneficiary\",\r\n\t\t\t\"payonDate\": null\r\n\t\t},\r\n\t\t\"remitData\": {\r\n\t\t\t\"amount\": \"" + oRemitTransfer.RemitData.Amount + "\",\r\n\t\t\t\"country\": \"" + oRemitTransfer.RemitData.Country + "\",\r\n\t\t\t\"currency\": \"BDT\",\r\n\t\t\t\"forex\": \"\",\r\n\t\t\t\"msisdn\": \"" + oRemitTransfer.RemitData.msisdn + "\"\r\n\t\t},\r\n\t\t\"senderData\": {\r\n\t\t\t\"address\": \"" + oRemitTransfer.SenderInfo.address + "\",\r\n\t\t\t\"dob\": \"\",\r\n\t\t\t\"documentNumber\":\"" + oRemitTransfer.SenderInfo.documentNumber + "\",\r\n\t\t\t\"documentType\": \"" + oRemitTransfer.SenderInfo.documentType + "\",\r\n\t\t\t\"firstName\": \"" + oRemitTransfer.SenderInfo.firstName + "\",\r\n\t\t\t\"kycPurpose\": null,\r\n\t\t\t\"kycSourceOfFunds\": null,\r\n\t\t\t\"lastName\": \"" + oRemitTransfer.SenderInfo.lastName + "\",\r\n\t\t\t\"location\": \"" + oRemitTransfer.SenderInfo.location + "\",\r\n\t\t\t\"msisdn\": \"" + oRemitTransfer.SenderInfo.msisdn + "\",\r\n\t\t\t\"nationality\": \"" + oRemitTransfer.SenderInfo.nationality + "\",\r\n\t\t\t\"pob\": null,\r\n\t\t\t\"idexpiryDate\": null,\r\n\t\t\t\"idissueDate\": null\r\n\t\t},\r\n\t\t\"systemInfo\": {\r\n\t\t\t\"checksum\": " + sCheckSum + ",\r\n\t\t\t\"password\": \"" + sPASSWORD + "\",\r\n\t\t\t\"timestamp\": \"" + sTimeStamp + "\",\r\n\t\t\t\"userName\": \"" + sUSER_NAME + "\"\r\n\t\t},\r\n\t\t\"transactionInfo\": {\r\n\t\t\t\"amount\": \"" + oRemitTransfer.TransactionInfo.Amount + "\",\r\n\t\t\t\"conversationID\": \"" + oRemitTransfer.TransactionInfo.ConversationID + "\",\r\n\t\t\t\"corridor\": \"" + oRemitTransfer.TransactionInfo.Corridor + "\",\r\n\t\t\t\"country\": \"" + oRemitTransfer.TransactionInfo.Country + "\",\r\n\t\t\t\"currency\": \"" + oRemitTransfer.TransactionInfo.Currency + "\",\r\n\t\t\t\"refTxnId\": \"" + oRemitTransfer.TransactionInfo.RefTxnId + "\",\r\n\t\t\t\"txnId\": \"" + oRemitTransfer.TransactionInfo.TxnId + "\",\r\n\t\t\t\"paymentInstrument\": {\r\n\t\t\t\t\"type\": \"1\",\r\n\t\t\t\t\"entity\": \"" + oRemitTransfer.PaymentInstrument.entity + "\",\r\n\t\t\t\t\"number\": null,\r\n\t\t\t\t\"city\": null,\r\n\t\t\t\t\"zipCode\": null\r\n\t\t}\r\n\t\t}\r\n\t}\r\n}";
                sRequestParamValueLog = "{\r\n\t\"remitRequest\": {\r\n\t\t\"miscellaneousData\": {\r\n\t\t\t\"expiry\": null,\r\n\t\t\t\"message\": \"Message to beneficiary\",\r\n\t\t\t\"payonDate\": null\r\n\t\t},\r\n\t\t\"remitData\": {\r\n\t\t\t\"amount\": \"" + oRemitTransfer.RemitData.Amount + "\",\r\n\t\t\t\"country\": \"" + oRemitTransfer.RemitData.Country + "\",\r\n\t\t\t\"currency\": \"BDT\",\r\n\t\t\t\"forex\": \"\",\r\n\t\t\t\"msisdn\": \"" + oRemitTransfer.RemitData.msisdn + "\"\r\n\t\t},\r\n\t\t\"senderData\": {\r\n\t\t\t\"address\": \"" + oRemitTransfer.SenderInfo.address + "\",\r\n\t\t\t\"dob\": \"\",\r\n\t\t\t\"documentNumber\":\"" + oRemitTransfer.SenderInfo.documentNumber + "\",\r\n\t\t\t\"documentType\": \"" + oRemitTransfer.SenderInfo.documentType + "\",\r\n\t\t\t\"firstName\": \"" + oRemitTransfer.SenderInfo.firstName + "\",\r\n\t\t\t\"kycPurpose\": null,\r\n\t\t\t\"kycSourceOfFunds\": null,\r\n\t\t\t\"lastName\": \"" + oRemitTransfer.SenderInfo.lastName + "\",\r\n\t\t\t\"location\": \"" + oRemitTransfer.SenderInfo.location + "\",\r\n\t\t\t\"msisdn\": \"" + oRemitTransfer.SenderInfo.msisdn + "\",\r\n\t\t\t\"nationality\":\"" + oRemitTransfer.SenderInfo.nationality + "\",\r\n\t\t\t\"pob\": null,\r\n\t\t\t\"idexpiryDate\": null,\r\n\t\t\t\"idissueDate\": null\r\n\t\t},\r\n\t\t\"systemInfo\": {\r\n\t\t\t\"checksum\": " + sCheckSum + ",\r\n\t\t\t\"password\": \"" + sPASSWORDLog + "\",\r\n\t\t\t\"timestamp\": \"" + sTimeStamp + "\",\r\n\t\t\t\"userName\": \"" + sUSER_NAMELog + "\"\r\n\t\t},\r\n\t\t\"transactionInfo\": {\r\n\t\t\t\"amount\": \"" + oRemitTransfer.TransactionInfo.Amount + "\",\r\n\t\t\t\"conversationID\": \"" + oRemitTransfer.TransactionInfo.ConversationID + "\",\r\n\t\t\t\"corridor\": \"" + oRemitTransfer.TransactionInfo.Corridor + "\",\r\n\t\t\t\"country\": \"" + oRemitTransfer.TransactionInfo.Country + "\",\r\n\t\t\t\"currency\": \"" + oRemitTransfer.TransactionInfo.Currency + "\",\r\n\t\t\t\"refTxnId\": \"" + oRemitTransfer.TransactionInfo.RefTxnId + "\",\r\n\t\t\t\"txnId\": \"" + oRemitTransfer.TransactionInfo.TxnId + "\",\r\n\t\t\t\"paymentInstrument\": {\r\n\t\t\t\t\"type\": \"1\",\r\n\t\t\t\t\"entity\": \"" + oRemitTransfer.PaymentInstrument.entity + "\",\r\n\t\t\t\t\"number\": null,\r\n\t\t\t\t\"city\": null,\r\n\t\t\t\t\"zipCode\": null\r\n\t\t}\r\n\t\t}\r\n\t}\r\n}";
                 

                var client = new RestClient(sURL);
                var request = new RestRequest(Method.POST);

                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("interface_id", InterfaceIDValue);
                request.AddHeader("content-type", "application/json");

                request.AddParameter("application/json", sRequestParamValue, ParameterType.RequestBody);

                //RequestLog
                CLog.Logger.Write(CLog.INFORMATION, "/Request ULR::" + sURL.ToString());
                CLog.Logger.Write(CLog.INFORMATION, "/Request:: RemitTransferAccount:" + sRequestParamValueLog);


                IRestResponse response = client.Execute(request);

                if (response.ResponseStatus == ResponseStatus.Completed)
                {
                    sResult = response.Content;
                    //Response
                    CLog.Logger.Write(CLog.INFORMATION, "/Response:: RemitTransferAccount:" + sResult);

                    try {

                        //Parse Response 
                        JObject transactionsObject = JObject.Parse(sResult);

                        oRemitTransfer.Response.ResponseCode = transactionsObject["response"]["responseCode"].ToString();
                        oRemitTransfer.Response.ResponseMessage = transactionsObject["response"]["responseMessage"].ToString();
                        oRemitTransfer.Response.ConversationID = transactionsObject["response"]["conversationID"].ToString();

                    
                    }
                    catch (Exception exp)
                    {
                        CLog.Logger.Write(CLog.EXCEPTION, "/RemitTransferAccount:: Error In Parsing::" + exp.ToString());
                    }
                   
                   
                }
                else
                {
                    //Response Error Message
                    CLog.Logger.Write(CLog.INFORMATION, "/ResponseError:: RemitTransferAccount:" + response.ErrorMessage);
                }


            }
            catch (Exception exp)
            {

                CLog.Logger.Write(CLog.EXCEPTION, "/RemitTransferAccount::" + exp.ToString());
            }

            return oRemitTransfer;
        }



        public static CRemitTransfer RemitStatusCheck(CRemitTransfer oRemitTransfer)
        {


            //Trust all certificates
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
            //Get API Credential
            string sUSER_NAME = Convert.ToString(ConfigurationManager.AppSettings["USER_NAME"]);
            string sPASSWORD = Convert.ToString(ConfigurationManager.AppSettings["PASSWORD"]);
            string InterfaceIDValue = Convert.ToString(ConfigurationManager.AppSettings["interface_id"]);
            //Get API URL
            string sURL = Convert.ToString(ConfigurationManager.AppSettings["cURLRemitStatusCheck"]);
            //Request Parameter Value
            string sRequestParamValue = string.Empty;
            
            //LogValue. These variables are used only to write log values
            string sRequestParamValueLog = string.Empty;
            string sUSER_NAMELog= string.Empty;
            string sPASSWORDLog = string.Empty;
            
            // Response Result
            string sResult = string.Empty;
            //14 digit time stamp. Do not Change the format
            string sTimeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");
           
            try
            {

               
                
                sRequestParamValue = "{\r\n\t\"statusQueryRequest\": {\r\n\t\t\"systemInfo\": {\r\n\t\t\t\"userName\": \"" + sUSER_NAME + "\",\r\n\t\t\t\"password\": \"" + sPASSWORD + "\",\r\n\t\t\t\"timestamp\": \"" + sTimeStamp + "\"\r\n\t\t},\r\n\t\t\"transferStatusQueryRequest\": {\r\n\t\t\t\"txnId\": \""+oRemitTransfer.TransactionInfo.TxnId+"\",\r\n\t\t\t\"conversationID\": \""+oRemitTransfer.TransactionInfo.ConversationID+"\"\r\n\t\t}\r\n\t}\r\n}";
                //Log Value
                sRequestParamValueLog = "{\r\n\t\"statusQueryRequest\": {\r\n\t\t\"systemInfo\": {\r\n\t\t\t\"userName\": \"" + sUSER_NAMELog + "\",\r\n\t\t\t\"password\": \"" + sPASSWORDLog + "\",\r\n\t\t\t\"timestamp\": \"" + sTimeStamp + "\"\r\n\t\t},\r\n\t\t\"transferStatusQueryRequest\": {\r\n\t\t\t\"txnId\": \"" + oRemitTransfer.TransactionInfo.TxnId + "\",\r\n\t\t\t\"conversationID\": \"" + oRemitTransfer.TransactionInfo.ConversationID + "\"\r\n\t\t}\r\n\t}\r\n}";


                var client = new RestClient(sURL);
                var request = new RestRequest(Method.POST);

                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("interface_id", InterfaceIDValue);
                request.AddHeader("content-type", "application/json");

                request.AddParameter("application/json", sRequestParamValue, ParameterType.RequestBody);

                //RequestLog
                CLog.Logger.Write(CLog.INFORMATION, "/Request ULR::" + sURL.ToString());
                CLog.Logger.Write(CLog.INFORMATION, "/Request:: RemitStatusCheck:" + sRequestParamValueLog);


                IRestResponse response = client.Execute(request);

                if (response.ResponseStatus == ResponseStatus.Completed)
                {
                    sResult = response.Content;
                    //Parse Response 
                    JObject transactionsObject = JObject.Parse(sResult);

                    oRemitTransfer.Response.ResponseCode = transactionsObject["response"]["responseCode"].ToString();
                    oRemitTransfer.Response.ResponseMessage = transactionsObject["response"]["responseMessage"].ToString();
                    oRemitTransfer.Response.ConversationID = transactionsObject["response"]["conversationID"].ToString();


                    //Response
                    CLog.Logger.Write(CLog.INFORMATION, "/Response:: RemitStatusCheck:" + sResult);


                }
                else
                {
                    //Response Error Message
                    CLog.Logger.Write(CLog.INFORMATION, "/ResponseError:: RemitStatusCheck:" + response.ErrorMessage);
                }



            }
            catch (Exception exp)
            {

                CLog.Logger.Write(CLog.INFORMATION, "/Exception-RemitStatusCheck::" + exp.ToString());
            }

            return oRemitTransfer;
        }

        #endregion BKashIntegration
    }
}
