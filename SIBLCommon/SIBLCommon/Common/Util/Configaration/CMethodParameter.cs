/*
 * File name            : CMethodParameter.cs
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
using System.Xml.Serialization;

namespace SIBLCommon.Common.Util.Configuration
{
    [Serializable]
    public class CMethodParameter
    {
        #region Protected Members
        protected string m_sParameterTypeName;
        #endregion Protected Members

        #region Constructor
        public CMethodParameter()
        {

        }
        public CMethodParameter(string sParameterType)
        {
            Initialization(sParameterType);
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization(string sParameterType)
        {
            m_sParameterTypeName = sParameterType;
        }
        #endregion Initialization

        #region Public Members
        [XmlAttributeAttribute("ParameterTypeName")]
        public string ParameterTypeName
        {
            get { return m_sParameterTypeName; }
            set { m_sParameterTypeName = value; }
        }
        #endregion Public Members
    }
}
