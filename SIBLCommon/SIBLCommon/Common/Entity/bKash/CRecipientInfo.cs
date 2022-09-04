/*
 * File name            : CRecipientInfo.cs
 * Author               : Munirul Islam
 * Date                 : April 23, 2020
 * Version              : 1.0
 *
 * Description          : Entity Class for CRecipientInfo
 *
 * Modification history :
 * Name                         Date                            Desc
 * 
 * 
 * Copyright (c) 2020: SOCIAL ISLAMI BANK LIMITED
 */


using SIBLCommon.Common.Entity.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SIBLCommon.SIBLCommon.Common.Entity.bKash
{
    [Serializable]
    [DataContract]
    public class CRecipientInfo : ASIBLEntityBase
    {
         #region Protectd Member

        protected string m_sCountryCode;
        protected string m_sMSISDN;
        protected string m_FirstName;
        protected string m_sLastName;
        protected string m_FullName;
        #endregion


        #region Constructor
        public CRecipientInfo()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {

            m_sCountryCode = String.Empty;
            m_sMSISDN = String.Empty;

            m_FirstName = string.Empty;
            m_sLastName = String.Empty;
            m_FullName = String.Empty;
           
        }
        #endregion Initialization
       
        #region public Member
        [DataMember]
        public string CountryCode
        {
            get { return m_sCountryCode; }
            set { m_sCountryCode = value; }
        }
        [DataMember]
        public string MSISDN
        {
            get { return m_sMSISDN; }
            set { m_sMSISDN = value; }
        }
        [DataMember]
        public string FirstName
        {
            get { return m_FirstName; }
            set { m_FirstName = value; }
        }
        [DataMember]
        public string LastName
        {
            get { return m_sLastName; }
            set { m_sLastName = value; }
        }
[DataMember]
        public string FullName
        {
            get { return m_FullName; }
            set { m_FullName = value; }
        }

        #endregion     
    }
}
