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

namespace SIBLCommon.Common.Entity.File
{
    [Serializable]
    public class CFileList:ASIBLEntityCollectionBase
    {
         protected List<CFile> m_liFileList;
         public CFileList()
        {
            m_liFileList = new List<CFile>();
        }
         public List<CFile> FileList
        {
            get { return m_liFileList; }
            set { m_liFileList = value; }
        }
    }
}
