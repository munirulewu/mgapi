/*
 * File name            : CRequestList.cs
 * Author               : Munirul Islam
 * Date                 : March 27, 2014
 * Version              : 1.0
 *
 * Description          : Entity class to keep the List of Database Configuration information
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
using SIBLCommon.Common.Util.Configuration;
using System.Xml.Serialization;

namespace SIBLCommon.Common.Util.Configuration
{
    [Serializable]
    [XmlInclude(typeof(CRequestList))]
    [XmlRoot("CRequestList")]
    public class CRequestList
    {
        #region Protected Members
        protected List<CRequest> m_liRequests; 
        #endregion Protected Members
        public CRequestList()
        {
            Initialization();
        }
        #region Initialization
        protected void Initialization()
        {
            m_liRequests = new List<CRequest>();
        }
        #endregion Initialization
        [XmlArrayAttribute("Requests")]
        public List<CRequest> Requests
        {
            get { return m_liRequests; }
            set { m_liRequests = value; }
        }
    }
}
