/*
 * File name            : CAllLookup.cs
 * Author               : Munirul Islam
 * Date                 : March 31, 2014
 * Version              : 1.0
 *
 * Description          : Entity Class for CAllLookup
 *
 * Modification history :
 * Name                         Date                            Desc
 * 
 * 
 * Copyright (c) 2014: SOCIAL ISLAMI BANK LIMITED
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.Common.Entity.District;
using SIBLCommon.Common.Entity.AllLookup;
using System.Runtime.Serialization;
using Newtonsoft.Json;
namespace  SIBLCommon.Common.Entity.Bank
{
    [Serializable]
    [DataContract]
    public class CBranch : ASIBLEntityBase
    {
        #region Protectd Member

        protected string m_sBankId;
        protected string m_sBranch_NAME;
        protected CBank  m_oBank;
        protected CDistrict m_oDistrict;
        protected string m_sRoutingNumber;
        protected DateTime m_dCREATE_DATE;
        protected CAllLookup m_oCompany;
        protected string m_sOperatiom_Type;
        protected string m_sBranchCode;
        protected string m_sRecordNumber;
       
        #endregion


        #region Constructor
        public CBranch()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_sBankId = string.Empty;
            m_sBranch_NAME = String.Empty;
            m_oBank = new CBank();
            m_oDistrict = new CDistrict();
            m_sRoutingNumber = String.Empty;
            m_dCREATE_DATE = new DateTime();
            m_oCompany = new CAllLookup();
            m_sOperatiom_Type = string.Empty;
            m_sBranchCode = string.Empty;
            m_sRoutingNumber = string.Empty;
            m_sRecordNumber = string.Empty;
        }
        #endregion Initialization
       
        #region public Member

        [DataMember]
        public string BankId
        {
            get { return m_sBankId; }
            set { m_sBankId = value; }
        }
         [DataMember]
        public string RecordNumber
        {
            get { return m_sRecordNumber; }
            set { m_sRecordNumber = value; }
        }
         [DataMember]
        public string BranchName
        {
            get { return m_sBranch_NAME; }
            set { m_sBranch_NAME = value; }
        }
         [DataMember]
        public string BranchCode
        {
            get { return m_sBranchCode; }
            set { m_sBranchCode = value; }
        }
         [DataMember]
        public string RoutingNumber
        {
            get { return m_sRoutingNumber; }
            set { m_sRoutingNumber = value; }
        }
              
        public CBank Bank
        {
            get { return m_oBank; }
            set { m_oBank = value; }
        }

        public  CDistrict District
        {
            get { return m_oDistrict; }
            set { m_oDistrict = value; }
        }

        
        

        public DateTime CREATE_DATE
        {
            get { return m_dCREATE_DATE; }
            set { m_dCREATE_DATE = value; }
        }

        public CAllLookup CompanyInfo
        {
            get { return m_oCompany; }
            set { m_oCompany = value; }
        }
        public string Operatiom_Type
        {
            get { return m_sOperatiom_Type; }
            set { m_sOperatiom_Type = value; }
        }
        #endregion     
    }
}
