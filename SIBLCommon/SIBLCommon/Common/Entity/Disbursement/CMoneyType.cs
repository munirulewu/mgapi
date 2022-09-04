
/*
 * File name            :  CMoneyType
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
    public class CMoneyType : ASIBLEntityBase
    {
        #region Protectd Member
        protected string m_sAmount;
        protected string m_sCurrency;

        #endregion


        #region Constructor
        public CMoneyType()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
             m_sAmount = string.Empty;
             m_sCurrency = string.Empty;
        }
    
        #endregion Initialization

        #region public Member
        public string Amount
        {
            get { return m_sAmount; }
            set { m_sAmount = value; }
        }

        public string Currency
        {
            get { return m_sCurrency; }
            set { m_sCurrency = value; }
        }
        #endregion
    }
}
