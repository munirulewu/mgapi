/*
 * File name            : CmiscellaneousData.cs
 * Author               : Munirul Islam
 * Date                 : April 23, 2020
 * Version              : 1.0
 *
 * Description          : Entity Class for CmiscellaneousData
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
    public class CmiscellaneousData : ASIBLEntityBase
    {
        string ms_expiry;
        string ms_message;
        string ms_payonDate;

        #region Constructor
        public CmiscellaneousData()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {

            ms_expiry = String.Empty;
            ms_message = String.Empty;
            ms_payonDate = string.Empty;
            
           
        }
        #endregion Initialization
       
        #region public Member

        public string expiry
        {
            get { return ms_expiry; }
            set { ms_expiry = value; }
        }
        public string message
        {
            get { return ms_message; }
            set { ms_message = value; }
        }
        public string payonDate
        {
            get { return ms_payonDate; }
            set { ms_payonDate = value; }
        }
        
        #endregion     
    }
}
