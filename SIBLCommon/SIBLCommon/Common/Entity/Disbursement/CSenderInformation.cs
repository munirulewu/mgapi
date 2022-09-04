using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SIBLCommon.Common.Entity.Bases;

namespace SIBLCommon.Common.Entity.Disbursement
{
    [Serializable]
    public class CSenderInformation : ASIBLEntityBase
    {
        #region Protectd Member
        
        protected string m_sSenderFirstName;
        protected string m_sSenderLastName;
        protected string m_sSenderMiddleName;
        protected string m_sSenderPhone;
        protected string m_sSenderCity;
        protected string m_sSenderState;
        protected string m_sSenderAddress;
        protected string m_sSenderCountry;
        protected string m_sSenderFinancialInstitution;

        #endregion


        #region Constructor
        public CSenderInformation()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_sSenderFirstName = string.Empty;
            m_sSenderLastName = string.Empty;
            m_sSenderCity = string.Empty;
            m_sSenderState = string.Empty;
            m_sSenderAddress = string.Empty;
            m_sSenderCountry = string.Empty;
            m_sSenderFinancialInstitution = string.Empty;
            m_sSenderMiddleName = string.Empty;
            m_sSenderPhone = string.Empty;
        }
    
        #endregion Initialization

        #region public Member

        public string SenderFirstName
        {
            get { return m_sSenderFirstName; }
            set { m_sSenderFirstName = value; }
        }
        public string SenderLastName
        {
            get { return m_sSenderLastName; }
            set { m_sSenderLastName = value; }
        }

        public string SenderMiddleName
        {
            get { return m_sSenderMiddleName; }
            set { m_sSenderMiddleName = value; }
        }
        public string SenderPhone
        {
            get { return m_sSenderPhone; }
            set { m_sSenderPhone = value; }
        }
        public string SenderCity
        {
            get { return m_sSenderCity; }
            set { m_sSenderCity = value; }
        }

        public string SenderState
        {
            get { return m_sSenderState; }
            set { m_sSenderState = value; }
        }

        public string SenderAddress
        {
            get { return m_sSenderAddress; }
            set { m_sSenderAddress = value; }
        }

        public string SenderCountry
        {
            get { return m_sSenderCountry; }
            set { m_sSenderCountry = value; }
        }
        public string SenderFinancialInstitution
        {
            get { return m_sSenderFinancialInstitution; }
            set { m_sSenderFinancialInstitution = value; }
        }
        #endregion
    }
}
