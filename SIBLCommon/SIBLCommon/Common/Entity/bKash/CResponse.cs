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
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SIBLCommon.SIBLCommon.Common.Entity.bKash
{
     [Serializable]
     [DataContract]  
    public class CResponse :ASIBLEntityBase
    {
         
         #region Protectd Member

         protected string m_sresponseCode;
         protected string m_sresponseMessage;
         protected string m_ConversationID;
         protected string ms_ResponseDescription;
         protected string ms_TxnId;
         protected string ms_approvalCode;
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

            m_sresponseCode = String.Empty;
            m_sresponseMessage = String.Empty;
            m_ConversationID = string.Empty;
            ms_ResponseDescription = string.Empty;
            ms_TxnId = string.Empty;
            ms_approvalCode = string.Empty;

        }
        #endregion Initialization
       
        #region public Member
        [DataMember]
        public string ResponseDescription
        {
            get { return ms_ResponseDescription; }
            set { ms_ResponseDescription = value; }
        }
         [DataMember]
        public string TxnId
        {
            get { return ms_TxnId; }
            set { ms_TxnId = value; }
        }
         [DataMember]
        public string ApprovalCode
        {
            get { return ms_approvalCode; }
            set { ms_approvalCode = value; }
        }
         [DataMember]
        public string ResponseCode
        {
            get { return m_sresponseCode; }
            set { m_sresponseCode = value; }
        }
         [DataMember]
        public string ResponseMessage
        {
            get { return m_sresponseMessage; }
            set { m_sresponseMessage = value; }
        }
         [DataMember]
        public string ConversationID
        {
            get { return m_ConversationID; }
            set { m_ConversationID = value; }
        }
        
        #endregion     
    }
}
