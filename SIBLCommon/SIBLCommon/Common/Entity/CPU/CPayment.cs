using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.Common.Entity.User;
using SIBLCommon.Common.Entity.CPU;


namespace SIBLXoomCommon.Common.Entity.CPU
{
    [Serializable]
    public class CPayment : ASIBLEntityBase
    {

        #region Protectd Member

        protected string m_sId_Type;
        protected string m_sId_no;
        protected string m_sIssueDate;
        protected string m_sExpiryDate;
        protected string m_sComments;
        protected string m_sReceiverPhone;
        protected string m_sFDate;
        protected string m_sTDate;
        // protected CBank m_oBank;
        protected CTransactionInformation oTransactionInformation;
        protected CDisbursementInformation oDisbursementInformation;
        protected CSenderInformation oSenderInformation;
        protected CSupplementalInformation oSupplementalInformation;
        protected CRecipientInformation oRecipientInformation;
        protected CUnDisbursementInformation oUnDisbursementInformation;
        protected string m_sDataType;
        protected string m_sOperationType;
        protected CUser m_oUser;
 

        #endregion


        #region Constructor
        public CPayment()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_sId_Type = string.Empty;
            m_sId_no = string.Empty;
            m_sIssueDate = string.Empty;
            m_sExpiryDate = string.Empty;
            m_sComments = string.Empty;
            m_sReceiverPhone = string.Empty;
            oTransactionInformation = new CTransactionInformation();
            oDisbursementInformation = new CDisbursementInformation();
            oSenderInformation = new CSenderInformation();
            oSupplementalInformation = new CSupplementalInformation();
            oRecipientInformation = new CRecipientInformation();
            oUnDisbursementInformation = new CUnDisbursementInformation();
            m_sFDate = string.Empty;
            m_sTDate = string.Empty;
            m_sDataType = string.Empty;
            m_sOperationType = string.Empty;
            m_oUser = new CUser();
        }
        #endregion Initialization

        #region public Member

        //public CBank Bank
        //{
        //    get { return m_oBank; }
        //    set { m_oBank = value; }
        //}

        public string Id_Type
        {
            get { return m_sId_Type; }
            set { m_sId_Type = value; }
        }
        public string Id_no
        {
            get { return m_sId_no; }
            set { m_sId_no = value; }
        }
        public string IssueDate
        {
            get { return m_sIssueDate; }
            set { m_sIssueDate = value; }
        }
        public string ExpiryDate
        {
            get { return m_sExpiryDate; }
            set { m_sExpiryDate = value; }
        }
        public string Comments
        {
            get { return m_sComments; }
            set { m_sComments = value; }
        }
        public string ReceiverPhone
        {
            get { return m_sReceiverPhone; }
            set { m_sReceiverPhone = value; }
        }

        public CTransactionInformation TransactionInformation
        {
            get { return oTransactionInformation; }
            set { oTransactionInformation = value; }
        }


        public CDisbursementInformation DisbursementInformation
        {
            get { return oDisbursementInformation; }
            set { oDisbursementInformation = value; }
        }

         public CSenderInformation SenderInformation
        {
            get { return oSenderInformation; }
            set { oSenderInformation = value; }
        }
         public CSupplementalInformation SupplementalInformation
        {
            get { return oSupplementalInformation; }
            set { oSupplementalInformation = value; }
        }
         public CRecipientInformation RecipientInformation
        {
            get { return oRecipientInformation; }
            set { oRecipientInformation = value; }
        }

         public CUnDisbursementInformation UnDisbursementInformation
         {
             get { return oUnDisbursementInformation; }
             set { oUnDisbursementInformation = value; }
         }

        public string FromDate
        {
            get { return m_sFDate; }
            set { m_sFDate = value; }
        }
        public string ToDate
        {
            get { return m_sTDate; }
            set { m_sTDate = value; }
        }
        public string DataType
        {
            get { return m_sDataType; }
            set { m_sDataType = value; }
        }
        public string OperationType
        {
            get { return m_sOperationType; }
            set { m_sOperationType = value; }
        }

        public CUser User
        {
            get { return m_oUser; }
            set { m_oUser = value; }
        }

        #endregion
    }
}
