/*
 * File name: CCacheManager.cs 
 * Author:  Munirul Islam
 * Date: June 07, 2009
 * 
 * Description: 
 * 
 * Modification history:
 * Name				Date				Desc
 * 		
 *  
 * Version: 1.0
 * Copyright (c) 2014: Social Islami Bank Limited
 */

using System;
using System.Collections;
using System.Diagnostics;

using SIBLCommon.Common.Util.Caching;
using SIBLCommon.Common.Util.Logger;


namespace SIBLCommon.Common.Util.Caching
{
	
	/// <summary>
	/// This class is the single gateway for accessing caching service.
	/// Any client of caching service will use this class access both
	/// web and non-web caching service.
	/// </summary>
	public class CCacheManager
	{
		/// <summary>
		/// m_oInstance is the instance of CCacheManager
		/// </summary>
		protected static CCacheManager m_oInstance;
        /// <summary>
        /// m_oCacheService is the instance of ICacheBase
        /// </summary>
        protected static ICacheBase m_oCacheService;

        /// <summary>
        ///  static constructor
        /// </summary>
        static CCacheManager()
        {           
            if (m_oInstance == null)
            {
                m_oInstance = new CCacheManager();
            }
        }

        /// <summary>
        /// constructor
        /// </summary>
        protected CCacheManager()
        {
            Init();
        }

        /// <summary>
        /// 
        /// </summary>
        protected void Init()
        {           
            m_oCacheService = CWebCacheService.GetInstance();            
        }

		/// <summary>
		/// This method returns the instance of CCacheManager
		/// </summary>
		/// <returns>
		/// Returns the instance of CCacheManager
		/// </returns>
		public static CCacheManager GetCacheInstance()
		{
            if (m_oInstance == null)
            {
                m_oInstance = new CCacheManager();
#if DEBUG
                Debug.WriteLine("The m_oInstance instance is null", "Caching.CCacheManager.GetCacheInstance");
#endif
            }
                
            return m_oInstance;
		}

		/// <summary>
		/// This method sets the data (oData) in cache m_htCache with
		/// the key (sCacheKey)
		/// </summary>
		/// <param name="sCacheKey">
		/// Is the key with which data will be cached.
		/// </param>
		/// <param name="oData">
		/// Is the data which will be cached.
		/// </param>
		/// <param name="iDurationInMinute">
		/// Is the expiration time in minute. This means
		/// after this amount of time the data will be removed from cache.
		/// </param>
        public void SetDataInCache(string sCacheKey, object oData)
        {
            m_oCacheService.SetDataInCache(sCacheKey, oData);
         
        }

		/// <summary>
		/// This method removes the data from cache associated
		/// with the key supplied
		/// </summary>
		/// <param name="sCacheKey">
		/// Is the key with which data will be 
		/// searched and finally removed
		/// </param>
        public void RemoveDataFromCache(string sCacheKey)
        {
            
            m_oCacheService.RemoveDataFromCache(sCacheKey);
          
        }

		/// <summary>
		/// This method retrieves the data from cache associated
		/// with the key supplied
		/// </summary>
		/// <param name="sCacheKey">
		/// Is the key with which data will be retrieved form cache.</param>
		/// <returns>
		/// It returns the object of type Object
		/// </returns>
        public object GetDataFromCache(string sCacheKey)
        {
          
            return m_oCacheService.GetDataFromCache(sCacheKey);
          
        }
	}		
}