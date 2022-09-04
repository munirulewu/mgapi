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
    public class CRemitInfoList : ASIBLEntityCollectionBase
    {
        protected List<CRemitInfo> m_liRemitInfoList;
         public CRemitInfoList()
        {
            m_liRemitInfoList = new List<CRemitInfo>();
        }
         public List<CRemitInfo> RemitInfoList
        {
            get { return m_liRemitInfoList; }
            set { m_liRemitInfoList = value; }
        }
    }
}
