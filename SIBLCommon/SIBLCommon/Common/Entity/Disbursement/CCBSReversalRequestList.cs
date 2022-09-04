using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLXoomCommon.SIBLXoom.Common.Entity.Disbursement
{
    [Serializable]
    public class CCBSReversalRequestList : ASIBLEntityBase
    {
         protected List<CCBSReversalRequest> m_liCCBSReversalRequest;
         public CCBSReversalRequestList()
        {
            m_liCCBSReversalRequest = new List<CCBSReversalRequest>();
        }
        public List<CCBSReversalRequest> CCBSReversalRequest
        {
            get { return m_liCCBSReversalRequest; }
            set { m_liCCBSReversalRequest = value; }
        }
    }
}
