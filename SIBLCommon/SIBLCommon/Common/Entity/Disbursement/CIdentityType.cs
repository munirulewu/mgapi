/*
 * File name            :  CIdentityType
 * Author               :  Md. Aminul Islam
 * Date                 :  27.05.2015
 *
 * Description          :  
 *
 * Modification history :
 * Name                         Date                            Desc
 *           
 * 
 * Copyright (c)  2015: Social Islami Bank Limited
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLCommon.Common.Entity.Disbursement
{
    [Serializable]
    public class CIdentityType : ASIBLEntityBase
    {
        #region Protectd Member

        protected string m_sName;
        protected string m_sValue;

        #endregion


        #region Constructor
        public CIdentityType()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_sName = string.Empty;
            m_sValue = string.Empty;
        }
        #endregion Initialization

        #region public Member


        public string Name
        {
            get { return m_sName; }
            set { m_sName = value; }
        }

        public string Value
        {
            get { return m_sValue; }
            set { m_sValue = value; }
        }

        #endregion

    }
}
