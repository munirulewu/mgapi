/*
 * File name            : CRemitData.cs
 * Author               : Munirul Islam
 * Date                 : April 23, 2020
 * Version              : 1.0
 *
 * Description          : Entity Class for CRemitData
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
    public class CRemitData : ASIBLEntityBase
    {
        string ms_amount;
        string ms_country;
        string ms_currency;
        string ms_forex;
        string ms_msisdn;

        
        #region Constructor
        public CRemitData()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {

            ms_amount = String.Empty;
            ms_country = String.Empty;
            ms_currency = string.Empty;
            ms_forex = string.Empty;
            ms_msisdn = string.Empty;
           
        }
        #endregion Initialization
       
        #region public Member

        public string Amount
        {
            get { return ms_amount; }
            set { ms_amount = value; }
        }
        public string Country
        {
            get { return ms_country; }
            set { ms_country = value; }
        }
        public string Currency
        {
            get { return ms_currency; }
            set { ms_currency = value; }
        }
        public string Forex
        {
            get { return ms_forex; }
            set { ms_forex = value; }
        }

        public string msisdn
        {
            get { return ms_msisdn; }
            set { ms_msisdn = value; }
        }
        
        #endregion     
    }
}
