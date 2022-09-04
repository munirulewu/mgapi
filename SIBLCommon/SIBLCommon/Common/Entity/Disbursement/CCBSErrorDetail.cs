/*
 * File name            : CCBSErrorDetail
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
    public class CCBSErrorDetail : ASIBLEntityBase
    {
        
        #region Protectd Member
        protected string m_sErrorCode;
        protected string m_sErrorMessage;          
        #endregion

        #region Constructor
        public CCBSErrorDetail()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_sErrorCode = string.Empty;
            m_sErrorMessage = string.Empty;            
        }
    
        #endregion Initialization

        #region public Member
        public string ErrorCode
        {
            get { return m_sErrorCode; }
            set { m_sErrorCode = value; }
        }
        public string ErrorMessage
        {
            get { return m_sErrorMessage; }
            set { m_sErrorMessage = value; }
        }
       
        #endregion
    }
}
