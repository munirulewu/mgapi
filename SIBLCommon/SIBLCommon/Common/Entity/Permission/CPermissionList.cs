/*
 * File name            : CPermissionList.cs
 * Author               : Munirul Islam
 * Date                 : November 10, 2014
 * Version              : 1.0
 *
 * Description          : Entity Class for CPermissionList
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

namespace SIBLCommon.Common.Entity.Permission
{
    [Serializable]
    public class CPermissionList : ASIBLEntityCollectionBase
    {
        protected List<CPermission> m_liPermissionList;
        public CPermissionList()
        {
            m_liPermissionList = new List<CPermission>();
        }
        public List<CPermission> PermissionList
        {
            get { return m_liPermissionList; }
            set { m_liPermissionList = value; }
        }
    }
}
