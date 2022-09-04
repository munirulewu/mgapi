using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.Common.Entity.User;
using SIBLCommon.Common.Entity.Bank;
using SIBLCommon.Common.Entity.Titas;

namespace SIBLCommon.SIBLCommon.Common.Entity.Titas
{
    [Serializable]
    public class CAddPayment : ASIBLEntityBase
    {
         #region Protectd Member

        protected string m_scustomerCode;
        protected string m_sinvoiceNo;
        protected string m_spaidAmount;
        protected string m_ssourceTaxAmount;
        protected string m_srevenueStamp;
        protected string m_stransactionDate;
        protected string m_sbranchRoutingNo;
        protected string m_soperators;
        protected string m_srefNo;
       
        protected string m_schalanNo;
        protected string m_schalanDate;
        protected string m_sbranchCode;
        protected string m_schalanBank;
        protected string m_schalanBranch;
        protected string m_scode;
        protected string m_smessage;
        protected string m_sFromDate;
        protected string m_sToDate;
      
        protected string m_sID;
        protected string m_sbank;
        protected string m_sbatchNo;
        protected string m_samount;
        protected string m_ssurcharge;
        protected string m_stotal;
        protected string m_svoucherDate;
        protected string m_sparticulars;      
        

        protected CUser oUser;
        protected CBranch oBranch;
        protected CNMCustomerInfo oNMCustomerInfo;
        protected string m_sBillType;
        protected string m_sOperationType;
        protected string m_sDuedate;
        protected string m_sPaymentId;
        protected string m_sAreacode;
        protected string m_sTrackerno;
        #endregion

        #region Constructor
        public CAddPayment()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_scustomerCode = string.Empty;
            m_sinvoiceNo = string.Empty;
            m_spaidAmount = string.Empty;
            m_ssourceTaxAmount = string.Empty;
            m_srevenueStamp = string.Empty;
            m_stransactionDate = string.Empty;
            m_sbranchRoutingNo = string.Empty;
            m_soperators = string.Empty;
            m_srefNo = string.Empty;
           
            m_schalanNo = string.Empty;
            m_schalanDate = string.Empty;
            m_sbranchCode = string.Empty;
            m_schalanBank = string.Empty;
            m_schalanBranch = string.Empty;
            m_scode = string.Empty;
            m_smessage = string.Empty;
            m_sFromDate = string.Empty;
            m_sToDate = string.Empty;
            oUser = new CUser();
            oBranch = new CBranch();


            m_sID = string.Empty;
            m_sbank = string.Empty;
            m_sbatchNo = string.Empty;
            m_samount = string.Empty;
            m_ssurcharge = string.Empty;
            m_stotal = string.Empty;
            m_svoucherDate = string.Empty;
            m_sparticulars = string.Empty;
            oNMCustomerInfo = new CNMCustomerInfo();
            m_sBillType = string.Empty;
            m_sOperationType = string.Empty;
            m_sDuedate = string.Empty;
            m_sPaymentId = string.Empty;
            m_sAreacode = string.Empty;
            m_sTrackerno = string.Empty;
        }
        #endregion Initialization

        #region public Member

       

        public string customerCode
        {
            get { return m_scustomerCode; }
            set { m_scustomerCode = value; }
        }

        public string invoiceNo
        {
            get { return m_sinvoiceNo; }
            set { m_sinvoiceNo = value; }
        }

        public string paidAmount
        {
            get { return m_spaidAmount; }
            set { m_spaidAmount = value; }
        }

        public string sourceTaxAmount
        {
            get { return m_ssourceTaxAmount; }
            set { m_ssourceTaxAmount = value; }
        }
        public string revenueStamp
        {
            get { return m_srevenueStamp; }
            set { m_srevenueStamp = value; }
        }

        public string transactionDate
        {
            get { return m_stransactionDate; }
            set { m_stransactionDate = value; }
        }
        public string branchRoutingNo
        {
            get { return m_sbranchRoutingNo; }
            set { m_sbranchRoutingNo = value; }
        }

        public string operators
        {
            get { return m_soperators; }
            set { m_soperators = value; }
        }


        public string refNo
        {
            get { return m_srefNo; }
            set { m_srefNo = value; }
        }
     
        public string chalanNo
        {
            get { return m_schalanNo; }
            set { m_schalanNo = value; }
        }


        public string chalanDate
        {
            get { return m_schalanDate; }
            set { m_schalanDate = value; }
        }
        
        public string branchCode
        {
            get { return m_sbranchCode; }
            set { m_sbranchCode = value; }
        } 
        
        public string chalanBank
        {
            get { return m_schalanBank; }
            set { m_schalanBank = value; }
        } 
        
        public string chalanBranch
        {
            get { return m_schalanBranch; }
            set { m_schalanBranch = value; }
        } 
        
        public string code
        {
            get { return m_scode; }
            set { m_scode = value; }
        }

         public string message
        {
            get { return m_smessage; }
            set { m_smessage = value; }
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


        public string ID
        {
            get { return m_sID; }
            set { m_sID = value; }
        }
        public string Bank
        {
            get { return m_sbank; }
            set { m_sbank = value; }
        }
        public string BatchNo
        {
            get { return m_sbatchNo; }
            set { m_sbatchNo = value; }
        }
        public string Amount
        {
            get { return m_samount; }
            set { m_samount = value; }
        }
        public string Surcharge
        {
            get { return m_ssurcharge; }
            set { m_ssurcharge = value; }
        }
        public string Total
        {
            get { return m_stotal; }
            set { m_stotal = value; }
        }
        public string VoucherDate
        {
            get { return m_svoucherDate; }
            set { m_svoucherDate = value; }
        }
        public string Particulars
        {
            get { return m_sparticulars; }
            set { m_sparticulars = value; }
        }
        public CNMCustomerInfo NMCustomerInfo
        {
            get { return oNMCustomerInfo; }
            set { oNMCustomerInfo = value; }
        }

        public string BillType
        {
            get { return m_sBillType; }
            set { m_sBillType = value; }
        }

        public string OperationType
        {
            get { return m_sOperationType; }
            set { m_sOperationType = value; }
        }

         public string Duedate
        {
            get { return m_sDuedate; }
            set { m_sDuedate = value; }
        }
         public string PaymentId
        {
            get { return m_sPaymentId; }
            set { m_sPaymentId = value; }
        }
         public string Areacode
        {
            get { return m_sAreacode; }
            set { m_sAreacode = value; }
        }

         public string Trackerno
        {
            get { return m_sTrackerno; }
            set { m_sTrackerno = value; }
        }
        

        #endregion

    }
}
