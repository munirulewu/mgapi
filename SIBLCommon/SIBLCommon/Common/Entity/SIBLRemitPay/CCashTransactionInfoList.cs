using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.SIBLCommon.Common.Entity.CashPayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SIBLCommon.SIBLCommon.Common.Entity.CashPayment
{
    [Serializable]
    public class CCashTransactionInfoList : ASIBLEntityCollectionBase
    {
        protected List<CCashTransactionInfo> m_liCashTransactionInfo;

        public CCashTransactionInfoList()
        {
            m_liCashTransactionInfo = new List<CCashTransactionInfo>();
        }
        public List<CCashTransactionInfo> CashTransactionInfoList
        {
            get { return m_liCashTransactionInfo; }
            set { m_liCashTransactionInfo = value; }
        }
    }
}
