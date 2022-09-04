/*
 * File name            : CResult.cs
 * Author               : Munirul Islam
 * Date                 : November 10, 2014
 * Version              : 1.0
 *
 * Description          : Result Class Entity 
 *
 * Modification history :
 * Name                         Date                            Desc
 * 
 * 
 * Copyright (c) 2014: SOCIAL ISLAMI BANK LIMITED
 */
using System;
using System.Collections.Generic;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLCommon.Common.Entity.Result
{

    [Serializable]
    public class CResult : ASIBLEntityBase
    {
        #region Protected Members
        
        protected object m_oReturn;
        protected bool m_bResult;
        protected string m_sMessage;
        protected Exception m_oException;
        protected string m_sStatus;
        protected Int32 m_itotalRecord;
        protected int m_iPageSize;
        protected string m_MoreRecord;
        protected string m_ResponseCode;
        #endregion Protected Members

        #region Constructor
        public CResult()
        {
            Initialization(null,false,string.Empty,Exception);
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization(object oReturn,bool bResult,string sMessage,Exception oException)
        {
            m_oReturn = oReturn;
            m_bResult = bResult;
            m_sMessage = sMessage;
            m_oException = oException;
            m_sStatus=string.Empty;
            m_itotalRecord = 0;
            m_iPageSize = 0;
            m_MoreRecord = string.Empty;
            m_ResponseCode = string.Empty;
        }
        #endregion Initialization

        #region Public Properties

        public Int32 TotalRecord
        {
            get { return m_itotalRecord; }
            set { m_itotalRecord = value; }
        }

        public int PageSize
        {
            get { return m_iPageSize; }
            set { m_iPageSize = value; }
        }

        public object Return
        {
            get { return m_oReturn; }
            set { m_oReturn = value; }
        }

        public bool Result
        {
            get { return m_bResult; }
            set { m_bResult = value; }
        }
        public string ResponseCode
        {
            get { return m_ResponseCode; }
            set { m_ResponseCode = value; }
        }
        public string MoreRecord
        {
            get { return m_MoreRecord; }
            set { m_MoreRecord = value; }
        }
        public string Message
        {
            get { return m_sMessage; }
            set { m_sMessage = value; }
        }

        public Exception Exception
        {
            get { return m_oException; }
            set { m_oException = value; }
        }

        public string Status
        {
            get { return m_sStatus; }
            set { m_sStatus = value;}
        }

        #endregion Public Properties
    }
}
