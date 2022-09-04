using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLCommon.Common.Entity.CPU
{
    [Serializable]
    public class CTransactionInformationList : ASIBLEntityCollectionBase
    {
        protected List<CTransactionInformation> m_liCTransactionInformation;
          public CTransactionInformationList()
        {
            m_liCTransactionInformation = new List<CTransactionInformation>();
        }
          public List<CTransactionInformation> TransactionInformationList
        {
            get { return m_liCTransactionInformation; }
            set { m_liCTransactionInformation = value; }
        }
    }
}
