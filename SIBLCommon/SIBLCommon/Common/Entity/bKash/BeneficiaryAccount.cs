/*
 * File name            : BeneficiaryAccount.cs
 * Author               : Munirul Islam
 * Date                 : April 23, 2020
 * Version              : 1.0
 *
 * Description          : Entity Class for BeneficiaryAccount
 *
 * Modification history :
 * Name                         Date                            Desc
 * 
 * 
 * Copyright (c) 2020: SOCIAL ISLAMI BANK LIMITED
 */

using SIBLCommon.Common.Entity.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIBLCommon.SIBLCommon.Common.Entity.bKash
{
     [Serializable]
    public class BeneficiaryAccount : ASIBLEntityBase
    {
        CSystemInfo m_oSystemInfo;
        CRecipientInfo m_oRecipientInfo;
        CResponse m_oResponse;


          #region Constructor
        public BeneficiaryAccount()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {

            m_oSystemInfo = new CSystemInfo();
            m_oRecipientInfo = new CRecipientInfo();
            m_oResponse = new CResponse();
           
        }
        #endregion Initialization
       
        #region public Member

        public CSystemInfo SystemInfo
        {
            get { return m_oSystemInfo; }
            set { m_oSystemInfo = value; }
        }
        public CRecipientInfo RecipientInfo
        {
            get { return m_oRecipientInfo; }
            set { m_oRecipientInfo = value; }
        }
        public CResponse Response
        {
            get { return m_oResponse; }
            set { m_oResponse = value; }
        }
        
        #endregion     

    }
}
