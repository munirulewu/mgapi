using SIBLCommon.Common.Entity.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SIBLCommon.SIBLCommon.Common.Entity.Country
{
      [Serializable]
    [DataContract]
    public class CCountry : ASIBLEntityBase
    {
         #region Protectd Member
      
        protected string m_sCountryCode;
        protected string m_sCountryName;
        protected string m_sCountryID;
        protected string m_sRecordNumber;
        #endregion


        #region Constructor
        public CCountry()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {

            m_sCountryCode = string.Empty;
            m_sCountryName = string.Empty;
            m_sCountryID = string.Empty;
            m_sRecordNumber=string.Empty;
        }
        #endregion Initialization
       
        #region public Member
          [DataMember]
        public string CID
        {
            get { return m_sCountryID; }
            set { m_sCountryID = value; }
        }

 [DataMember]
        public string CountryCode
        {
            get { return m_sCountryCode; }
            set { m_sCountryCode = value; }
        }
         [DataMember]
        public string CountryName
        {
            get { return m_sCountryName; }
            set { m_sCountryName = value; }
        }
         [DataMember]
         public string RecordNumber
         {
             get { return m_sRecordNumber; }
             set { m_sRecordNumber = value; }
         }
        
        #endregion     
    }
}
