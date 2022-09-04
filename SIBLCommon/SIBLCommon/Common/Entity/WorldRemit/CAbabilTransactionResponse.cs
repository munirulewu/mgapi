using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.Common.Entity.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIBLCommon.SIBLCommon.Common.Entity.WorldRemit
{

    [Serializable]
    public class CAbabilTransactionResponse : ASIBLEntityBase
 
    {
        #region Protectd Member

        protected string m_sDescription;
        protected string m_sDescriptionField;
        protected string m_sErrorDetail;
        protected string m_sErrorDetailField;
        protected string m_sPropertyChanged;
        protected string m_sRequestReference;
        protected string m_sRequestReferenceField;
        protected string m_sResponseReference;
        protected string m_sResponseReferenceField;
        protected string m_sTransactionStatus;
        protected string m_sTransactionStatusField;
        protected string m_sTransactionStatusFieldSpecified;
        protected string m_sTransactionStatusSpecified;
        protected string m_sXoomTrackingNo;
        protected string m_sAppRequestId;
        protected string m_sOperationType;
        protected CUser m_oUser;

        #endregion


        #region Constructor
        public CAbabilTransactionResponse()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {

            m_sDescription = string.Empty;
            m_sDescriptionField = string.Empty;
            m_sErrorDetail = string.Empty;
            m_sErrorDetailField = string.Empty;
            m_sPropertyChanged = string.Empty;
            m_sRequestReference = string.Empty;
            m_sRequestReferenceField = string.Empty;
            m_sResponseReference = string.Empty;
            m_sResponseReferenceField = string.Empty;
            m_sTransactionStatus = string.Empty;
            m_sTransactionStatusField = string.Empty;
            m_sTransactionStatusFieldSpecified = string.Empty;
            m_sTransactionStatusSpecified = string.Empty;
            m_sOperationType = string.Empty;
            m_sXoomTrackingNo = string.Empty;
            m_sAppRequestId = string.Empty;
            m_oUser = new CUser();
        }
        #endregion Initialization

        #region public Member

        public string XoomTrackingNo
        {
            get { return m_sXoomTrackingNo; }
            set { m_sXoomTrackingNo = value; }
        }

        public string AppRequestId
        {
            get { return m_sAppRequestId; }
            set { m_sAppRequestId = value; }
        }

        public string Description
        {
            get { return m_sDescription; }
            set { m_sDescription = value; }
        }
        public string DescriptionField
        {
            get { return m_sDescriptionField; }
            set { m_sDescriptionField = value; }
        }

        public string ErrorDetail
        {
            get { return m_sErrorDetail; }
            set { m_sErrorDetail = value; }
        }
        public string ErrorDetailField
        {
            get { return m_sErrorDetailField; }
            set { m_sErrorDetailField = value; }
        }

        public string PropertyChanged
        {
            get { return m_sPropertyChanged; }
            set { m_sPropertyChanged = value; }
        }

        public string RequestReference
        {
            get { return m_sRequestReference; }
            set { m_sRequestReference = value; }
        }

        public string RequestReferenceField
        {
            get { return m_sRequestReferenceField; }
            set { m_sRequestReferenceField = value; }
        }


        public string ResponseReference
        {
            get { return m_sResponseReference; }
            set { m_sResponseReference = value; }
        }

        public string ResponseReferenceField
        {
            get { return m_sResponseReferenceField; }
            set { m_sResponseReferenceField = value; }
        }

        public string TransactionStatus
        {
            get { return m_sTransactionStatus; }
            set { m_sTransactionStatus = value; }
        }
        public string TransactionStatusField
        {
            get { return m_sTransactionStatusField; }
            set { m_sTransactionStatusField = value; }
        }

        public string TransactionStatusFieldSpecified
        {
            get { return m_sTransactionStatusFieldSpecified; }
            set { m_sTransactionStatusFieldSpecified = value; }
        }
        public string TransactionStatusSpecified
        {
            get { return m_sTransactionStatusSpecified; }
            set { m_sTransactionStatusSpecified = value; }
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
