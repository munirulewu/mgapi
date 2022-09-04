using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.Common.Entity.Disbursement;

namespace SIBLXoomCommon.SIBLXoom.Common.Entity.Disbursement
{
    [Serializable]
    public class CMoneyTypeList : ASIBLEntityBase
    {
        protected List<CMoneyType> m_liMoneyTypeList;
        public CMoneyTypeList()
        {
            m_liMoneyTypeList = new List<CMoneyType>();
        }
        public List<CMoneyType> MoneyTypeList
        {
            get { return m_liMoneyTypeList; }
            set { m_liMoneyTypeList = value; }
        }
    }
}
