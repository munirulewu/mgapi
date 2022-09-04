using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.Common.Entity.User;
using SIBLCommon.Common.Entity.Bank;

namespace SIBLCommon.SIBLCommon.Common.Entity.Titas
{
    [Serializable]
    public class CCancel : ASIBLEntityBase
    {
        #region Protected Member

        protected string m_spaymentId;
        protected string m_sinvoiceNo;
        protected string m_scustomerCode;
        protected string m_soperators;
        protected string m_sreason;
        protected string m_scode;
        protected string m_smessage;
        protected CUser oUser;
        protected CBranch oBranch;

        #endregion Protected Member

        #region Constructor
        public CCancel()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_spaymentId = string.Empty;
            m_sinvoiceNo = string.Empty;
            m_scustomerCode = string.Empty;
            m_soperators = string.Empty;
            m_sreason = string.Empty;
            m_scode = string.Empty;
            m_smessage = string.Empty;
            oUser = new CUser();
            oBranch = new CBranch();
        }
        #endregion Initialization

        #region public Member

        public string paymentId
        {
            get { return m_spaymentId; }
            set { m_spaymentId = value; }
        }

        public string invoiceNo
        {
            get { return m_sinvoiceNo; }
            set { m_sinvoiceNo = value; }
        }

        public string customerCode
        {
            get { return m_scustomerCode; }
            set { m_scustomerCode = value; }
        }

        public string operators
        {
            get { return m_soperators; }
            set { m_soperators = value; }
        }
        public string reason
        {
            get { return m_sreason; }
            set { m_sreason = value; }
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


        #endregion
    }
}
