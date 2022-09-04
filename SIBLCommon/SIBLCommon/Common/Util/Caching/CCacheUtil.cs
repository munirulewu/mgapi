/*
 * File name: CCacheUtil.cs
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
 * Copyright (c)  2014: Social Islami Bank Limited
 * */

using System;
using System.Collections;
using System.Web;
using System.Xml;
using System.IO;
using System.Reflection;

using SIBLCommon.Common.Util.Caching;
using SIBLCommon.Common.Util.Logger;
using SIBLCommon.Common.Util.Configuration;
using SIBLCommon.Common.Entity.Caching;
namespace SIBLCommon.Common.Util.Caching
{

   

    /// <summary>
    /// This class provides some utility method to retrieve cached data 
    /// for web and non web based application.
    /// </summary>
    /// 
    public class CCacheUtil
    {     
       
        /// <summary>
        /// Handle to the instance of CCacheUtil
        /// </summary>
        protected static CCacheUtil m_oInstance;

        /// <summary>
        /// The Constructor
        /// </summary>
        protected CCacheUtil()
        {       
           
        }

        /// <summary>
        /// Gets the instance of CCacheUtil
        /// </summary>
        /// <returns>
        /// Returns m_oInstance of type CCacheUtil
        /// </returns>
        public static CCacheUtil GetInstance()
        {
            if (m_oInstance == null)
                m_oInstance = new CCacheUtil();
            return m_oInstance;
        }

     

        

        /// <summary>
        /// This method checks whether the object be cachable or not against 
        /// the key supplied. Not to be confused with the key with which data
        /// are cached and the key which is read from xml file. The key with
        /// which data are cached may look like "MessageBoard_g1_msg1". But
        /// in order to check whether the data will be cached or not by checking
        /// simply the value "MessageBoard" which is the key for m_htCacheConfig.
        /// So in order to do that we first to substring the "MessageBoard" part
        /// and then check whether it will be cached or not by checking the vlue
        /// in the m_htCacheConfig associated with the key "MessageBoard"
        /// </summary>
        /// <param name="sCacheKey">
        /// Is the key with which the data are cached
        /// </param>
        /// <returns>
        /// It returns true or false
        /// </returns>
        public bool IsCacheable(string sSuppliedCacheKey)
        {                  
            if (null != sSuppliedCacheKey && !"".Equals(sSuppliedCacheKey))
            {
                CCacheMapping oCacheMapping = CCacheUtil.GetCacheMappingObject();

                if (oCacheMapping.IsCachingAllowed == false)
                {
                    return false;
                }
                else
                {
                    CWebCacheService.CacheIdleTimeInMin(oCacheMapping.CacheIdleTimeInMin);
                    foreach (CacheKey oCacheKey in oCacheMapping.CacheKeyList)
                    {
                        if (oCacheKey.Name.ToUpper().Equals(sSuppliedCacheKey.Trim().ToUpper()) && oCacheKey.Enabled == true)
                        {
                            return true;
                        }
                    }
                }
            }
                   
            return false;
        }

        /**
        * Deserializing cache  mapping object XML
        * Please don't check cacheable or not
        * */
        public static CCacheMapping GetCacheMappingObject()
        {
            try
            {
                CCacheManager oCacheManager = CCacheManager.GetCacheInstance();
                CCacheMapping oCacheMapping = (CCacheMapping)oCacheManager.GetDataFromCache(CConstants.CSCACHEMAPPINGXMLDATA);

                if (oCacheMapping == null)
                {
                    oCacheMapping = CXMLDataHandler.GetCacheMappingObject();
                    //CWebCacheService.CacheIdleTimeInMin(oCacheMapping.CacheIdleTimeInMin);
                    oCacheManager.SetDataInCache(CConstants.CSCACHEMAPPINGXMLDATA, oCacheMapping);
                }
                return oCacheMapping;
            }
            catch (Exception exp)
            {
                return null; ;
            }
        }

    }
}