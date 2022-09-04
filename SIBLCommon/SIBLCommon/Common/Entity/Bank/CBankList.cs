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
using System.Runtime.Serialization;
namespace SIBLCommon.Common.Entity.Bank
{
    [Serializable]
    public class CBankList : ASIBLEntityCollectionBase
    {
        protected List<CBank> m_liCBankList;
        public CBankList()
        {
            m_liCBankList = new List<CBank>();
        }
        [DataMember]
        public List<CBank> BankList
        {
            get { return m_liCBankList; }
            set { m_liCBankList = value; }
        }

    }
}
