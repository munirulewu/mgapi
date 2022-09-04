using SIBLCommon.Common.Entity.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIBLCommon.SIBLCommon.Common.Entity.MGAPI
{
   public class CAdditionalDataList
    {
        protected List<CAdditionData> m_liAdditionalDataList;
        public CAdditionalDataList()
        {
            m_liAdditionalDataList = new List<CAdditionData>();
        }
        public List<CAdditionData> AdditionalData
        {
            get { return m_liAdditionalDataList; }
            set { m_liAdditionalDataList = value; }
        }
    }
}
