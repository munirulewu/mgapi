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
using System.Runtime.Serialization;
using System.Text;

namespace SIBLCommon.SIBLCommon.Common.Entity.RemitGlobalAPI
{
    [Serializable]
    [DataContract]

    public enum CurrentKycSourceOfFund
    {
        IncomeFromSalary = 1,
        IncomeFromBusiness = 2,
        Others = 3
    }

    public enum CurrentKycPurpose
    {
        Personal = 1,
        Business = 2,
        Others = 3
    }
    public enum CurrentDocumentType
    {
        NationalIdentityCard = 1,
        Passport = 2,
        DrivingLicense = 3,
        Others = 4
    }
    public class CSenderInfo : ASIBLEntityBase
    {
        string ms_address;
        string ms_dob;
        string ms_documentNumber;
        string ms_documentType;
        string ms_firstName;
        string ms_lastName;       
        string ms_location;
        string ms_msisdn;
        string ms_nationality;
        string ms_pob;
        string ms_idexpiryDate;
        string ms_idissueDate;
        private CurrentKycSourceOfFund? _KycSourceOfFund;
        private CurrentKycPurpose? _KycPurpose;
        private CurrentDocumentType _DocumentType;


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
            ms_lastName = string.Empty;
            ms_location = string.Empty;
            ms_msisdn = string.Empty;
            ms_nationality = string.Empty;
            ms_pob = string.Empty;
            ms_idexpiryDate = string.Empty;
            ms_idissueDate = string.Empty;
           

        }
        #endregion Initialization
       
        #region public Member

      

        public string Pob
        {
            get { return ms_pob; }
            set { ms_pob = value; }
        }


        public string IdExpiryDate
        {
            get { return ms_idexpiryDate; }
            set { ms_idexpiryDate = value; }
        }

        public string IdIssueDate
        {
            get { return ms_idissueDate; }
            set { ms_idissueDate = value; }
        }

        public string Nationality
        {
            get { return ms_nationality; }
            set { ms_nationality = value; }
        }

        public string ContactNo
        {
            get { return ms_msisdn; }
            set { ms_msisdn = value; }
        }

        public string Location
        {
            get { return ms_location; }
            set { ms_location = value; }
        }

        public string LastName
        {
            get { return ms_lastName; }
            set { ms_lastName = value; }
        }

       
        
        public string FirstName
        {
            get { return ms_firstName; }
            set { ms_firstName = value; }
        }

        public string Address
        {
            get { return ms_address; }
            set { ms_address = value; }
        }
        public string Dob
        {
            get { return ms_dob; }
            set { ms_dob = value; }
        }
        public string DocumentNumber
        {
            get { return ms_documentNumber; }
            set { ms_documentNumber = value; }
        }
       
        public CurrentKycSourceOfFund? KycSourceOfFund
        {
            get { return _KycSourceOfFund; }
            set { _KycSourceOfFund = value; }
        }

        public CurrentKycPurpose? KycPurpose
        {
            get { return _KycPurpose; }
            set { _KycPurpose = value; }
        }

        public CurrentDocumentType DocumentType 
        {
            get { return _DocumentType; }
            set { _DocumentType = value; }
        }

        

//get
//DocumentType theDocumentType = CurrentDocumentType;

        #endregion     
    }
}
