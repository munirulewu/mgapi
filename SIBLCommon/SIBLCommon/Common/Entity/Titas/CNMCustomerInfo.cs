using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace  SIBLCommon.Common.Entity.Titas
{
    [Serializable]
    public class CNMCustomerInfo : ASIBLEntityBase
    {
        #region Protected Member

        protected string m_sCustomerCode;
        protected string m_sCustomerName;
        protected string m_sApplianceSummary;
        protected string m_sConnectionAddress;
        protected string m_sMobile;
        protected string m_sAmount;
        protected string m_sSurcharge;
        protected string m_sParticulars;
        protected string m_sID;
        protected string m_sInvoiceNo;
        protected string m_sOperationType;
        protected string m_sTransactionType;       
        protected string m_sCustomerId;
        protected string m_sbankTransactionId;
        
        #endregion Protected Member


        #region Constructor
        public CNMCustomerInfo()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_sCustomerCode = string.Empty;
            m_sCustomerName = string.Empty;
            m_sApplianceSummary = string.Empty;
            m_sConnectionAddress = string.Empty;
            m_sMobile = string.Empty;
            m_sID = string.Empty;
            m_sAmount = string.Empty;
            m_sSurcharge = string.Empty;
            m_sParticulars = string.Empty;
            m_sOperationType = string.Empty;
            m_sTransactionType = string.Empty;
            m_sCustomerId = string.Empty;
            m_sbankTransactionId = string.Empty;
        }
        #endregion Initialization

        #region public Member

        public string BankTransactionId
        {
            get { return m_sbankTransactionId; }
            set { m_sbankTransactionId = value; }
        }
        public string CustomerId
        {
            get { return m_sCustomerId; }
            set { m_sCustomerId = value; }
        }
        public string CustomerCode
        {
            get { return m_sCustomerCode; }
            set { m_sCustomerCode = value; }
        }

        public string CustomerName
        {
            get { return m_sCustomerName; }
            set { m_sCustomerName = value; }
        }

        public string ApplianceSummary
        {
            get { return m_sApplianceSummary; }
            set { m_sApplianceSummary = value; }
        }

        public string ConnectionAddress
        {
            get { return m_sConnectionAddress; }
            set { m_sConnectionAddress = value; }
        }
        public string Mobile
        {
            get { return m_sMobile; }
            set { m_sMobile = value; }
        }

        public string ID
        {
            get { return m_sID; }
            set { m_sID = value; }
        }
        public string Amount
        {
            get { return m_sAmount; }
            set { m_sAmount = value; }
        }
        public string Surcharge
        {
            get { return m_sSurcharge; }
            set { m_sSurcharge = value; }
        }
        public string Particulars
        {
            get { return m_sParticulars; }
            set { m_sParticulars = value; }
        }

        public string InvoiceNo
        {
            get { return m_sInvoiceNo; }
            set { m_sInvoiceNo = value; }
        }

        public string OperationType
        {
            get { return m_sOperationType; }
            set { m_sOperationType = value; }
        }

        public string TransactionType
        {
            get { return m_sTransactionType; }
            set { m_sTransactionType = value; }
        }


        #endregion
    }
}
