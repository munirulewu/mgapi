/*
 * File name            : CAccDeposit.cs
 * Author               : Munirul Islam
 * Date                 : June 13, 2022
 * Version              : 1.0
 *
 * Description          : Result Class Entity 
 *
 * Modification history :
 * Name                         Date                            Desc
 * 
 * 
 * Copyright (c) 2022: SOCIAL ISLAMI BANK LIMITED
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIBLCommon.SIBLCommon.Common.Entity.ITCL
{
   public  class CAccDeposit
    {

       string ms_SIBLtransactionNo;
       string ms_mgiTransactionId;
       string ms_Amount;
        string ms_keyID;

        string ms_userName;
        string ms_password;
        string ms_bankCode;

        string ms_extId;
        string ms_userId;
        string ms_bankName;

        string ms_fullPan;
        string ms_provider;
        string ms_termName;
        string ms_sourceOfFund;
        
       string ms_comment;

       public CAccDeposit()
       {
           ms_keyID = string.Empty;
           ms_userName = string.Empty;
           ms_password = string.Empty;
           ms_bankCode = string.Empty;

           ms_extId = string.Empty;
           ms_userId = string.Empty;
           ms_bankName = string.Empty;

           ms_fullPan = string.Empty;
           ms_provider = string.Empty;
           ms_termName = string.Empty;
           ms_sourceOfFund = string.Empty;

           ms_comment = string.Empty;
           ms_SIBLtransactionNo = string.Empty;
           ms_mgiTransactionId = string.Empty;
           ms_Amount = string.Empty;
       }

       public string Amount
       {
           get { return ms_Amount; }
           set { ms_Amount = value; }
       }

       public string SIBLtransactionNo
       {
           get { return ms_SIBLtransactionNo; }
           set { ms_SIBLtransactionNo = value; }
       }
       public string mgiTransactionId
       {
           get { return ms_mgiTransactionId; }
           set { ms_mgiTransactionId = value; }
       }

       public string keyID
       {
           get { return ms_keyID; }
           set { ms_keyID = value; }
       }
       public string userName
       {
           get { return ms_userName; }
           set { ms_userName = value; }
       }
       public string password
       {
           get { return ms_password; }
           set { ms_password = value; }
       }
       public string bankCode
       {
           get { return ms_bankCode; }
           set { ms_bankCode = value; }
       }

       public string extId
       {
           get { return ms_extId; }
           set { ms_extId = value; }
       }

       public string userId
       {
           get { return ms_userId; }
           set { ms_userId = value; }
       }
       public string bankName
       {
           get { return ms_bankName; }
           set { ms_bankName = value; }
       }
       public string fullPan
       {
           get { return ms_fullPan; }
           set { ms_fullPan = value; }
       }
       public string provider
       {
           get { return ms_provider; }
           set { ms_provider = value; }
       }

       public string termName
       {
           get { return ms_termName; }
           set { ms_termName = value; }
       }

       public string sourceOfFund
       {
           get { return ms_sourceOfFund; }
           set { ms_sourceOfFund = value; }
       }
       public string comment
       {
           get { return ms_comment; }
           set { ms_comment = value; }
       }
    }
}
