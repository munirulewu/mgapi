using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.Common.Entity.Disbursement;

namespace SIBLXoomCommon.SIBLXoom.Common.Entity.Disbursement
{
    [Serializable]
    public class CIdentityTypeList : ASIBLEntityBase
    {
        protected List<CIdentityType> m_liIdentityTypeList;
        public CIdentityTypeList()
        {
            m_liIdentityTypeList = new List<CIdentityType>();
        }        
        public List<CIdentityType> IdentityTypeList
        {
            get { return m_liIdentityTypeList; }
            set { m_liIdentityTypeList = value; }
        }
    }
}
