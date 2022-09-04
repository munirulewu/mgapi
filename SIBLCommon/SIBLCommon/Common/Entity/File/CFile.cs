/*
 * File name            : CFile.cs
 * Author               : Munirul Islam
 * Date                 : August 03, 2014
 * Version              : 1.0
 *
 * Description          : Entity Class for CFile
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

namespace SIBLCommon.Common.Entity.File
{
    [Serializable]
    public class CFile : ASIBLEntityBase
    {
         #region Protectd Member
      
        protected string m_sFileName;
        protected string m_sCompanyId;
        protected DateTime m_dCREATE_DATE;
        
        #endregion


        #region Constructor
        public CFile()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {

            m_sFileName = String.Empty;
            m_sCompanyId = String.Empty;
            m_dCREATE_DATE = new DateTime();
          
        }
        #endregion Initialization
       
        #region public Member

        public string CompanyId
        {
            get { return m_sCompanyId; }
            set { m_sCompanyId = value; }
        }
        public string FileName
        {
            get { return m_sFileName; }
            set { m_sFileName = value; }
        }
        public DateTime CREATE_DATE
        {
            get { return m_dCREATE_DATE; }
            set { m_dCREATE_DATE = value; }
        }
        
        #endregion     
    }
}
