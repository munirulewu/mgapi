/*
 * File name: ICache.cs 
 * Author: MI
 * Date: June 07, 2009
 * 
 * Description: 
 * 
 * Modification history:
 * Name				Date				Desc
 *  	
 *  
 * Version: 1.0
 * Copyright (c)  2014: SIBL
 * */

using System;

namespace SIBLCommon.Common.Util.Caching
{

	/// <summary>
	/// ICache is the interface which will be implemented
	/// by any type of caching services like web caching
	/// service or non-web cache service.
	/// </summary>
	public interface ICacheBase
	{
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
		void SetDataInCache(string sCacheKey, Object oData);

		/// <summary>
		/// This method removes the data from cache associated
		/// with the key supplied
		/// </summary>
		/// <param name="sCacheKey">
		/// Is the key with which data will be 
		/// searched and finally removed
		/// </param>
		void RemoveDataFromCache(string sCacheKey);

		/// <summary>
		/// This method retrieves the data from cache associated
		/// with the key supplied
		/// </summary>
		/// <param name="sCacheKey">
		/// Is the key with which data will be retrieved form cache.</param>
		/// <returns>
		/// It returns the object of type Object
		/// </returns>
		Object GetDataFromCache(string sCacheKey);

		/// <summary>
		/// This method makes the CacheDataModel object cached
		/// in cache valid.
		/// </summary>
		/// <param name="sCacheKey">
		/// Is the key with which data is searched and made valid.
		/// </param>
		void ValidateCache(string sCacheKey);

		/// <summary>
		/// This method makes the CacheDataModel object cached 
		/// in cache invalid.
		/// </summary>
		/// <param name="sCacheKey">
		/// Is the key with which data is searched and made invalid.
		/// </param>
		void InvalidateCache(string sCacheKey);
	}		
}