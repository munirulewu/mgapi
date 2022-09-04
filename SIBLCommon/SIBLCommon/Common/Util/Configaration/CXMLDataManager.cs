/**
 * File name            : CXMLDataManager.cs
 * Author               : Munirul Islam
 * Date                 : March 27, 2014
 * Version              : 1.0
 *
 * Description          : All kind of XML file manipulator
 *
 * Modification history:
 * Name                         Date                            Desc
 * 
 * 
 * Copyright (c) 2014: Social Islami Bank Limited
 */
using System;
using System.Collections.Generic;
using System.Text;
using SIBLCommon.Common.Services.Bases;
using System.Collections;
using System.Data;
using System.Diagnostics;

using SIBLCommon.Common;
using SIBLCommon.Common.Entity.Application;
//using CJ.Common.Common.Services.Mailer;
using SIBLCommon.Common.Util.Caching;
using SIBLCommon.Common.Util.Logger;
//using CJ.Common.Common.Entity.PageNavigation;

namespace SIBLCommon.Common.Util.Configuration
{
    public class CXMLDataManager : ACRPUServiceBase
    {
        #region Constructor
        public CXMLDataManager()
        {

        }
        #endregion Constructor

        public override void Update()
        {
            
        }       
       

        /**
         * Deserializing request mapping XML file
         * */
        public static CRequestList GetRequestMappingObject()
        {
            try
            {
                CRequestList oRequestList = (CRequestList)GetXMLObject(CConstants.CSREQMAPPINGXMLDATA);              
                return oRequestList;
            }
            catch (Exception exp)
            {
                return null; ;
            }  
        }
        


        /**
         * Deserializing application config object XML
         * */
        public static CAppConfig GetAppConfigObject()
        {            
            try
            {
                CAppConfig oAppConfig = (CAppConfig)GetXMLObject(CConstants.CSAPPCONFIGXMLDATA);              
                return oAppConfig;
            }
            catch (Exception exp)
            {
                return null; ;
            }         
        }


        

        /**
        * Deserializing URL text object XML
        * */
        //public static CPageInfoList GetPageInfoList()
        //{
        //    try
        //    {
        //        CPageInfoList oPageInfoListConfig = (CPageInfoList)GetXMLObject(CConstants.CSPageInfoListXMLDATA);
        //        return oPageInfoListConfig;
        //    }
        //    catch (Exception exp)
        //    {
        //        return null; ;
        //    }
        //}

        
        /**
         * Deserializing tooltip text object XML
         * */
        //public static CSmtpData GetSmtpDataObject()
        //{
        //    try
        //    {               
        //        CSmtpData oSmtpConfig = (CSmtpData)GetXMLObject(CConstants.CSSMTPXMLDATA);              
        //        return oSmtpConfig;
        //    }
        //    catch (Exception exp)
        //    {
        //        return null; ;
        //    }       
        //}




        /**
         * Read XML data for occupation filtering for different type of people like father, mother, guardian etc.
         * */
        //public static DataSet GetFilteredOccupation()
        //{
        //    try
        //    {              
        //        DataSet oFilteredOccu = (DataSet)GetXMLObject(CConstants.CSSTATICXMLDATA);               
        //        return oFilteredOccu;
        //    }
        //    catch (Exception exp)
        //    {
        //        return null; ;
        //    }
        //}

        /**
        * Read section from web.config file
        * */
        public static IDictionary GetXMLWebConfigFile(string sSectionName)
        {
            try
            {
                return CXMLDataHandler.GetXMLWebConfigFile(sSectionName);
            }
            catch (Exception exp)
            {
                return null; ;
            }
        }


       

       /**
        * Deserializing application config object XML
        * */
        public static Object GetXMLObject(String sCacheKey)
        {
            Object oData = null;
            try
            {
                if (CCacheUtil.GetInstance().IsCacheable(sCacheKey) == false)
                {
                    oData = CXMLDataManager.GetSpecificObject(sCacheKey);
                    Debug.WriteLine("Caching is off and loading all time: " + sCacheKey, "DEBUG");
                    return oData;
                }

                CCacheManager oCacheManager = CCacheManager.GetCacheInstance();
                oData = oCacheManager.GetDataFromCache(sCacheKey);
                if (oData == null)
                {
                    oData = CXMLDataManager.GetSpecificObject(sCacheKey);
                    Debug.WriteLine("Caching is on and loading cached data: " + sCacheKey, "DEBUG");
                    oCacheManager.SetDataInCache(sCacheKey, oData);
                }
                return oData;
            }
            catch (Exception exp)
            {
                CLog.Logger.Write(CLog.EXCEPTION, "CRPU.Common.Util.Configuration.CXMLDataManager" + "/GetXMLObject: " + exp.Message);

                return null; ;
            }
        }


       /**
        * Get data based on key string application config object XML
        * */
        public static Object GetSpecificObject(String sCacheKey)
        {   
            if (sCacheKey.Equals(CConstants.CSAPPCONFIGXMLDATA))
            {
                return CXMLDataHandler.GetAppConfigObject();
            }
            else if (sCacheKey.Equals(CConstants.CSREQMAPPINGXMLDATA))
            {
                return CXMLDataHandler.GetRequestMappingObject();
            }
           
           
            return null;
        }               
    }
}

