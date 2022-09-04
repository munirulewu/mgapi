using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLCommon.Common.Entity.CPU
{
    [Serializable]
    public class CSupplementalInformationList : ASIBLEntityCollectionBase
    {
        protected List<CSupplementalInformation> m_liCSupplementalInformation;
       public CSupplementalInformationList()
        {
            m_liCSupplementalInformation = new List<CSupplementalInformation>();
        }
       public List<CSupplementalInformation> SupplementalInformationList
        {
            get { return m_liCSupplementalInformation; }
            set { m_liCSupplementalInformation = value; }
        }
    }
}
