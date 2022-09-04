/*
 * File name            :  CUpdateResponse
 * Author               :  Md. Aminul Islam
 * Date                 :  27.05.2015
 *
 * Description          :  
 *
 * Modification history :
 * Name                         Date                            Desc
 *           
 * 
 * Copyright (c)  2015: Social Islami Bank Limited
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLCommon.Common.Entity.Disbursement
{
    [Serializable]
    public class CUpdateResponse : ASIBLEntityBase
    {
 
        #region Protectd Member
        protected string m_sXoomTrackingNumber;
        protected string m_sPartnerReference;
        protected string   m_oUpdateResponseStatus;
        protected string  m_oUpdateResponseReason;

        protected string m_oUpdateStatus;
        protected string m_oUpdateReason;
        protected string m_sCBSStatus;

        #endregion

        #region Constructor
        public CUpdateResponse()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization

        protected void Initialization()
        {
            m_sXoomTrackingNumber = string.Empty;
            m_sPartnerReference = string.Empty;
            m_oUpdateResponseStatus = string.Empty;
            m_oUpdateResponseReason = string.Empty;
            m_oUpdateStatus = string.Empty;
            m_oUpdateReason = string.Empty;
            m_sCBSStatus = string.Empty;
        }
    
        #endregion Initialization

        #region public Member

        public string XoomTrackingNumber
        {
            get { return m_sXoomTrackingNumber; }
            set { m_sXoomTrackingNumber = value; }
        }

        public string PartnerReference
        {
            get { return m_sPartnerReference; }
            set { m_sPartnerReference = value; }
        }

        public string UpdateResponseReason
        {
            get { return m_oUpdateResponseReason; }
            set { m_oUpdateResponseReason = value; }
        }

        public string UpdateResponseStatus
        {
            get { return m_oUpdateResponseStatus; }
            set { m_oUpdateResponseStatus = value; }
        }

        public string UpdateStatus
        {
            get { return m_oUpdateStatus; }
            set { m_oUpdateStatus = value; }
        }

        public string UpdateReason
        {
            get { return m_oUpdateReason; }
            set { m_oUpdateReason = value; }
        }

        public string CBSStatus
        {
            get { return m_sCBSStatus; }
            set { m_sCBSStatus = value; }
        }


        #endregion
    }
}
