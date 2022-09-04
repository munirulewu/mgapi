/**
 * File name: CDatabaseConfiguration.cs
 * Author: Munirul Islam
 * Date: March 27, 2014
 * Version: 1.0
 *
 * Description: Entity class to keep the database information
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
using System.Xml.Serialization;

namespace SIBLCommon.Common.Util.Configuration
{
    [Serializable]
    
    public class CDatabase
    {
        #region Protected Members
        protected string m_sName;
        protected bool m_bDefaultDatabase;
        protected string m_sProvider;
        protected string m_sServer;
        protected string m_sDataSource;
        protected string m_sUserID;
        protected string m_sPassword;
        protected string m_sIntegratedSecurity;
        protected string m_sPersistSecurityInfo;
        protected string m_sUnicode;
        protected string m_sEnlist;
        protected string m_sPooling;
        protected int m_iConnectionLifetime;
        protected int m_iMaxPoolSize;
        protected int m_iMinPoolSize;
        protected string m_sConnectionString;
        #endregion Protected Members

        #region Constructor
        public CDatabase()
        {
            Initialization(string.Empty,false,string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "false", "false", "false", "true", "true", 0, 100, 0,string.Empty);
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization(string sName, bool bDefaultDatabase, string sProvider, string sServer, string sDataSource, string sUserID, string sPassword, string sIntegratedSecurity, string sPersistSecurityInfo, string sUnicode, string sEnlist, string sPooling, int iConnectionLifeTime, int iMaxPoolSize, int iMinPoolSize, string sConnectionString)
        {
            m_sName = sName;
            m_bDefaultDatabase = bDefaultDatabase;
            m_sPassword = sProvider;
            m_sServer = sServer;
            m_sDataSource = sDataSource;
            m_sUserID = sUserID;
            m_sPassword = sPassword;
            m_sIntegratedSecurity = sIntegratedSecurity;
            m_sPersistSecurityInfo = sPersistSecurityInfo;
            m_sUnicode = sUnicode;
            m_sEnlist = sEnlist;
            m_sPooling = sPooling;
            m_iConnectionLifetime = iConnectionLifeTime;
            m_iMaxPoolSize = iMaxPoolSize;
            m_iMinPoolSize = iMinPoolSize;
            m_sConnectionString = sConnectionString;
        }
        #endregion Initialization

        #region Public Properties
        [XmlAttribute("Name")]
        public string Name
        {
            get { return m_sName; }
            set { m_sName = value; }
        }

        [XmlAttribute("DefaultDatabase")]
        public bool DefaultDatabase
        {
            get { return m_bDefaultDatabase; }
            set { m_bDefaultDatabase = value; }
        }

        [XmlElement("Provider")]
        public string Provider
        {
            get { return m_sProvider; }
            set { m_sProvider = value; }
        }
        
        [XmlElement("Server")]
        public string Server
        {
            get { return m_sServer; }
            set { m_sServer = value; }
        }

        [XmlElement("DataSource")]
        public string DataSource
        {
            get { return m_sDataSource; }
            set { m_sDataSource = value; }
        }

        [XmlElement("UserID")]
        public string UserID
        {
            get { return m_sUserID; }
            set { m_sUserID = value; }
        }

        [XmlElement("Password")]
        public string Password
        {
            get { return m_sPassword; }
            set { m_sPassword = value; }
        }

        [XmlElement("IntegratedSecurity")]
        public string IntegratedSecurity
        {
            get { return m_sIntegratedSecurity; }
            set { m_sIntegratedSecurity = value; }
        }

        [XmlElement("PersistSecurityInfo")]
        public string PersistSecurityInfo
        {
            get { return m_sPersistSecurityInfo; }
            set { m_sPersistSecurityInfo = value; }
        }

        [XmlElement("Unicode")]
        public string Unicode
        {
            get { return m_sUnicode; }
            set { m_sUnicode = value; }
        }

        [XmlElement("Enlist")]
        public string Enlist
        {
            get { return m_sEnlist; }
            set { m_sEnlist = value; }
        }

        [XmlElement("Pooling")]
        public string Pooling
        {
            get { return m_sPooling; }
            set { m_sPooling = value; }
        }

        [XmlElement("ConnectionLifeTime")]
        public int ConnectionLifeTime
        {
            get { return m_iConnectionLifetime; }
            set { m_iConnectionLifetime = value; }
        }

        [XmlElement("MaxPoolSize")]
        public int MaxPoolSize
        {
            get { return m_iMaxPoolSize; }
            set { m_iMaxPoolSize = value; }
        }

        [XmlElement("MinPoolSize")]
        public int MinPoolSize
        {
            get { return m_iMinPoolSize; }
            set { m_iMinPoolSize = value; }
        }

        [XmlElement("ConnectionString")]
        public string ConnectionString
        {
            get { return m_sConnectionString; }
            set { m_sConnectionString = value; }
        }
        #endregion Public Properties
    }
}
