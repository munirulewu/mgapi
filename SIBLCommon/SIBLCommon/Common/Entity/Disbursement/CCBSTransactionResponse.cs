/*
 * File name            : CCBSTransactionResponse
 * Author               : Md. Aminul Islam
 * Date                 : 06-07-2015
 * Version              : 1.0
 *
 * Description          : 
 *
 * Modification history :
 * Name                         Date                            Desc
 * 
 * 
 * Copyright (c) 2015: SOCIAL ISLAMI BANK LIMITED
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLXoomCommon.SIBLXoom.Common.Entity.Disbursement
{
    [Serializable]
    public class CCBSTransactionResponse : ASIBLEntityBase
    {
        #region Protectd Member
        protected string m_sDescription;
        //protected string m_sErrorDetail;
        protected string m_sRequestReference;
        protected string m_sResponseReference;
        protected string m_sTransactionStatus;        
        #endregion

        #region Constructor
        public CCBSTransactionResponse()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_sDescription = string.Empty;
            //m_sErrorDetail = string.Empty;
            m_sRequestReference = string.Empty;
            m_sResponseReference = string.Empty;
            m_sTransactionStatus = string.Empty;
        }
    
        #endregion Initialization

        #region public Member
        public string Description
        {
            get { return m_sDescription; }
            set { m_sDescription = value; }
        }
        public string RequestReference
        {
            get { return m_sRequestReference; }
            set { m_sRequestReference = value; }
        }
        public string ResponseReference
        {
            get { return m_sResponseReference; }
            set { m_sResponseReference = value; }
        }

        public string TransactionStatus
        {
            get { return m_sTransactionStatus; }
            set { m_sTransactionStatus = value; }
        }
        #endregion
    }
}
