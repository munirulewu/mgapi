using SIBLCommon.Common.Entity.AllLookup;
using SIBLCommon.Common.Entity.Bases;
/*
 * File name            : CRemiAccount.cs
 * Author               : Munirul Islam
 * Date                 : January 21.2020
 * Version              : 1.0
 *
 * Description          : Entity Class for CRemiAccount
 *
 * Modification history :
 * Name                         Date                            Desc
 * 
 * 
 * Copyright (c) 2020: SOCIAL ISLAMI BANK LIMITED
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIBLCommon.SIBLCommon.Common.Entity.SIBLRemitPay
{
    [Serializable]
    public class CRemiAccount : ASIBLEntityBase
    {
        #region Protectd Member


        
        protected string ms_FromAccNum;
        protected string ms_ToAccNum;
        protected string ms_IncentiveAccountNum;
        protected string ms_Charge;
        protected CAllLookup ms_Company;

        #endregion

        #region Constructor

        public CRemiAccount()
            : base()
        {
            Initialization();
        }

        #endregion Constructor

        #region Initialization

        void Initialization()
        {
            ms_FromAccNum = string.Empty;
            ms_ToAccNum = string.Empty;
            ms_Charge = string.Empty;
            ms_Company = new CAllLookup();
            ms_IncentiveAccountNum = string.Empty;
        }
        #endregion

        #region public Member


        public string FromAccNum
        {
            get { return ms_FromAccNum; }
            set { ms_FromAccNum = value; }
        }
        public string ToAccNum
        {
            get { return ms_ToAccNum; }
            set { ms_ToAccNum = value; }
        }
        public string IncentiveAccountNum
        {
            get { return ms_IncentiveAccountNum; }
            set { ms_IncentiveAccountNum = value; }
        }

        public CAllLookup Company
        {
            get { return ms_Company; }
            set { ms_Company = value; }

        }

        public string Charge
        {
            get { return ms_Charge; }
            set { ms_Charge = value; }
        }
       
    

        #endregion
    }
}
