/*
 * File name            : CRoleList.cs
 * Author               : Munirul Islam
 * Date                 : April 15, 2014November 10, 2014
 * Version              : 1.0
 *
 * Description          : Entity Class for RoleList
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
using SIBLCommon.Common.Entity.Role;


namespace SIBLCommon.Common.Entity.Role
{
    [Serializable]
    public class CRoleList : ASIBLEntityCollectionBase
    {
        protected List<CRole> m_liRoleList;
        public CRoleList()
        {
            m_liRoleList = new List<CRole>();
        }
        public List<CRole> RoleDataList
        {
            get { return m_liRoleList; }
            set { m_liRoleList = value; }
        }

    }
}
