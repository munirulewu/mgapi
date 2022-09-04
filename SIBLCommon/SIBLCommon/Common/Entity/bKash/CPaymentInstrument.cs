/*
 * File name            : CPaymentInstrument.cs
 * Author               : Munirul Islam
 * Date                 : April 23, 2020
 * Version              : 1.0
 *
 * Description          : Entity Class for CPaymentInstrument
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
    public class CPaymentInstrument : ASIBLEntityBase
    {
        string ms_type;
        string ms_entity;
        string ms_NameOfMTO;
        string ms_number;
        string ms_city;
        string ms_zipCode;

        
        #region Constructor
        public CPaymentInstrument()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {

            ms_type = String.Empty;
            ms_entity = String.Empty;
            ms_NameOfMTO = string.Empty;
            ms_number = string.Empty;

            ms_city = string.Empty;
            ms_zipCode = string.Empty;

           
        }
        #endregion Initialization
       
        #region public Member

        public string city
        {
            get { return ms_city; }
            set { ms_city = value; }
        }
        public string zipCode
        {
            get { return ms_zipCode; }
            set { ms_zipCode = value; }
        }

        public string type
        {
            get { return ms_type; }
            set { ms_type = value; }
        }
        public string entity
        {
            get { return ms_entity; }
            set { ms_entity = value; }
        }
        public string NameOfMTO
        {
            get { return ms_NameOfMTO; }
            set { ms_NameOfMTO = value; }
        }
        public string number
        {
            get { return ms_number; }
            set { ms_number = value; }
        }
        
        #endregion     
    }
}
