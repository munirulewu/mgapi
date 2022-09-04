using SIBLCommon.Common.Entity.Bank;
using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.Common.Entity.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace SIBLCommon.SIBLCommon.Common.Entity.WorldRemit
{
    [Serializable]
    public class CWRTransactionDetails : ASIBLEntityBase
    {

        #region Protectd Member

        protected string m_sbank_account;
        protected string m_sbank_name;
        protected string m_sbank_code;
        protected string m_slocal_bank_code;
        protected string m_sswift_code;
        protected string m_sbank_branch_name;
        protected string m_sbank_address;
        protected string m_sbank_city;
        protected string m_sbank_country;
        protected string m_siban;
        protected string m_swr_transaction_id;
        protected string m_swr_transaction_number;
        protected string m_sstatus;
        protected string m_stransaction_date;
        protected string m_stransaction_pay_date;
        protected string m_sPayOutStatus;

        protected string m_soriginating_country;
        protected string m_sdestination_country;
        protected string m_ssending_amount;
        protected string m_soriginating_currency;
        protected string m_sdestination_currency;
        protected string m_scustomer_fee;
        protected string m_spayout_amount;
        protected string m_sexchange_rate;
        protected string m_sproduct;
        protected string m_ssender_id;
        protected string m_ssender_first_name;
        protected string m_ssender_middle_name;
        protected string m_ssender_last_name;
        protected string m_ssender_country;
        protected string m_ssender_city;
        protected string m_ssender_dob;
        protected string m_ssender_id_issuance;
        protected string m_sreceiver_first_name;
        protected string m_sreceiver_middle_name;
        protected string m_sreceiver_last_name;
        protected string m_sreceiver_address_1;
        protected string m_sreceiver_address_2;
        protected string m_sreceiver_city;
        protected string m_sreceiver_state;
        protected string m_sreceiver_postcode;
        protected string m_sreceiver_country;
        protected string m_sreceiver_mobile_number;
        protected string m_sreceiver_landline_number;
        protected string m_sreceiver_email_id;
        protected string m_sreceiver_momo_account;
        protected string m_sreceiver_accounttype_name;
        protected string m_sreceiver_accounttype_code;
        protected string m_sreceiver_idtype_name;
        protected string m_sreceiver_id_no;
        protected string m_ssettlement_currency;
        protected string m_ssettlement_amount;
        protected string m_scorrespondent_commission;
        protected string m_ssettlement_exchange_rate;
        protected string m_stransaction_reference;
        protected string m_ssend_reason_code;
        protected string m_ssend_reason_desc;
        protected string m_sreceiver_country_code;
        protected string m_scodeDescription;
        protected string m_sstatuscode;

         

        protected string m_sPayoutReceiverIDTypeName;
        protected string m_sPayoutReceiverIDTypeNo;
        protected string m_sPayoutReceiverIDIssueDate;
        protected string m_sPayoutReceiverIDExpiryDate;
        protected string m_sPayoutComments;
        protected string m_sPayoutReceiverPhone;
        protected string m_sEntryDate;
        protected string m_sPayoutReceiverIDTypeValue;

        protected string m_sFromDate;
        protected string m_sToDate;

        protected CUser m_sUser;
        protected CBranch m_oBranch;
        #endregion


        #region Constructor
        public CWRTransactionDetails()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_sbank_account = string.Empty;
            m_sbank_name = string.Empty;
            m_sbank_code = string.Empty;
            m_slocal_bank_code = string.Empty;
            m_sswift_code = string.Empty;
            m_sbank_branch_name = string.Empty;
            m_sbank_address = string.Empty;
            m_sbank_city = string.Empty;
            m_sbank_country = string.Empty;
            m_siban = string.Empty;
            m_swr_transaction_id = string.Empty;
            m_swr_transaction_number = string.Empty;
            m_sstatus = string.Empty;
            m_stransaction_date = string.Empty;
            m_soriginating_country = string.Empty;
            m_sdestination_country = string.Empty;
            m_ssending_amount = string.Empty;
            m_soriginating_currency = string.Empty;
            m_sdestination_currency = string.Empty;
            m_scustomer_fee = string.Empty;
            m_spayout_amount = string.Empty;
            m_sexchange_rate = string.Empty;
            m_sproduct = string.Empty;
            m_ssender_id = string.Empty;
            m_ssender_first_name = string.Empty;
            m_ssender_middle_name = string.Empty;
            m_ssender_last_name = string.Empty;
            m_ssender_country = string.Empty;
            m_ssender_city = string.Empty;
            m_ssender_dob = string.Empty;
            m_ssender_id_issuance = string.Empty;
            m_sreceiver_first_name = string.Empty;
            m_sreceiver_middle_name = string.Empty;
            m_sreceiver_last_name = string.Empty;
            m_sreceiver_address_1 = string.Empty;
            m_sreceiver_address_2 = string.Empty;
            m_sreceiver_city = string.Empty;
            m_sreceiver_state = string.Empty;
            m_sreceiver_postcode = string.Empty;
            m_sreceiver_country = string.Empty;
            m_sreceiver_mobile_number = string.Empty;
            m_sreceiver_landline_number = string.Empty;
            m_sreceiver_email_id = string.Empty;
            m_sreceiver_momo_account = string.Empty;
            m_sreceiver_accounttype_name = string.Empty;
            m_sreceiver_accounttype_code = string.Empty;
            m_sreceiver_idtype_name = string.Empty;
            m_sreceiver_id_no = string.Empty;
            m_ssettlement_currency = string.Empty;
            m_ssettlement_amount = string.Empty;
            m_scorrespondent_commission = string.Empty;
            m_ssettlement_exchange_rate = string.Empty;
            m_stransaction_reference = string.Empty;
            m_ssend_reason_code = string.Empty;
            m_ssend_reason_desc = string.Empty;
            m_sreceiver_country_code = string.Empty;
            m_stransaction_pay_date = string.Empty;
            m_scodeDescription = string.Empty;
            m_sstatuscode = string.Empty;

            m_sPayoutReceiverIDTypeName = string.Empty;
            m_sPayoutReceiverIDTypeNo = string.Empty;
            m_sPayoutReceiverIDIssueDate = string.Empty;
            m_sPayoutReceiverIDExpiryDate = string.Empty;
            m_sPayoutComments = string.Empty;
            m_sPayoutReceiverPhone = string.Empty;

            m_sFromDate = string.Empty;
            m_sToDate = string.Empty;

            m_sUser = new CUser();
            m_oBranch = new CBranch();
            m_sPayOutStatus = string.Empty;
            m_sPayoutReceiverIDTypeValue = string.Empty;

        }
        #endregion Initialization

        #region public Member


        [JsonProperty("bank_account")]
        public string bank_account
        {
            get { return m_sbank_account; }
            set { m_sbank_account = value; }
        }

        [JsonProperty("bank_name")]
        public string bank_name
        {
            get { return m_sbank_name; }
            set { m_sbank_name = value; }
        }
        [JsonProperty("bank_code")]
        public string bank_code
        {
            get { return m_sbank_code; }
            set { m_sbank_code = value; }
        }
        [JsonProperty("local_bank_code")]
        public string local_bank_code
        {
            get { return m_slocal_bank_code; }
            set { m_slocal_bank_code = value; }
        }

        [JsonProperty("swift_code")]
        public string swift_code
        {
            get { return m_sswift_code; }
            set { m_sswift_code = value; }
        }

        [JsonProperty("bank_branch_name")]
        public string bank_branch_name
        {
            get { return m_sbank_branch_name; }
            set { m_sbank_branch_name = value; }
        }
        [JsonProperty("bank_address")]
        public string bank_address
        {
            get { return m_sbank_address; }
            set { m_sbank_address = value; }
        }
        [JsonProperty("bank_city")]
        public string bank_city
        {
            get { return m_sbank_city; }
            set { m_sbank_city = value; }
        }
        [JsonProperty("bank_country")]
        public string bank_country
        {
            get { return m_sbank_country; }
            set { m_sbank_country = value; }
        }
        [JsonProperty("iban")]
        public string iban
        {
            get { return m_siban; }
            set { m_siban = value; }
        }
        [JsonProperty("wr_transaction_id")]
        public string wr_transaction_id
        {
            get { return m_swr_transaction_id; }
            set { m_swr_transaction_id = value; }
        }
        [JsonProperty("wr_transaction_number")]
        public string wr_transaction_number
        {
            get { return m_swr_transaction_number; }
            set { m_swr_transaction_number = value; }
        }
        [JsonProperty("status")]
        public string TransactionStatus
        {
            get { return m_sstatus; }
            set { m_sstatus = value; }
        }
        [JsonProperty("transaction_date")]
        public string transaction_date
        {
            get { return m_stransaction_date; }
            set { m_stransaction_date = value; }
        }

        [JsonProperty("transaction_pay_date")]
        public string transaction_pay_date
        {
            get { return m_stransaction_pay_date; }
            set { m_stransaction_pay_date = value; }
        }
        [JsonProperty("originating_country")]
        public string originating_country
        {
            get { return m_soriginating_country; }
            set { m_soriginating_country = value; }
        }
        [JsonProperty("destination_country")]
        public string destination_country
        {
            get { return m_sdestination_country; }
            set { m_sdestination_country = value; }
        }
        [JsonProperty("sending_amount")]
        public string sending_amount
        {
            get { return m_ssending_amount; }
            set { m_ssending_amount = value; }
        }
        [JsonProperty("originating_currency")]
        public string originating_currency
        {
            get { return m_soriginating_currency; }
            set { m_soriginating_currency = value; }
        }
        [JsonProperty("destination_currency")]
        public string destination_currency
        {
            get { return m_sdestination_currency; }
            set { m_sdestination_currency = value; }
        }
        [JsonProperty("customer_fee")]
        public string customer_fee
        {
            get { return m_scustomer_fee; }
            set { m_scustomer_fee = value; }
        }
        [JsonProperty("payout_amount")]
        public string payout_amount
        {
            get { return m_spayout_amount; }
            set { m_spayout_amount = value; }
        }
        [JsonProperty("exchange_rate")]
        public string exchange_rate
        {
            get { return m_sexchange_rate; }
            set { m_sexchange_rate = value; }
        }
        [JsonProperty("product")]
        public string product
        {
            get { return m_sproduct; }
            set { m_sproduct = value; }
        }

        [JsonProperty("sender_id")]
        public string sender_id
        {
            get { return m_ssender_id; }
            set { m_ssender_id = value; }
        }
        [JsonProperty("sender_first_name")]
        public string sender_first_name
        {
            get { return m_ssender_first_name; }
            set { m_ssender_first_name = value; }
        }
        [JsonProperty("sender_middle_name")]
        public string sender_middle_name
        {
            get { return m_ssender_middle_name; }
            set { m_ssender_middle_name = value; }
        }
        [JsonProperty("sender_last_name")]
        public string sender_last_name
        {
            get { return m_ssender_last_name; }
            set { m_ssender_last_name = value; }
        }
        [JsonProperty("sender_country")]
        public string sender_country
        {
            get { return m_ssender_country; }
            set { m_ssender_country = value; }
        }
        [JsonProperty("sender_city")]
        public string sender_city
        {
            get { return m_ssender_city; }
            set { m_ssender_city = value; }
        }
        [JsonProperty("sender_dob")]
        public string sender_dob
        {
            get { return m_ssender_dob; }
            set { m_ssender_dob = value; }
        }
        [JsonProperty("sender_id_issuance")]
        public string sender_id_issuance
        {
            get { return m_ssender_id_issuance; }
            set { m_ssender_id_issuance = value; }
        }
        [JsonProperty("receiver_first_name")]
        public string receiver_first_name
        {
            get { return m_sreceiver_first_name; }
            set { m_sreceiver_first_name = value; }
        }
        [JsonProperty("receiver_middle_name")]
        public string receiver_middle_name
        {
            get { return m_sreceiver_middle_name; }
            set { m_sreceiver_middle_name = value; }
        }
        [JsonProperty("receiver_last_name")]
        public string receiver_last_name
        {
            get { return m_sreceiver_last_name; }
            set { m_sreceiver_last_name = value; }
        }
        [JsonProperty("receiver_address_1")]
        public string receiver_address_1
        {
            get { return m_sreceiver_address_1; }
            set { m_sreceiver_address_1 = value; }
        }

        [JsonProperty("receiver_address_2")]
        public string receiver_address_2
        {
            get { return m_sreceiver_address_2; }
            set { m_sreceiver_address_2 = value; }
        }
        [JsonProperty("receiver_city")]
        public string receiver_city
        {
            get { return m_sreceiver_city; }
            set { m_sreceiver_city = value; }
        }
        [JsonProperty("receiver_state")]
        public string receiver_state
        {
            get { return m_sreceiver_state; }
            set { m_sreceiver_state = value; }
        }
        [JsonProperty("receiver_postcode")]
        public string receiver_postcode
        {
            get { return m_sreceiver_postcode; }
            set { m_sreceiver_postcode = value; }
        }
        [JsonProperty("receiver_country")]
        public string receiver_country
        {
            get { return m_sreceiver_country; }
            set { m_sreceiver_country = value; }
        }
        [JsonProperty("receiver_mobile_number")]
        public string receiver_mobile_number
        {
            get { return m_sreceiver_mobile_number; }
            set { m_sreceiver_mobile_number = value; }
        }
        [JsonProperty("receiver_landline_number")]
        public string receiver_landline_number
        {
            get { return m_sreceiver_landline_number; }
            set { m_sreceiver_landline_number = value; }
        }
        [JsonProperty("receiver_email_id")]
        public string receiver_email_id
        {
            get { return m_sreceiver_email_id; }
            set { m_sreceiver_email_id = value; }
        }
        [JsonProperty("receiver_momo_account")]
        public string receiver_momo_account
        {
            get { return m_sreceiver_momo_account; }
            set { m_sreceiver_momo_account = value; }
        }
        [JsonProperty("receiver_accounttype_name")]
        public string receiver_accounttype_name
        {
            get { return m_sreceiver_accounttype_name; }
            set { m_sreceiver_accounttype_name = value; }
        }
        [JsonProperty("receiver_accounttype_code")]
        public string receiver_accounttype_code
        {
            get { return m_sreceiver_accounttype_code; }
            set { m_sreceiver_accounttype_code = value; }
        }
        [JsonProperty("receiver_idtype_name")]
        public string receiver_idtype_name
        {
            get { return m_sreceiver_idtype_name; }
            set { m_sreceiver_idtype_name = value; }
        }

        [JsonProperty("receiver_id_no")]

        public string receiver_id_no
        {
            get { return m_sreceiver_id_no; }
            set { m_sreceiver_id_no = value; }
        }
        [JsonProperty("settlement_currency")]
        public string settlement_currency
        {
            get { return m_ssettlement_currency; }
            set { m_ssettlement_currency = value; }
        }
        [JsonProperty("settlement_amount")]
        public string settlement_amount
        {
            get { return m_ssettlement_amount; }
            set { m_ssettlement_amount = value; }
        }
        [JsonProperty("correspondent_commission")]
        public string correspondent_commission
        {
            get { return m_scorrespondent_commission; }
            set { m_scorrespondent_commission = value; }
        }
        [JsonProperty("settlement_exchange_rate")]
        public string settlement_exchange_rate
        {
            get { return m_ssettlement_exchange_rate; }
            set { m_ssettlement_exchange_rate = value; }
        }
        [JsonProperty("transaction_reference")]
        public string transaction_reference
        {
            get { return m_stransaction_reference; }
            set { m_stransaction_reference = value; }
        }
        [JsonProperty("send_reason_code")]
        public string send_reason_code
        {
            get { return m_ssend_reason_code; }
            set { m_ssend_reason_code = value; }
        }
        [JsonProperty("send_reason_desc")]
        public string send_reason_desc
        {
            get { return m_ssend_reason_desc; }
            set { m_ssend_reason_desc = value; }
        }
        [JsonProperty("receiver_country_code")]
        public string receiver_country_code
        {
            get { return m_sreceiver_country_code; }
            set { m_sreceiver_country_code = value; }
        }
        [JsonProperty("FromDate")]
        public string FromDate
        {
            get { return m_sFromDate; }
            set { m_sFromDate = value; }
        }
        [JsonProperty("ToDate")]
        public string ToDate
        {
            get { return m_sToDate; }
            set { m_sToDate = value; }
        }

        [JsonProperty("codeDescription")]
        public string codeDescription
        {
            get { return m_scodeDescription; }
            set { m_scodeDescription = value; }
        }


        [JsonProperty("statuscode")]
        public string statuscode
        {
            get { return m_sstatuscode; }
            set { m_sstatuscode = value; }
        }


        [JsonProperty("PayoutReceiverIDTypeName")]
        public string PayoutReceiverIDTypeName
        {
            get { return m_sPayoutReceiverIDTypeName; }
            set { m_sPayoutReceiverIDTypeName = value; }
        }

        [JsonProperty("PayoutReceiverIDTypeNo")]
        public string PayoutReceiverIDTypeNo
        {
            get { return m_sPayoutReceiverIDTypeNo; }
            set { m_sPayoutReceiverIDTypeNo = value; }
        }


        [JsonProperty("PayoutReceiverIDIssueDate")]
        public string PayoutReceiverIDIssueDate
        {
            get { return m_sPayoutReceiverIDIssueDate; }
            set { m_sPayoutReceiverIDIssueDate = value; }
        }


        [JsonProperty("PayoutReceiverIDExpiryDate")]
        public string PayoutReceiverIDExpiryDate
        {
            get { return m_sPayoutReceiverIDExpiryDate; }
            set { m_sPayoutReceiverIDExpiryDate = value; }
        }

        [JsonProperty("PayoutComments")]
        public string PayoutComments
        {
            get { return m_sPayoutComments; }
            set { m_sPayoutComments = value; }
        }

        [JsonProperty("PayoutReceiverPhone")]
        public string PayoutReceiverPhone
        {
            get { return m_sPayoutReceiverPhone; }
            set { m_sPayoutReceiverPhone = value; }
        }


        public CUser User
        {
            get { return m_sUser; }
            set { m_sUser = value; }
        }


        public CBranch BranchInfo
        {
            get { return m_oBranch; }
            set { m_oBranch = value; }
        }

        public string EntryDate
        {
            get { return m_sEntryDate; }
            set { m_sEntryDate = value; }
        }

        public string PayOutStatus
        {
            get { return m_sPayOutStatus; }
            set { m_sPayOutStatus = value; }
        }

        public string PayoutReceiverIDTypeValue
        {
            get { return m_sPayoutReceiverIDTypeValue; }
            set { m_sPayoutReceiverIDTypeValue = value; }
        }

        
        #endregion
    }
}
