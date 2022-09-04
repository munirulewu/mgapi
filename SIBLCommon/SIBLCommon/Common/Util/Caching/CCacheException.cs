/*
 * File name: CCacheException.cs 
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


namespace SIBLCommon.Common.Util.Caching
{

    /// <summary>
    /// Cache exception
    /// </summary>
    public class CCacheException : ApplicationException
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public CCacheException() : base("Invalid caching operation is performed.")
        {
        }

        /// <summary>
        /// Constructor with message
        /// </summary>
        /// <param name="sMessage">
        /// The message which will be passed throw this exception
        /// </param>
        public CCacheException(string sMessage)
            : base(sMessage)
        {
        }

        /// <summary>
        /// Constructor with message and inner exception
        /// </summary>
        /// <param name="sMessage">
        /// The message which will be passed throw this exception
        /// </param>
        /// <param name="oEx">
        /// The exception which was originally generated
        /// </param>
        public CCacheException(string sMessage, Exception oEx)
            : base(sMessage, oEx)
        {
        }
    }
}