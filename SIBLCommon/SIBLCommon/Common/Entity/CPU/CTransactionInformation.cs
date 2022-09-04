using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace  SIBLCommon.Common.Entity.CPU
{
    [Serializable]
    public class CTransactionInformation : ASIBLEntityBase
    {
        private string m_sTID;
        private string m_sdisbursementSendAmount;
        private string m_sdisbursementSendCurrency;
         private string m_sdisbursementReceiveAmount;
        private string m_sdisbursementReceiveCurrency;
        private string m_sLASTRECIPIENTIDTYPE;
        private string m_sLASTRECIPIENTIDVALUE;
        private string m_sSECRETWORD;
        private string m_sXOOMTRACKINGNUMBER;
        private string m_sCREATED;
        private string m_sSTATUS;
        private string m_sPAYMENTDATE;
        private string m_sAPPROVED;
        private string m_sAPPROVEDBY;
        private string m_sAPPROVEDDATE;
        private string m_sCREATEBY;
        private string m_sUPDATEBY;
        private string m_sUPDATEDATE;
        private string m_sENTRYDATE;
        private string m_sBRANCHID;
        private string m_sAppRequestId;
        
        #region Constructor
        public CTransactionInformation()
            : base()
        {
            Initialization();
        }
        #endregion Constructor 

        #region Initialization
        protected void Initialization()
        {
            m_sTID = string.Empty;
            m_sdisbursementSendAmount = string.Empty;
            m_sdisbursementSendCurrency = string.Empty;
            m_sdisbursementReceiveAmount = string.Empty;
            m_sdisbursementReceiveCurrency = string.Empty;
            m_sLASTRECIPIENTIDTYPE = string.Empty;
            m_sLASTRECIPIENTIDVALUE = string.Empty;
            m_sSECRETWORD = string.Empty;
            m_sXOOMTRACKINGNUMBER = string.Empty;
            m_sCREATED = string.Empty;
            m_sPAYMENTDATE = string.Empty;
            m_sSTATUS = string.Empty;
            m_sAPPROVED = string.Empty;
            m_sAPPROVEDBY = string.Empty;
            m_sAPPROVEDDATE = string.Empty;
            m_sCREATEBY = string.Empty;
            m_sUPDATEBY = string.Empty;
            m_sUPDATEDATE = string.Empty;
            m_sENTRYDATE = string.Empty;
            m_sBRANCHID = string.Empty;
            m_sAppRequestId = string.Empty;
        }
        #endregion Initialization


        public string TID
        {
            get { return m_sTID; }
            set { m_sTID = value; }
        }

        public string AppRequestId
        {
            get { return m_sAppRequestId; }
            set { m_sAppRequestId = value; }
        }

        public string disbursementSendAmount
        {
            get { return m_sdisbursementSendAmount; }
            set { m_sdisbursementSendAmount = value; }
        }

        public string disbursementSendCurrency
        {
            get { return m_sdisbursementSendCurrency; }
            set { m_sdisbursementSendCurrency = value; }
        }

         public string disbursementReceiveAmount
        {
            get { return m_sdisbursementReceiveAmount; }
            set { m_sdisbursementReceiveAmount = value; }
        }

        public string disbursementReceiveCurrency
        {
            get { return m_sdisbursementReceiveCurrency; }
            set { m_sdisbursementReceiveCurrency = value; }
        }

       
        public string SLASTRECIPIENTIDTYPE
        {
            get { return m_sLASTRECIPIENTIDTYPE; }
            set { m_sLASTRECIPIENTIDTYPE = value; }
        }


        public string SLASTRECIPIENTIDVALUE
        {
            get { return m_sLASTRECIPIENTIDVALUE; }
            set { m_sLASTRECIPIENTIDVALUE = value; }
        }

        public string SSECRETWORD
        {
            get { return m_sSECRETWORD; }
            set { m_sSECRETWORD = value; }
        }

        public string SXOOMTRACKINGNUMBER
        {
            get { return m_sXOOMTRACKINGNUMBER; }
            set { m_sXOOMTRACKINGNUMBER = value; }
        }

        public string CREATED
        {
            get { return m_sCREATED; }
            set { m_sCREATED = value; }
        }

        public string PAYMENTDATE
        {
            get { return m_sPAYMENTDATE; }
            set { m_sPAYMENTDATE = value; }
        }


        public string STATUS
        {
            get { return m_sSTATUS; }
            set { m_sSTATUS = value; }
        }

        public string APPROVED
        {
            get { return m_sAPPROVED; }
            set { m_sAPPROVED = value; }
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

        public string UPDATEBY
        {
            get { return m_sUPDATEBY; }
            set { m_sUPDATEBY = value; }
        }


        public string UPDATEDATE
        {
            get { return m_sUPDATEDATE; }
            set { m_sUPDATEDATE = value; }
        }

        public string ENTRYDATE
        {
            get { return m_sENTRYDATE; }
            set { m_sENTRYDATE = value; }
        }


        public string BRANCHID
        {
            get { return m_sBRANCHID; }
            set { m_sBRANCHID = value; }
        }

    }
}
