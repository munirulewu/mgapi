
/*
 * File name            :  CRecipientBankDetails
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
    public class CRecipientBankDetails : ASIBLEntityBase
    {
        #region Protectd Member
        protected string m_sBankName;
        protected string m_sAccountNumber;
        protected string m_sAccountName;
        protected string m_sAccountType;
        protected string m_sRoutingNumber;
        protected string m_sBankCode;
        protected string m_sBranchName;
        protected string m_sBranchCode;
        protected string m_sBranchDistrict;
        protected string m_sSubBankName;
        protected string m_sSubBankCode;
        #endregion


        #region Constructor
        public CRecipientBankDetails()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_sBankName = string.Empty;
            m_sAccountNumber = string.Empty;
            m_sAccountName = string.Empty;
            m_sAccountType = string.Empty;
            m_sRoutingNumber = string.Empty;
            m_sBankCode = string.Empty;
            m_sBranchName = string.Empty;
            m_sBranchCode = string.Empty;
            m_sBranchDistrict = string.Empty;
            m_sSubBankName = string.Empty;
            m_sSubBankCode = string.Empty;
        }
    
        #endregion Initialization

        #region public Member


        public string BankName
        {
            get { return m_sBankName; }
            set { m_sBankName = value; }
        }

        public string AccountNumber
        {
            get { return m_sAccountNumber; }
            set { m_sAccountNumber = value; }
        }

        public string AccountName
        {
            get { return m_sAccountName; }
            set { m_sAccountName = value; }
        }

        public string AccountType
        {
            get { return m_sAccountType; }
            set { m_sAccountType = value; }
        }

        public string RoutingNumber
        {
            get { return m_sRoutingNumber; }
            set { m_sRoutingNumber = value; }
        }
        public string BankCode
        {
            get { return m_sBankCode; }
            set { m_sBankCode = value; }
        }
        public string BranchName
        {
            get { return m_sBranchName; }
            set { m_sBranchName = value; }
        }
        public string BranchCode
        {
            get { return m_sBranchCode; }
            set { m_sBranchCode = value; }
        }
        public string BranchDistrict
        {
            get { return m_sBranchDistrict; }
            set { m_sBranchDistrict = value; }
        }
        public string SubBankName
        {
            get { return m_sSubBankName; }
            set { m_sSubBankName = value; }
        }
        public string SubBankCode
        {
            get { return m_sSubBankCode; }
            set { m_sSubBankCode = value; }
        }

        #endregion
    }
}
