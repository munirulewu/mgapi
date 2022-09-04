/*
 * File name            : CSIBLRemitPay.cs
 * Author               : Munirul Islam
 * Date                 : January 21.2020
 * Version              : 1.0
 *
 * Description          : Entity Class for SIBLRemitPayment
 *
 * Modification history :
 * Name                         Date                            Desc
 * 
 * 
 * Copyright (c) 2020: SOCIAL ISLAMI BANK LIMITED
 */

using SIBLCommon.Common.Entity.AllLookup;
using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.Common.Entity.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIBLCommon.SIBLCommon.Common.Entity.SIBLRemitPay
{
    [Serializable]
    public class CSIBLRemitPay : ASIBLEntityBase
    {
        #region Protectd Member


        protected string ms_ENTRYDATE;
        protected CUser mo_CREATEBY;
        protected CAllLookup mo_COMPANYID;
        protected CRemiAccount mo_RemiAccount;
        protected string ms_BENEFICIARY_NAME;
        protected string ms_PINNO;
        protected string ms_AMOUNT;
        protected string ms_INCENTIVE_AMOUNT;
        protected string ms_REMARKS;
        protected string ms_RNIDNO;
        protected string ms_RNIDISSUEDATE;
        protected string ms_RNIDEXPIRYDATE;
        protected string ms_RNIDTYPE;
        protected string ms_PAYOUTBY;
        protected string ms_PAYOUTDATE;
        protected string ms_BENEFICIARYADDRESS;
        protected string ms_SENDERNAME;
        protected string ms_SENDERCOUNTRY;
        protected string ms_SENDERPASSPORTNO;
        protected string ms_SENDERBUSINESSLICENSE;
        protected string ms_SENDERIDTYPE;
        protected string ms_SENDERIDTYPENO;
        protected string ms_DOCAPPROVED;
        protected string ms_DOCUMENTAPPROVEDATE;
        protected string ms_ROUTINGNO;
        protected string ms_CBSMStatus;
        protected string ms_CBSMDate;
        protected string ms_CBSIStatus;
        protected string ms_CBSIDate;
        protected string ms_TransactionApproved;
        protected string ms_ReceiverPhoneNo;
        protected string ms_PaymentType;
        protected string ms_AgentID;
        protected string ms_AgentName;
        protected string ms_ReportType;
        protected string ms_TransactionType;
        
        protected string ms_SenderForeign;
        protected string ms_RemitOnly;
        protected string ms_MainRemitDirverse;

        protected string ms_AppRequestId;

        protected string ms_AppRequestIdIncentive;
        protected string ms_AppRequestIdMainRemit;
        #endregion

        #region Constructor

        public CSIBLRemitPay()
            : base()
        {
            Initialization();
        }

        #endregion Constructor

        #region Initialization

        void Initialization()
        {

            ms_ENTRYDATE = string.Empty;
            mo_CREATEBY = new CUser();
            mo_COMPANYID = new CAllLookup();
            mo_RemiAccount = new CRemiAccount();
            ms_BENEFICIARY_NAME = string.Empty;
            ms_PINNO = string.Empty;
            ms_AMOUNT = string.Empty;
            ms_INCENTIVE_AMOUNT = string.Empty;
            ms_REMARKS = string.Empty;
            ms_RNIDNO = string.Empty;
            ms_RNIDISSUEDATE = string.Empty;
            ms_RNIDEXPIRYDATE = string.Empty;
            ms_RNIDTYPE = string.Empty;
            ms_PAYOUTBY = string.Empty;
            ms_PAYOUTDATE = string.Empty;
            ms_BENEFICIARYADDRESS = string.Empty;
            ms_SENDERNAME = string.Empty;
            ms_SENDERCOUNTRY = string.Empty;
            ms_SENDERPASSPORTNO = string.Empty;
            ms_SENDERBUSINESSLICENSE = string.Empty;
            ms_SENDERIDTYPE = string.Empty;
            ms_SENDERIDTYPENO = string.Empty;
            ms_DOCAPPROVED = string.Empty;
            ms_DOCUMENTAPPROVEDATE = string.Empty;
            ms_ROUTINGNO = string.Empty;
            ms_CBSMStatus = string.Empty;
            ms_CBSMDate = string.Empty;
            ms_CBSIStatus = string.Empty;
            ms_CBSIDate = string.Empty;
            ms_TransactionApproved = string.Empty;
            ms_PaymentType = string.Empty;
            ms_ReceiverPhoneNo = string.Empty;
            ms_AgentID = string.Empty;
            ms_AgentName = string.Empty;
            ms_ReportType = string.Empty;
            ms_TransactionType = string.Empty;


            ms_SenderForeign = string.Empty;
            ms_RemitOnly = string.Empty;
            ms_MainRemitDirverse = string.Empty;
            ms_AppRequestId = string.Empty;
        }
        #endregion

        #region public Member


        public string RequestId
        {
            get { return ms_AppRequestId; }
            set { ms_AppRequestId = value; }
        }

        public string ForeignSender
        {
            get { return ms_SenderForeign; }
            set { ms_SenderForeign = value; }
        }
        public string RemitOnly
        {
            get { return ms_RemitOnly; }
            set { ms_RemitOnly = value; }
        }
        public string IsMainRemitDisverse
        {
            get { return ms_MainRemitDirverse; }
            set { ms_MainRemitDirverse = value; }
        }
        public string TransactionType
        {
            get { return ms_TransactionType; }
            set { ms_TransactionType = value; }
        }
        public string ReportType
        {
            get { return ms_ReportType; }
            set { ms_ReportType = value; }
        }

        public string AgentID
        {
            get { return ms_AgentID; }
            set { ms_AgentID = value; }
        }

        public string AgentName
        {
            get { return ms_AgentName; }
            set { ms_AgentName = value; }
        }
        public string ENTRYDATE
        {
            get { return ms_ENTRYDATE; }
            set { ms_ENTRYDATE = value; }
        }
        public string ReceiverPhoneNo
        {
            get { return ms_ReceiverPhoneNo; }
            set { ms_ReceiverPhoneNo = value; }
        }
        
        
        public CUser CREATEBY
        {
            get { return mo_CREATEBY; }
            set { mo_CREATEBY = value; }

        }

        public string IsTransactionApproved
        {
            get { return ms_TransactionApproved; }
            set { ms_TransactionApproved = value; }

        }


        public string PaymentType
        {
            get { return ms_PaymentType; }
            set { ms_PaymentType = value; }

        }
        public CAllLookup COMPANY
        {
            get { return mo_COMPANYID; }
            set { mo_COMPANYID = value; }
        }

        public CRemiAccount AccountInfo
        {
            get { return mo_RemiAccount; }
            set { mo_RemiAccount = value; }
        }
        public string BENEFICIARY_NAME
        {
            get { return ms_BENEFICIARY_NAME; }
            set { ms_BENEFICIARY_NAME = value; }
        }
        public string PINNO
        {
            get { return ms_PINNO; }
            set { ms_PINNO = value; }
        }
        public string AMOUNT
        {
            get { return ms_AMOUNT; }
            set { ms_AMOUNT = value; }
        }

        public string INCENTIVE_AMOUNT
        {
            get { return ms_INCENTIVE_AMOUNT; }
            set { ms_INCENTIVE_AMOUNT = value; }
        }

        public string REMARKS
        {
            get { return ms_REMARKS; }
            set { ms_REMARKS = value; }
        }

        public string RNIDNO
        {
            get { return ms_RNIDNO; }
            set { ms_RNIDNO = value; }
        }

        public string RNIDISSUEDATE
        {
            get { return ms_RNIDISSUEDATE; }
            set { ms_RNIDISSUEDATE = value; }
        }

        public string RNIDEXPIRYDATE
        {
            get { return ms_RNIDEXPIRYDATE; }
            set { ms_RNIDEXPIRYDATE = value; }
        }

        public string RNIDTYPE
        {
            get { return ms_RNIDTYPE; }
            set { ms_RNIDTYPE = value; }
        }

        public string PAYOUTBY
        {
            get { return ms_PAYOUTBY; }
            set { ms_PAYOUTBY = value; }
        }

        public string PAYOUTDATE
        {
            get { return ms_PAYOUTDATE; }
            set { ms_PAYOUTDATE = value; }
        }
        public string BENEFICIARYADDRESS
        {
            get { return ms_BENEFICIARYADDRESS; }
            set { ms_BENEFICIARYADDRESS = value; }
        }

        public string SENDERNAME
        {
            get { return ms_SENDERNAME; }
            set { ms_SENDERNAME = value; }
        }

        public string SENDERCOUNTRY
        {
            get { return ms_SENDERCOUNTRY; }
            set { ms_SENDERCOUNTRY = value; }
        }

        public string SENDERPASSPORTNO
        {
            get { return ms_SENDERPASSPORTNO; }
            set { ms_SENDERPASSPORTNO = value; }
        }

        public string SENDERBUSINESSLICENSE
        {
            get { return ms_SENDERBUSINESSLICENSE; }
            set { ms_SENDERBUSINESSLICENSE = value; }
        }

        public string SENDERIDTYPE
        {
            get { return ms_SENDERIDTYPE; }
            set { ms_SENDERIDTYPE = value; }
        }

        public string SENDERIDTYPENO
        {
            get { return ms_SENDERIDTYPENO; }
            set { ms_SENDERIDTYPENO = value; }
        }

        public string DOCAPPROVED
        {
            get { return ms_DOCAPPROVED; }
            set { ms_DOCAPPROVED = value; }
        }

        public string DOCUMENTAPPROVEDATE
        {
            get { return ms_DOCUMENTAPPROVEDATE; }
            set { ms_DOCUMENTAPPROVEDATE = value; }
        }

        public string ROUTINGNO
        {
            get { return ms_ROUTINGNO; }
            set { ms_ROUTINGNO = value; }
        }

        public string CBSMStatus
        {
            get { return ms_CBSMStatus; }
            set { ms_CBSMStatus = value; }
        }


        public string CBSMDate
        {
            get { return ms_CBSMDate; }
            set { ms_CBSMDate = value; }
        }

        public string CBSIStatus
        {
            get { return ms_CBSIStatus; }
            set { ms_CBSIStatus = value; }
        }
        public string CBSIDate
        {
            get { return ms_CBSIDate; }
            set { ms_CBSIDate = value; }
        }

    

        #endregion
    }
}
