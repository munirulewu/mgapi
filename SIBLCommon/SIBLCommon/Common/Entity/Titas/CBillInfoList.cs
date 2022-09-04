using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.Common.Entity.Titas;

namespace SIBLCommon.SIBLCommon.Common.Entity.Titas
{
    [Serializable]
    public class CBillInfoList : ASIBLEntityBase
    {
         protected List<CBillInfo> m_liBillInfoList;
         public CBillInfoList()
        {
            m_liBillInfoList = new List<CBillInfo>();
        }
         public List<CBillInfo> BillInfoList
        {
            get { return m_liBillInfoList; }
            set { m_liBillInfoList = value; }
        }

    }
}
