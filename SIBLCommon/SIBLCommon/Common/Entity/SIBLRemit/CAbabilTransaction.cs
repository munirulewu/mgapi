using SIBLCommon.Common.Entity.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIBLCommon.SIBLCommon.Common.Entity.SIBLRemit
{

    [Serializable]
    public class CAbabilTransaction : ASIBLEntityBase
    {
        private string m_sFromAccount;
        private string m_sToAccount;
        private string m_sFromAccountType;
        private string m_sToAccountType;
        private string m_sAmount;
        private string m_sCharges;
        private string m_sNarration;


        #region Constructor
        public CAbabilTransaction()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {

            m_sFromAccount = string.Empty;
            m_sToAccount = string.Empty;
            m_sFromAccountType = string.Empty;
            m_sToAccountType = string.Empty;
            m_sAmount = string.Empty;
            m_sCharges = string.Empty;
            m_sNarration = string.Empty;


        }
        #endregion Initialization


        public string FromAccount
        {
            get { return m_sFromAccount; }
            set { m_sFromAccount = value; }
        }
        public string ToAccount
        {
            get { return m_sToAccount; }
            set { m_sToAccount = value; }
        }
        public string FromAccountType
        {
            get { return m_sFromAccountType; }
            set { m_sFromAccountType = value; }
        }

        public string ToAccountType
        {
            get { return m_sToAccountType; }
            set { m_sToAccountType = value; }
        }

        public string Amount
        {
            get { return m_sAmount; }
            set { m_sAmount = value; }
        }

        public string Charges
        {
            get { return m_sCharges; }
            set { m_sCharges = value; }
        }

        public string Narration
        {
            get { return m_sNarration; }
            set { m_sNarration = value; }
        }
    }
}
