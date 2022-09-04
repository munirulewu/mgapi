using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLCommon.Common.Entity.CPU
{
    [Serializable]
    public class CSupplementalInformation : ASIBLEntityBase
    {
        private string m_sSUPLIMENTALID;
        //private string m_sTID;
        protected CTransactionInformation m_oTransactionInformation;
        private string m_sSENDERMESSAGETORECIPIENT;
        private string m_sTRANSACTIONAVAILABLEETA;
        private string m_sXOOMCONTACTEMAIL;
        private string m_sXOOMCONTACTPHONE;
        private string m_sXOOMMESSAGETORECIPIENT;
        private string m_sXOOMMESSAGETOTELLER;

        
        #region Constructor
        public CSupplementalInformation()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        //#region Initialization
        //protected void Initialization()
        //{
        //     m_sSUPLIMENTALID = string.Empty;
        //     //m_sTID = string.Empty;
        //     m_oTransactionInformation = new CTransactionInformation();
        //     m_sSENDERMESSAGETORECIPIENT = string.Empty;
        //     m_sTRANSACTIONAVAILABLEETA = string.Empty;
        //     m_sXOOMCONTACTEMAIL = string.Empty;
        //     m_sXOOMCONTACTPHONE = string.Empty;
        //     m_sXOOMMESSAGETORECIPIENT = string.Empty;
        //     m_sXOOMMESSAGETOTELLER = string.Empty;
        //}
        //#endregion Initialization

        public string SUPLIMENTALID
        {
            get { return m_sSUPLIMENTALID; }
            set { m_sSUPLIMENTALID = value; }
        }
        public CTransactionInformation TransactionInformation
        {
            get { return m_oTransactionInformation; }
            set { m_oTransactionInformation = value; }
        }
        public string SENDERMESSAGETORECIPIENT
        {
            get { return m_sSENDERMESSAGETORECIPIENT; }
            set { m_sSENDERMESSAGETORECIPIENT = value; }
        }
        public string TRANSACTIONAVAILABLEETA
        {
            get { return m_sTRANSACTIONAVAILABLEETA; }
            set { m_sTRANSACTIONAVAILABLEETA = value; }
        }
        public string XOOMCONTACTEMAIL
        {
            get { return m_sXOOMCONTACTEMAIL; }
            set { m_sXOOMCONTACTEMAIL = value; }
        }
        public string XOOMCONTACTPHONE
        {
            get { return m_sXOOMCONTACTPHONE; }
            set { m_sXOOMCONTACTPHONE = value; }
        }
        public string XOOMMESSAGETORECIPIENT
        {
            get { return m_sXOOMMESSAGETORECIPIENT; }
            set { m_sXOOMMESSAGETORECIPIENT = value; }
        }
        public string XOOMMESSAGETOTELLER
        {
            get { return m_sXOOMMESSAGETOTELLER; }
            set { m_sXOOMMESSAGETOTELLER = value; }
        }
    }
}
