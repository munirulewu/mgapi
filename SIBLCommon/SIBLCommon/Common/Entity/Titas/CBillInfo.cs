using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.Common.Entity.User;
using SIBLCommon.Common.Entity.Bank;
using SIBLXoomCommon.SIBLXoom.Common.Entity.CPU;

namespace  SIBLCommon.Common.Entity.Titas
{
    [Serializable]
    public class CBillInfo : ASIBLEntityBase
    {
        #region Protectd Member
        protected string m_sbillid;
        protected string m_scustomerCode;
        protected string m_scustomerName;
        protected string m_sinvoiceNo;
        protected string m_sinvoiceAmount;
        protected string m_sissueMonth;
        protected string m_ssettleDate;
        protected string m_szone;
        protected string m_sEntryDate;
        protected string m_sCreateBy;
        protected string m_sBranchId;
        protected string m_sBillStatus;
        protected string m_sstatuscode;
        protected string m_sIssusDate;
        protected string m_sFromDate;
        protected string m_sToDate;
        protected string m_spaidAmount;
        protected string m_sourceTaxAmount;
        protected string m_sTrackerNo;
        protected string m_srevenueStamp;
        protected string m_stransactionDate;
        protected string m_sbranchRoutingNo;
        protected string m_soperator;
        protected string m_srefNo;
        protected string m_sReason;

        protected string m_sChalanNo;
        protected string m_sChalanDate;

        protected string m_sChalanBank;
        protected string m_sChalanBranch;
        protected string m_sPaymentId;
        protected string m_sTransactionType;


        protected string m_sOperationType;
        protected string m_ssurchargeAmount;
        protected string m_sdueDate;
        protected string m_sapplianceSummery;
        protected string m_sconnectionAddress;
        protected string m_sMobile;
        protected string m_sMessage;
        protected string m_sCustomerId;
        protected string m_sComments;
        protected string m_sTotalNumberOfBills;

        protected CUser oUser;
        protected CBranch oBranch;
        protected CAbabilTransactionResponse oAbabilTransactionResponse;
        #endregion

        #region Constructor
        public CBillInfo()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {


            m_sbillid = string.Empty;
            m_scustomerCode = string.Empty;
            m_scustomerName = string.Empty;
            m_sinvoiceNo = string.Empty;
            m_sinvoiceAmount = string.Empty;
            m_sissueMonth = string.Empty;
            m_ssettleDate = string.Empty;
            m_szone = string.Empty;
            m_sEntryDate = string.Empty;
            m_sCreateBy = string.Empty;
            m_sBranchId = string.Empty;
            m_sBillStatus = string.Empty;
            m_sstatuscode = string.Empty;
            m_sFromDate = string.Empty;
            m_sToDate = string.Empty;
            m_sIssusDate = string.Empty;

            m_sOperationType = string.Empty;
            oUser = new CUser();
            oBranch = new CBranch();
            oAbabilTransactionResponse = new CAbabilTransactionResponse();

            m_sTrackerNo = string.Empty;
            m_spaidAmount = string.Empty;
            m_sourceTaxAmount = string.Empty;
            m_srevenueStamp = string.Empty;
            m_stransactionDate = string.Empty;
            m_sbranchRoutingNo = string.Empty;
            m_soperator = string.Empty;
            m_srefNo = string.Empty;
            m_sReason = string.Empty;
            m_sChalanNo = string.Empty;
            m_sChalanDate = string.Empty;

            m_sChalanBank = string.Empty;
            m_sChalanBranch = string.Empty;
            m_sPaymentId = string.Empty;
            m_sTransactionType = string.Empty;

            m_sOperationType = string.Empty;
            m_ssurchargeAmount = string.Empty;
            m_sdueDate = string.Empty;
            m_sapplianceSummery = string.Empty;
            m_sconnectionAddress = string.Empty;
            m_sMobile = string.Empty;
            m_sCustomerId = string.Empty;
            m_sComments = string.Empty;
            m_sTotalNumberOfBills = string.Empty;
        }
        #endregion Initialization

        #region public Member
        public string CustomerId
        {
            get { return m_sCustomerId; }
            set { m_sCustomerId = value; }
        }
        public string TotalNumberOfBills
        {
            get { return m_sTotalNumberOfBills; }
            set { m_sTotalNumberOfBills = value; }
        }

        public string billid
        {
            get { return m_sbillid; }
            set { m_sbillid = value; }
        }

        public string customerCode
        {
            get { return m_scustomerCode; }
            set { m_scustomerCode = value; }
        }

