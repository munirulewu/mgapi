/*
 * File name            : CUser.cs
 * Author               : Munirul Islam
 * Date                 : November 10,2014
 * Version              : 1.0
 *
 * Description          : Entity Class for User
 *
 * Modification history :
 * Name                         Date                            Desc
 * 
 * 
 * Copyright (c) 2014: SOCIAL ISLAMI BANK LIMITED
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.Common.Entity.Role;
using SIBLCommon.Common.Entity.AllLookup;
using System.Data;
using System.Runtime.Serialization;
using SIBLCommon.Common.Entity.Account;
using SIBLCommon.Common.Entity.Bank;
using System.Collections;

namespace SIBLCommon.Common.Entity.User
{
    [Serializable]
    public class CUser : ASIBLEntityBase
    {
        #region Protectd Member

        private string m_sUSERNAME;
        private string m_sSPASSWORD;
        private string m_sEmpId;
        private string m_sNEWPASSWORD;
        private string m_sEMAIL;
        private string m_sFULL_NAME;
        private string m_sDESIGNATION;
        private string m_sUserLocked;
        private string m_sCREATE_DATE;
       protected CRoleList m_oRoleList;
       private Hashtable mHashTable;
        protected string m_sSessionKey;
        protected DataTable m_dtRoleList;
        protected string m_sOperationType;
        protected bool m_sIsUserAuthenticate;
        protected string m_sRole;

        protected string m_sUSERACTIVE;

        protected string m_sPASSWORDCHANGE_DATE;
        protected string m_sUSERALTERFLAG;

        protected string m_sUSERIP;

        protected string m_sPasswordExpiredMessage;
        protected bool m_bIsPasswordWillExpired;
        protected string m_UISessionKey;

        protected CAccInfo m_oAccInfo;
        protected CAccList m_oAccList;

        protected string m_sACCNAME;
        protected string m_sACCNO;
        protected string m_sCUST_ID;
        protected string m_sMOBILE_NO;
        protected string m_sDEVICEID;
        protected string m_EmpId;
        protected string m_ReferenceNumber;
        protected string m_BranchRoutingNumber;

        protected CAllLookup m_oBranchInfo;
        protected CAllLookup m_oDivInfo;
        protected CBranch m_oBranch;
        protected string m_sBranchCode;
        protected string m_IAPPID;


    #endregion

        #region Constructor

        public CUser()
            : base()
        {
            Initialization();
        }

        #endregion Constructor

        #region Initialization

        protected void Initialization()
        {
            m_sUSERNAME = string.Empty;
            m_sSPASSWORD = string.Empty;
            m_sEmpId = string.Empty;
            m_sEMAIL = string.Empty;
            m_sFULL_NAME = string.Empty;
            m_sDESIGNATION = string.Empty;
            m_sUserLocked = string.Empty;
            m_sCREATE_DATE = string.Empty;
            m_sSessionKey = string.Empty;
            m_oRoleList = new CRoleList();
            m_dtRoleList = new DataTable();
            m_sOperationType = string.Empty;
            m_sNEWPASSWORD = string.Empty;
            m_sIsUserAuthenticate = false;
            m_sRole = string.Empty;


            m_sUSERACTIVE = string.Empty;

            m_sPASSWORDCHANGE_DATE = string.Empty;
            m_sUSERALTERFLAG = string.Empty;
            m_sUSERIP = string.Empty;
            m_UISessionKey = string.Empty;


            m_sACCNAME = string.Empty;
            m_sACCNO = string.Empty;
            m_sCUST_ID = string.Empty;
            m_sMOBILE_NO = string.Empty;
            m_sDEVICEID = string.Empty;
            m_EmpId = string.Empty;
            m_ReferenceNumber = string.Empty;
            m_oAccList = new CAccList();
            m_oAccInfo = new CAccInfo();

            m_oBranchInfo = new CAllLookup();
            m_oDivInfo = new CAllLookup();
            m_BranchRoutingNumber = string.Empty;
            m_sBranchCode = string.Empty;
            m_oBranch = new CBranch();
        }
        #endregion

        #region public Member

        

        public string EmpId
        {
            get { return m_sEmpId; }
            set { m_sEmpId = value; }
        }
         
        public string IAPPID
        {
            get { return m_IAPPID; }
            set { m_IAPPID = value; }
        }

        public string LoginKey
        {
            get { return m_UISessionKey; }
            set { m_UISessionKey = value; }

        }

        public string UserRole
        {
            get { return m_sRole; }
            set { m_sRole = value; }
        }
        public string USERACTIVE
        {
            get { return m_sUSERACTIVE; }
            set { m_sUSERACTIVE = value; }
        }
        public string PASSWORDCHANGE_DATE
        {
            get { return m_sPASSWORDCHANGE_DATE; }
            set { m_sPASSWORDCHANGE_DATE = value; }
        }
        public string UserAlterFlag
        {
            get { return m_sUSERALTERFLAG; }
            set { m_sUSERALTERFLAG = value; }
        }
      


        public DataTable RoleListdt 
        {
            get { return m_dtRoleList; }
            set { m_dtRoleList = value; }
        }

        public string SessionKey
        {
            get { return m_sSessionKey; }
            set { m_sSessionKey = value; }
        }
        public string USERNAME
        {
            get { return m_sUSERNAME; }
            set { m_sUSERNAME = value; }
        }

        public string NEWPASSWORD
        {
            get { return m_sNEWPASSWORD; }
            set { m_sNEWPASSWORD = value; }
        }
        public string PASSWORD
        {
            get { return m_sSPASSWORD; }
            set { m_sSPASSWORD = value; }
        }

        public string EMAIL
        {
            get { return m_sEMAIL; }
            set { m_sEMAIL = value; }
        }

        public string FULL_NAME
        {
            get { return m_sFULL_NAME; }
            set { m_sFULL_NAME = value; }
        }

        public string DESIGNATION
        {
            get { return m_sDESIGNATION; }
            set { m_sDESIGNATION = value; }
        }
        public string UserLocked
        {
            get { return m_sUserLocked; }
            set { m_sUserLocked = value; }
        }
        public string CREATE_DATE
        {
            get { return m_sCREATE_DATE; }
            set { m_sCREATE_DATE = value; }
        }

        public CRoleList RoleList
        {
            get { return m_oRoleList; }
            set { m_oRoleList = value; }
        }
        public string OperationType
        {
            get { return m_sOperationType; }
            set { m_sOperationType = value; }
        }
        public bool IsUserAuthenticate
        {
            get { return m_sIsUserAuthenticate; }
            set { m_sIsUserAuthenticate = value; }
        }

        public string USERIP
        {
            get { return m_sUSERIP; }
            set { m_sUSERIP = value; }
        }


        public string PasswordExpiredMessage
        {
            get { return m_sPasswordExpiredMessage; }
            set { m_sPasswordExpiredMessage = value; }
        }

        public bool IsPasswordWillExpired
        {
            get { return m_bIsPasswordWillExpired; }
            set { m_bIsPasswordWillExpired = value; }
        }


        public string DEVICEID
        {
            get { return m_sDEVICEID; }
            set { m_sDEVICEID = value; }

        }


        public string MOBILE_NO
        {
            get { return m_sMOBILE_NO; }
            set { m_sMOBILE_NO = value; }

        }

        public string CUST_ID
        {
            get { return m_sCUST_ID; }
            set { m_sCUST_ID = value; }

        }

        public string ACCNO
        {
            get { return m_sACCNO; }
            set { m_sACCNO = value; }

        }

        public string ACCNAME
        {
            get { return m_sACCNAME; }
            set { m_sACCNAME = value; }

        }

        public string EmployeeId
        {
            get { return m_EmpId; }
            set { m_EmpId = value; }

        }

        public string ReferenceNumber
        {
            get { return m_ReferenceNumber; }
            set { m_ReferenceNumber = value; }


        }

        public CAccList AccountList
        {
            get { return m_oAccList; }
            set { m_oAccList = value; }


        }
        public CAccInfo AccInfo
        {
            get { return m_oAccInfo; }
            set { m_oAccInfo = value; }
            
        }

        public CAllLookup BranchInfo
        {
            get { return m_oBranchInfo; }
            set { m_oBranchInfo = value; }

        }
        public CAllLookup DivInfo
        {
            get { return m_oDivInfo; }
            set { m_oDivInfo = value; }

        }

        public string BranchCode
        {
            get { return m_sBranchCode; }
            set { m_sBranchCode = value; }
        }
        public CBranch Branch
        {
            get { return m_oBranch; }
            set { m_oBranch = value; }

        }

        /// <summary>
        /// Hash Table for Role List
        /// </summary>
        public Hashtable RoleListh
        {
            get { return mHashTable; }
            set { mHashTable = value; }
        }

        public string BranchRoutingNumber
        {
            get { return m_BranchRoutingNumber; }
            set { m_BranchRoutingNumber = value; }
        }
        
        #endregion


    }
}
