using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.Common.Entity.Disbursement;

namespace SIBLXoomCommon.SIBLXoom.Common.Entity.Disbursement
{
    [Serializable]
    public class CTransactionUpdateList : ASIBLEntityBase
    {
         protected List<CUpdateResponse> m_liUpdateResponse;
         public CTransactionUpdateList()
        {
            m_liUpdateResponse = new List<CUpdateResponse>();
        }
        public List<CUpdateResponse> UpdateResponse
        {
            get { return m_liUpdateResponse; }
            set { m_liUpdateResponse = value; }
        }
    }
}
