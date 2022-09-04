using Newtonsoft.Json;
using SIBLCommon.Common.Entity.Result;
using SIBLCommon.Common.Util.Logger;
using SIBLCommon.SIBLCommon.Common.Entity.MGAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using SIBLRemit.DA.SIBLRemit;
//using SIBLRemitDA.SIBLRemit.DA.MGAPIDA;
using System.Text;
using SIBLMGAPI.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using FluentValidation.Results;
using SIBLRemitDA.SIBLRemit.DA.MGAPIDA;



namespace SIBLMGAPI.Controllers
{
    [BasicAuthentication]
    public class TransLoadController : ApiController
    {
        [HttpPost]
        public IHttpActionResult TransactionLoad(dynamic jsonData)
        {

            CLog.Logger.Write(CLog.INFORMATION, " TransactionLoad API Request before parsing:" + jsonData.ToString());
            CResult oResult = new CResult();

            CTransaction transaction = new CTransaction();
            CTransactionValidation oTransactionValidation = new CTransactionValidation();
         

            string output = string.Empty;
            string outputLog = string.Empty;
            string totlaMessage = "";
            JObject jsonObject = new JObject();
            try
            {
                transaction = JsonConvert.DeserializeObject<CTransaction>(jsonData.ToString());
                ValidationResult result = oTransactionValidation.Validate(transaction);
                if (result.IsValid)
                {

                    foreach (CAdditionData oAdditionalData in transaction.transaction.additionalData)
                    {
                        if (oAdditionalData.key.Contains("senderPhone"))
                            transaction.transaction.sender.person.mobileNo = oAdditionalData.value;
                        else if (oAdditionalData.key.Contains("senderAddress"))
                            transaction.transaction.sender.person.address = oAdditionalData.value;
                    }

                    //output = JsonConvert.SerializeObject(transaction, Formatting.Indented);
                    //CLog.Logger.Write(CLog.INFORMATION, " TransactionLoad API  Request::" + output);
                    CMGAPIDA oDA = new CMGAPIDA();
                    transaction.transaction.operationType = "ADD";
                    oResult = oDA.CRUD_Transaction(transaction);
                    CSuccessResponse oSuccessResponse = new CSuccessResponse();
                    if (oResult.Result == true)
                    {
                        oSuccessResponse = (CSuccessResponse)oResult.Return;
                        output = JsonConvert.SerializeObject(oSuccessResponse);
                        
                        outputLog = JsonConvert.SerializeObject(oSuccessResponse, Formatting.Indented);
                        CLog.Logger.Write(CLog.INFORMATION, "TransactionLoad API  Response::" + outputLog);
                        //CITCLDA oDAITCL = new CITCLDA();
                        //string sIBLTransactionId = oSuccessResponse.partnerTransactionId;
                        //oDAITCL.DoNPSBTransaction();
                    }
                    else
                    {
                        oSuccessResponse = (CSuccessResponse)oResult.Return;
                        output = JsonConvert.SerializeObject(oSuccessResponse);
                      
                        outputLog = JsonConvert.SerializeObject(oSuccessResponse, Formatting.Indented);
                        CLog.Logger.Write(CLog.INFORMATION, "TransactionLoad API  Response::" + outputLog);
                    }

                    jsonObject = JObject.Parse(output);
                    //return JObject.Parse(output);
                    return Ok(jsonObject);

                }
                else
                {
                   
                    CErrorResponse oErrResponse = new CErrorResponse();
                    foreach (var failure in result.Errors)
                    {
                        totlaMessage = totlaMessage + " Code: "+failure.ErrorCode + " Message: "+failure.ErrorMessage;
                        oErrResponse.error.code = failure.ErrorCode;
                        oErrResponse.error.message = failure.ErrorMessage;
                       
                    }
                    output = JsonConvert.SerializeObject(oErrResponse);
                    var ss=JObject.Parse(output);
                    outputLog = JsonConvert.SerializeObject(oErrResponse, Formatting.Indented);
                    CLog.Logger.Write(CLog.ERROR, "TransactionLoad API  ERROR Response:" + outputLog);
                    jsonObject = JObject.Parse(output);
                    return Ok(jsonObject);
                  //  return BadRequest(oErrResponse.error.message);  
                }
               
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, " API  Exception::" + exp.ToString());
            }
            jsonObject = JObject.Parse(output);
            return  Ok( jsonObject);
        }

    }
}
