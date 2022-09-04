using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLXoomCommon.SIBLXoom.Common.Entity.BEFTN
{
    [Serializable]
    public class CBEFTNList : ASIBLEntityCollectionBase
    {
        protected List<CBEFTN> m_liBEFTNList;
         public CBEFTNList()
        {
            m_liBEFTNList = new List<CBEFTN>();
        }
         public List<CBEFTN> BEFTNList
        {
            get { return m_liBEFTNList; }
            set { m_liBEFTNList = value; }
        }
    }
}
