using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;
using Newtonsoft.Json;

namespace SIBLCommon.SIBLCommon.Common.Entity.Shipping
{
    public class CCheckFees //: ASIBLEntityBase
    {
        protected string sApplicationId;
        protected string sInstituteName;
        protected string sPhoneNumber;
        protected string sType;
        protected string sToken;
        protected string sAmount;
        protected string sComission;
        protected string sTotalAmount;
        protected string sstatusCode;
        protected string smessage;
        
        #region Constructor
        public CCheckFees()
            : base()
        {
            Initialization();
        }
        protected void Initialization()
        {

            sApplicationId = string.Empty;
            sInstituteName = string.Empty;
            sPhoneNumber = string.Empty;
            sType = string.Empty;
            sAmount = string.Empty;
            sComission = string.Empty;
            sTotalAmount = string.Empty;
            sToken = string.Empty;
            sstatusCode = string.Empty;
            smessage = string.Empty;
        }
        #endregion Constructor
        [JsonProperty("afRef")]
        public string afRef
        {
            get { return sApplicationId; }
            set { sApplicationId = value; }
        }

        [JsonProperty("applicationId")]
        public string applicationId
        {
            get { return sApplicationId; }
            set { sApplicationId = value; }
        }

        [JsonProperty("type")]
        public string type
        {
            get { return sType; }
            set { sType = value; }
        }
        [JsonProperty("token")]
        public string token
        {
            get { return sToken; }
            set { sToken = value; }
        }

        [JsonProperty("instituteName")]
        public string instituteName
        {
            get { return sInstituteName; }
            set { sInstituteName = value; }
        }
        [JsonProperty("phoneNumber")]
        public string PhoneNumber
        {
            get { return sPhoneNumber; }
            set { sPhoneNumber = value; }
        }
        [JsonProperty("amount")]
        public string amount
        {
            get { return sAmount; }
            set { sAmount = value; }
        }
        [JsonProperty("commission")]
        public string comission
        {
            get { return sComission; }
            set { sComission = value; }
        }
        [JsonProperty("totalAmount")]
        public string totalAmount
        {
            get { return sTotalAmount; }
            set { sTotalAmount = value; }
        }

        [JsonProperty("statusCode")]
        public string statusCode
        {
            get { return sstatusCode; }
            set { sstatusCode = value; }
        }
        [JsonProperty("message")]
        public string message
        {
            get { return smessage; }
            set { smessage = value; }
        }

        
    }
}
