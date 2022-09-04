using SIBLCommon.Common.Entity.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIBLCommon.SIBLCommon.Common.Entity.RemitGlobalAPI
{
    [Serializable]
    public class CRemitInfo : ASIBLEntityBase
    {
        CSystemInfo m_oSysteminfo;
        CTransactionInfo m_oTransactionInfo;
        CRecipientInfo m_oRecipientInfo;
        CSenderInfo m_oSenderInfo;
        private string m_soperationType;
        #region Constructor
        public CRemitInfo()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_oSysteminfo = new CSystemInfo();
            m_oTransactionInfo = new CTransactionInfo();
            m_oSenderInfo = new CSenderInfo();
            m_oRecipientInfo = new CRecipientInfo();
            m_soperationType = string.Empty;

        }
        #endregion Initialization

        #region public Member

        public CSystemInfo SystemInfo
        {
            get { return m_oSysteminfo; }
            set { m_oSysteminfo = value; }
        }
        public CTransactionInfo TransactionInfo
        {
            get { return m_oTransactionInfo; }
            set { m_oTransactionInfo = value; }
        }
        public CRecipientInfo RecipientInfo
        {
            get { return m_oRecipientInfo; }
            set { m_oRecipientInfo = value; }
        }
        public CSenderInfo SenderInfo
        {
            get { return m_oSenderInfo; }
            set { m_oSenderInfo = value; }
        }

        #endregion
    }
}
