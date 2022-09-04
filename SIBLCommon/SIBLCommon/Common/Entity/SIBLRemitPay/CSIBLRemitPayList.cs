/*
 * File name            : CSIBLRemitPayList.cs
 * Author               : Munirul Islam
 * Date                 : January 21.2020
 * Version              : 1.0
 *
 * Description          : Entity List Class for SIBLRemitPayment
 *
 * Modification history :
 * Name                         Date                            Desc
 * 
 * 
 * Copyright (c) 2020: SOCIAL ISLAMI BANK LIMITED
 */

using SIBLCommon.Common.Entity.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIBLCommon.SIBLCommon.Common.Entity.SIBLRemitPay
{
    [Serializable]
    public class CSIBLRemitPayList:ASIBLEntityCollectionBase
    {
         protected List<CSIBLRemitPay> m_liSIBLRemitPayList;
         public CSIBLRemitPayList()
        {
            m_liSIBLRemitPayList = new List<CSIBLRemitPay>();
        }
        public List<CSIBLRemitPay> SIBLRemitPayList
        {
            get { return m_liSIBLRemitPayList; }
            set { m_liSIBLRemitPayList = value; }   
        }
    }
}
