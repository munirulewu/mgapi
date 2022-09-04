/*
 * File name            : CSystemInfo.cs
 * Author               : Munirul Islam
 * Date                 : April 23, 2020
 * Version              : 1.0
 *
 * Description          : Entity Class for CSystemInfo
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
    public class CSystemInfo:ASIBLEntityBase
    {
         #region Protectd Member
      
        protected string m_sUserName;
        protected string m_sPassword;
        protected string m_sTimeStamp;
        protected string m_sChecksum;
       
        #endregion


        #region Constructor
        public CSystemInfo()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {

            m_sUserName = String.Empty;
            m_sPassword = String.Empty;
            m_sChecksum = string.Empty;
            m_sTimeStamp = string.Empty;
           
        }
        #endregion Initialization
       
        #region public Member

        public string UserName
        {
            get { return m_sUserName; }
            set { m_sUserName = value; }
        }
        public string Password
        {
            get { return m_sPassword; }
            set { m_sPassword = value; }
        }
        public string Checksum
        {
            get { return m_sChecksum; }
            set { m_sChecksum = value; }
        }
        public string TimeStamp
        {
            get { return m_sTimeStamp; }
            set { m_sTimeStamp = value; }
        }
        
        #endregion     
    }
}
