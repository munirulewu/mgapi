using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLCommon.SIBLCommon.Common.Entity.Titas
{
    [Serializable]
    public class CZone : ASIBLEntityBase
    {
        #region Protected Member

        protected string m_sZone_Name;
        protected string m_sEmail;
        protected string m_sIsActive;
        #endregion Protected Member


        #region Constructor
        public CZone()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_sZone_Name = string.Empty;
            m_sEmail = string.Empty;
            m_sIsActive = string.Empty;

        }
        #endregion Initialization

        #region public Member

        public string Zone_Name
        {
            get { return m_sZone_Name; }
            set { m_sZone_Name = value; }
        }

        public string Email
        {
            get { return m_sEmail; }
            set { m_sEmail = value; }
        }

        public string IsActive
        {
            get { return m_sIsActive; }
            set { m_sIsActive = value; }
        }

        #endregion
    }
}
