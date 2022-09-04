using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLXoomCommon.SIBLXoom.Common.Entity.CPU
{
    [Serializable]
    public class CChequeList : ASIBLEntityBase
    {
        protected List<CCheque> m_liChequeList;
        public CChequeList()
        {
            m_liChequeList = new List<CCheque>();
        }
        public List<CCheque> ChequeList
        {
            get { return m_liChequeList; }
            set { m_liChequeList = value; }
        }
    }
}
