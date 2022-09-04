/*
 * File name            : CRole.cs
 * Author               : MUnirul Islam
 * Date                 : November 10, 2014
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

namespace SIBLCommon.Common.Entity.Role
{
    [Serializable]
    public class CRole : ASIBLEntityBase
    {
        #region Protectd Member

        private string m_sRoleName;
        private string m_sCREATE_DATE;
        protected string m_sOperationType;

        #endregion

        #region Constructor

        public CRole(): base()
        {
            Initialization();
        }

        #endregion Constructor

        #region Initialization

        protected void Initialization()
        {
            m_sRoleName = string.Empty;
            m_sCREATE_DATE = string.Empty;
            m_sOperationType = string.Empty;
        }
        #endregion

        #region public Member

        public string RoleName
        {
            get { return m_sRoleName; }
            set { m_sRoleName = value; }
        }

        public string CREATE_DATE
        {
            get { return m_sCREATE_DATE; }
            set { m_sCREATE_DATE = value; }
        }
        public string OperationType
        {
            get { return m_sOperationType; }
            set { m_sOperationType = value; }
        }
        #endregion
    }
}
