/*
 * File name            : CSenderInfo.cs
 * Author               : Munirul Islam
 * Date                 : April 23, 2020
 * Version              : 1.0
 *
 * Description          : Entity Class for CSenderInfo
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
using System.Text;

namespace SIBLCommon.SIBLCommon.Common.Entity.bKash
{
    [Serializable]
    public class CSenderInfo : ASIBLEntityBase
    {
        string ms_address;
        string ms_dob;
        string ms_documentNumber;
        string ms_documentType;
        string ms_firstName;
        string ms_kycPurpose;
        string ms_kycSourceOfFunds;
        string ms_lastName;
        string ms_location;
        string ms_msisdn;
        string ms_nationality;
        string ms_pob;
        string ms_idexpiryDate;
        string ms_idissueDate;

        string ms_TimeStamp;
        string ms_RefTimeStamp;


        #region Constructor
        public CSenderInfo()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {

            ms_address = string.Empty;
            ms_dob = string.Empty;
            ms_documentNumber = string.Empty;
            ms_documentType = string.Empty;
            ms_firstName = string.Empty;
            ms_kycPurpose = string.Empty;
            ms_kycSourceOfFunds = string.Empty;
            ms_lastName = string.Empty;
            ms_location = string.Empty;
            ms_msisdn = string.Empty;
            ms_nationality = string.Empty;
            ms_pob = string.Empty;
            ms_idexpiryDate = string.Empty;
            ms_idissueDate = string.Empty;
            ms_TimeStamp = string.Empty;
            ms_RefTimeStamp = string.Empty;

        }
        #endregion Initialization
       
        #region public Member

        public string TimeStamp
        {
            get { return ms_TimeStamp; }
            set { ms_TimeStamp = value; }
        }
        public string RefTimeStamp
        {
            get { return ms_RefTimeStamp; }
            set { ms_RefTimeStamp = value; }
        }

        public string pob
        {
            get { return ms_pob; }
            set { ms_pob = value; }
        }


        public string idexpiryDate
        {
            get { return ms_idexpiryDate; }
            set { ms_idexpiryDate = value; }
        }

        public string idissueDate
        {
            get { return ms_idissueDate; }
            set { ms_idissueDate = value; }
        }

        public string nationality
        {
            get { return ms_nationality; }
            set { ms_nationality = value; }
        }

        public string msisdn
        {
            get { return ms_msisdn; }
            set { ms_msisdn = value; }
        }

        public string location
        {
            get { return ms_location; }
            set { ms_location = value; }
        }

        public string lastName
        {
            get { return ms_lastName; }
            set { ms_lastName = value; }
        }

        public string kycSourceOfFunds
        {
            get { return ms_kycSourceOfFunds; }
            set { ms_kycSourceOfFunds = value; }
        }

        public string kycPurpose
        {
            get { return ms_kycPurpose; }
            set { ms_kycPurpose = value; }
        }
        public string firstName
        {
            get { return ms_firstName; }
            set { ms_firstName = value; }
        }

        public string address
        {
            get { return ms_address; }
            set { ms_address = value; }
        }
        public string dob
        {
            get { return ms_dob; }
            set { ms_dob = value; }
        }
        public string documentNumber
        {
            get { return ms_documentNumber; }
            set { ms_documentNumber = value; }
        }
        public string documentType
        {
            get { return ms_documentType; }
            set { ms_documentType = value; }
        }
        
        #endregion     
    }
}
