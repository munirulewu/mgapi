/*
 * File name            :  CInstruction
 * Author               :  Md. Aminul Islam
 * Date                 :  27.05.2015
 *
 * Description          :  
 *
 * Modification history :
 * Name                         Date                            Desc
 *           
 * 
 * Copyright (c)  2015: Social Islami Bank Limited
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;
using SIBLXoomCommon.SIBLXoom.Common.Entity.Disbursement;
using SIBLXoomCommon.SIBLXoom.Common.Entity.CPU;

namespace SIBLCommon.Common.Entity.Disbursement
{
    [Serializable]
    public class CInstruction : ASIBLEntityBase
    {
        #region Protectd Member
        protected string m_sInstructionId;
        protected string m_sInstructionDateTime;
        protected string m_oInstructionType;
        protected string m_sXoomTrackingNumber;
        protected CRecipientBankDetails m_oRecipientBankDetails;
        protected CDisbursementType m_oDisbursementType;
        protected CMapModeller m_oMapModeller;
        protected string m_sPartnerReference;
        protected CDeliveryRecipient m_oDeliveryRecipient;
        protected string m_sOperationType;
        protected string m_sInstructionACK;
        protected string m_sCBSStatus;
        protected string m_sFinalStatus;
        protected string m_sCBSUpdateStatus;
        protected string m_sCBSUpdateReason;
        protected string m_sFromDate;
        protected string m_sToDate;       
        protected string m_sIssueDate;
        protected string m_sBEFTNGenerated;
        protected string m_sBEFTNTime;
        protected string m_sPaymentDate;
        protected string m_sPaymentMode;
        protected string m_sComments;
        protected CAbabilTransactionResponse m_oAbabilTransactionResponse;
        protected CSenderInformation m_oSenderInformation;
        #endregion


        #region Constructor
        public CInstruction()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_sInstructionId = string.Empty;
            m_sInstructionDateTime = string.Empty;
            m_oInstructionType = string.Empty;
            m_sXoomTrackingNumber = string.Empty;
            m_oRecipientBankDetails = new CRecipientBankDetails();
            m_oDisbursementType = new CDisbursementType();
            m_oMapModeller = new CMapModeller();
            m_sPartnerReference = string.Empty;
            m_oDeliveryRecipient = new CDeliveryRecipient();
            m_sOperationType = string.Empty;
            m_sInstructionACK = string.Empty;
            m_sCBSStatus = string.Empty;
            m_sFinalStatus = string.Empty;
            m_sCBSUpdateStatus = string.Empty;
            m_sCBSUpdateReason = string.Empty;
            m_sFromDate = string.Empty;
            m_sToDate = string.Empty;
            m_sIssueDate = string.Empty;
            m_sBEFTNGenerated = string.Empty;
            m_sBEFTNTime = string.Empty;
            m_sPaymentDate = string.Empty;
            m_sPaymentMode = string.Empty;
            m_sComments = string.Empty;
            m_oAbabilTransactionResponse = new CAbabilTransactionResponse();
            m_oSenderInformation = new CSenderInformation();
           
        }
    
        #endregion Initialization

        #region public Member
        public string InstructionACK
        {
            get { return m_sInstructionACK; }
            set { m_sInstructionACK = value; }
        }
        public string CBSStatus
        {
            get { return m_sCBSStatus; }
            set { m_sCBSStatus = value; }
        }

        public string FinalStatus
        {
            get { return m_sFinalStatus; }
            set { m_sFinalStatus = value; }
        }
        public string InstructionId
        {
            get { return m_sInstructionId; }
            set { m_sInstructionId = value; }
        }

        public string InstructionDateTime
        {
            get { return m_sInstructionDateTime; }
            set { m_sInstructionDateTime = value; }
        }

        public string InstructionType
        {
            get { return m_oInstructionType; }
            set { m_oInstructionType = value; }
        }

        public string XoomTrackingNumber
        {
            get { return m_sXoomTrackingNumber; }
            set { m_sXoomTrackingNumber = value; }
        }

        public CRecipientBankDetails RecipientBankDetails
        {
            get { return m_oRecipientBankDetails; }
            set { m_oRecipientBankDetails = value; }
        }

        public CDisbursementType DisbursementType
        {
            get { return m_oDisbursementType; }
            set { m_oDisbursementType = value; }
        }

        public CMapModeller MapModeller
        {
            get { return m_oMapModeller; }
            set { m_oMapModeller = value; }
        }

        public string PartnerReference
        {
            get { return m_sPartnerReference; }
            set { m_sPartnerReference = value; }
        }


        public CDeliveryRecipient DeliveryRecipient
        {
            get { return m_oDeliveryRecipient; }
            set { m_oDeliveryRecipient = value; }
        }
        public string OperationType
        {
            get { return m_sOperationType; }
            set { m_sOperationType = value; }
        }

        public string CBSUpdateStatus
        {
            get { return m_sCBSUpdateStatus; }
            set { m_sCBSUpdateStatus = value; }
        }

        public string CBSUpdateReason
        {
            get { return m_sCBSUpdateReason; }
            set { m_sCBSUpdateReason = value; }
        }

         public string FromDate
        {
            get { return m_sFromDate; }
            set { m_sFromDate = value; }
        }

         public string ToDate
        {
            get { return m_sToDate; }
            set { m_sToDate = value; }
        }

         public string IssueDate
        {
            get { return m_sIssueDate; }
            set { m_sIssueDate = value; }
        }
         public string BEFTNGenerated
        {
            get { return m_sBEFTNGenerated; }
            set { m_sBEFTNGenerated = value; }
        }
         public string BEFTNTime
        {
            get { return m_sBEFTNTime; }
            set { m_sBEFTNTime = value; }
        }
         public string PaymentDate
        {
            get { return m_sPaymentDate; }
            set { m_sPaymentDate = value; }
        }

         public string PaymentMode
        {
            get { return m_sPaymentMode; }
            set { m_sPaymentMode = value; }
        }

         public string Comments
        {
            get { return m_sComments; }
            set { m_sComments = value; }
        }

         public CAbabilTransactionResponse AbabilTransactionResponse
         {
             get { return m_oAbabilTransactionResponse; }
             set { m_oAbabilTransactionResponse = value; }
         }

         public CSenderInformation SenderInformation
         {
             get { return m_oSenderInformation; }
             set { m_oSenderInformation = value; }
         }
         
        #endregion
    }
}
