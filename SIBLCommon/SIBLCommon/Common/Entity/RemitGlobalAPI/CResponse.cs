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

namespace SIBLCommon.SIBLCommon.Common.Entity.RemitGlobalAPI
{
    [Serializable]
    [DataContract]
    public class CResponse
    {

        #region Protectd Member

        protected string m_sresponseCode;
        protected string m_sresponseMessage;
        protected string m_txnIdSIBL;
        protected string ms_TxnId;

        #endregion


        #region Constructor
        public CResponse()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {

            m_sresponseCode = string.Empty;
            m_sresponseMessage = string.Empty;
            m_txnIdSIBL = string.Empty;
            ms_TxnId = string.Empty;


        }
        #endregion Initialization

        #region public Member


        [DataMember]
        [Display(Order = 1)]
        public string ResponseCode
        {
            get { return m_sresponseCode; }
            set { m_sresponseCode = value; }
        }
        [DataMember]
        [Display(Order = 2)]
        public string ResponseMessage
        {
            get { return m_sresponseMessage; }
            set { m_sresponseMessage = value; }
        }


        [DataMember]
        [Display(Order = 3)]
        public string TxnIdSIBL
        {
            get { return m_txnIdSIBL; }
            set { m_txnIdSIBL = value; }
        }

        [DataMember]
        [Display(Order = 4)]
        public string RefTxnId
        {
            get { return ms_TxnId; }
            set { ms_TxnId = value; }
        }
        #endregion
    }
}
