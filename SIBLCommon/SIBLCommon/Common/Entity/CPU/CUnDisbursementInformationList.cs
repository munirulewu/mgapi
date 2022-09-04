using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLCommon.Common.Entity.CPU
{
    [Serializable]
    public class CUnDisbursementInformationList : ASIBLEntityCollectionBase
    {
        protected List<CUnDisbursementInformation> m_liCUnDisbursementInformationList;
         public CUnDisbursementInformationList()
        {
            m_liCUnDisbursementInformationList = new List<CUnDisbursementInformation>();
        }
         public List<CUnDisbursementInformation> UnDisbursementInformationList
        {
            get { return m_liCUnDisbursementInformationList; }
            set { m_liCUnDisbursementInformationList = value; }
        }
    }
}