        public string customerName
        {
            get { return m_scustomerName; }
            set { m_scustomerName = value; }
        }

        public string invoiceNo
        {
            get { return m_sinvoiceNo; }
            set { m_sinvoiceNo = value; }
        }
        public string invoiceAmount
        {
            get { return m_sinvoiceAmount; }
            set { m_sinvoiceAmount = value; }
        }

        public string issueMonth
        {
            get { return m_sissueMonth; }
            set { m_sissueMonth = value; }
        }
        public string settleDate
        {
            get { return m_ssettleDate; }
            set { m_ssettleDate = value; }
        }

        public string zone
        {
            get { return m_szone; }
            set { m_szone = value; }
        }

        public string TrackerNo
        {
            get { return m_sTrackerNo; }
            set { m_sTrackerNo = value; }
        }


        public string Reason
        {
            get { return m_sReason; }
            set { m_sReason = value; }
        }

        public string EntryDate
        {
            get { return m_sEntryDate; }
            set { m_sEntryDate = value; }
        }


        public string CreateBy
        {
            get { return m_sCreateBy; }
            set { m_sCreateBy = value; }
        }


        public string BillStatus
        {
            get { return m_sBillStatus; }
            set { m_sBillStatus = value; }
        }

        public string statuscode
        {
            get { return m_sstatuscode; }
            set { m_sstatuscode = value; }
        }

        public string OperationType
        {
            get { return m_sOperationType; }
            set { m_sOperationType = value; }
        }


        public CUser UserInfo
        {
            get { return oUser; }
            set { oUser = value; }
        }

        public CBranch Branch
        {
            get { return oBranch; }
            set { oBranch = value; }
        }

        public CAbabilTransactionResponse AbabilTransactionResponse
        {
            get { return oAbabilTransactionResponse; }
            set { oAbabilTransactionResponse = value; }
        }

        public string ToDate
        {
            get { return m_sToDate; }
            set { m_sToDate = value; }
        }

        public string FromDate
        {
            get { return m_sFromDate; }
            set { m_sFromDate = value; }
        }
        public string IssusDate
        {
            get { return m_sIssusDate; }
            set { m_sIssusDate = value; }
        }


        public string PaidAmount
        {
            get { return m_spaidAmount; }
            set { m_spaidAmount = value; }
        }

        public string SourceTaxAmount
        {
            get { return m_sourceTaxAmount; }
            set { m_sourceTaxAmount = value; }
        }
        public string RevenueStamp
        {
            get { return m_srevenueStamp; }
            set { m_srevenueStamp = value; }
        }

        public string TransactionDate
        {
            get { return m_stransactionDate; }
            set { m_stransactionDate = value; }
        }
        public string BranchRoutingNo
        {
            get { return m_sbranchRoutingNo; }
            set { m_sbranchRoutingNo = value; }
        }

        public string Operator
        {
            get { return m_soperator; }
            set { m_soperator = value; }
        }
        public string RefNo
        {
            get { return m_srefNo; }
            set { m_srefNo = value; }
        }

        public string ChalanNo
        {
            get { return m_sChalanNo; }
            set { m_sChalanNo = value; }

        }
        public string ChalanDate
        {
            get { return m_sChalanDate; }
            set { m_sChalanDate = value; }
        }
        public string ChalanBank
        {
            get { return m_sChalanBank; }
            set { m_sChalanBank = value; }
        }

        public string ChalanBranch
        {
            get { return m_sChalanBranch; }
            set { m_sChalanBranch = value; }
        }

        public string PaymentId
        {
            get { return m_sPaymentId; }
            set { m_sPaymentId = value; }
        }

        public string TransactionType
        {
            get { return m_sTransactionType; }
            set { m_sTransactionType = value; }
        }


        public string SurchargeAmount
        {
            get { return m_ssurchargeAmount; }
            set { m_ssurchargeAmount = value; }
        }
        public string DueDate
        {
            get { return m_sdueDate; }
            set { m_sdueDate = value; }
        }
        public string ApplianceSummery
        {
            get { return m_sapplianceSummery; }
            set { m_sapplianceSummery = value; }
        }
        public string ConnectionAddress
        {
            get { return m_sconnectionAddress; }
            set { m_sconnectionAddress = value; }
        }
        public string Mobile
        {
            get { return m_sMobile; }
            set { m_sMobile = value; }
        }

        public string Comments
        {
            get { return m_sComments; }
            set { m_sComments = value; }
        }
        

        #endregion
    }
}
