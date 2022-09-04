/*
 * File name            : CTransactionInfo.cs
 * Author               : Munirul Islam
 * Date                 : April 23, 2020
 * Version              : 1.0
 *
 * Description          : Entity Class for CTransactionInfo
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
    public class CTransactionInfo : ASIBLEntityBase
    {
        string ms_amount;
        string ms_conversationID;
        string ms_corridor;
        string ms_country;
        string ms_currency;
        string ms_refTxnId;
        string ms_txnId;

         #region Constructor
        public CTransactionInfo()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {

            ms_amount = string.Empty;
            ms_conversationID = string.Empty;
            ms_corridor = string.Empty;
            ms_country = string.Empty;
            ms_currency = string.Empty;
            ms_refTxnId = string.Empty;
            ms_txnId = string.Empty;

        }
        #endregion Initialization
       
        #region public Member

        public string Amount
        {
            get { return ms_amount; }
            set { ms_amount = value; }
        }
        public string ConversationID
        {
            get { return ms_conversationID; }
            set { ms_conversationID = value; }
        }
        public string Corridor
        {
            get { return ms_corridor; }
            set { ms_corridor = value; }
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

        public string RefTxnId
        {
            get { return ms_refTxnId; }
            set { ms_refTxnId = value; }
        }
        public string TxnId
        {
            get { return ms_txnId; }
            set { ms_txnId = value; }
        }
        
        #endregion     
    }
}
