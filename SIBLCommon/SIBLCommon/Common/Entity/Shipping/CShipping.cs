using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.Common.Entity.User;
using SIBLCommon.Common.Entity.Bank;

namespace SIBLCommon.SIBLCommon.Common.Entity.Shipping
{
    [Serializable]
    public class CShipping : ASIBLEntityBase
    {
        #region Protectd Member

        protected string m_sAPPLICATIONID;
        protected string m_sTRACKINGNUMBER;
        protected string m_sAPPLICANTNAME;
        protected string m_sENTRYDATE;
        protected string m_sCREATEBY;
        protected string m_sAmount;
        protected string m_sRECEIVEAMOUNT;
        protected string m_sCOMMISSION;
        protected string m_sPHONENO;
        protected string m_sIDTYPE;
        
        protected string m_sIDNO;
        protected string m_sSTATUS;
        protected string m_sFromDate;
        protected string m_sToDate;

        protected CUser m_sUser;
        protected CBranch m_oBranch;
        #endregion


        #region Constructor
        public CShipping()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_sAPPLICATIONID = string.Empty;
            m_sTRACKINGNUMBER = string.Empty;
            m_sAPPLICANTNAME = string.Empty;
            m_sENTRYDATE = string.Empty;
            m_sCREATEBY = string.Empty;
            m_sAmount = string.Empty;
            m_sRECEIVEAMOUNT = string.Empty;
            m_sCOMMISSION = string.Empty;
            m_sPHONENO = string.Empty;
            m_sIDTYPE = string.Empty;
            //m_sOperationType = string.Empty;
            m_sIDNO = string.Empty;
            m_sSTATUS = string.Empty;
            m_sFromDate = string.Empty;
            m_sToDate = string.Empty;

            m_sUser = new CUser();
            m_oBranch = new CBranch();


        }
        #endregion Initialization

        #region public Member


        
        public string applicationId
        {
            get { return m_sAPPLICATIONID; }
            set { m_sAPPLICATIONID = value; }
        }



        public string TRACKINGNUMBER
        {
            get { return m_sTRACKINGNUMBER; }
            set { m_sTRACKINGNUMBER = value; }
        }

        public string APPLICANTNAME
        {
            get { return m_sAPPLICANTNAME; }
            set { m_sAPPLICANTNAME = value; }
        }


        public string ENTRYDATE
        {
            get { return m_sENTRYDATE; }
            set { m_sENTRYDATE = value; }
        }

        public string CREATEBY
        {
            get { return m_sCREATEBY; }
            set { m_sCREATEBY = value; }
        }

        public string RECEIVEAMOUNT
        {
            get { return m_sRECEIVEAMOUNT; }
            set { m_sRECEIVEAMOUNT = value; }
        }

        public string Amount
        {
            get { return m_sAmount; }
            set { m_sAmount = value; }
        }

        public string COMMISSION
        {
            get { return m_sCOMMISSION; }
            set { m_sCOMMISSION = value; }
        }


        public string PHONENO
        {
            get { return m_sPHONENO; }
            set { m_sPHONENO = value; }
        }
        public string IDTYPE
        {
            get { return m_sIDTYPE; }
            set { m_sIDTYPE = value; }
        }

       

        public string IDNO
        {
            get { return m_sIDNO; }
            set { m_sIDNO = value; }
        }

        public string STATUS
        {
            get { return m_sSTATUS; }
            set { m_sSTATUS = value; }
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


        public CUser User
        {
            get { return m_sUser; }
            set { m_sUser = value; }
        }


        public CBranch BranchInfo
        {
            get { return m_oBranch; }
            set { m_oBranch = value; }
        }


        #endregion
    }
}
