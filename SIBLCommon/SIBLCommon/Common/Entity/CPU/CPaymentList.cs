using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLXoomCommon.Common.Entity.CPU;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLXoomCommon.SIBLXoom.Common.Entity.CPU
{
   [Serializable]
    public class CPaymentList : ASIBLEntityCollectionBase
    {
       protected List<CPayment> m_liCPaymentList;
       public CPaymentList()
        {
            m_liCPaymentList = new List<CPayment>();
        }
       public List<CPayment> PaymentList
        {
            get { return m_liCPaymentList; }
            set { m_liCPaymentList = value; }
        }

    }   
}
