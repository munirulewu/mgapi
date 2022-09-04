/*
 * File name: CWebCacheImpl.cs 
 * Author:  MI
 * Date: June 07, 2009
 * 
 * Description: 
 * 
 * Modification history:
 * Name				Date				Desc
 *  
 *  
 * Version: 1.0
 * Copyright (c) 2014:SIBL
 * */

using System;
using System.Web;
using System.Web.Caching;

using System.Diagnostics;
using System.Threading;

using SIBLCommon.Common.Util.Caching;
using SIBLCommon.Common.Util.Logger;
using SIBLCommon.Common.Util.Configuration;
using SIBLCommon.Common.Entity.Application;




namespace SIBLCommon.Common.Util.Caching
{

    /// <summary>
    /// Implements ICache interface definition for non-web caching
    /// Method List:
    /// 1. SetDataInCache
    /// 2. RemoveDataFromCache
    /// 3. GetDataFromCache
    /// 4. ValidateCache
    /// 5. InvalidateCache
    /// </summary>
    public class CWebCacheService : ICacheBase
    {
        /// <summary>
        /// The instance of CWebCacheService
        /// </summary>
        protected static CWebCacheService m_oInstance;
        /// <summary>
        /// To determine whether any data is removed or not
        /// </summary>
        protected static bool m_isItemRemoved;
        /// <summary>
        /// The reason why data is removed from cache
        /// </summary>
        protected static CacheItemRemovedReason m_oRemoveReason;
        /// <summary>
        /// TO WRITE:
        /// </summary>
        protected static CacheItemRemovedCallback m_oItemRemoveCallback;

        /// <summary>
        /// Idle time
        /// </summary>

        protected static int m_iCacheIdleTimeInMin = 0;
       

        /// <summary>
        /// Static constructor
        /// </summary>
        static CWebCacheService()
        {
            m_oInstance = new CWebCacheService();
        }

        /// <summary>
        /// Default constructor which calls the Init() method
        /// </summary>
        protected CWebCacheService()
        {
            try
            {
                Init();
            }
            catch (Exception ex)
            {
               Debug.WriteLine(ex.Message + " has been thrown in the constructior of " + this.GetType().ToString(), "DEBUG");
               throw ex;
            }

        }

        /// <summary>
        /// Initializes the member variables
        /// </summary>
        protected void Init()
        {
            m_isItemRemoved = false;
            m_oItemRemoveCallback = null;          
        }


        /// <summary>
        /// Returns the instance of CWebCacheService
        /// </summary>
        /// <returns>
        /// Returns the instance of CWebCacheService
        /// </returns>
        public static void CacheIdleTimeInMin(int iMins)
        {
            CWebCacheService.m_iCacheIdleTimeInMin = iMins; 
        }


        /// <summary>
        /// Returns the instance of CWebCacheService
        /// </summary>
        /// <returns>
        /// Returns the instance of CWebCacheService
        /// </returns>
        public static CWebCacheService GetInstance()
        {
            return m_oInstance;
        }



