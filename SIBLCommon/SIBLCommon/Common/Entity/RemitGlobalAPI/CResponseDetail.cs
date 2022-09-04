/*
 * File name            : CResponse.cs
 * Author               : Munirul Islam
 * Date                 : April 23, 2020
 * Version              : 1.0
 *
 * Description          : Entity Class for CResponse
 *
 * Modification history :
 * Name                         Date                            Desc
 * 
 * 
 * Copyright (c) 2020: SOCIAL ISLAMI BANK LIMITED
 */

using SIBLCommon.Common.Entity.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SIBLCommon.SIBLCommon.Common.Entity.RemitGlobalAPI
{
     [Serializable]
     [DataContract]
    public class CResponseDetail
    {
         
         #region Protectd Member

         protected string m_sresponseCode;
         protected string m_sresponseMessage;

         protected string ms_TransactionAmount;
         protected string ms_TransactionAccountNo;
         protected string ms_TransactionRefTxnId;
         protected string ms_TransactionTxnIdSIBL;
         protected string ms_TransactionCountry;
         protected string ms_TransactionCurrency;
         protected string ms_TransactionBankName;
         protected string ms_TransactionBranchName;
         protected string ms_TransactionDistrict;
         protected string ms_TransactionRoutingNumber;
         private CurrentTransactionType _TransactionType;

         protected string ms_SenderFirstName;
         protected string ms_SenderLastName;
         protected string ms_SenderAddress;
         protected string ms_SenderDob;
         protected string ms_SenderDocumentNumber;
         protected string ms_SenderLocation;
         protected string ms_SenderMsisdn;
         protected string ms_SenderNationality;
         protected string ms_SenderPob;
         protected string ms_SenderIdExpiryDate;
         protected string ms_SenderIdIssueDate;
         private CurrentKycSourceOfFund? _SenderKycSourceOfFund;
         private CurrentKycPurpose? _SenderKycPurpose;
         private CurrentDocumentType _SenderDocumentType;

         protected string ms_RecipientMSISDN;
         protected string ms_RecipientFirstName;
         protected string ms_RecipientLastName;
         protected string ms_RecipientCountryCode;
         protected string ms_RecipientFullName;
         protected string ms_RecipientLocation;
         protected string ms_RecipientIdNo;
         private CurrentIdType? _RecipientIdType;
         protected string ms_RecipientIdIssueDate;
         protected string ms_RecipientIdExpiryDate;

         
        #endregion


        #region Constructor
         public CResponseDetail()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_sresponseCode = string.Empty;
            m_sresponseMessage = string.Empty;
                        
            ms_TransactionAmount = string.Empty;
            ms_TransactionAccountNo = string.Empty;
            ms_TransactionRefTxnId = string.Empty;
            ms_TransactionTxnIdSIBL = string.Empty;
            ms_TransactionCountry = string.Empty;
            ms_TransactionCurrency = string.Empty;
            ms_TransactionBankName = string.Empty;
            ms_TransactionBranchName = string.Empty;
            ms_TransactionDistrict = string.Empty;
            ms_TransactionRoutingNumber = string.Empty;
            
            ms_SenderFirstName = string.Empty;
            ms_SenderLastName = string.Empty;
            ms_SenderAddress = string.Empty;
            ms_SenderDob = string.Empty;
            ms_SenderDocumentNumber = string.Empty;
            ms_SenderLocation = string.Empty;
            ms_SenderMsisdn = string.Empty;
            ms_SenderNationality = string.Empty;
            ms_SenderPob = string.Empty;
            ms_SenderIdExpiryDate = string.Empty;
            ms_SenderIdIssueDate = string.Empty;

            ms_RecipientMSISDN = string.Empty;
            ms_RecipientFirstName = string.Empty;
            ms_RecipientLastName = string.Empty;
            ms_RecipientCountryCode = string.Empty;
            ms_RecipientFullName = string.Empty;
            ms_RecipientLocation = string.Empty;
            ms_RecipientIdNo = string.Empty;
            ms_RecipientIdIssueDate = string.Empty;
            ms_RecipientIdExpiryDate = string.Empty;

        }
        #endregion Initialization
       
        #region public Member
       
       
         [DataMember]
         [Display(Order = 1)]
        public string ResponseCode
        {
            get { return m_sresponseCode; }
            set { m_sresponseCode = value; }
        }
         [DataMember]
         [Display(Order = 2)]
        public string ResponseMessage
        {
            get { return m_sresponseMessage; }
            set { m_sresponseMessage = value; }
        }

        
         [DataMember]
         [Display(Order = 3)]
         public string TxnIdSIBL
         {
             get { return ms_TransactionTxnIdSIBL; }
             set { ms_TransactionTxnIdSIBL = value; }
         }

         [DataMember]
         [Display(Order = 4)]
         public string RefTxnId
         {
             get { return ms_TransactionRefTxnId; }
             set { ms_TransactionRefTxnId = value; }
         }

         [DataMember]
         public string TransactionAmount
         {
             get { return ms_TransactionAmount; }
             set { ms_TransactionAmount = value; }
         }

         [DataMember]
         public string TransactionAccountNo
         {
             get { return ms_TransactionAccountNo; }
             set { ms_TransactionAccountNo = value; }
         }

         [DataMember]
         public string TransactionCountry
         {
             get { return ms_TransactionCountry; }
             set { ms_TransactionCountry = value; }
         }

         [DataMember]
         public string TransactionCurrency
         {
             get { return ms_TransactionCurrency; }
             set { ms_TransactionCurrency = value; }
         }
         [DataMember]
         public string TransactionBankName
         {
             get { return ms_TransactionBankName; }
             set { ms_TransactionBankName = value; }
         }
         [DataMember]
         public string TransactionBranchName
         {
             get { return ms_TransactionBranchName; }
             set { ms_TransactionBranchName = value; }
         }
         [DataMember]
         public string TransactionDistrict
         {
             get { return ms_TransactionDistrict; }
             set { ms_TransactionDistrict = value; }
         }
         [DataMember]
         public string TransactionRoutingNumber
         {
             get { return ms_TransactionRoutingNumber; }
             set { ms_TransactionRoutingNumber = value; }
         }
         [DataMember]
         public CurrentTransactionType TransactionType
         {
             get { return _TransactionType; }
             set { _TransactionType = value; }
         }
         


         [DataMember]
         public string SenderFirstName
         {
             get { return ms_SenderFirstName; }
             set { ms_SenderFirstName = value; }
         }

         [DataMember]
         public string SenderLastName
         {
             get { return ms_SenderLastName; }
             set { ms_SenderLastName = value; }
         }

         [DataMember]
         public string SenderAddress
         {
             get { return ms_SenderAddress; }
             set { ms_SenderAddress = value; }
         }
         [DataMember]
         public string SenderDob
         {
             get { return ms_SenderDob; }
             set { ms_SenderDob = value; }
         }
         [DataMember]
         public string SenderDocumentNumber
         {
             get { return ms_SenderDocumentNumber; }
             set { ms_SenderDocumentNumber = value; }
         }                  
         [DataMember]
         public string SenderLocation
         {
             get { return ms_SenderLocation; }
             set { ms_SenderLocation = value; }
         }
         [DataMember]
         public string SenderContactNo
         {
             get { return ms_SenderMsisdn; }
             set { ms_SenderMsisdn = value; }
         }
         [DataMember]
         public string SenderNationality
         {
             get { return ms_SenderNationality; }
             set { ms_SenderNationality = value; }
         }
         [DataMember]
         public string SenderPob
         {
             get { return ms_SenderPob; }
             set { ms_SenderPob = value; }
         }
         [DataMember]
         public string SenderIdExpiryDate
         {
             get { return ms_SenderIdExpiryDate; }
             set { ms_SenderIdExpiryDate = value; }
         }
         [DataMember]
         public string SenderIdIssueDate
         {
             get { return ms_SenderIdIssueDate; }
             set { ms_SenderIdIssueDate = value; }
         }
         [DataMember]
         public CurrentDocumentType SenderDocumentType
         {
             get { return _SenderDocumentType; }
             set { _SenderDocumentType = value; }
         }
         [DataMember]
         public CurrentKycSourceOfFund? SenderKycSourceOfFund
         {
             get { return _SenderKycSourceOfFund; }
             set { _SenderKycSourceOfFund = value; }
         }
         [DataMember]
         public CurrentKycPurpose? SenderKycPurpose
         {
             get { return _SenderKycPurpose; }
             set { _SenderKycPurpose = value; }
         }


         [DataMember]
         public string RecipientContactNo
         {
             get { return ms_RecipientMSISDN; }
             set { ms_RecipientMSISDN = value; }
         }

         [DataMember]
         public string RecipientFirstName
         {
             get { return ms_RecipientFirstName; }
             set { ms_RecipientFirstName = value; }
         }

         [DataMember]
         public string RecipientLastName
         {
             get { return ms_RecipientLastName; }
             set { ms_RecipientLastName = value; }
         }         

         [DataMember]
         public string RecipientCountryCode
         {
             get { return ms_RecipientCountryCode; }
             set { ms_RecipientCountryCode = value; }
         }
         [DataMember]
         public string RecipientFullName
         {
             get { return ms_RecipientFullName; }
             set { ms_RecipientFullName = value; }
         }
        
         [DataMember]
         public string RecipientLocation
         {
             get { return ms_RecipientLocation; }
             set { ms_RecipientLocation = value; }
         }
         [DataMember]
         public string RecipientIdNo
         {
             get { return ms_RecipientIdNo; }
             set { ms_RecipientIdNo = value; }
         }
         [DataMember]
         public CurrentIdType? RecipientIdType
         {
             get { return _RecipientIdType; }
             set { _RecipientIdType = value; }
         }
         [DataMember]
         public string RecipientIdIssueDate
         {
             get { return ms_RecipientIdIssueDate; }
             set { ms_RecipientIdIssueDate = value; }
         }
         [DataMember]
         public string RecipientIdExpiryDate
         {
             get { return ms_RecipientIdExpiryDate; }
             set { ms_RecipientIdExpiryDate = value; }
         }
         


        #endregion     
    }
}
