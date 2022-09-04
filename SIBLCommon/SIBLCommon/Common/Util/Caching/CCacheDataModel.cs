/*
 * File name: CCacheDataModel.cs 
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
 * */

using System;

namespace SIBLCommon.Common.Util.Caching
{

	/// <summary>
	/// This is an entity object which will actually contain the actual data
	/// that will be cached with some additional information like whether the
	/// data are cached and whether cached data is valid. Suppose we want to
	/// cache MessageBoard data. In order to do that we need an object of 
	/// CCachedDataModel in which we will set the MessageBoard data as m_oCacheObj.
	/// For some sorts of cheking we will also set whether the data is cached and
	/// will also set whether it is valid cache.
	/// </summary>
	public class CCacheDataModel
	{
		/// <summary>
        /// The actual data which will be cached
		/// </summary>
		protected Object m_oCacheObj;
		/// <summary>
        /// The time when data is added in cache
		/// </summary>
		protected DateTime m_dtCachingTime;
		/// <summary>
        /// The time after when the data will be expired
        /// removed from cache
		/// </summary>
		protected int m_iDurationInMinute;
		/// <summary>
        /// To check whether the data is cached	
		/// </summary>
		protected bool m_bIsCached;
		/// <summary>
        /// To check whether cached data is valid
		/// </summary>
		protected bool m_bIsValidCache;

		/// <summary>
		/// Constructor
		/// </summary>
		public CCacheDataModel()
		{
			Init(new Object(), false, false, DateTime.Now, 0);
		}

		/// <summary>
		/// Initialize the Class variable.
		/// </summary>
		/// <param name="oData">
		/// Is the actual data which will be cached.
		/// </param>
        /// <param name="bIsCached">
		/// Is the flag to check whether the data is cached.
		/// </param>
        /// <param name="bIsValidCache">
		/// Is the flag to check whether cached data is valid.
		/// </param>
        /// <param name="dtCachingTime">
        /// Is the time when data was cached
        /// </param>
        /// <param name="iDurationInMinute">
        /// Is the duration of caching in minute
        /// </param>
		protected void Init(Object oData, bool bIsCached, bool bIsValidCache, 
        DateTime dtCachingTime, int iDurationInMinute)
		{
			m_oCacheObj = oData;
			m_bIsCached = bIsCached;
			m_bIsValidCache = bIsValidCache;
			m_dtCachingTime = dtCachingTime;
            m_iDurationInMinute = iDurationInMinute;
		}

		/// <summary>
		/// </summary>  
        /// <value>
        /// Gets/sets cache object
        /// </value>
		public Object CachedObject
		{
			get
			{
				return m_oCacheObj;
			}
			set
			{
				m_oCacheObj = value;
			}
		}

		/// <summary>
		/// This method gets/sets the the time when the data is cached
		/// </summary>
		public DateTime CachingTime
		{
			get
			{
				return m_dtCachingTime;
			}
			set
			{
				m_dtCachingTime = value;
			}
		}

		/// <summary>
		/// This property gets/sets the duration for cached data
		/// </summary>
		public int DurationInMinute
		{
			get
			{
				return m_iDurationInMinute;
			}
			set
			{
				m_iDurationInMinute = value;
			}
		}

		/// <summary>
		/// Gets/Sets whether the data is cached or not.
		/// </summary>
		/// <returns>It returns true or false</returns>
		public bool IsCached
		{
			get
			{
				return m_bIsCached;
			}
			set
			{
				m_bIsCached = value;
			}
		}

		/// <summary>
		/// Gets/Sets whether the cached data is valid or not.
		/// </summary>
		/// <returns>Returns true if the data is valid otherwise false
        /// </returns>
		public bool IsValidCache
		{
			get
			{
				return m_bIsValidCache;
			}
			set
			{
				m_bIsValidCache = value;
			}
		}
	}		
}