using SIBLCommon.Common.Entity.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SIBLCommon.SIBLCommon.Common.Entity.RemitGlobalAPI
{
    public class CDAS
    {
        CSystemInfo m_oSysteminfo;
        string sLastRecordId;
        string sBankId;
        #region Constructor
        public CDAS()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_oSysteminfo = new CSystemInfo();
            sLastRecordId = string.Empty;
            sBankId = string.Empty;

        }
        #endregion Initialization
         [DataMember]
        public string LastRecordId
        {
            get { return sLastRecordId; }
            set { sLastRecordId = value; }
        }

         [DataMember]
         public string BankId
         {
             get { return sBankId; }
             set { sBankId = value; }
         }

         [DataMember]
        public CSystemInfo SystemInfo
        {
            get { return m_oSysteminfo; }
            set { m_oSysteminfo = value; }
        }

    }
}
