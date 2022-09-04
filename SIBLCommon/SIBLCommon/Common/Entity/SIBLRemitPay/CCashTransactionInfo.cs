using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.Common.Entity.User;
using SIBLCommon.Common.Entity.Bank;
using SIBLCommon.Common.Entity.File;
using SIBLCommon.Common.Entity.AllLookup;

namespace SIBLCommon.SIBLCommon.Common.Entity.CashPayment
{
    [Serializable]
    public class CCashTransactionInfo : ASIBLEntityBase
    {

        #region Protectd Member

        protected string m_sPINNO;
        protected string m_sSENDERNAME;
        protected string m_sSENDERADDRESS;
        protected string m_sSENDERPHONE;
        protected string m_sRECEIVERNAME;
        protected string m_sRECEIVERADDRESS;
        protected string m_sRECEIVERPHONE;
        protected string m_sAMOUNT;
        protected string m_sAPPROVEDBY;
        protected string m_sAPPROVEDDATE;
        protected string m_sCREATEBY;
        protected string m_sENTRYDATE;
        protected string m_sSTATUS;
        protected CAllLookup m_oCOMPANYID;
        protected string m_sFILEID;
        protected CUser m_oUSER;
        protected CBranch m_oBranch;
        protected string m_sFromDate;
        protected string m_sToDate;
        protected string m_sOperationType;
        protected CFile m_oFileName;
        protected string m_sComments;

        #endregion


        #region Constructor
        public CCashTransactionInfo()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_sPINNO = String.Empty;
            m_sSENDERNAME = String.Empty;
            m_sSENDERADDRESS = String.Empty;
            m_sSENDERPHONE = String.Empty;
            m_sRECEIVERNAME = String.Empty;
            m_sRECEIVERADDRESS = String.Empty;
            m_sRECEIVERPHONE = String.Empty;
            m_sAMOUNT = String.Empty;
            m_sAPPROVEDBY = String.Empty;
            m_sAPPROVEDDATE = String.Empty;
            m_sCREATEBY = String.Empty;
            m_sENTRYDATE = String.Empty;
            m_sSTATUS = String.Empty;
            m_oCOMPANYID = new CAllLookup();
            m_sFILEID = String.Empty;
            m_oUSER = new CUser();
            m_oBranch = new CBranch();
            m_sOperationType = String.Empty;
            m_sFromDate = string.Empty;
            m_sToDate = string.Empty;
            m_oFileName = new CFile();
        }
        #endregion Initialization

        #region public Member

        public string OperationType
        {
            get { return m_sOperationType; }
            set { m_sOperationType = value; }
        }

        public CFile FileInfo
        {
            get { return m_oFileName; }
            set { m_oFileName = value; }
        }
        public CUser USERID
        {
            get { return m_oUSER; }
            set { m_oUSER = value; }
        }

        public CBranch Branch
        {
            get { return m_oBranch; }
            set { m_oBranch = value; }
        }


        public CAllLookup CompanyInfo
        {
            get { return m_oCOMPANYID; }
            set { m_oCOMPANYID = value; }
        }

        public string FILEID
        {
            get { return m_sFILEID; }
            set { m_sFILEID = value; }
        }
        public string PINNO
        {
            get { return m_sPINNO; }
            set { m_sPINNO = value; }
        }        

        public string SENDERNAME
        {
            get { return m_sSENDERNAME; }
            set { m_sSENDERNAME = value; }
        }

        public string SENDERADDRESS
        {
            get { return m_sSENDERADDRESS; }
            set { m_sSENDERADDRESS = value; }
        }

        public string SENDERPHONE
        {
            get { return m_sSENDERPHONE; }
            set { m_sSENDERPHONE = value; }
        }

        public string RECEIVERNAME
        {
            get { return m_sRECEIVERNAME; }
            set { m_sRECEIVERNAME = value; }
        }

        public string RECEIVERADDRESS
        {
            get { return m_sRECEIVERADDRESS; }
            set { m_sRECEIVERADDRESS = value; }
        }

        public string RECEIVERPHONE
        {
            get { return m_sRECEIVERPHONE; }
            set { m_sRECEIVERPHONE = value; }
        }
        public string AMOUNT
        {
            get { return m_sAMOUNT; }
            set { m_sAMOUNT = value; }
        }
        public string APPROVEDBY
        {
            get { return m_sAPPROVEDBY; }
            set { m_sAPPROVEDBY = value; }
        }
        public string APPROVEDDATE
        {
            get { return m_sAPPROVEDDATE; }
            set { m_sAPPROVEDDATE = value; }
        }
        public string CREATEBY
        {
            get { return m_sCREATEBY; }
            set { m_sCREATEBY = value; }
        }
        public string ENTRYDATE
        {
            get { return m_sENTRYDATE; }
            set { m_sENTRYDATE = value; }
        }
        public string STATUS
        {
            get { return m_sSTATUS; }
            set { m_sSTATUS = value; }
        }

        #endregion
    }
}
