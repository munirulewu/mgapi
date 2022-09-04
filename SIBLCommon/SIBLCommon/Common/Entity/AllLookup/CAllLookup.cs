/*
 * File name            : CAllLookup.cs
 * Author               : Munirul Islam
 * Date                 : November 10, 2014
 * Version              : 1.0
 *
 * Description          : Entity Class for CAllLookup
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
namespace SIBLCommon.Common.Entity.AllLookup
{
    [Serializable]
    public class CAllLookup : ASIBLEntityBase
    {
        #region Protectd Member
      
        protected string m_sTYPE_NAME;
        protected int m_iPARENT_ID;
        protected string m_sTYPE_KEY;
        protected DateTime m_dCREATE_DATE;
        #endregion


        #region Constructor
        public CAllLookup()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {

            m_sTYPE_NAME = String.Empty;
            m_iPARENT_ID =0;
            m_sTYPE_KEY = String.Empty;
            m_dCREATE_DATE = new DateTime();
        }
        #endregion Initialization
       
        #region public Member

        public string TYPE_NAME
        {
            get { return m_sTYPE_NAME; }
            set { m_sTYPE_NAME = value; }
        }
        public int PARENT_ID
        {
            get { return m_iPARENT_ID; }
            set { m_iPARENT_ID = value; }
        }

        public string TYPE_KEY
        {
            get { return m_sTYPE_KEY; }
            set { m_sTYPE_KEY = value; }
        }

        public DateTime CREATE_DATE
        {
            get { return m_dCREATE_DATE; }
            set { m_dCREATE_DATE = value; }
        }

        #endregion     
    }
}
