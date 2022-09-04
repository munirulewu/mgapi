using SIBLCommon.Common.Entity.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SIBLCommon.SIBLCommon.Common.Entity.RemitGlobalAPI
{
     [Serializable]
    public class CSystemInfo 
    {
        #region Protectd Member

        protected string m_sUserName;
        protected string m_sPassword;
        protected string m_sRemitCompanyCode;
        protected string m_sRemitCompanyName;
        #endregion


        #region Constructor
        public CSystemInfo()
          
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {

            m_sUserName = String.Empty;
            m_sPassword = String.Empty;
            m_sRemitCompanyCode = string.Empty;
            m_sRemitCompanyName = string.Empty;
            

        }
        #endregion Initialization

        #region public Member

        [DataMember]
        public string UserName
        {
            get { return m_sUserName; }
            set { m_sUserName = value; }
        }
          [DataMember]
        public string Password
        {
            get { return m_sPassword; }
            set { m_sPassword = value; }
        }
 [DataMember]
        public string RemitCompanyCode
        {
            get { return m_sRemitCompanyCode; }
            set { m_sRemitCompanyCode = value; }
        }
          [DataMember]
        public string RemitCompanyName
        {
            get { return m_sRemitCompanyName; }
            set { m_sRemitCompanyName = value; }
        }

        #endregion     
    }
}
