using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.Common.Entity.Disbursement;

namespace SIBLXoomCommon.SIBLXoom.Common.Entity.Disbursement
{
    [Serializable]
    public class CRecipientBankDetailsList : ASIBLEntityBase
    {
        protected List<CRecipientBankDetails> m_liRecipientBankDetails;
         public CRecipientBankDetailsList()
        {
            m_liRecipientBankDetails = new List<CRecipientBankDetails>();
        }
         public List<CRecipientBankDetails> RecipientBankDetails
        {
            get { return m_liRecipientBankDetails; }
            set { m_liRecipientBankDetails = value; }
        }
    }
}
