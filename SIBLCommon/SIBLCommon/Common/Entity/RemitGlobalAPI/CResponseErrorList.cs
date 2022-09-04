/*
 * File name            : CFileList.cs
 * Author               : Munirul Islam
 * Date                 : August 03, 2014
 * Version              : 1.0
 *
 * Description          : Entity Class for CFileList
 *
 * Modification history :
 * Name                         Date                            Desc
 * 
 * 
 * Copyright (c) 2014: SOCIAL ISLAMI BANK LIMITED
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.SIBLCommon.Common.Entity.RemitGlobalAPI;

namespace SIBLCommon.SIBLCommon.Common.Entity.RemitGlobalAPI
{
    [Serializable]
    public class CResponseErrorList : ASIBLEntityCollectionBase
    {
        protected List<CResponseError> m_liResponseErrorList;
        public CResponseErrorList()
        {
            m_liResponseErrorList = new List<CResponseError>();
        }
        public List<CResponseError> ResponseErrorList
        {
            get { return m_liResponseErrorList; }
            set { m_liResponseErrorList = value; }
        }
    }
}
