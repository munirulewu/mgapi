using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLXoomCommon.SIBLXoom.Common.Entity.Disbursement
{
    [Serializable]
    public class CAuthenticationInformationList : ASIBLEntityBase
    {
        protected List<CAuthenticationInformation> m_liAuthenticationInformationList;
        public CAuthenticationInformationList()
        {
            m_liAuthenticationInformationList = new List<CAuthenticationInformation>();
        }
        public List<CAuthenticationInformation> AuthenticationInformationList
        {
            get { return m_liAuthenticationInformationList; }
            set { m_liAuthenticationInformationList = value; }
        }
    }
}
