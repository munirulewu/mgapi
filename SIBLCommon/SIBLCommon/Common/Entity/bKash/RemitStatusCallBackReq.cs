using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SIBLCommon.SIBLCommon.Common.Entity.bKash
{
    [Serializable]
    [DataContract]
    public class RemitStatusCallBackReq
    {
        protected string m_sUserName;
        protected string m_sPassword;

        protected string m_sconversionID;
        protected string m_sSIBLREFNO;

        protected string m_sResponseCode;
        protected string m_sResponseDescription;



          #region Constructor
        public RemitStatusCallBackReq()
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
            m_sSIBLREFNO = string.Empty;

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
        public string SIBLREFNO
        {
            get { return m_sSIBLREFNO; }
            set { m_sSIBLREFNO = value; }
        }
         
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
