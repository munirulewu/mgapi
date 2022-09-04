/*
 * File name            :  CAddressType
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
    public class CAddressTypeList : ASIBLEntityBase
    {
        #region Protectd Member

        protected string m_sAddress1;
        protected string m_sAddress2;
        protected string m_sAddress3;
        protected string m_sNeighborhood;
        protected string m_sCity;
        protected string m_sCounty;
        protected string m_sState;
        protected string m_sRegion;
        protected string m_sPostalCode;
        protected string m_sCountry;

        #endregion


        #region Constructor
        public CAddressTypeList()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_sAddress1 = string.Empty;
            m_sAddress2 = string.Empty;
            m_sAddress3 = string.Empty;
            m_sNeighborhood = string.Empty;
            m_sCity = string.Empty;
            m_sCounty = string.Empty;
            m_sState = string.Empty;
            m_sRegion = string.Empty;
            m_sPostalCode = string.Empty;
            m_sCountry = string.Empty;
        }
    
        #endregion Initialization

        #region public Member
        public string Address1
        {
            get { return m_sAddress1; }
            set { m_sAddress1 = value; }
        }
        public string Address2
        {
            get { return m_sAddress2; }
            set { m_sAddress2 = value; }
        }
        public string Address3
        {
            get { return m_sAddress3; }
            set { m_sAddress3 = value; }
        }

        public string Neighborhood
        {
            get { return m_sNeighborhood; }
            set { m_sNeighborhood = value; }
        }

        public string City
        {
            get { return m_sCity; }
            set { m_sCity = value; }
        }
        
        public string County
        {
            get { return m_sCounty; }
            set { m_sCounty = value; }
        }
        public string State
        {
            get { return m_sState; }
            set { m_sState = value; }
        }
        public string Region
        {
            get { return m_sRegion; }
            set { m_sRegion = value; }
        }
        public string PostalCode
        {
            get { return m_sPostalCode; }
            set { m_sPostalCode = value; }
        }
         public string Country
        {
            get { return m_sCountry; }
            set { m_sCountry = value; }
        }

        #endregion
    }
}
