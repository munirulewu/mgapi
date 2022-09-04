using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.Common.Entity.User;
using SIBLCommon.Common.Entity.Bank;
using SIBLXoomCommon.SIBLXoom.Common.Entity.CPU;

namespace SIBLCommon.SIBLCommon.Common.Entity.SGCL
{
    [Serializable]
    public class CSGCL : ASIBLEntityBase
    {
        #region Protectd Member

        protected string m_sCUSTOMERCODE;
        protected string m_sMOBILENO;
        protected string m_sCUSTOMERNAME;
        protected string m_sCATEGORY;
        protected string m_sZONE;
        protected string m_sBILLAMOUNT;
        protected string m_sSURCHARGE;
        protected string m_sMETERCHARGE;
        protected string m_sTOTALAMOUNT;
        protected string m_sAITAMOUNT;
        protected string m_sBILLMONTH;
        protected string m_sBILLYEAR;
        protected string m_sLASTPAYMENTDATE;
        protected string m_sGASBILL;
        protected string m_sISGOVT;
        protected string m_sPOCBRANCHNAME;
        protected string m_sPOCBANKNAME;
        protected string m_sPOCDATE;
        protected string m_sPOCNO;
        protected string m_sPOAUTHORIZEDDATE;
        protected string m_sBPONO;
        protected string m_sBPODATE;
        protected string m_sBPOBANK;
        protected string m_sBPOBRANCH;
        protected string m_sINSTALLMENTNO;
        protected string m_sINSTALLMENTAMOUNT;
        protected string m_sTXID;
        protected string m_sRemoteMessage;
        protected string m_sStatusCode;
        protected string m_sENTRYDATE;
        protected string m_sCREATEBY;
        protected string m_sBRANCHID;        
        protected string m_sAPITYPE;
        protected string m_sTransactionType;
        protected string m_sBillType;

        protected string m_sBILLSTATUS;
        protected string m_sDNCODE;
        protected string m_sDNDETAILS;
        protected string m_sDNBATYPE;
        protected string m_sTRANSACTIONNO;
        protected string m_sSTAMPAMOUNT;
        protected string m_sCOMMENTS;

        protected string m_sFromDate;
        protected string m_sToDate;
        protected string m_sOperationType;
        protected CUser oUser;
        protected CBranch oBranch;
        protected CBank oBank;
        protected CAbabilTransactionResponse oAbabilTransactionResponse;
        protected string m_sBankAccountType;
        protected int m_hasValue;
        protected string m_sBPODetails;
        protected string m_sTransactionDate;
        protected string m_sBranchRoutingNo;
        #endregion

        #region Constructor
        public CSGCL()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_sCUSTOMERCODE = string.Empty;
            m_sMOBILENO = string.Empty;
            m_sCUSTOMERNAME = string.Empty;
            m_sCATEGORY = string.Empty;
            m_sZONE = string.Empty;
            m_sBILLAMOUNT = string.Empty;
            m_sSURCHARGE = string.Empty;
            m_sMETERCHARGE = string.Empty;
            m_sTOTALAMOUNT = string.Empty;
            m_sAITAMOUNT = string.Empty;
            m_sBILLMONTH = string.Empty;
            m_sBILLYEAR = string.Empty;
            m_sGASBILL = string.Empty;
            m_sLASTPAYMENTDATE = string.Empty;
            m_sISGOVT = string.Empty;
            m_sPOCBRANCHNAME = string.Empty;
            m_sPOCBANKNAME = string.Empty;
            m_sPOCDATE = string.Empty;
            m_sPOCNO = string.Empty;
            m_sPOAUTHORIZEDDATE = string.Empty;
            m_sBPONO = string.Empty;
            m_sBPODATE = string.Empty;
            m_sBPOBANK = string.Empty;
            m_sBPOBRANCH = string.Empty;
            m_sINSTALLMENTNO = string.Empty;
            m_sINSTALLMENTAMOUNT = string.Empty;
            m_sTXID = string.Empty;
            m_sRemoteMessage = string.Empty;
            m_sStatusCode = string.Empty;
            m_sENTRYDATE = string.Empty;
            m_sCREATEBY = string.Empty;
            m_sBRANCHID = string.Empty;
            m_sAPITYPE = string.Empty;
            m_sTransactionType = string.Empty;
            m_sBillType = string.Empty;
            m_sBILLSTATUS = string.Empty;
            m_sDNCODE = string.Empty;
            m_sDNDETAILS = string.Empty;
            m_sDNBATYPE = string.Empty;
            m_sTRANSACTIONNO = string.Empty;
            m_sSTAMPAMOUNT = string.Empty;
            m_sCOMMENTS = string.Empty;

