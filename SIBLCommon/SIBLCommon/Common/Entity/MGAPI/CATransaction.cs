using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.SIBLCommon.Common.Entity.RemitGlobalAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SIBLCommon.SIBLCommon.Common.Entity.MGAPI
{
    [Serializable]
    [DataContract]
    public class CATransaction 
    {
        CSender m_oSender;
        CReceiver m_oReceiver;
        CAmount m_oReceiveAmount;
        CAmount m_oSendAmount;
        protected string ms_CN;
        protected  string ms_accountCode;
        protected string ms_accountNumber;
        protected string ms_ReceiverCountryCode;
        protected string ms_SenderCountryCode;
        protected string ms_receiveAmount;
        protected string ms_MGITransactionId;
        protected string ms_SIBLTransactionId;
        protected string ms_SouceofFound;
        protected string ms_TransactionType;
        protected string ms_OperationType;
        protected string ms_convertionId;
        protected string ms_convertionDate;
        List<CAdditionData> m_oAdditionalData;

        #region Constructor
        public CATransaction()
             
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            ms_CN = string.Empty;
            m_oSender = new CSender();
            m_oReceiver = new CReceiver();
            m_oReceiveAmount = new CAmount();
            m_oSendAmount = new CAmount();
            ms_accountCode = string.Empty;
            ms_accountNumber = string.Empty;
            ms_ReceiverCountryCode = string.Empty;
            ms_SenderCountryCode = string.Empty;
            ms_receiveAmount = string.Empty;
            ms_MGITransactionId = string.Empty;
            ms_SIBLTransactionId = string.Empty;
            ms_SouceofFound  = string.Empty;
            ms_TransactionType = string.Empty;
            ms_OperationType = string.Empty;
            m_oAdditionalData = new List<CAdditionData>();
            ms_convertionId = string.Empty;
            ms_convertionDate = string.Empty;
        }


        /// <summary>
        /// MoneyGram Unique Transaction id
        /// </summary>
        [DataMember]
        public string mgiTransactionId
        {
            get { return ms_MGITransactionId; }
            set { ms_MGITransactionId = value; }
        }

        [DataMember]
        public string receiveCountryCode
        {
            get { return ms_ReceiverCountryCode; }
            set { ms_ReceiverCountryCode = value; }
        }
        [DataMember]
        public string sendCountryCode
        {
            get { return ms_SenderCountryCode; }
            set { ms_SenderCountryCode = value; }
        }

        [DataMember]
        public CAmount sendAmount
        {
            get { return m_oSendAmount; }
            set { m_oSendAmount = value; }
        }
        /// <summary>
        /// Receiver Amount
        /// </summary>
        [DataMember]
        public CAmount receiveAmount
        {
            get { return m_oReceiveAmount; }
            set { m_oReceiveAmount = value; }
        }

        /// <summary>
        /// Sender Information
        /// </summary>
        [DataMember]
        public CSender sender
        {
            get { return m_oSender; }
            set { m_oSender = value; }
        }
        /// <summary>
        /// Receiver Information
        /// </summary>
        [DataMember]
        public CReceiver receiver
        {
            get { return m_oReceiver; }
            set { m_oReceiver = value; }
        }
        /// <summary>
        /// Unique Transaction id of SIBL
        /// </summary>
        public  string siblTransactionId
        {
            get { return ms_SIBLTransactionId; }
            set { ms_SIBLTransactionId = value; }
        }

        public string SouceofFund
        {
            get { return ms_SouceofFound; }
            set { ms_SouceofFound = value; }
        }
            
        public string CN 
        {
            get { return ms_CN; }
            set { ms_CN = value; }
        }
        public string TransactionType
        {
            get { return ms_TransactionType; }
            set { ms_TransactionType = value; }
        }
        public string convertionId
        {
            get { return ms_convertionId; }
            set { ms_convertionId = value; }
        }
        public string convertionDate
        {
            get { return ms_convertionDate; }
            set { ms_convertionDate = value; }
        }
        public string operationType
        {
            get { return ms_OperationType; }
            set { ms_OperationType = value; }
        }
        /// <summary>
        /// Routing Number
        /// </summary>
        
        //public string accountCode
        //{
        //    get { return ms_accountCode; }
        //    set { ms_accountCode = value; }
        //}
        ///// <summary>
        ///// Account Number
        ///// </summary>
        
        //public string accountNumber
        //{
        //    get { return ms_accountNumber; }
        //    set { ms_accountNumber = value; }
        //}
       
 
      [DataMember]
       public List<CAdditionData> additionalData

        {
            get { return m_oAdditionalData; }
            set { m_oAdditionalData = value; }
        }
        #endregion Initialization
    }
}
