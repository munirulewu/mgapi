using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLXoomCommon.SIBLXoom.Common.Entity.Disbursement
{
    [Serializable]
    public class CCBSTransactionRequestList : ASIBLEntityBase
    {
         protected List<CCBSTransactionRequest> m_liCCBSTransactionRequest;
         public CCBSTransactionRequestList() 
        {
            m_liCCBSTransactionRequest = new List<CCBSTransactionRequest>();
        }
        public List<CCBSTransactionRequest> CCBSTransactionRequest
        {
            get { return m_liCCBSTransactionRequest; }
            set { m_liCCBSTransactionRequest = value; }
        }
    }
}
