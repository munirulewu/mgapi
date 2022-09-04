using SIBLCommon.Common.Entity.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIBLCommon.SIBLCommon.Common.Entity.WorldRemit
{
    [Serializable]
    public class CWRTransactionDetailsList : ASIBLEntityCollectionBase
    {

        protected List<CWRTransactionDetails> m_liWRTransactionDetailsList;
        public CWRTransactionDetailsList()
        {
            m_liWRTransactionDetailsList = new List<CWRTransactionDetails>();
        }
         public List<CWRTransactionDetails> WRTransactionDetailsList
        {
            get { return m_liWRTransactionDetailsList; }
            set { m_liWRTransactionDetailsList = value; }
        }
    }
}
