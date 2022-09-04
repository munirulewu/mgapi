using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.Common.Entity.Disbursement;

namespace SIBLXoomCommon.SIBLXoom.Common.Entity.Disbursement
{
    [Serializable]
    public class CDeliveryRecipientList : ASIBLEntityBase
    {
        protected List<CDeliveryRecipient> m_liDeliveryRecipientList;
        public CDeliveryRecipientList()
        {
            m_liDeliveryRecipientList = new List<CDeliveryRecipient>();
        }
        public List<CDeliveryRecipient> DeliveryRecipientList
        {
            get { return m_liDeliveryRecipientList; }
            set { m_liDeliveryRecipientList = value; }
        }
    }
}
