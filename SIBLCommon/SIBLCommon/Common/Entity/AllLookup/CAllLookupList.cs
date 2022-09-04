/*
 * File name            : CAllLookupList.cs
 * Author               : Munirul Islam
 * Date                 :November 10, 2014
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
namespace SIBLCommon.Common.Entity.AllLookup
{
    [Serializable]
    public class CAllLookupList : ASIBLEntityCollectionBase
    {
        protected List<CAllLookup> m_liAllLookupList;
        protected string m_sTableKey;
        public CAllLookupList()
        {
            m_liAllLookupList = new List<CAllLookup>();
        }
        public List<CAllLookup> AllLookupList
        {
            get { return m_liAllLookupList; }
            set { m_liAllLookupList = value; }
        }
        public string TableKey 
        {
            get { return m_sTableKey; }
            set { m_sTableKey = value; }
        }
    }
}
