using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLCommon.SIBLCommon.Common.Entity.Shipping
{
    [Serializable]
    public class CShippingList : ASIBLEntityCollectionBase
    {
         protected List<CShipping> m_liShippingList;
         public CShippingList()
        {
            m_liShippingList = new List<CShipping>();
        }
         public List<CShipping> ShippingList
        {
            get { return m_liShippingList; }
            set { m_liShippingList = value; }
        }
    }
}
