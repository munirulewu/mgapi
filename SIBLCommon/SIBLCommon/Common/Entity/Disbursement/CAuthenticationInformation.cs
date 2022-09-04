/*
 * File name            :  CAuthenticationInformation
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
using SIBLCommon.Common.Entity.Disbursement;

namespace SIBLXoomCommon.SIBLXoom.Common.Entity.Disbursement
{
    [Serializable]
    public class CAuthenticationInformation : ASIBLEntityBase
    {
        #region Protectd Member
        protected string m_sPartnerId;
        protected string m_sUserName;
        protected string m_sPassword;
        #endregion


        #region Constructor
        public CAuthenticationInformation()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_sPartnerId = string.Empty;
            m_sUserName = string.Empty;
            m_sPassword = string.Empty;
        }
    
        #endregion Initialization

        #region public Member

        public string PartnerId 
        {
            get { return m_sPartnerId; }
            set { m_sPartnerId = value; }
        }

        public string UserName
        {
            get { return m_sUserName; }
            set { m_sUserName = value; }
        }


        public string Password
        {
            get { return m_sPassword; }
            set { m_sPassword = value; }
        }

        #endregion
    }
}
