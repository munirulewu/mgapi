/*
 * File name            : CDatabaseConfiguration.cs
 * Author: Munirul Islam
 * Date: March 27, 2014
 * Version: 1.0
 *
 *  Entity class to keep the List of Database Configuration information
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
using SIBLCommon.Common.Entity.Bases;
using System.Xml.Serialization;

namespace SIBLCommon.Common.Util.Configuration
{
    [Serializable]
    [XmlRoot("Configuration")]
    public class CDatabaseConfiguration
    {
        protected List<CDatabase> m_liDatabaseList;

        public CDatabaseConfiguration()
        {
            Initialization();
        }
        public void Initialization()
        {
            m_liDatabaseList = new List<CDatabase>();
        }

        [XmlElement("Database")]
        public List<CDatabase> Database
        {
            get { return m_liDatabaseList; }
            set { m_liDatabaseList = value; }
        }
    }
}
