using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLCommon.Common.Entity.CPU
{
    [Serializable]
    public class CRecipientInformationList : ASIBLEntityCollectionBase
    {
       protected List<CRecipientInformation> m_liCRecipientInformationList;
       public CRecipientInformationList()
        {
            m_liCRecipientInformationList = new List<CRecipientInformation>();
        }
       public List<CRecipientInformation> RecipientInformationList
        {
            get { return m_liCRecipientInformationList; }
            set { m_liCRecipientInformationList = value; }
        }
    }
}
