using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLXoomCommon.SIBLXoom.Common.Entity.Disbursement
{
    [Serializable]
    public class CCBSTransactionResponseList : ASIBLEntityBase
    {
         protected List<CCBSTransactionResponse> m_liCCBSTransactionResponse;
         public CCBSTransactionResponseList() 
        {
            m_liCCBSTransactionResponse = new List<CCBSTransactionResponse>();
        }
        public List<CCBSTransactionResponse> CCBSTransactionResponse
        {
            get { return m_liCCBSTransactionResponse; }
            set { m_liCCBSTransactionResponse = value; }
        }
    }
}
