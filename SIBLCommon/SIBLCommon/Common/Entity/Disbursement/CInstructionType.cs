
/*
 * File name            :  CInstructionType
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
    public class CInstructionType : ASIBLEntityBase
    {
        #region Protectd Member

        protected string m_sDeposit;
        protected string m_sCancel;
        protected string m_sDelivery;

        #endregion


        #region Constructor
        public CInstructionType()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_sDeposit = string.Empty;
            m_sCancel = string.Empty;
            m_sDelivery = string.Empty;
        }
    
        #endregion Initialization

        #region public Member


        public string Deposit
        {
            get { return m_sDeposit; }
            set { m_sDeposit = value; }
        }

        public string Cancel
        {
            get { return m_sCancel; }
            set { m_sCancel = value; }
        }

        public string Delivery
        {
            get { return m_sDelivery; }
            set { m_sDelivery = value; }
        }


        #endregion
    }
}
