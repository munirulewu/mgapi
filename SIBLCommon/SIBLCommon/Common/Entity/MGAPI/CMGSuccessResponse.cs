using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SIBLCommon.SIBLCommon.Common.Entity.MGAPI
{
    public class CMGSuccessResponse
    {
        #region Protectd Member

        protected string m_sresponseCode;
        protected string m_sresponseMessage;
        

        #endregion


        #region Constructor
        public CMGSuccessResponse()
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
           

        }
        #endregion Initialization

        #region public Member


        [DataMember]
        [Display(Order = 1)]
        public string responseCode
        {
            get { return m_sresponseCode; }
            set { m_sresponseCode = value; }
        }
        [DataMember]
        [Display(Order = 2)]
        public string message
        {
            get { return m_sresponseMessage; }
            set { m_sresponseMessage = value; }
        }


        

       
        #endregion
    }
}
