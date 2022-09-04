/*
 * File name            : CCacheMapping.cs
 * Author               : MI
 * Date                 : March 19, 2009
 * Version              : 1.0
 *
 * Description          : Cache mapping information
 *
 * Modification history :
 * Name                         Date                            Desc
 *                                          
 * 
 * Copyright (c) 2014: Social Islami Bank Limited
 */

using System;
using System.Collections.Generic;
using System.Text;
using SIBLCommon.Common.Entity.Caching;
using SIBLCommon.Common.Entity.Bases;
using System.Collections;

namespace SIBLCommon.Common.Entity.Caching
{
    [Serializable]
    public class CCacheMapping
    {

        #region Protected Members
        protected bool m_bIsCachingAllowed;
        protected int m_iCacheIdleTimeInMin;
        protected List<CacheKey> m_liCacheKey;
        #endregion Protected Members

        #region Constructor
        public CCacheMapping()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {
            m_bIsCachingAllowed = false;
            m_iCacheIdleTimeInMin = 0;
            m_liCacheKey = new List<CacheKey>();
        }
        #endregion Initialization

        #region Public Properties

        public bool IsCachingAllowed
        {
            get { return m_bIsCachingAllowed; }
            set { m_bIsCachingAllowed = value; }
        }

        public int CacheIdleTimeInMin
        {
            get { return m_iCacheIdleTimeInMin; }
            set { m_iCacheIdleTimeInMin = value; }
        }

        public List<CacheKey> CacheKeyList
        {
            get { return m_liCacheKey; }
            set { m_liCacheKey = value; }
        }
        #endregion Public Properties
    }
}


namespace SIBLCommon.Common.Entity.Caching
{

    [Serializable]
    public class CacheKey 
    {
        #region Protected Members
        protected string m_sName;
        protected bool  m_bEnabled;       
        #endregion Protected Members

        #region Constructor
        public CacheKey()
        {
            m_sName = "";
            m_bEnabled = false;
        }
        #endregion Constructor
      

        #region Public Properties
        public string Name
        {
            get { return m_sName; }
            set { m_sName = value; }
        }

        public bool Enabled
        {
            get { return m_bEnabled; }
            set { m_bEnabled = value; }
        }
       
        #endregion Public Properties
    }
}

