using FluentValidation.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
//using SIBLBKash.Common;
using SIBLCommon.Common.Entity.Result;
using SIBLCommon.Common.Util.Logger;
using SIBLCommon.SIBLCommon.Common.Entity.bKash;
using SIBLCommon.SIBLCommon.Common.Entity.MGAPI;
using SIBLMGAPI.Models;
using SIBLRemitDA.SIBLRemit.DA.MGAPIDA;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace SIBLMGAPI.Controllers
{
     [BasicAuthentication]
    public class TransValidationController : ApiController
    {
        //[HttpPost]
        //public string TransactionValidation_old(dynamic jsonData)
        //{
        //    CLog.Logger.Write(CLog.INFORMATION, " TransactionValidation API Request before parsing:" + jsonData.ToString());
        //    CResult oResult = new CResult();
        //    string sVal = jsonData.ToString();
        //    CTransaction transaction = new CTransaction();
        //    CTransactionValidation oTransactionValidation = new CTransactionValidation();
        //    transaction = JsonConvert.DeserializeObject<CTransaction>(jsonData.ToString());
        //    BeneficiaryAccount oAccount = new BeneficiaryAccount();
        //    string output = string.Empty;
        //    string sPlusSign = "+";
        //    try
        //    {
        //        if (transaction.transaction.accountCode.ToLower().Equals("bkash"))
        //        {
        //            #region
        //            // Bkash Transaction

        //            ValidationResult result = oTransactionValidation.Validate(transaction);
        //            if (result.IsValid)
        //            {
        //                CLog.Logger.Write(CLog.INFORMATION, " TransactionValidation API Validate:True" );
        //                foreach (CAdditionData oAdditionalData in transaction.transaction.additionalData)
        //                {
        //                    if (oAdditionalData.key.Contains("senderPhone"))
        //                        transaction.transaction.sender.person.mobileNo = oAdditionalData.value;
        //                    else if (oAdditionalData.key.Contains("senderAddress"))
        //                        transaction.transaction.sender.person.address = oAdditionalData.value;
        //                }

        //                output = CUIUtil2.CalltoBAVAPIS(sVal);

        //            }
        //            else
        //            {
        //                CLog.Logger.Write(CLog.INFORMATION, " TransactionValidation API Validate:False");
        //                //Invalid Request
        //                CErrorResponse oErrResponse = new CErrorResponse();
        //                foreach (var failure in result.Errors)
        //                {
                            
        //                    oErrResponse.error.code = failure.ErrorCode;
        //                    oErrResponse.error.message = failure.ErrorMessage;

        //                }
        //                output = JsonConvert.SerializeObject(oErrResponse, Formatting.Indented);
        //                CLog.Logger.Write(CLog.ERROR, "TransactionValidation API  ERROR Response:" + output);
                      
        //            }

                  
        //            #endregion
        //        }
        //        else
        //        { 
        //            //Other Transaction
        //        }

        //        return output;
        //    }
        //    catch (Exception exp)
        //    {
                
        //        CLog.Logger.Write(CLog.EXCEPTION, " API  Exception::" + exp.ToString());
        //    }
        //    return output;
        //}

          [HttpPost]
        public JObject TransactionValidation(dynamic jsonData)
        {
            CLog.Logger.Write(CLog.INFORMATION, " TransactionValidation API Request before parsing:" + jsonData.ToString());
           
            CbKashValidationRequest transaction = new CbKashValidationRequest();
          
            transaction = JsonConvert.DeserializeObject<CbKashValidationRequest>(jsonData.ToString());

            string output = string.Empty;
            JObject jsonObject = new JObject();
            try
            {
                if (transaction.accountCode.ToLower().Equals("bkash"))
                {
                   output= bKashValidation(transaction);
                }
                else
                {
                    //Other Transaction
                }

                //return output;
            }
            catch (Exception exp)
            {

                CLog.Logger.Write(CLog.EXCEPTION, " API  Exception::" + exp.ToString());
            }
            jsonObject = JObject.Parse(output);
            return jsonObject;
        }

          private string bKashValidation(CbKashValidationRequest transaction)
          {
              string output = "";
              string outputLog = "";
              CResult oResultSave = new CResult();
              CResult oResultCallBack = new CResult();
              CbKashTransValidation oTransactionValidation = new CbKashTransValidation();
              JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
              try {

                  #region
                  // Bkash Transaction

                  ValidationResult result = oTransactionValidation.Validate(transaction);
                  if (result.IsValid)
                  {

                      foreach (CAdditionData oAdditionalData in transaction.additionalData)
                      {
                          if (oAdditionalData.key.Contains("senderPhone"))
                              transaction.sender.person.mobileNo = oAdditionalData.value;
                          else if (oAdditionalData.key.Contains("senderAddress"))
                              transaction.sender.person.address = oAdditionalData.value;
                      }

                      outputLog = JsonConvert.SerializeObject(transaction, Formatting.Indented);
                      CLog.Logger.Write(CLog.INFORMATION, " TransactionLoad API  Request::" + outputLog);
                      CMGAPIDA oDA = new CMGAPIDA();
                      transaction.operationType = "ADD";
                      oResultSave = oDA.CRUD_bKashValidation(transaction);
                      if (oResultSave.Result == true)
                      {
                          transaction = (CbKashValidationRequest)oResultSave.Return;
                      }

                      try
                      {
                          CLog.Logger.Write(CLog.ERROR, "bKashValidation GetbKashCalBackResult:calling");
                          oResultCallBack = oDA.GetbKashCalBackResult(transaction);
                      }
                      catch (Exception exp2)
                      {
                          CLog.Logger.Write(CLog.ERROR, "bKashValidation GetbKashCallbackexception:" +exp2.ToString());
                      }


                      CMgBkashSuccess oSuccessResponse = new CMgBkashSuccess();
                      if (oResultCallBack.Result == true)
                      {
                          oSuccessResponse = (CMgBkashSuccess)oResultCallBack.Return;
                          outputLog = JsonConvert.SerializeObject(oSuccessResponse, Formatting.Indented);
                          output = JsonConvert.SerializeObject(oSuccessResponse);
                          output = jsSerializer.Serialize(oSuccessResponse);
                          CLog.Logger.Write(CLog.INFORMATION, "TransactionValidation API  Response::" + outputLog);
                      }
                      else
                      {
                          CErrorResponse oError = new CErrorResponse();
                          oError = (CErrorResponse)oResultCallBack.Return;
                          outputLog = JsonConvert.SerializeObject(oError, Formatting.Indented);
                          output = JsonConvert.SerializeObject(oError);
                          CLog.Logger.Write(CLog.INFORMATION, "TransactionValidation API  Response::" + outputLog);
                      }


                  }
                  else
                  {
                      //Invalid Request
                      CErrorResponse oErrResponse = new CErrorResponse();
                      foreach (var failure in result.Errors)
                      {

                          oErrResponse.error.code = failure.ErrorCode;
                          oErrResponse.error.message = failure.ErrorMessage;

                      }
                      outputLog = JsonConvert.SerializeObject(oErrResponse, Formatting.Indented);
                      output = JsonConvert.SerializeObject(oErrResponse);

                     
                      output = jsSerializer.Serialize(oErrResponse);

                      CLog.Logger.Write(CLog.ERROR, "TransactionValidation API  ERROR Response:" + outputLog);

                  }


                  #endregion
              
              }
              catch (Exception exp)
              {
                  CLog.Logger.Write(CLog.EXCEPTION, " Function:bKashValidation::" + exp.ToString());
              }

              return output;
          }
    }
}
