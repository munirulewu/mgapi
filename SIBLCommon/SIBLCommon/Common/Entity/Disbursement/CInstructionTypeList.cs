using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.Common.Entity.Disbursement;

namespace SIBLXoomCommon.SIBLXoom.Common.Entity.Disbursement
{
    [Serializable]
    public class CInstructionTypeList : ASIBLEntityBase
    {
        protected List<CInstructionType> m_liInstructionTypeList;
        public CInstructionTypeList()
        {
            m_liInstructionTypeList = new List<CInstructionType>();
        }
        public List<CInstructionType> InstructionTypeList
        {
            get { return m_liInstructionTypeList; }
            set { m_liInstructionTypeList = value; }
        }
    }
}
