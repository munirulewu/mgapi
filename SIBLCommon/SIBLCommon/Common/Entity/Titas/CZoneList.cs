using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLCommon.SIBLCommon.Common.Entity.Titas
{
    [Serializable]
    public class CZoneList : ASIBLEntityBase
    {
        protected List<CZone> m_liZoneList;
        public CZoneList()
        {
            m_liZoneList = new List<CZone>();
        }
        public List<CZone> ZoneList
        {
            get { return m_liZoneList; }
            set { m_liZoneList = value; }
        }
    }
}
