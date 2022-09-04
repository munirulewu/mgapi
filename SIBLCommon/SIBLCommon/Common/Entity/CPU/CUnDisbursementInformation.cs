using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLCommon.Common.Entity.CPU
{
    [Serializable]
    public class CUnDisbursementInformation : ASIBLEntityBase
    {
        private string m_sUDID;
        private string m_sRCITY;
        private string m_sRCOUNTRY;
        private string m_sRFIRST_NAME;
        private string m_sRLAST_NAME;
        private string m_sRPHONE1;
        private string m_sRSTATE;
        private string m_sSADDRESS1;
        private string m_sSCITY;
        private string m_sSCOUNTRY;
        private string m_sSEMAIL;
        private string m_sSFIRST_NAME;
        private string m_sSLAST_NAME;
        private string m_sSPHONE1;
        private string m_sSPOSTALCODE;
        private string m_sSSTATE;
        private string m_sTRANS_CREATED;
        private string m_sENTRYDATE;
        private string m_sUSERID;
        private string m_sCOMMENTS;
        private string m_sUNDISBURSETRANSRESCODE;

        #region Constructor
        public CUnDisbursementInformation()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_sUDID = string.Empty;
            m_sRCITY = string.Empty;
            m_sRCOUNTRY = string.Empty;
            m_sRFIRST_NAME = string.Empty;
            m_sRLAST_NAME = string.Empty;
            m_sRPHONE1 = string.Empty;
            m_sRSTATE = string.Empty;
            m_sSADDRESS1 = string.Empty;
            m_sSCITY = string.Empty;
            m_sSCOUNTRY = string.Empty;
            m_sSEMAIL = string.Empty;
            m_sSFIRST_NAME = string.Empty; 
            m_sSLAST_NAME = string.Empty; 
            m_sSPHONE1 = string.Empty; 
            m_sSPOSTALCODE = string.Empty;
            m_sSSTATE = string.Empty;
            m_sTRANS_CREATED = string.Empty;
            m_sENTRYDATE = string.Empty;
            m_sCOMMENTS = string.Empty;
            m_sUNDISBURSETRANSRESCODE = string.Empty;
        }
        #endregion Initialization

        public string UDID
        {
            get { return m_sUDID; }
            set { m_sUDID = value; }
        }

        public string RCITY
        {
            get { return m_sRCITY; }
            set { m_sRCITY = value; }
        }

        public string RCOUNTRY
        {
            get { return m_sRCOUNTRY; }
            set { m_sRCOUNTRY = value; }
        }
                

        public string RFIRST_NAME
        {
            get { return m_sRFIRST_NAME; }
            set { m_sRFIRST_NAME = value; }
        }


        public string RLAST_NAME
        {
            get { return m_sRLAST_NAME; }
            set { m_sRLAST_NAME = value; }
        }

        public string RPHONE1
        {
            get { return m_sRPHONE1; }
            set { m_sRPHONE1 = value; }
        }

        public string RSTATE
        {
            get { return m_sRSTATE; }
            set { m_sRSTATE = value; }
        }

           public string SADDRESS1
        {
            get { return m_sSADDRESS1; }
            set { m_sSADDRESS1 = value; }
        }

          
           
        public string SCITY
        {
            get { return m_sSCITY; }
            set { m_sSCITY = value; }
        }

        public string SCOUNTRY
        {
            get { return m_sSCOUNTRY; }
            set { m_sSCOUNTRY = value; }
        }

        public string SEMAIL
        {
            get { return m_sSEMAIL; }
            set { m_sSEMAIL = value; }
        }


        public string SFIRST_NAME
        {
            get { return m_sSFIRST_NAME; }
            set { m_sSFIRST_NAME = value; }
        }

        public string SLAST_NAME
        {
            get { return m_sSLAST_NAME; }
            set { m_sSLAST_NAME = value; }
        }

        public string SPHONE1
        {
            get { return m_sSPHONE1; }
            set { m_sSPHONE1 = value; }
        }


        public string SPOSTALCODE
        {
            get { return m_sSPOSTALCODE; }
            set { m_sSPOSTALCODE = value; }
        }

        public string SSTATE
        {
            get { return m_sSSTATE; }
            set { m_sSSTATE = value; }
        }


        public string TRANS_CREATED
        {
            get { return m_sTRANS_CREATED; }
            set { m_sTRANS_CREATED = value; }
        }

         public string ENTRYDATE
        {
            get { return m_sENTRYDATE; }
            set { m_sENTRYDATE = value; }
        }
         public string COMMENTS
        {
            get { return m_sCOMMENTS; }
            set { m_sCOMMENTS = value; }
        }
         public string UNDISBURSETRANSRESCODE
        {
            get { return m_sUNDISBURSETRANSRESCODE; }
            set { m_sUNDISBURSETRANSRESCODE = value; }
        }


    }
}
