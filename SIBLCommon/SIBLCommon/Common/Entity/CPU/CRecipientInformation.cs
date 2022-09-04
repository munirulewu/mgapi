using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLCommon.Common.Entity.CPU
{
    [Serializable]
    public class CRecipientInformation : ASIBLEntityBase
    {
        #region Protectd Member
        protected string m_sRID;        
        protected CTransactionInformation m_oTransactionInformation;
        protected string m_sADDRESS1;
        protected string m_sADDRESS2;
        protected string m_sADDRESS3;
        protected string m_sCITY;
        protected string m_sCOUNTRY;
        protected string m_sCOUNTY;

        protected string m_sEMAIL;
        protected string m_sFIRSTAME;
        protected string m_sIDTYPE;
        protected string m_sIDVALUE;
        protected string m_sLASTNAME;
        protected string m_sLOCALIZEDLASTNAME;
        protected string m_sMATERNALLASTNAME;
        protected string m_sMIDDLENAME;
        protected string m_sNEIGHBORHOOD;
        protected string m_sPHONE1;
        protected string m_sPHONE2;
        protected string m_sPHONE3;
        protected string m_sPOSTALCODE;
        protected string m_sREGION;
        protected string m_sSTATE;
        #endregion

        
        #region Constructor
        public CRecipientInformation()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_sRID = string.Empty;
            m_oTransactionInformation = new CTransactionInformation();
            m_sADDRESS1 = string.Empty;
            m_sADDRESS2 = string.Empty;
            m_sADDRESS3 = string.Empty;
            m_sCITY = string.Empty;
            m_sCOUNTRY = string.Empty;
            m_sCOUNTY = string.Empty;
            m_sEMAIL = string.Empty;
            m_sFIRSTAME = string.Empty;
            m_sIDTYPE = string.Empty;
            m_sIDVALUE = string.Empty;
            m_sLASTNAME = string.Empty;
            m_sLOCALIZEDLASTNAME = string.Empty;
            m_sMATERNALLASTNAME = string.Empty;
            m_sMIDDLENAME = string.Empty;
            m_sNEIGHBORHOOD = string.Empty;
            m_sPHONE1 = string.Empty;
            m_sPHONE2 = string.Empty;
            m_sPHONE3 = string.Empty;
            m_sPOSTALCODE = string.Empty;
            m_sREGION = string.Empty;
            m_sSTATE = string.Empty;
        }
        #endregion Initialization

        #region public Member

        
        public string RID
        {
            get { return m_sRID; }
            set { m_sRID = value; }
        }
        public CTransactionInformation TransactionInformation
        {
            get { return m_oTransactionInformation; }
            set { m_oTransactionInformation = value; }
        }
        public string ADDRESS1
        {
            get { return m_sADDRESS1; }
            set { m_sADDRESS1 = value; }
        }
        public string ADDRESS2
        {
            get { return m_sADDRESS2; }
            set { m_sADDRESS2 = value; }
        }
        public string ADDRESS3
        {
            get { return m_sADDRESS3; }
            set { m_sADDRESS3 = value; }
        }
        public string CITY
        {
            get { return m_sCITY; }
            set { m_sCITY = value; }
        }

        public string COUNTRY
        {
            get { return m_sCOUNTRY; }
            set { m_sCOUNTRY = value; }
        }
        public string COUNTY
        {
            get { return m_sCOUNTY; }
            set { m_sCOUNTY = value; }
        }       
           
        public string EMAIL
        {
            get { return m_sEMAIL; }
            set { m_sEMAIL = value; }
        }
        public string FIRSTAME
        {
            get { return m_sFIRSTAME; }
            set { m_sFIRSTAME = value; }
        }
        public string IDTYPE
        {
            get { return m_sIDTYPE; }
            set { m_sIDTYPE = value; }
        }

        public string IDVALUE
        {
            get { return m_sIDVALUE; }
            set { m_sIDVALUE = value; }
        }
        public string LASTNAME
        {
            get { return m_sLASTNAME; }
            set { m_sLASTNAME = value; }
        }
         public string LOCALIZEDLASTNAME
         {
            get { return m_sLOCALIZEDLASTNAME; }
            set { m_sLOCALIZEDLASTNAME = value; }
        }          
            
        public string MATERNALLASTNAME
        {
            get { return m_sMATERNALLASTNAME; }
            set { m_sMATERNALLASTNAME = value; }
        }
        public string MIDDLENAME
        {
            get { return m_sMIDDLENAME; }
            set { m_sMIDDLENAME = value; }
        }
        public string NEIGHBORHOOD
        {
            get { return m_sNEIGHBORHOOD; }
            set { m_sNEIGHBORHOOD = value; }
        }
        public string PHONE1
        {
            get { return m_sPHONE1; }
            set { m_sPHONE1 = value; }
        }
        public string PHONE2
        {
            get { return m_sPHONE2; }
            set { m_sPHONE2 = value; }
        }
        public string PHONE3
        {
            get { return m_sPHONE3; }
            set { m_sPHONE3 = value; }
        }
        public string POSTALCODE
        {
            get { return m_sPOSTALCODE; }
            set { m_sPOSTALCODE = value; }
        }
        public string REGION
        {
            get { return m_sREGION; }
            set { m_sREGION = value; }
        }
        public string STATE
        {
            get { return m_sSTATE; }
            set { m_sSTATE = value; }
        }
        #endregion
    }
}
