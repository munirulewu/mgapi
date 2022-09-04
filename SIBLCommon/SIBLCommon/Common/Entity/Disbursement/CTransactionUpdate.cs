
/*
 * File name            :  CTransactionUpdate
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
    public class CTransactionUpdate : ASIBLEntityBase
    {
        
        #region Protectd Member
        protected string m_sXoomTrackingNumber;
        protected string m_sPartnerReference;
        //protected CUpdateStatus m_oUpdateStatus;
        //protected CUpdateReason m_oUpdateReason;
        protected string m_sPartnerCode;
        protected string m_sPartnerDetail;
        protected string m_sDisbursementDetails;      
        #endregion

        #region Constructor
        public CTransactionUpdate()
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
            //protected CUpdateStatus m_oUpdateStatus;
            //protected CUpdateReason m_oUpdateReason;
            m_sPartnerCode = string.Empty;
            m_sPartnerDetail = string.Empty;
            m_sDisbursementDetails = string.Empty;
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

        public string PartnerCode
        {
            get { return m_sPartnerCode; }
            set { m_sPartnerCode = value; }
        }

        public string PartnerDetail
        {
            get { return m_sPartnerDetail; }
            set { m_sPartnerDetail = value; }
        }

        public string DisbursementDetails
        {
            get { return m_sDisbursementDetails; }
            set { m_sDisbursementDetails = value; }
        }

        #endregion
    }
}
