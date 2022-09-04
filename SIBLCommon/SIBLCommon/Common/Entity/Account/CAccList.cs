using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.Common.Entity.Account;

namespace SIBLCommon.Common.Entity.Account
{
    [Serializable]
    public  class CAccList :ASIBLEntityCollectionBase

    {
      protected List<CAccInfo> m_liAccInfoList;
        protected string m_sTableKey;
        public CAccList()
        {
            m_liAccInfoList = new List<CAccInfo>();
        }
        public List<CAccInfo>  AccInfoList
        {
            get { return m_liAccInfoList; }
            set { m_liAccInfoList = value; }
        }
    }
}
