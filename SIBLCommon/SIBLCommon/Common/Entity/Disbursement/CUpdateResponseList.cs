using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.Common.Entity.Disbursement;

namespace SIBLXoomCommon.SIBLXoom.Common.Entity.Disbursement
{
    [Serializable]
    public class CUpdateResponseList : ASIBLEntityBase
    {
        protected List<CTransactionUpdate> m_liTransactionUpdate;
        public CUpdateResponseList()
        {
            m_liTransactionUpdate = new List<CTransactionUpdate>();
        }
        public List<CTransactionUpdate> TransactionUpdate
        {
            get { return m_liTransactionUpdate; }
            set { m_liTransactionUpdate = value; }
        }

    }
}
