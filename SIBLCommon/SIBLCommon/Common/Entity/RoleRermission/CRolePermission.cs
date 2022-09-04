/*
 * File name            : CRolePermission.cs
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
using SIBLCommon.Common.Entity.Role;
using SIBLCommon.Common.Entity.Permission;

namespace SIBLCommon.Common.Entity.RolePermission
{
    [Serializable]
    public class CRolePermission : ASIBLEntityBase
    {
        #region Protectd Member
        protected CRole m_oRole;
        protected CPermission m_oPermission;
        protected string m_sReadOnly;
        protected string m_sWriteOnly;
        protected string m_sEditOnly;
        protected string m_sDeleteOnly;
        protected string m_sOperationType;
       
        #endregion

        #region Constructor
        public CRolePermission()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {

            m_oRole = new CRole();
            m_oPermission = new CPermission();
            m_sReadOnly = string.Empty;
            m_sWriteOnly = string.Empty;
            m_sEditOnly = string.Empty;
            m_sDeleteOnly = string.Empty;
            m_sOperationType = string.Empty;
           

        }
        #endregion Initialization

        #region public Member

        
        public string Read_Only
        {
            get { return m_sReadOnly; }
            set { m_sReadOnly = value; }
        }
        public string Write_Only
        {
            get { return m_sWriteOnly; }
            set { m_sWriteOnly = value; }
        }
        public string Edit_Only
        {
            get { return m_sEditOnly; }
            set { m_sEditOnly = value; }
        }
        public string Delete_Only
        {
            get { return m_sDeleteOnly; }
            set { m_sDeleteOnly = value; }
        }
        public CRole Role
        {
            get { return m_oRole; }
            set { m_oRole = value; }
        }
        public CPermission Permission
        {
            get { return m_oPermission; }
            set { m_oPermission = value; }
        }
        public string OperationType
        {
            get { return m_sOperationType; }
            set { m_sOperationType = value; }
        }
        #endregion

    }
}
