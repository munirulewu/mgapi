using SIBLCommon.Common.Entity.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIBLCommon.SIBLCommon.Common.Entity.RemitGlobalAPI
{
    public class CTransactionStatus : ASIBLEntityBase
    {
        CSystemInfo m_oSysteminfo;
        private string m_refTxnId;
        private string m_txnIdSIBL;
        private string m_transactionStatus;
        private string m_CBSStatus;

        #region Constructor
        public CTransactionStatus()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_oSysteminfo = new CSystemInfo();
            m_refTxnId = string.Empty;
            m_txnIdSIBL = string.Empty;
            m_transactionStatus = string.Empty;
            m_CBSStatus = string.Empty;
             

        }
        #endregion Initialization

        #region public Member

        public CSystemInfo SystemInfo
        {
            get { return m_oSysteminfo; }
            set { m_oSysteminfo = value; }
        }
        public string RefTxnId
        {
            get { return m_refTxnId; }
            set { m_refTxnId = value; }
        }

        public string TxnIdSIBL
        {
            get { return m_txnIdSIBL; }
            set { m_txnIdSIBL = value; }
        }

        public string transactionStatus
        {
            get { return m_transactionStatus; }
            set { m_transactionStatus = value; }
        }
        public string CBSStatus
        {
            get { return m_CBSStatus; }
            set { m_CBSStatus = value; }
        }
        #endregion     
    }
}
