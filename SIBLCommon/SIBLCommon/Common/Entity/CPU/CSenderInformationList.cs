using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLCommon.Common.Entity.CPU
{
    [Serializable]
    public class CSenderInformationList : ASIBLEntityCollectionBase
    {
        protected List<CSenderInformation> m_liCSenderInformation;
       public CSenderInformationList()
        {
            m_liCSenderInformation = new List<CSenderInformation>();
        }
       public List<CSenderInformation> SenderInformationList
        {
            get { return m_liCSenderInformation; }
            set { m_liCSenderInformation = value; }
        }
    }
}
