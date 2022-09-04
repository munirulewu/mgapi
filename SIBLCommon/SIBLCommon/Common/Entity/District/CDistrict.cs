/*
 * File name            : CDistrict.cs
 * Author               : Munirul Islam
 * Date                 : April 06, 2014
 * Version              : 1.0
 *
 * Description          : Entity Class for CDistrict
 *
 * Modification history :
 * Name                         Date                            Desc
 * 
 * 
 * Copyright (c) 2014: SOCIAL ISLAMI BANK LIMITED
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.Common.Entity.Bank;
using System.Runtime.Serialization;
namespace  SIBLCommon.Common.Entity.District
{
    [Serializable]
    [DataContract]
    public class CDistrict : ASIBLEntityBase
    {
        #region Protectd Member
      
        protected string m_sDistrict_NAME;
        protected string m_sdistrict_code;
        protected string m_sRecordNumber;
        protected CBank m_oBank;
        #endregion


        #region Constructor
        public CDistrict()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_sDistrict_NAME = string.Empty;
            m_sdistrict_code = string.Empty;
            m_sRecordNumber = string.Empty;
            m_oBank = new CBank();
        }
        #endregion Initialization
       
        #region public Member

        public CBank Bank
        {
            get { return m_oBank; }
            set { m_oBank = value; }
        }
        
        [DataMember]
        public string DistrictName
        {
            get { return m_sDistrict_NAME; }
            set { m_sDistrict_NAME = value; }
        }
        [DataMember]
        public string DistrictCode
        {
            get { return m_sdistrict_code; }
            set { m_sdistrict_code = value; }
        }
        [DataMember]
        public string RecordNumber
        {
            get { return m_sRecordNumber; }
            set { m_sRecordNumber = value; }
        }
        

        #endregion     
    }
}
