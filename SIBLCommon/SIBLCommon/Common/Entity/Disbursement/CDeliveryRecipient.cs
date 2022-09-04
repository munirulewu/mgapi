/*
 * File name            :  CDeliveryRecipient
 * Author               :  Md. Aminul Islam
 * Date                 :  27.05.2015
 *
 * Description          :  
 *
 * Modification history :
 * Name                         Date                            Desc
 *           
 * 
 * Copyright (c)  2015: Social Islami Bank Limited
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLCommon.Common.Entity.Disbursement
{
    [Serializable]
    public class CDeliveryRecipient : ASIBLEntityBase
    {
        
        #region Protectd Member

        protected string m_sInstruction;
        protected string m_sSecretWord;
        protected string m_sMessageToRecipient;
        protected string m_sAdditionalInformation;
        protected CIdentityType m_oIdentityType;

        #endregion


        #region Constructor
        public CDeliveryRecipient()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
             m_sInstruction = string.Empty;
             m_sSecretWord = string.Empty;
             m_sMessageToRecipient = string.Empty;
             m_sAdditionalInformation = string.Empty;
             m_oIdentityType = new CIdentityType();
        }
    
        #endregion Initialization

        #region public Member


        public string Instruction
        {
            get { return m_sInstruction; }
            set { m_sInstruction = value; }
        }

        public string SecretWord
        {
            get { return m_sSecretWord; }
            set { m_sSecretWord = value; }
        }

        public string MessageToRecipient
        {
            get { return m_sMessageToRecipient; }
            set { m_sMessageToRecipient = value; }
        }

        public string AdditionalInformation
        {
            get { return m_sAdditionalInformation; }
            set { m_sAdditionalInformation = value; }
        }

        public CIdentityType IdentityType
        {
            get { return m_oIdentityType; }
            set { m_oIdentityType = value; }
        }

        #endregion

    }
}
