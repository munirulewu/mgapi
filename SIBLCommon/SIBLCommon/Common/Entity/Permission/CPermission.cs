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
using SIBLCommon.Common.Entity.Role;

namespace SIBLCommon.Common.Entity.Permission
{
    [Serializable]
    public class CPermission : ASIBLEntityBase    
    {
        protected string m_sPermission_NAME;
        protected string m_sPermission_code;


        #region Constructor
        public CPermission()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_sPermission_NAME = string.Empty;
            m_sPermission_code = string.Empty;
        }
        #endregion Initialization

        #region public Member

        public string PermissionName
        {
            get { return m_sPermission_NAME; }
            set { m_sPermission_NAME = value; }
        }
        public string PermissiontCode
        {
            get { return m_sPermission_code; }
            set { m_sPermission_code = value; }
        }

        #endregion  



    }
}
