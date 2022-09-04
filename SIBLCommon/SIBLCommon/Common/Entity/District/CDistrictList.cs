/*
 * File name            : CDistrictList.cs
 * Author               : Munirul Islam
 * Date                 : April 06, 2014
 * Version              : 1.0
 *
 * Description          : Entity Class for CAllLookupList
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
namespace SIBLCommon.Common.Entity.District
{
    [Serializable]
    public class CDistrictList : ASIBLEntityCollectionBase
    {
        protected List<CDistrict> m_liCDistrictList;
        public CDistrictList()
        {
            m_liCDistrictList = new List<CDistrict>();
        }
        public List<CDistrict> DistrictList
        {
            get { return m_liCDistrictList; }
            set { m_liCDistrictList = value; }
        }

    }
}