            m_sFromDate = string.Empty;
            m_sToDate = string.Empty;
            m_sOperationType = string.Empty;
            oUser = new  CUser();
            oBranch = new CBranch();
            oBank = new CBank();
            oAbabilTransactionResponse = new CAbabilTransactionResponse();
            m_sBankAccountType = string.Empty;
            m_hasValue = 0;
            m_sBPODetails = string.Empty;
            m_sTransactionDate = string.Empty;
            m_sBranchRoutingNo = string.Empty;

        }
        #endregion Initialization

        #region public Member

        public string BranchRoutingNo
        {
            get { return m_sBranchRoutingNo; }
            set { m_sBranchRoutingNo = value; }
        }

        public string TransactionDate
        {
            get { return m_sTransactionDate; }
            set { m_sTransactionDate = value; }
        }
        public string BPODetails
        {
            get { return m_sBPODetails; }
            set { m_sBPODetails = value; }
        }

        public string BankAccountType
        {
            get { return m_sBankAccountType; }
            set { m_sBankAccountType = value; }
        }
        public int HasValue
        {
            get { return m_hasValue; }
            set { m_hasValue = value; }
        }


        public string CUSTOMERCODE
        {
            get { return m_sCUSTOMERCODE; }
            set { m_sCUSTOMERCODE = value; }
        }

         public string CATEGORY
        {
            get { return m_sCATEGORY; }
            set { m_sCATEGORY = value; }
        }


         public string ZONE
        {
            get { return m_sZONE; }
            set { m_sZONE = value; }
        }

        public string MOBILENO
        {
            get { return m_sMOBILENO; }
            set { m_sMOBILENO = value; }
        }

        public string CUSTOMERNAME
        {
            get { return m_sCUSTOMERNAME; }
            set { m_sCUSTOMERNAME = value; }
        }

        public string BILLAMOUNT
        {
            get { return m_sBILLAMOUNT; }
            set { m_sBILLAMOUNT = value; }
        }

        public string SURCHARGE
        {
            get { return m_sSURCHARGE; }
            set { m_sSURCHARGE = value; }
        }

        public string METERCHARGE
        {
            get { return m_sMETERCHARGE; }
            set { m_sMETERCHARGE = value; }
        }

        public string TOTALAMOUNT
        {
            get { return m_sTOTALAMOUNT; }
            set { m_sTOTALAMOUNT = value; }
        }

        public string AITAMOUNT
        {
            get { return m_sAITAMOUNT; }
            set { m_sAITAMOUNT = value; }
        }

        public string BILLMONTH
        {
            get { return m_sBILLMONTH; }
            set { m_sBILLMONTH = value; }
        }

        public string BILLYEAR
        {
            get { return m_sBILLYEAR; }
            set { m_sBILLYEAR = value; }
        }

        public string GASBILL
        {
            get { return m_sGASBILL; }
            set { m_sGASBILL = value; }
        }
               

        public string LASTPAYMENTDATE
        {
            get { return m_sLASTPAYMENTDATE; }
            set { m_sLASTPAYMENTDATE = value; }
        }

        public string ISGOVT
        {
            get { return m_sISGOVT; }
            set { m_sISGOVT = value; }
        }


