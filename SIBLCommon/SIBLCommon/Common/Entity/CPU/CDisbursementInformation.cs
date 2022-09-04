using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLCommon.Common.Entity.CPU
{
    [Serializable]
    public class CDisbursementInformation : ASIBLEntityBase
    {
        private string m_sDID;
        //private string m_sTID;
        protected CTransactionInformation m_oTransactionInformation;
        private string m_sPARTNERINVOICE;
        private string m_sPARTNERRECIPIENTRECORDLOCATOR;
        private string m_sRECIPIENTADDRESS1;
        private string m_sRECIPIENTADDRESS2;
        private string m_sRECIPIENTADDRESS3;
        private string m_sRECIPIENTCITY;
        private string m_sRECIPIENTCOUNTRY;
        private string m_sRECIPIENTCOUNTY;
        private string m_sRECIPIENTFIRSTNAME;
        private string m_sRECIPIENTIDTYPE;
        private string m_sRECIPIENTIDVALUE;
        private string m_sRECIPIENTLASTNAME;
        private string m_sRECIPIENTLOCALIZEDLASTNAME;
        private string m_sRECIPIENTMATERNALLASTNAME;
        private string m_sRECIPIENTMIDDLENAME;
        private string m_sRECIPIENTNEIGHBORHOOD;
        private string m_sRECIPIENTPOSTALCODE;
        private string m_sRECIPIENTREGION;
        private string m_sRECIPIENTSTATE;
        private string m_sSUBPARTNER;

            #region Constructor
        public CDisbursementInformation()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
             m_sDID = string.Empty;
             m_oTransactionInformation = new CTransactionInformation();
             m_sPARTNERINVOICE = string.Empty;
             m_sPARTNERRECIPIENTRECORDLOCATOR = string.Empty;
             m_sRECIPIENTADDRESS1 = string.Empty;
             m_sRECIPIENTADDRESS2 = string.Empty;
             m_sRECIPIENTADDRESS3 = string.Empty;
             m_sRECIPIENTCITY = string.Empty;
             m_sRECIPIENTCOUNTRY = string.Empty;
             m_sRECIPIENTCOUNTY = string.Empty;
             m_sRECIPIENTFIRSTNAME = string.Empty;
             m_sRECIPIENTIDTYPE = string.Empty;
             m_sRECIPIENTIDVALUE = string.Empty;
             m_sRECIPIENTLASTNAME = string.Empty;
             m_sRECIPIENTLOCALIZEDLASTNAME = string.Empty;
             m_sRECIPIENTMATERNALLASTNAME = string.Empty;
             m_sRECIPIENTMIDDLENAME = string.Empty;
             m_sRECIPIENTNEIGHBORHOOD = string.Empty;
             m_sRECIPIENTPOSTALCODE = string.Empty;
             m_sRECIPIENTREGION = string.Empty;
             m_sRECIPIENTSTATE = string.Empty;
             m_sSUBPARTNER = string.Empty;
        }
        #endregion Initialization


        public string DID
        {
            get { return m_sDID; }
            set { m_sDID = value; }
        }

        public CTransactionInformation TransactionInformation
        {
            get { return m_oTransactionInformation; }
            set { m_oTransactionInformation = value; }
        }

        public string PARTNERINVOICE
        {
            get { return m_sPARTNERINVOICE; }
            set { m_sPARTNERINVOICE = value; }
        }

        public string PARTNERRECIPIENTRECORDLOCATOR
        {
            get { return m_sPARTNERRECIPIENTRECORDLOCATOR; }
            set { m_sPARTNERRECIPIENTRECORDLOCATOR = value; }
        }
      

        public string RECIPIENTADDRESS1
        {
            get { return m_sRECIPIENTADDRESS1; }
            set { m_sRECIPIENTADDRESS1 = value; }
        }

        public string RECIPIENTADDRESS2
        {
            get { return m_sRECIPIENTADDRESS2; }
            set { m_sRECIPIENTADDRESS2 = value; }
        }

        public string RECIPIENTADDRESS3
        {
            get { return m_sRECIPIENTADDRESS3; }
            set { m_sRECIPIENTADDRESS3 = value; }
        }

        public string RECIPIENTCOUNTRY
        {
            get { return m_sRECIPIENTCOUNTRY; }
            set { m_sRECIPIENTCOUNTRY = value; }
        }
        public string RECIPIENTFIRSTNAME
        {
            get { return m_sRECIPIENTFIRSTNAME; }
            set { m_sRECIPIENTFIRSTNAME = value; }
        }
        public string RECIPIENTIDTYPE
        {
            get { return m_sRECIPIENTIDTYPE; }
            set { m_sRECIPIENTIDTYPE = value; }
        }
        public string RECIPIENTIDVALUE
        {
            get { return m_sRECIPIENTIDVALUE; }
            set { m_sRECIPIENTIDVALUE = value; }
        }

        

        public string RECIPIENTLASTNAME
        {
            get { return m_sRECIPIENTLASTNAME; }
            set { m_sRECIPIENTLASTNAME = value; }
        }

        public string RECIPIENTLOCALIZEDLASTNAME
        {
            get { return m_sRECIPIENTLOCALIZEDLASTNAME; }
            set { m_sRECIPIENTLOCALIZEDLASTNAME = value; }
        }

        public string RECIPIENTMATERNALLASTNAME
        {
            get { return m_sRECIPIENTMATERNALLASTNAME; }
            set { m_sRECIPIENTMATERNALLASTNAME = value; }
        }
        public string RECIPIENTMIDDLENAME
        {
            get { return m_sRECIPIENTMIDDLENAME; }
            set { m_sRECIPIENTMIDDLENAME = value; }
        }
   
        public string RECIPIENTNEIGHBORHOOD
        {
            get { return m_sRECIPIENTNEIGHBORHOOD; }
            set { m_sRECIPIENTNEIGHBORHOOD = value; }
        }
        public string RECIPIENTPOSTALCODE
        {
            get { return m_sRECIPIENTPOSTALCODE; }
            set { m_sRECIPIENTPOSTALCODE = value; }
        }
        public string RECIPIENTREGION
        {
            get { return m_sRECIPIENTREGION; }
            set { m_sRECIPIENTCITY = value; }
        }
        public string RECIPIENTSTATE
        {
            get { return m_sRECIPIENTSTATE; }
            set { m_sRECIPIENTSTATE = value; }
        }

        public string SUBPARTNER
        {
            get { return m_sSUBPARTNER; }
            set { m_sSUBPARTNER = value; }
        }

    }
}
