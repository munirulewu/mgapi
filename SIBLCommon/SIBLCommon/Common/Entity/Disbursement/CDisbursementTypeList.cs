using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.Common.Entity.Disbursement;

namespace SIBLXoomCommon.SIBLXoom.Common.Entity.Disbursement
{
    [Serializable]
    public class CDisbursementTypeList : ASIBLEntityBase
    {
        protected List<CIdentityType> m_liDisbursementTypeList;
         public CDisbursementTypeList()
        {
            m_liDisbursementTypeList = new List<CIdentityType>();
        }
         public List<CIdentityType> DisbursementTypeList
        {
            get { return m_liDisbursementTypeList; }
            set { m_liDisbursementTypeList = value; }
        }
    }
}
