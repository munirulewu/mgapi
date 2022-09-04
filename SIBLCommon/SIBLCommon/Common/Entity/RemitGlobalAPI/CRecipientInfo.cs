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

namespace SIBLCommon.SIBLCommon.Common.Entity.RemitGlobalAPI
{
    [Serializable]
    [DataContract]

    public enum CurrentIdType
    {
        NationalIdentityCard = 1,
        Passport = 2,
        DrivingLicense = 3,
        Others = 4
    }
    public class CRecipientInfo : ASIBLEntityBase
    {
        #region Protectd Member

        protected string m_sCountryCode;
        protected string m_sMSISDN;
        protected string m_FirstName;
        protected string m_sLastName;
        protected string m_FullName;
        protected string m_Address;
        protected string m_Location;
        protected string m_IdNo;
        protected string m_IdType;
        protected string m_IdIssueDate;
        protected string m_IdExpiryDate;
        private CurrentIdType? _IdType;
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

            m_Address = string.Empty;
            m_Location = string.Empty;
            m_IdNo = string.Empty;
            m_IdType = string.Empty;
            m_IdIssueDate = string.Empty;
            m_IdExpiryDate = string.Empty;


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
        public string ContactNo
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
        [DataMember]
        public string Location
        {
            get { return m_Location; }
            set { m_Location = value; }
        }
        [DataMember]
        public string IdNo
        {
            get { return m_IdNo; }
            set { m_IdNo = value; }
        }
        [DataMember]
        public CurrentIdType? IdType
        {
            get { return _IdType; }
            set { _IdType = value; }
        }
        [DataMember]
        public string IdIssueDate
        {
            get { return m_IdIssueDate; }
            set { m_IdIssueDate = value; }
        }
        [DataMember]
        public string IdExpiryDate
        {
            get { return m_IdExpiryDate; }
            set { m_IdExpiryDate = value; }
        }
        #endregion
    }
}
