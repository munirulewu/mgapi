using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.Common.Entity.User;

namespace SIBLXoomCommon.SIBLXoom.Common.Entity.CPU
{
    [Serializable]
    public class CCheque : ASIBLEntityBase
    {
        #region Protectd Member

        protected string m_sCHEQUENO;
        protected string m_sISACTIVE;
        protected string m_sCREATEBY;
        protected string m_sCOMPANYID;
        protected string m_sBENEFICIARY_NAME;
        protected string m_sPINNO;
        protected string m_sAMOUNT;
        protected string m_sREMARKS;
        protected string m_sNIDNO;
        protected string m_sNIDISSUEDATE;
        protected string m_sNIDEXPIRYDATE;
        protected string m_sdUSEDDATE;
        protected string m_sUSEDBY;
        protected string m_sId_Type; 
        protected string m_sOperationType;
        protected string m_sFDate;
        protected string m_sTDate;

        protected CUser m_oUser;
        protected string m_sRoutingNumber;
        protected string m_sStatus;


        #endregion

        #region Constructor
        public CCheque()
            : base()
        {
            Initialization();
        }
        #endregion Constructor


        #region Initialization
        protected void Initialization()
        {
            m_sCHEQUENO = string.Empty;
            m_sISACTIVE = string.Empty;
            m_sCREATEBY = string.Empty;
            m_sCOMPANYID = string.Empty;
            m_sBENEFICIARY_NAME = string.Empty;
            m_sPINNO = string.Empty;
            m_sAMOUNT = string.Empty;
            m_sREMARKS = string.Empty;
            m_sNIDNO = string.Empty;
            m_sNIDISSUEDATE = string.Empty;
            m_sNIDEXPIRYDATE = string.Empty;
            m_sdUSEDDATE = string.Empty;
            m_sUSEDBY = string.Empty;
            m_sOperationType = string.Empty;
            m_oUser = new CUser();
            m_sId_Type = string.Empty;
            m_sFDate = string.Empty;
            m_sTDate = string.Empty;
            m_sRoutingNumber = string.Empty;
            m_sStatus = string.Empty;
        }
        #endregion Initialization

        #region public Member

        public string CHEQUENO
        {
            get { return m_sCHEQUENO; }
            set { m_sCHEQUENO = value; }
        }
        public string ISACTIVE
        {
            get { return m_sISACTIVE; }
            set { m_sISACTIVE = value; }
        }

        
        public string CREATEBY
        {
            get { return m_sCREATEBY; }
            set { m_sCREATEBY = value; }
        }

        public string COMPANYID
        {
            get { return m_sCOMPANYID; }
            set { m_sCOMPANYID = value; }
        }

        public string BENEFICIARY_NAME
        {
            get { return m_sBENEFICIARY_NAME; }
            set { m_sBENEFICIARY_NAME = value; }
        }

        public string PINNO
        {
            get { return m_sPINNO; }
            set { m_sPINNO = value; }
        }



        public string AMOUNT
        {
            get { return m_sAMOUNT; }
            set { m_sAMOUNT = value; }
        }

        public string REMARKS
        {
            get { return m_sREMARKS; }
            set { m_sREMARKS = value; }
        }

        public string NIDNO
        {
            get { return m_sNIDNO; }
            set { m_sNIDNO = value; }
        }

        public string NIDISSUEDATE
        {
            get { return m_sNIDISSUEDATE; }
            set { m_sNIDISSUEDATE = value; }
        }

        public string NIDEXPIRYDATE
        {
            get { return m_sNIDEXPIRYDATE; }
            set { m_sNIDEXPIRYDATE = value; }
        }

        public string dUSEDDATE
        {
            get { return m_sdUSEDDATE; }
            set { m_sdUSEDDATE = value; }
        }

        public string USEDBY
        {
            get { return m_sUSEDBY; }
            set { m_sUSEDBY = value; }
        }

        public string OperationType
        {
            get { return m_sOperationType; }
            set { m_sOperationType = value; }
        }
        public CUser User
        {
            get { return m_oUser; }
            set { m_oUser = value; }
        }
        public string Id_Type
        {
            get { return m_sId_Type; }
            set { m_sId_Type = value; }
        }

        public string FDate
        {
            get { return m_sFDate; }
            set { m_sFDate = value; }
        }

        public string TDate
        {
            get { return m_sTDate; }
            set { m_sTDate = value; }
        }

        public string RoutingNumber
        {
            get { return m_sRoutingNumber; }
            set { m_sRoutingNumber = value; }
        }

        public string Status
        {
            get { return m_sStatus; }
            set { m_sStatus = value; }
        }

        #endregion     
    }
}
