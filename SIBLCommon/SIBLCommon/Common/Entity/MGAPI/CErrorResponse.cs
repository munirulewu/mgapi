/*
 * File name            : CResponse.cs
 * Author               : Munirul Islam
 * Date                 : April 23, 2020
 * Version              : 1.0
 *
 * Description          : Entity Class for CResponse
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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SIBLCommon.SIBLCommon.Common.Entity.MGAPI
{
    [Serializable]
    [DataContract]
    public class CErrorResponse
    {

        #region Protectd Member

        CMGErrorResponse m_oResponse;

        #endregion


        #region Constructor
        public CErrorResponse()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {

            m_oResponse = new CMGErrorResponse();


        }
        #endregion Initialization

        #region public Member


        [DataMember]
        [Display(Order = 1)]
        public CMGErrorResponse error
        {
            get { return m_oResponse; }
            set { m_oResponse = value; }
        }
        
        #endregion
    }
}
