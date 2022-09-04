using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLCommon.Common.Entity.CPU
{
    [Serializable]
    public class CDisbursementInformationList : ASIBLEntityCollectionBase
    {
       protected List<CDisbursementInformation> m_liCDisbursementInformationList;
       public CDisbursementInformationList()
        {
            m_liCDisbursementInformationList = new List<CDisbursementInformation>();
        }
       public List<CDisbursementInformation> DisbursementInformationList
        {
            get { return m_liCDisbursementInformationList; }
            set { m_liCDisbursementInformationList = value; }
        }
    }
}
