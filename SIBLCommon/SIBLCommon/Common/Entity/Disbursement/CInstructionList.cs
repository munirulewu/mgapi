using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.Common.Entity.Disbursement;

namespace SIBLCommon.Common.Entity.Disbursement
{
   
    [Serializable]
    public class CInstructionList : ASIBLEntityCollectionBase
    {
        protected List<CInstruction> m_liInstructionList;
        public CInstructionList()
        {
            m_liInstructionList = new List<CInstruction>();
        }
        public List<CInstruction>  InstructionList
        {
            get { return m_liInstructionList; }
            set { m_liInstructionList = value; }
        }

    }

}
