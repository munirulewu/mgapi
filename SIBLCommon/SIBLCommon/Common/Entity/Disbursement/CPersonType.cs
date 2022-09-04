
/*
 * File name            :  CPersonType
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
using SIBLXoomCommon.SIBLXoom.Common.Entity.Disbursement;

namespace SIBLCommon.Common.Entity.Disbursement
{
    [Serializable]
    public class CPersonType : ASIBLEntityBase 
    {
        #region Protectd Member
        //protected string m_sName;
        //protected string m_sAddress;
        protected string m_sTelephone;
        protected string m_sTelephone2;
        protected string m_sTelephone3;
        protected string m_sEmail;
        protected CNameType m_oNameType;
        protected CAddress m_oAddress;
        #endregion

        #region Constructor
        public CPersonType()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization

        protected void Initialization()
        {
           // m_sName = string.Empty;
            //m_sAddress = string.Empty;
            m_sTelephone = string.Empty;
            m_sTelephone2 = string.Empty;
            m_sTelephone3 = string.Empty;
            m_sEmail = string.Empty;
            m_oNameType = new CNameType();
            m_oAddress = new CAddress();
        }
    
        #endregion Initialization

        #region public Member

        //public string Name
        //{
        //    get { return m_sName; }
        //    set { m_sName = value; }
        //}

        //public string Address
        //{
        //    get { return m_sAddress; }
        //    set { m_sAddress = value; }
        //}

        public string Telephone
        {
            get { return m_sTelephone; }
            set { m_sTelephone = value; }
        }

        public string Telephone2
        {
            get { return m_sTelephone2; }
            set { m_sTelephone2 = value; }
        }

        public string Telephone3
        {
            get { return m_sTelephone3; }
            set { m_sTelephone3 = value; }
        }

        public string Email
        {
            get { return m_sEmail; }
            set { m_sEmail = value; }
        }

        public CNameType NameType
        {
            get { return m_oNameType; }
            set { m_oNameType = value; }
        }

        public CAddress Address
        {
            get { return m_oAddress; }
            set { m_oAddress = value; }
        }

        
        #endregion
    }
}
