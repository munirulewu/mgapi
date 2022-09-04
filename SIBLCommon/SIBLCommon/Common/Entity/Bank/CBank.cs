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
using System.Runtime.Serialization;

namespace  SIBLCommon.Common.Entity.Bank
{
    [Serializable]
    [DataContract]
    public class CBank : ASIBLEntityBase
    {
        #region Protectd Member
      
        protected string m_sBankName;
        protected string m_sBankCode;
        protected DateTime m_dCREATE_DATE;
        protected string m_sOperatiom_Type;
        protected string m_sFromDate;
        protected string m_sToDate;
        protected string m_sCompany;
        string m_sRecordNumber;
        #endregion


        #region Constructor
        public CBank()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {

            m_sBankName = String.Empty;
            m_sBankCode = String.Empty;
            m_dCREATE_DATE = new DateTime();
            m_sOperatiom_Type = string.Empty;
            m_sFromDate = String.Empty;
            m_sToDate = String.Empty;
            m_sCompany = string.Empty;
            m_sRecordNumber = string.Empty;
        }
        #endregion Initialization
       
        #region public Member

        public string Company
        {
            get { return m_sCompany; }
            set { m_sCompany = value; }
        }
        [DataMember]
         public string BankCode
        {
            get { return m_sBankCode; }
            set { m_sBankCode = value; }
        }
         [DataMember]
        public string BankName
        {
            get { return m_sBankName; }
            set { m_sBankName = value; }
        }

         [DataMember]
         public string RecordNumber
         {
             get { return m_sRecordNumber; }
             set { m_sRecordNumber = value; }
         }
        public DateTime CREATE_DATE
        {
            get { return m_dCREATE_DATE; }
            set { m_dCREATE_DATE = value; }
        }
        public string Operatiom_Type
        {
            get { return m_sOperatiom_Type; }
            set { m_sOperatiom_Type = value; }
        }

        public string FromDate
        {
            get { return m_sFromDate; }
            set { m_sFromDate = value; }
        }

        public string ToDate
        {
            get { return m_sToDate; }
            set { m_sToDate = value; }
        }
        #endregion     
    }
}
