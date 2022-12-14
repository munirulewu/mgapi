/*
 * File name            : CCBSTransactionRequest
 * Author               : Md. Aminul Islam
 * Date                 : 06-07-2015
 * Version              : 1.0
 *
 * Description          : 
 *
 * Modification history :
 * Name                         Date                            Desc
 * 
 * 
 * Copyright (c) 2015: SOCIAL ISLAMI BANK LIMITED
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLXoomCommon.SIBLXoom.Common.Entity.Disbursement
{
    [Serializable]
    public class CCBSTransactionRequest : ASIBLEntityBase
    {
        #region Protectd Member
        protected string m_sAmount;
        protected string m_sCharge;
        protected string m_sEntryUser;
        protected string m_sFromAccount;
        protected string m_sFromAccountType;
        protected string m_sNarration;
        protected string m_sReference;
        protected string m_sRequestId;
        protected string m_sTerminal;
        protected string m_sToAccount;
        protected string m_sToAccountType;
        protected string m_sTransactionDate;
        #endregion

        #region Constructor
        public CCBSTransactionRequest()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_sAmount = string.Empty;
            m_sCharge = string.Empty;
            m_sEntryUser = string.Empty;
            m_sFromAccount = string.Empty;
            m_sFromAccountType = string.Empty;
            m_sNarration = string.Empty;
            m_sReference = string.Empty;
            m_sRequestId = string.Empty;
            m_sTerminal = string.Empty;
            m_sToAccount = string.Empty;
            m_sToAccountType = string.Empty;
            m_sTransactionDate = string.Empty;
        }
    
        #endregion Initialization

        #region public Member
        public string Amount
        {
            get { return m_sAmount; }
            set { m_sAmount = value; }
        }
        public string Charge
        {
            get { return m_sCharge; }
            set { m_sCharge = value; }
        }
        public string EntryUser
        {
            get { return m_sEntryUser; }
            set { m_sEntryUser = value; }
        }
        public string FromAccount
        {
            get { return m_sFromAccount; }
            set { m_sFromAccount = value; }
        }

        public string FromAccountType
        {
            get { return m_sFromAccountType; }
            set { m_sFromAccountType = value; }
        }
        public string Narration
        {
            get { return m_sNarration; }
            set { m_sNarration = value; }
        }
        public string Reference
        {
            get { return m_sReference; }
            set { m_sReference = value; }
        }
        public string RequestId
        {
            get { return m_sRequestId; }
            set { m_sRequestId = value; }
        }
        public string Terminal
        {
            get { return m_sTerminal; }
            set { m_sTerminal = value; }
        }
        public string ToAccount
        {
            get { return m_sToAccount; }
            set { m_sToAccount = value; }
        }
        public string ToAccountType
        {
            get { return m_sToAccountType; }
            set { m_sToAccountType = value; }
        }
        public string TransactionDate
        {
            get { return m_sTransactionDate; }
            set { m_sTransactionDate = value; }
        }
       
        #endregion
    }
}
