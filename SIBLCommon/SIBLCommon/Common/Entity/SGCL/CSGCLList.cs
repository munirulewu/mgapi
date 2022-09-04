using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIBLCommon.SIBLCommon.Common.Entity.SGCL
{
    [Serializable]
    public class CSGCLList
    {
         protected List<CSGCL> m_liBillInfoList;
         public CSGCLList()
        {
            m_liBillInfoList = new List<CSGCL>();
        }
         public List<CSGCL> BillInfoList
        {
            get { return m_liBillInfoList; }
            set { m_liBillInfoList = value; }
        }
    }
}
