/*
 * File name            : CRequest.cs
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
using System.Collections;
using System.Xml.Serialization;
using SIBLCommon.Common.Entity.Bases;


namespace SIBLCommon.Common.Util.Configuration
{
    [Serializable]
    [XmlInclude(typeof(CRequest))]
    public class CRequest
    {
        #region Protected Members
        protected string m_sRequestName;
        protected List<CMethod> m_liMethodList;
        protected string m_sMapObjectPath;
        protected List<CMethodParameter> m_liParameterList;
        #endregion Protected Members

        #region Constructor
        public CRequest()
        {
            Initialization(string.Empty, string.Empty);
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization(string sRequestName, string sMapObjectPath)
        {
            m_sRequestName = sRequestName;
            m_liMethodList = new List<CMethod>();
            m_sMapObjectPath = sMapObjectPath;
            m_liParameterList = new List<CMethodParameter>();
        }
        #endregion Initialization

        #region Public Members
        [XmlAttributeAttribute("RequestName")]
        public string RequestName
        {
            get { return m_sRequestName; }
            set { m_sRequestName = value; }
        }

        [XmlArrayAttribute("MethodList")]
        public List<CMethod> MethodList
        {
            get { return m_liMethodList; }
            set { m_liMethodList = value; }
        }

        [XmlAttributeAttribute("MapObjectPath")]
        public string MapObjectPath
        {
            get { return m_sMapObjectPath; }
            set { m_sMapObjectPath = value; }
        }

        [XmlArrayAttribute("ParameterList")]
        public List<CMethodParameter> ParameterList
        {
            get { return m_liParameterList; }
            set { m_liParameterList = value; }
        }
        #endregion Public Members
    }
}
