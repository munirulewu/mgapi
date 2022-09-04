using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.Common.Entity.Disbursement;

namespace SIBLXoomCommon.SIBLXoom.Common.Entity.Disbursement
{
    [Serializable]
    public class CNameTypeList : ASIBLEntityBase
    { 
        protected List<CNameType> m_liNameTypeList;
        public CNameTypeList()
        {
            m_liNameTypeList = new List<CNameType>();
        }
         public List<CNameType> NameTypeList
        {
            get { return m_liNameTypeList; }
            set { m_liNameTypeList = value; }
        }
    }
}
