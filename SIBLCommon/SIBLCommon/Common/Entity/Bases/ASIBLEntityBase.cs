/*
 * File name            : ASIBLXoomEntityBase.cs
 * Author               : Munirul Islam
 * Date                 : November 10, 2014
 * Version              : 1.0
 *
 * Description          : Base class for all Entity objects
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

namespace SIBLCommon.Common.Entity.Bases
{
    [Serializable]
    public class ASIBLEntityBase : ISIBLEntityBase
    {
        protected string m_sCN;
        protected string m_sStatus;
        protected string m_OperationType;
        protected string m_IAPPLICATIONID;
        protected string m_sMessage;
        protected string m_sFromDate;
        protected string m_sToDate;
        public ASIBLEntityBase()
            : base()
        {
            Initialization();
        }
        public ASIBLEntityBase(string sCN)
            : base()
        {
            Initialization(sCN);
        }

        protected void Initialization()
        {
            m_sCN = string.Empty;
            m_sStatus = string.Empty;
            m_OperationType = string.Empty;
            m_sMessage = string.Empty;
            m_sFromDate = string.Empty;
            m_sToDate = string.Empty;

        }

        protected void Initialization(string sCN)
        {
            m_sCN = sCN;
            m_sStatus = string.Empty;
        }

        public string CN
        {
            get { return m_sCN; }
            set { m_sCN = value; }
        }
        public string Status
        {
            get { return m_sStatus; }
            set { m_sStatus = value; }
        }

        public string OperationType
        {
            get { return m_OperationType; }
            set { m_OperationType = value; }
        }

        public string IAPPID
        {
            get { return m_IAPPLICATIONID; }
            set { m_IAPPLICATIONID = value; }
        }
        public string Message
        {
            get { return m_sMessage; }
            set { m_sMessage = value; }
        }

        public string FromDate
        {
            get { return m_sFromDate; }
            set { m_sFromDate = value; }
        }

        public string ToDate
        {
            get { return m_sToDate; }
            set { m_sToDate = value; }
        }



    }
}
