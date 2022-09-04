/**
 * File name            : CXMLDataHandler.cs
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
using System.Data;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using SIBLCommon.Common.Entity;
using System.Collections;


using SIBLCommon.Common.Entity.Application;
//using CJ.Common.Common.Services.Mailer;
//using CJ.Common.Common.Entity.UIText;
//using CJ.Common.Common.Util.Logger;
using SIBLCommon.Common.Entity.Caching;
using SIBLCommon.Common.Util.Configuration;
//using CJ.Common.Common.Entity.Caching;
//using CJ.Common.Common.Entity.PageNavigation;



namespace SIBLCommon.Common.Util.Configuration
{
    public class CXMLDataHandler
    {

       
       private static CCacheMapping m_oCacheMappingObject = null;
        private static CAppConfig m_oAppConfigObject = null;
        //private static CSmtpData m_oSmtpdataObject = null;
        private static CRequestList m_oRequestMappingObject = null;
        private static DataSet m_oDataSet = null;
        private static IDictionary m_oIDictionary = null;

        //private static CTooltipText m_oTooltipTextObject = null;
        //private static CFAQList m_oFAQDataObject = null;
        //private static CContactUs m_oContactUsObject = null;
        //private static CAboutUsList m_oAboutUsObject = null;
        //private static CCJServiceList m_oCJServiceObject = null;
        //private static CNeedForAccount m_oNeedForAccountObject = null;
        //private static CCJADSList m_oCJADSObject = null;

       // private static CPageInfoList m_oPageInfoObject = null;

        #region Constructor
        public CXMLDataHandler()
        {
        }
        #endregion Constructor


        /**
        * Utility method just to create an serializable XML file
        */
        //public static void CreateXMLFile()
        //{
        //    CMailer o = new CMailer();
        //    o.Update();

        //    FileStream fs = new FileStream("c:\\Database.config", FileMode.Create);
        //    try
        //    {
        //        XmlSerializer serializer = new XmlSerializer(typeof(CDatabaseConfiguration));  
        //        XmlWriter writer = XmlWriter.Create(fs);
        //        CDatabaseConfiguration oDbConfig = new CDatabaseConfiguration();
        //        CDatabase oDatabase = new CDatabase();

        //        oDatabase.Name = "Oracle";
        //        oDatabase.DefaultDatabase = true;
        //        oDatabase.Server = "CJSERVER";
        //        oDatabase.DataSource = "CJDB";
        //        oDatabase.Provider = "System.Data.OracleClient";
        //        oDatabase.UserID = "CJDB";
        //        oDatabase.Password = "CJDB";
        //        oDatabase.ConnectionLifeTime = 0;
        //        oDatabase.Enlist = "true";
        //        oDatabase.IntegratedSecurity = "false";
        //        oDatabase.PersistSecurityInfo = "false";
        //        oDatabase.MaxPoolSize = 100;
        //        oDatabase.MinPoolSize = 0;
        //        oDatabase.Pooling = "true";
        //        oDatabase.Unicode = "false";
        //        oDatabase.ConnectionString = "Server=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = cjserver)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = mportal))),uid=cj,pwd=cj";

        //        oDbConfig.Database.Add(oDatabase);

        //        oDatabase = new CDatabase();

        //        oDatabase.Name = "Sql";
        //        oDatabase.DefaultDatabase = true;
        //        oDatabase.Server = "sadfs";
        //        oDatabase.DataSource = "sdf";
        //        oDatabase.Provider = "System.Data.SqlClient";
        //        oDatabase.UserID = "sdf";
        //        oDatabase.Password = "sdf";
        //        oDatabase.ConnectionLifeTime = 0;
        //        oDatabase.Enlist = "true";
        //        oDatabase.IntegratedSecurity = "false";
        //        oDatabase.PersistSecurityInfo = "false";
        //        oDatabase.MaxPoolSize = 100;
        //        oDatabase.MinPoolSize = 0;
        //        oDatabase.Pooling = "true";
        //        oDatabase.Unicode = "false";
        //        oDatabase.ConnectionString = "Server=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = cjserver)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = mportal))),uid=cj,pwd=cj";

        //        oDbConfig.Database.Add(oDatabase);

        //        serializer.Serialize(writer, oDbConfig);
        //        writer.Flush();

        //        //CRequestList oRequestList = new CRequestList();
        //        //CRequest oRequest = new CRequest();
        //        //oRequest.RequestName = "CreateUser";
        //        //oRequest.MapObjectPath = "CJ.Common.BO.User.CUserBO,ServiceProviderBO";
        //        //oRequest.MethodList.Add(new CMethod("Create"));
        //        //oRequest.ParameterList.Add(new CMethodParameter("CJ.Common.Common.Entity.Bases.ICJEntityBase,ServiceProviderCommon"));
        //        ////oRequest.ParameterList.Add(new CMethodParameter("string"));
        //        //oRequestList.Requests.Add(oRequest);
        //        //oRequest = new CRequest();
        //        //oRequest.RequestName = "ReadUser";
        //        //oRequest.MapObjectPath = "CJ.Common.BO.User.CUserBO,ServiceProviderBO";
        //        //oRequest.MethodList.Add(new CMethod("Read"));
        //        //oRequest.ParameterList.Add(new CMethodParameter("CJ.Common.Common.Entity.Bases.ICJEntityBase,ServiceProviderCommon"));
        //        //oRequestList.Requests.Add(oRequest);
        //        //serializer.Serialize(writer, oRequestList);
        //        //writer.Flush();

        //        fs.Close();
        //    }
        //    catch (Exception exp)
        //    {
        //        fs.Close();
        //        CLog.Logger.Write(CLog.EXCEPTION, "CJ.Common.Util.Configuration.CXMLDataManager/CreateXMLFile: " + exp.Message);
        //        throw exp;
        //    }
        //}

       /**
        * Read XML file from specified path and set in data set
        */
        public static DataSet ReadXMLFileIntoDataSet(string sFilePath)
        {
            try
            {
                DataSet dsResult = new DataSet();

                if (File.Exists(sFilePath))
                {
                    dsResult.ReadXml(sFilePath);
                }
                else
                {
                    throw new Exception();
                }
                return dsResult;
            }
            catch (Exception exp)
            {
                //CLog.Logger.Write(CLog.EXCEPTION, "CJ.Common.Util.Configuration.CXMLDataManager/ReadXMLFileIntoDataSet: " + exp.Message);
                return null;
            }
        }


       /**
        * Read XML file from specified path
        */
        public static object DeserializeXMLObject(Type tObjectType, string sFilePath)
        {
            FileStream fs = null;
            Object oConfig = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(tObjectType);               
                fs = File.OpenRead(sFilePath);
                oConfig = serializer.Deserialize(fs);
                Console.Write("Read Entity Configuration:: SUCCESS");
            }
            catch (Exception exp)
            {
                //CLog.Logger.Write(CLog.EXCEPTION, "CJ.Common.Util.Configuration.CXMLDataManager/DeserializeXMLObject: " + exp.Message);

                throw exp;
            }
            finally
            {
                if (null != fs)
                {
                    fs.Close();
                }
            }
            return oConfig;
        }

        /**
         * Read section from web.config file
         */
        public static IDictionary ReadXMLWebConfigFile(string sSectionName)
        {
            try
            {
                IDictionary oIDictionary = (IDictionary)(System.Configuration.ConfigurationSettings.GetConfig(sSectionName));
                return oIDictionary;
            }
            catch (Exception exp)
            {
                //CLog.Logger.Write(CLog.EXCEPTION, "CJ.Common.Common.Util.Configuration.CXMLDataManager" + "/ReadXMLWebConfigFile: " + exp.Message);
                return null;
            }
        }


        /**
         * Deserializing request mapping XML file
         * */
        public static CRequestList GetRequestMappingObject()
        {
            try
            {
                if (m_oRequestMappingObject == null)
                {
                    String sPath = System.AppDomain.CurrentDomain.BaseDirectory;
                    m_oRequestMappingObject = (CRequestList)CXMLDataHandler.DeserializeXMLObject(typeof(CRequestList), sPath + @"config\Request.config");
                }
            }
            catch (Exception exp)
            {
                //CLog.Logger.Write(CLog.EXCEPTION, "CJ.Common.Common.Util.Configuration.CXMLDataManager" + "/GetRequestMappingObject: " + exp.Message);

                return null;
            }

            return m_oRequestMappingObject;
        }

        /**
         * Deserializing cache  mapping object XML
         * */
        public static CCacheMapping GetCacheMappingObject()
        {
            try
            {
                if (m_oCacheMappingObject == null)
                {
                    String sPath = System.AppDomain.CurrentDomain.BaseDirectory;
                    m_oCacheMappingObject = (CCacheMapping)CXMLDataHandler.DeserializeXMLObject(typeof(CCacheMapping), sPath + @"config\CacheMapping.config");
                }
            }
            catch (Exception exp)
            {
                // CLog.Logger.Write(CLog.EXCEPTION, "CJ.Common.Common.Util.Configuration.CXMLDataManager" + "/GetCacheMappingObject: " + exp.Message);

                return null;
            }

            return m_oCacheMappingObject;
        }

        /**
         * Deserializing application config object XML
         * */
        public static CAppConfig GetAppConfigObject()
        {
            try
            {
                if (m_oAppConfigObject == null)
                {
                    String sPath = System.AppDomain.CurrentDomain.BaseDirectory;
                    m_oAppConfigObject = (CAppConfig)CXMLDataHandler.DeserializeXMLObject(typeof(CAppConfig), sPath + @"config\Application.config");
                }
            }
            catch (Exception exp)
            {
                //CLog.Logger.Write(CLog.EXCEPTION, "CJ.Common.Common.Util.Configuration.CXMLDataManager" + "/GetAppConfigObject: " + exp.Message);

                return null; ;
            }

            return m_oAppConfigObject;
        }


        /**
         * Deserializing PageInformation for URL text object XML
         * */

        //public static CPageInfoList GetPageURLTextObject()
        //{
        //    try
        //    {
        //        if (m_oPageInfoObject == null)
        //        {
        //            String sPath = System.AppDomain.CurrentDomain.BaseDirectory;

        //            m_oPageInfoObject = (CPageInfoList)CXMLDataHandler.DeserializeXMLObject(typeof(CPageInfoList), sPath + @"config\Page.config");
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //       // CLog.Logger.Write(CLog.EXCEPTION, "CJ.Common.Common.Util.Configuration.CXMLDataManager" + "/GetFAQTextObject: " + exp.Message);

        //        return null;
        //    }

        //    return m_oPageInfoObject;
        //}



        /**
         * Deserializing tooltip text object XML
         * */
        //public static CSmtpData GetSmtpDataObject()
        //{
        //    try
        //    {
        //        if (m_oSmtpdataObject == null)
        //        {
        //            String sPath = System.AppDomain.CurrentDomain.BaseDirectory;
        //            m_oSmtpdataObject = (CSmtpData)CXMLDataHandler.DeserializeXMLObject(typeof(CSmtpData), sPath + @"config\Smtp.config");
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        //CLog.Logger.Write(CLog.EXCEPTION, "CJ.Common.Common.Util.Configuration.CXMLDataManager" + "/GetSmtpDataObject: " + exp.Message);

        //        return null;
        //    }

        //    return m_oSmtpdataObject;
        //}


        /**
         * DRead section from web.config file
         * */
        public static IDictionary GetXMLWebConfigFile(string sSectionName)
        {
            try
            {
                if (m_oIDictionary == null)
                {
                    m_oIDictionary = CXMLDataHandler.ReadXMLWebConfigFile(sSectionName);
                }
                return m_oIDictionary;
            }
            catch (Exception exp)
            {
                //CLog.Logger.Write(CLog.EXCEPTION, "CJ.Common.Common.Util.Configuration.CXMLDataManager" + "/GetXMLWebConfigFile: " + exp.Message);

                return null;
            }
        }


        /**
         * Read XML data for occupation filtering for different type of people like father, mother, guardian etc.
         * */
        //public static DataSet GetFilteredOccupation()
        //{
        //    try
        //    {
        //        String sPath = System.AppDomain.CurrentDomain.BaseDirectory;
        //        m_oDataSet = CXMLDataHandler.ReadXMLFileIntoDataSet(sPath + CConstants.STATIC_DATA_FILE);
        //        return m_oDataSet;
        //    }
        //    catch (Exception exp)
        //    {
        //        //CLog.Logger.Write(CLog.EXCEPTION, "CJ.Common.Common.Util.Configuration.CXMLDataManager" + "/GetFilteredOccupation: " + exp.Message);

        //        return null; ;
        //    }
        //}
    }
}
