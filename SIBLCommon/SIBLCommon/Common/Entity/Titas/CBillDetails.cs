using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;
using Newtonsoft.Json;

namespace SIBLCommon.SIBLCommon.Common.Entity.Titas
{
    [Serializable]
    public class CBillDetails : ASIBLEntityBase
    {
        protected string scustomerCode;
        protected string scustomerName;
        protected string sinvoiceNo;
        protected string sinvoiceAmount;
        protected string sissueMonth;
        protected string ssettleDate;
        protected string szone;
        protected string sstatus;
        protected string smessage;

        #region Constructor
        public CBillDetails()
            : base()
        {
            Initialization();
        }
        protected void Initialization()
        {
            scustomerCode = string.Empty;
            scustomerName = string.Empty;
            sinvoiceNo = string.Empty;
            sinvoiceAmount = string.Empty;
            sissueMonth = string.Empty;
            ssettleDate = string.Empty;
            szone = string.Empty;
            sstatus = string.Empty;
            smessage = string.Empty;
        }
        #endregion Constructor

        [JsonProperty("customerCode")]
        public string customerCode
        {
            get { return scustomerCode; }
            set { scustomerCode = value; }
        }

        [JsonProperty("customerName")]
        public string customerName
        {
            get { return scustomerName; }
            set { scustomerName = value; }
        }

        [JsonProperty("invoiceNo")]
        public string invoiceNo
        {
            get { return sinvoiceNo; }
            set { sinvoiceNo = value; }
        }
        [JsonProperty("invoiceAmount")]
        public string invoiceAmount
        {
            get { return sinvoiceAmount; }
            set { sinvoiceAmount = value; }
        }

        [JsonProperty("issueMonth")]
        public string issueMonth
        {
            get { return sissueMonth; }
            set { sissueMonth = value; }
        }
        [JsonProperty("settleDate")]
        public string settleDate
        {
            get { return ssettleDate; }
            set { ssettleDate = value; }
        }

        [JsonProperty("zone")]
        public string zone
        {
            get { return szone; }
            set { szone = value; }
        }

        [JsonProperty("status")]
        public string status
        {
            get { return sstatus; }
            set { sstatus = value; }
        }
        [JsonProperty("message")]
        public string message
        {
            get { return smessage; }
            set { smessage = value; }
        }
    }
}
