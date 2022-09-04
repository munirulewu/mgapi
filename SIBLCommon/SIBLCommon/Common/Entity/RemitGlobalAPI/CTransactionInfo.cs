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
using System.Runtime.Serialization;
using System.Text;

namespace SIBLCommon.SIBLCommon.Common.Entity.RemitGlobalAPI
{
    [Serializable]
    [DataContract]
    public enum CurrentTransactionType { CASH=1, OWNBANK=2, BEFTN=3, BKASH=4 };

    public class CTransactionInfo : ASIBLEntityBase
    {
        string ms_amount;
        string ms_IncentiveAmount;
        string ms_AccountNo;
        string ms_country;
        string ms_currency;
        string ms_refTxnId;
        string ms_txnIdSIBL;
        string sBankName;
        string sBranchName;
        string sDistrict;

        string sBankCode;
        string sBranchCode;
        string sDistrictCode;

        string sRoutingNumber;
        string sMessageForReceiver;
        string ms_EntryDate;
        
        private CurrentTransactionType _TransactionType;
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
            ms_IncentiveAmount = string.Empty;
            ms_txnIdSIBL = string.Empty;
            
            ms_country = string.Empty;
            ms_currency = string.Empty;
            ms_refTxnId = string.Empty;
        


            sBankName = string.Empty;
            sBranchName = string.Empty;
            sDistrict = string.Empty;
            sRoutingNumber = string.Empty;
            sMessageForReceiver = string.Empty;
            ms_AccountNo = string.Empty;
            //sTransactionType = new sTransactionType  string.Empty;
            ms_EntryDate = string.Empty;

            sBankCode = string.Empty;
            sBranchCode = string.Empty;
            sDistrictCode = string.Empty;

        }
        #endregion Initialization
       
        #region public Member

        public string Amount
        {
            get { return ms_amount; }
            set { ms_amount = value; }
        }

        public string IncentiveAmount
        {
            get { return ms_IncentiveAmount; }
            set { ms_IncentiveAmount = value; }
        }


        public string AccountNo
        {
            get { return ms_AccountNo; }
            set { ms_AccountNo = value; }
        }
        public string TxnIdSIBL
        {
            get { return ms_txnIdSIBL; }
            set { ms_txnIdSIBL = value; }
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
        public string BankName
        {
            get { return sBankName; }
            set { sBankName = value; }
        }

        public string BankCode
        {
            get { return sBankCode; }
            set { sBankCode = value; }
        }

        public string BranchName
        {
            get { return sBranchName; }
            set { sBranchName = value; }
        }

        public string BranchCode
        {
            get { return sBranchCode; }
            set { sBranchCode = value; }
        }
        public string District
        {
            get { return sDistrict; }
            set { sDistrict = value; }
        }

        public string DistrictCode
        {
            get { return sDistrictCode; }
            set { sDistrictCode = value; }
        }

        public string RoutingNumber
        {
            get { return sRoutingNumber; }
            set { sRoutingNumber = value; }
        }

        public CurrentTransactionType TransactionType 
        {

            get { return _TransactionType; }
            set { _TransactionType = value; }
        }


        public string EntryDate
        {
            get { return ms_EntryDate; }
            set { ms_EntryDate = value; }
        }
        
        #endregion     
    }
}
