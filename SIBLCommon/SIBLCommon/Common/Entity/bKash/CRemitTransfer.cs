/*
 * File name            : CRemitTransfer.cs
 * Author               : Munirul Islam
 * Date                 : April 23, 2020
 * Version              : 1.0
 *
 * Description          : Entity Class for CRemitTransfer
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
    public class CRemitTransfer : ASIBLEntityBase
    {
        CSystemInfo m_oSysteminfo;
        CTransactionInfo m_oTransactionInfo;
        CRemitData m_oRemitData;
        CSenderInfo m_oSenderInfo;
        CPaymentInstrument m_oPaymentInstrument;
        CmiscellaneousData m_omiscellaneousData;
        CCallBackResult m_oCallBackResult;
        CResponse m_oResponse;
        #region Constructor
        public CRemitTransfer()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
              m_oSysteminfo= new CSystemInfo ();
              m_oTransactionInfo= new CTransactionInfo ();
              m_oRemitData = new CRemitData();
              m_oSenderInfo = new CSenderInfo();
              m_oPaymentInstrument = new CPaymentInstrument();
              m_omiscellaneousData = new CmiscellaneousData();
              m_oCallBackResult = new CCallBackResult();
              m_oResponse = new CResponse();
           
        }
        #endregion Initialization
       
        #region public Member

        public CSystemInfo Systeminfo
        {
            get { return m_oSysteminfo; }
            set { m_oSysteminfo = value; }
        }
        public CTransactionInfo TransactionInfo
        {
            get { return m_oTransactionInfo; }
            set { m_oTransactionInfo = value; }
        }
        public CRemitData RemitData
        {
            get { return m_oRemitData; }
            set { m_oRemitData = value; }
        }
        public CSenderInfo SenderInfo
        {
            get { return m_oSenderInfo; }
            set { m_oSenderInfo = value; }
        }

        public CPaymentInstrument PaymentInstrument
        {
            get { return m_oPaymentInstrument; }
            set { m_oPaymentInstrument = value; }
        }
        public CmiscellaneousData MiscellaneousData 
        {
            get { return m_omiscellaneousData; }
            set { m_omiscellaneousData = value; }
        }

        public CCallBackResult CallBackResult 
        {
            get { return m_oCallBackResult; }
            set { m_oCallBackResult = value; }
        }

        public CResponse Response
        {
            get { return m_oResponse; }
            set { m_oResponse = value; }
        }
        #endregion     

    }
}
