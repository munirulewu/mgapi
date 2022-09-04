/*
 * File name            : CAllLookupList.cs
 * Author               : Munirul Islam
 * Date                 : March 31, 2014
 * Version              : 1.0
 *
 * Description          : Entity Class for CAllLookupList
 *
 * Modification history :
 * Name                         Date                            Desc
 * 
 * 
 * Copyright (c) 2014: SOCIAL ISLAMI BANK LIMITED
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;
namespace SIBLCommon.Common.Entity.Bank
{
    [Serializable]
    public class CBranchList : ASIBLEntityCollectionBase
    {
        protected List<CBranch> m_liBranchList;
        public CBranchList()
        {
            m_liBranchList = new List<CBranch>();
        }
        public List<CBranch> BranchList
        {
            get { return m_liBranchList; }
            set { m_liBranchList = value; }
        }

    }
}
