using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLXoomCommon.SIBLXoom.Common.Entity.Disbursement
{
    [Serializable]
    public class CCBSErrorDetailList : ASIBLEntityBase
    {
        protected List<CCBSErrorDetail> m_liCCBSErrorDetailList;
        public CCBSErrorDetailList()
        {
            m_liCCBSErrorDetailList = new List<CCBSErrorDetail>();
        }
        public List<CCBSErrorDetail> CCCBSErrorDetailList
        {
            get { return m_liCCBSErrorDetailList; }
            set { m_liCCBSErrorDetailList = value; }
        }
    }
}
