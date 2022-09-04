/*
 * File name            : CCallBackResult.cs
 * Author               : Munirul Islam
 * Date                 : April 23, 2020
 * Version              : 1.0
 *
 * Description          : Entity Class for CCallBackResult
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
using System.Runtime.Serialization;

namespace SIBLCommon.SIBLCommon.Common.Entity.bKash
{

[Serializable]
    [DataContract]
    public class CCallBackResult : ASIBLEntityBase
    {
        #region Protectd Member
      
         CResponse m_oTransactionInfo;
         CRecipientInfo m_oWalletData;
        #endregion


        #region Constructor
        public CCallBackResult()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {

            m_oTransactionInfo = new CResponse();
            m_oWalletData = new CRecipientInfo();
            
           
        }
        #endregion Initialization
       
        #region public Member
[DataMember]
        public CResponse TransactionInfo
        {
            get { return m_oTransactionInfo; }
            set { m_oTransactionInfo = value; }
        }
        [DataMember]
        public CRecipientInfo WalletData
        {
            get { return m_oWalletData; }
            set { m_oWalletData = value; }
        }
        

        #endregion     
    }
}