        public string POCBRANCHNAME
        {
            get { return m_sPOCBRANCHNAME; }
            set { m_sPOCBRANCHNAME = value; }
        }

        public string POCBANKNAME
        {
            get { return m_sPOCBANKNAME; }
            set { m_sPOCBANKNAME = value; }
        }

        public string POCDATE
        {
            get { return m_sPOCDATE; }
            set { m_sPOCDATE = value; }
        }

        public string POCNO
        {
            get { return m_sPOCNO; }
            set { m_sPOCNO = value; }
        }

        public string POAUTHORIZEDDATE
        {
            get { return m_sPOAUTHORIZEDDATE; }
            set { m_sPOAUTHORIZEDDATE = value; }
        }

         public string BPONO
        {
            get { return m_sBPONO; }
            set { m_sBPONO = value; }
        }

        public string BPODATE
        {
            get { return m_sBPODATE; }
            set { m_sBPODATE = value; }
        }

         public string BPOBANK
        {
            get { return m_sBPOBANK; }
            set { m_sBPOBANK = value; }
        }

          public string BPOBRANCH
        {
            get { return m_sBPOBRANCH; }
            set { m_sBPOBRANCH = value; }
        }


        public string INSTALLMENTNO
        {
            get { return m_sINSTALLMENTNO; }
            set { m_sINSTALLMENTNO = value; }
        }

        public string INSTALLMENTAMOUNT
        {
            get { return m_sINSTALLMENTAMOUNT; }
            set { m_sINSTALLMENTAMOUNT = value; }
        }

        public string TXID
        {
            get { return m_sTXID; }
            set { m_sTXID = value; }
        }

        public string RemoteMessage
        {
            get { return m_sRemoteMessage; }
            set { m_sRemoteMessage = value; }
        }


        public string StatusCode
        {
            get { return m_sStatusCode; }
            set { m_sStatusCode = value; }
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

        public string BRANCHID
        {
            get { return m_sBRANCHID; }
            set { m_sBRANCHID = value; }
        }

        public string APITYPE
        {
            get { return m_sAPITYPE; }
            set { m_sAPITYPE = value; }
        }

        public string TransactionType
        {
            get { return m_sTransactionType; }
            set { m_sTransactionType = value; }
        }

        public string BillType
        {
            get { return m_sBillType; }
            set { m_sBillType = value; }
        }

        

        public string BILLSTATUS
        {
            get { return m_sBILLSTATUS; }
            set { m_sBILLSTATUS = value; }
        }

        public string DNCODE
        {
            get { return m_sDNCODE; }
            set { m_sDNCODE = value; }
        }

        public string DNDETAILS
        {
            get { return m_sDNDETAILS; }
            set { m_sDNDETAILS = value; }
        }

        public string DNBATYPE
        {
            get { return m_sDNBATYPE; }
            set { m_sDNBATYPE = value; }
        }

        public string TRANSACTIONNO
        {
            get { return m_sTRANSACTIONNO; }
            set { m_sTRANSACTIONNO = value; }
        }


        public string STAMPAMOUNT
        {
            get { return m_sSTAMPAMOUNT; }
            set { m_sSTAMPAMOUNT = value; }
        }
         
        public string COMMENTS
        {
            get { return m_sCOMMENTS; }
            set { m_sCOMMENTS = value; }
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

        public string OperationType
        {
            get { return m_sOperationType; }
            set { m_sOperationType = value; }
        }

        public CUser UserInfo
        {
            get { return oUser; }
            set { oUser = value; }
        }

        public CBranch Branch
        {
            get { return oBranch; }
            set { oBranch = value; }
        }

        public CBank Bank
        {
            get { return oBank; }
            set { oBank = value; }
        }

        public CAbabilTransactionResponse AbabilTransactionResponse
        {
            get { return oAbabilTransactionResponse; }
            set { oAbabilTransactionResponse = value; }
        }


        #endregion


    }
}
