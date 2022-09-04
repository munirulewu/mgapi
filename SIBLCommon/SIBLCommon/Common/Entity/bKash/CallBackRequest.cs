using SIBLCommon.Common.Entity.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace SIBLCommon.SIBLCommon.Common.Entity.bKash
{
     [Serializable]
    [DataContract]
    public class CallBackRequest 
    {
        protected string m_sUserName;
        protected string m_sPassword;

        protected string m_sconversionID;
        protected string m_scountryCode;
        protected string m_smsisdn;
        protected string m_sfirstName;
        protected string m_slastName;
        protected string m_sfullName;
        //protected string m_sSIBLREFNO;
        protected string m_sResponseCode;
        protected string m_sResponseDescription;
          #region Constructor
        public CallBackRequest()
            : base()
        {
            Initialization();
        }
        #endregion Constructor
        #region Initialization
        protected void Initialization()
        {

            m_sUserName = string.Empty;
            m_sPassword = string.Empty;

            m_sconversionID = string.Empty;
            m_scountryCode = string.Empty;
            m_smsisdn = string.Empty;
            m_sfirstName = string.Empty;
            m_slastName = string.Empty;
            m_sfullName = string.Empty;
            //m_sSIBLREFNO = string.Empty;
            m_sResponseCode = string.Empty;
            m_sResponseDescription = string.Empty;

        }
        #endregion Initialization
         [DataMember]
        public string UserName
        {
            get { return m_sUserName; }
            set { m_sUserName = value; }
        }
         [DataMember]
         [Required]
        public string Password
        {
            get { return m_sPassword; }
            set { m_sPassword = value; }
        }
         [DataMember]
         [Required(ErrorMessage="Converion id is required")]
        public string conversionID
        {
            get { return m_sconversionID; }
            set { m_sconversionID = value; }
        }
         [DataMember]
        public string countryCode
        {
            get { return m_scountryCode; }
            set { m_scountryCode = value; }
        }
         [DataMember]
         [Required(ErrorMessage = "msisdn id is required"),MaxLength(20)]
        public string msisdn
        {
            get { return m_smsisdn; }
            set { m_smsisdn = value; }
        }
         [DataMember]
        public string firstName
        {
            get { return m_sfirstName; }
            set { m_sfirstName = value; }
        }
         [DataMember]
        public string lastName
        {
            get { return m_slastName; }
            set { m_slastName = value; }
        }
         [DataMember]
        public string fullName
        {
            get { return m_sfullName; }
            set { m_sfullName = value; }
        }
        /*
         [DataMember]
        public string SIBLREFNO
        {
            get { return m_sSIBLREFNO; }
            set { m_sSIBLREFNO = value; }
        }
          */
          [DataMember]
         public string ResponseCode
         {
             get { return m_sResponseCode; }
             set { m_sResponseCode = value; }
         }
          [DataMember]
         public string ResponseDescription
         {
             get { return m_sResponseDescription; }
             set { m_sResponseDescription = value; }
         }
    }
}
