using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.Common.Entity.User;
using SIBLCommon.Common.Entity.Disbursement;
using SIBLCommon.Common.Entity.CPU;

namespace SIBLXoomCommon.SIBLXoom.Common.Entity.BEFTN
{
    [Serializable]
    public class CBEFTN : ASIBLEntityBase
    {
        #region Protectd Member
        protected string m_sFDate;
        protected string m_sTDate;
        protected string m_sOperationType;
        //protected CTransactionInformation m_oTransactionInformation;
        protected CRecipientBankDetails m_oRecipientBankDetails;
        protected CInstruction m_oInstruction;
        protected CUser m_oUser;       
        #endregion


        #region Constructor
        public CBEFTN()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_sFDate = string.Empty;
            m_sTDate = string.Empty;
            m_sOperationType = string.Empty;
            //m_oTransactionInformation = new CTransactionInformation();
            m_oRecipientBankDetails = new CRecipientBankDetails();
            m_oInstruction = new CInstruction();
            m_oUser = new CUser();
        }
    
        #endregion Initialization

        #region public Member


        public string FDate
        {
            get { return m_sFDate; }
            set { m_sFDate = value; }
        }

        public string TDate
        {
            get { return m_sTDate; }
            set { m_sTDate = value; }
        }

        public string OperationType
        {
            get { return m_sOperationType; }
            set { m_sOperationType = value; }
        }

        //public CTransactionInformation TransactionInformation
        //{
        //    get { return m_oTransactionInformation; }
        //    set { m_oTransactionInformation = value; }
        //}

        public CRecipientBankDetails RecipientBankDetails
        {
            get { return m_oRecipientBankDetails; }
            set { m_oRecipientBankDetails = value; }
        }

        public CInstruction Instruction
        {
            get { return m_oInstruction; }
            set { m_oInstruction = value; } 
        }

        

        public CUser User {
            get { return m_oUser; }
            set { m_oUser = value; }
        }


        #endregion
    }
}
