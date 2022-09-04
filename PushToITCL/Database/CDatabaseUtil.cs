/*
 * File name            : CDatabaseConfiguration.cs
 * Author               : Munirul Islam
 * Date                 : March 24, 2014
 * Version              : 1.0
 *
 * Description          : The base class of database specific factory 
 *
 * Modification history :
 * Name                         Date                            Desc
 * 
 * 
 * Copyright (c) 2014: SOCIAL ISLAMI BANK LIMITED
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace PushDataMGoITCL
{
    /// <summary>
    /// The database configuration data utility class can be used over the application
    /// </summary>
    public class CDatabaseUtil
    {

        private static CDatabaseConfiguration m_oConfig = null;


        /// <summary>
        /// Read database configuration by XML deserializer
        /// </summary>
        protected static object DeserializeXMLObject(Type oObjectType, string sFilePath)
        {
            FileStream oFileStream = null;
            Object oConfig = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(oObjectType);
                oFileStream = File.OpenRead(sFilePath);
                oConfig = serializer.Deserialize(oFileStream);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (null != oFileStream)
                {
                    oFileStream.Close();
                }
            }
            return oConfig;
        }

        /// <summary>
        /// Singleton method to get info of database config
        /// </summary>
        public static CDatabaseConfiguration GetConnectionConfig()
        {
            try
            {
                if (m_oConfig == null)
                {
                    m_oConfig = (CDatabaseConfiguration)CDatabaseUtil.DeserializeXMLObject(typeof(CDatabaseConfiguration), System.AppDomain.CurrentDomain.BaseDirectory + @"Config\Database.config");
                }

                return m_oConfig;
            }
            catch (Exception oEx)
            {
                throw oEx;
            }
        }
    }
    

    /// <summary>
    /// The entity object contains database config info
    /// </summary>
    [Serializable]
    [XmlRoot("Configuration")]
    public class CDatabaseConfiguration
    {
        protected string m_sDbConfig;
        protected string m_sDefault;

        public CDatabaseConfiguration()
        {
            Initialization();
        }
        public void Initialization()
        {
            m_sDbConfig = "";
            m_sDefault = "";
        }
        
        public string Default
        {
            get { return m_sDefault; }
            set { m_sDefault = value; }
        }

        public string ConnectionString
        {
            get { return m_sDbConfig; }
            set { m_sDbConfig = value; }
        }
    }
}
