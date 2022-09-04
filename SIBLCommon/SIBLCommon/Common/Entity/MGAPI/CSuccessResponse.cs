using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SIBLCommon.SIBLCommon.Common.Entity.MGAPI
{
    public class CSuccessResponse
    {
        
        #region Protectd Member

        CMGSuccessResponse m_oResponse;
        protected string m_partnerTransactionId;
        #endregion


        #region Constructor
        public CSuccessResponse()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {

            m_oResponse = new CMGSuccessResponse();
            m_partnerTransactionId = string.Empty;

        }
        #endregion Initialization

        #region public Member


        [DataMember]
        [Display(Order = 1)]
        public CMGSuccessResponse response
        {
            get { return m_oResponse; }
            set { m_oResponse = value; }
        }
        [DataMember]
        [Display(Order = 2)]
        public string partnerTransactionId
        {
            get { return m_partnerTransactionId; }
            set { m_partnerTransactionId = value; }
        }
        #endregion
    }
}
