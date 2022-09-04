/*
 * File name            :  CNameType
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
    public class CNameType : ASIBLEntityBase
    {

        #region Protectd Member
        protected string m_sFirstName;
        protected string m_sLastName;
        protected string m_sMaternalLastName;
        protected string m_sMiddleName;
        protected string m_sLocalizedLastName;

        #endregion


        #region Constructor
        public CNameType()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            
        m_sFirstName = string.Empty;
        m_sLastName = string.Empty;
        m_sMaternalLastName = string.Empty;
        m_sMiddleName = string.Empty;
        m_sLocalizedLastName = string.Empty;
        }
    
        #endregion Initialization

        #region public Member
        public string FirstName
        {
            get { return m_sFirstName; }
            set { m_sFirstName = value; }
        }

        public string LastName
        {
            get { return m_sLastName; }
            set { m_sLastName = value; }
        }

        public string MaternalLastName
        {
            get { return m_sMaternalLastName; }
            set { m_sMaternalLastName = value; }
        }

        public string MiddleName
        {
            get { return m_sMiddleName; }
            set { m_sMiddleName = value; }
        }

        public string LocalizedLastName
        {
            get { return m_sLocalizedLastName; }
            set { m_sLocalizedLastName = value; }
        }
        #endregion
    }
}
