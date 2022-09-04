/*
 * File name            : CUser.cs
 * Author               : Munirul Islam
 * Date                 : November 10,2014
 * Version              : 1.0
 *
 * Description          : Entity Class for UserList
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

namespace SIBLCommon.Common.Entity.User
{
    [Serializable]
    public class CUserList : ASIBLEntityCollectionBase
    {
        protected List<CUser> m_liUserList;
        public CUserList()
        {
            m_liUserList = new List<CUser>();
        }
        public List<CUser> UserDataList
        {
            get { return m_liUserList; }
            set { m_liUserList = value; }   
        }
    }
}
