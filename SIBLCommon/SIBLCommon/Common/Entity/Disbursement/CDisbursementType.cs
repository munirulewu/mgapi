/*
 * File name            :  CDisbursementType
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
    public class CDisbursementType : ASIBLEntityBase
    {
        #region Protectd Member

        protected CMoneyType m_oMoneyType;
        protected CPersonType m_oPersonType;
        protected string m_sMessage;

        #endregion


        #region Constructor
        public CDisbursementType()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_oMoneyType = new CMoneyType();
            m_oPersonType = new CPersonType();
            m_sMessage = string.Empty;
        }
    
        #endregion Initialization

        #region public Member


        public CMoneyType MoneyType
        {
            get { return m_oMoneyType; }
            set { m_oMoneyType = value; }
        }

        public CPersonType PersonType
        {
            get { return m_oPersonType; }
            set { m_oPersonType = value; }
        }

        public string Message
        {
            get { return m_sMessage; }
            set { m_sMessage = value; }
        }

        #endregion
    }
}
