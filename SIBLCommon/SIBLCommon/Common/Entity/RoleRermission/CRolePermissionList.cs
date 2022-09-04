/*
 * File name            : CRolePermissionList.cs
 * Author               : Munirul Islam
 * Date                 : November 10, 2014
 * Version              : 1.0
 *
 * Description          : Entity Class for CRolePermission  
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

namespace SIBLCommon.Common.Entity.RolePermission
{
    [Serializable]
    public class CRolePermissionList : ASIBLEntityCollectionBase  
    {
        protected List<CRolePermission> m_liRolePermissionList;
        public CRolePermissionList()
        {
            m_liRolePermissionList = new List<CRolePermission>();
        }
        public List<CRolePermission> RolePermissionList
        {
            get { return m_liRolePermissionList; }    
            set { m_liRolePermissionList = value; }
        }
    }
}
