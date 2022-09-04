using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLCommon.Common.Entity.CPU
{
    [Serializable]
    public class CSenderInformation : ASIBLEntityBase
    {
        protected string m_sSENDERID;
        //protected string m_sTID;
        protected CTransactionInformation m_oTransactionInformation;
        protected string m_sADDRESS1;
        protected string m_sADDRESS2;
        protected string m_sADDRESS3;
        protected string m_sCITY;
        protected string m_sCOUNTRY;
        protected string m_sCOUNTY;
        protected string m_sEMAIL;
        protected string m_sFIRSTNAME;
        protected string m_sLASTNAME;
        protected string m_sMATERNALLASTNAME;
        protected string m_sMIDDLENAME;
        protected string m_sNEIGHBORHOOD;
        protected string m_sPHONE1;
        protected string m_sPHONE2;
        protected string m_sPHONE3;
        protected string m_sPOSTALCODE;
        protected string m_sREGION;
        protected string m_sSTATE;


         #region Constructor
        public CSenderInformation()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
             m_sSENDERID = string.Empty;
             m_oTransactionInformation = new CTransactionInformation();
             m_sADDRESS1 = string.Empty;
             m_sADDRESS2 = string.Empty;
             m_sADDRESS3 = string.Empty;
             m_sCITY = string.Empty;
             m_sCOUNTRY = string.Empty;
             m_sCOUNTY = string.Empty;
             m_sEMAIL = string.Empty;
             m_sFIRSTNAME = string.Empty;
             m_sLASTNAME = string.Empty;
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

        public string SENDERID
        {
            get { return m_sSENDERID; }
            set { m_sSENDERID = value; }
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
        public string FIRSTNAME
        {
            get { return m_sFIRSTNAME; }
            set { m_sFIRSTNAME = value; }
        }

         public string LASTNAME
        {
            get { return m_sLASTNAME; }
            set { m_sLASTNAME = value; }
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



    }
}