        /// <summary>
        /// This method caches the data oData in cache with the key
        /// sCacheKey and with idle time iIdleTime
        /// </summary>
        /// <param name="sCacheKey">
        /// Is the key with which data will be cached
        /// </param>
        /// <param name="oData">
        /// Is the data which will be cached
        /// </param>
        public void SetDataInCache(string sCacheKey, object oData)
        {
            try
            {
                lock (this)
                {
                    // The instance of CCacheDataModel is taken
                    CCacheDataModel oDataModel = new CCacheDataModel();

                    // Setting the flag that it is not removed
                    m_isItemRemoved = false;
                    m_oItemRemoveCallback = new CacheItemRemovedCallback(RemovedCallback);
                    m_oItemRemoveCallback(sCacheKey, oDataModel, CacheItemRemovedReason.Removed);

                    oDataModel.CachedObject = oData;
                    oDataModel.IsValidCache = true;
                    oDataModel.IsCached = true;
                    if (HttpContext.Current != null)
                    {
                        if (null != HttpContext.Current.Cache[sCacheKey])
                        {
                            HttpContext.Current.Cache.Remove(sCacheKey);
                            HttpContext.Current.Cache.Add(sCacheKey, oDataModel, null, DateTime.MaxValue, new TimeSpan(0, CWebCacheService.m_iCacheIdleTimeInMin, 0), CacheItemPriority.Normal, m_oItemRemoveCallback);
                            Debug.WriteLine("WebCacheService: ReSetInCache - CacheKey: " + sCacheKey + " , value:" + oData.ToString(), "DEBUG");
                        }
                        else
                        {
                            HttpContext.Current.Cache.Add(sCacheKey, oDataModel, null, DateTime.MaxValue, new TimeSpan(0, CWebCacheService.m_iCacheIdleTimeInMin, 0), CacheItemPriority.Normal, m_oItemRemoveCallback);
                            Debug.WriteLine("WebCacheService: SetInCache - CacheKey: " + sCacheKey + " , value:" + oData.ToString(), "DEBUG");
                        }
                    }
                }
               
            }           
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + " has been thrown in the SetDataInCache() mehtod of " + this.GetType().ToString(), "DEBUG");
                throw ex;
            }

        }

        /// <summary>
        /// This method notifies users when the cache item is removed 
        /// </summary>
        /// <param name="sArg"></param>
        /// <param name="oArg"></param>
        /// <param name="oArgReason"></param>
        protected void RemovedCallback(string sArg, object oArg, CacheItemRemovedReason oArgReason)
        {
            m_isItemRemoved = true;
            m_oRemoveReason = oArgReason;
            
            Debug.WriteLine("WebCacheService: RemovedCallBack - CacheKey: " + sArg + " , value:" + oArg.ToString(),   "DEBUG");

        }

        /// <summary>
        /// This method will retrieve the data associated wtih the key supplied
        /// as parameter.
        /// </summary>
        /// <param name="sCacheKey">
        /// Is the key with which the data is cached.
        /// </param>
        /// <returns>
        /// It returns data of type Object.
        /// </returns>
        public object GetDataFromCache(string sCacheKey)
        {
            CCacheDataModel oDataModel = null;
            Object oCachedObject = null;
            try
            {
                if (HttpContext.Current == null)
                {
                     Debug.WriteLine("WebCacheService - GetDataFromCache.Current is NULL - ThreadID: " + Thread.CurrentThread.ManagedThreadId.ToString() + " - ThreadName: " + Thread.CurrentThread.Name,   "DEBUG");
                }
                else
                {    
                    if (null != sCacheKey && !"".Equals(sCacheKey))
                    {
                        if (null != HttpContext.Current.Cache[sCacheKey])
                        {
                            oDataModel = (CCacheDataModel)HttpContext.Current.Cache.Get(sCacheKey);
                            if (null != oDataModel)
                            {
                                if (oDataModel.IsValidCache)
                                {
                                    oCachedObject = oDataModel.CachedObject;
                                    Debug.WriteLine("oDataModel.IsValidCache: WebCacheService: GetDataFromCache - CacheKey: " + sCacheKey, "DEBUG");
                                }
                            }
                        }
                        else
                        {
                            Debug.WriteLine("WebCacheService: GetDataFromCache: Else - CacheKey: " + sCacheKey, "DEBUG");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("WebCacheService: GetDataFromCache - CacheKey: " + ex.Message, "DEBUG");
                throw ex;           
            }
            return oCachedObject;
        }

        /// <summary>
        /// This method will remove the data associated with the key supplied
        /// from cache if the data exists in cache.
        /// </summary>
        /// <param name="sCacheKey">
        /// Is the key with which the data is cached.
        /// </param>
        public void RemoveDataFromCache(string sCacheKey)
        {
            try
            {
                lock (this)
                {
                    if (null != sCacheKey && !"".Equals(sCacheKey))
                    {
                        HttpContext.Current.Cache.Remove(sCacheKey);
                        Debug.WriteLine("WebCacheService: RemoveDataFromCache - CacheKey: " + sCacheKey, "DEBUG");

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + " has been thrown in the RemoveDataFromCache() mehtod",  "DEBUG");
                throw ex;
            }
        }

        /// <summary>
        /// This method will make the data associated with the key supplied
        /// in cache valid. Now when anyone wants to retrieve the data from
        /// cache, system will return the data from the cache. It will not
        /// be retrieved from cache.
        /// </summary>
        /// <param name="sCacheKey">Is the key with which the data is cached.</param>
        public void ValidateCache(string sCacheKey)
        {
            CCacheDataModel oDataModel = null;
            try
            {
                if (null != sCacheKey && !"".Equals(sCacheKey))
                {
                    oDataModel = (CCacheDataModel)HttpContext.Current.Cache.Get(sCacheKey);
                    if (null != oDataModel)
                    {
                        oDataModel.IsValidCache = true;
                        HttpContext.Current.Cache.Remove(sCacheKey);
                        HttpContext.Current.Cache.Add(sCacheKey, oDataModel, null, DateTime.MaxValue, new TimeSpan(0, oDataModel.DurationInMinute, 0), CacheItemPriority.Normal, m_oItemRemoveCallback);
                    }
                }
            }            
            catch (Exception ex)
            {
                Debug.WriteLine("WebCacheService: ValidateCache - CacheKey: " + sCacheKey + " , value:" + ex.Message, "DEBUG");
                throw ex;
            }
        }

        /// <summary>
        /// This method will make the cached data associated with
        /// the key supplied as parameter invalid. This means that
        /// when any one wants to retrieve the data from cache using
        /// this key, although the data exists in the cache, system
        /// will ignore it and will retrieve it from the database.
        /// </summary>
        /// <param name="sCacheKey">
        /// Is the key with which the data is cached.
        /// </param>
        public void InvalidateCache(string sCacheKey)
        {
            CCacheDataModel oDataModel = null;
            try
            {
                if (null != sCacheKey && !"".Equals(sCacheKey))
                {
                    oDataModel = (CCacheDataModel)HttpContext.Current.Cache.Get(sCacheKey);
                    if (null != oDataModel)
                    {
                        oDataModel.IsValidCache = false;
                        HttpContext.Current.Cache.Remove(sCacheKey);
                        HttpContext.Current.Cache.Add(sCacheKey, oDataModel, null, DateTime.MaxValue, new TimeSpan(0, oDataModel.DurationInMinute, 0), CacheItemPriority.Normal, m_oItemRemoveCallback);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + " has been thrown in the InvalidateCache()", "DEBUG");
                throw ex;
            }
        }
    }
}