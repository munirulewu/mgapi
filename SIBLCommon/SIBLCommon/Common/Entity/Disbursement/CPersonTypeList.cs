using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.Common.Entity.Disbursement;

namespace SIBLXoomCommon.SIBLXoom.Common.Entity.Disbursement
{
    [Serializable]
    public class CPersonTypeList : ASIBLEntityBase
    {
        protected List<CPersonType> m_liPersonTypeList;
        public CPersonTypeList()
        {
            m_liPersonTypeList = new List<CPersonType>();
        }
         public List<CPersonType> PersonTypeList
        {
            get { return m_liPersonTypeList; }
            set { m_liPersonTypeList = value; }
        }
    }
}
