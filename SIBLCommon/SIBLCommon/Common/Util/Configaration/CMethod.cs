/*
 * File name            : CMethod.cs
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
    public class CMethod
    {
        #region Protected Members
        protected string s_mMethodName;
        #endregion Protected Members
        #region Constructor
        public CMethod()
        {
        }
        public CMethod(string sMethodName)
        {
            Initialization(sMethodName);
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization(string sMethodName)
        {
            s_mMethodName = sMethodName;
        }
        #endregion Initialization

        #region Public Members
        [XmlAttributeAttribute("MethodName")]
        public string MethodName
        {
            get { return s_mMethodName; }
            set { s_mMethodName = value; }
        }
        #endregion Public Members
    }
}
