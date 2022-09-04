using SIBLCommon.Common.Entity.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
 
namespace SIBLCommon.SIBLCommon.Common.Entity.bKash
{
    [Serializable]
    [DataContract]
    public class BAVResponse : ASIBLEntityBase
    {
        protected string ms_responseCode;
        protected string ms_responseDescription;
        protected string m_sconversionID;
        
        #region Constructor
        public BAVResponse()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {

            ms_responseCode = string.Empty;
            ms_responseDescription = string.Empty;
            m_sconversionID = string.Empty;
           
        }
        #endregion Initialization

        [DataMember]
        public string ResponseCode
        {
            get { return ms_responseCode; }
            set { ms_responseCode = value; }
        }
        [DataMember]
        public string ResponseDescription
        {
            get { return ms_responseDescription; }
            set { ms_responseDescription = value; }
        }
        [DataMember]
        public string conversionID
        {
            get { return m_sconversionID; }
            set { m_sconversionID = value; }
        }
    }
}
