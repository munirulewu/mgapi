/*
 * File name            : CToken.cs
 * Author               : Munirul Islam
 * Date                 : June 13, 2022
 * Version              : 1.0
 *
 * Description          : Result Class Entity 
 *
 * Modification history :
 * Name                         Date                            Desc
 * 
 * 
 * Copyright (c) 2022: SOCIAL ISLAMI BANK LIMITED
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIBLCommon.SIBLCommon.Common.Entity.ITCL
{
    public class CToken
    {
        string ms_bankCode;
        string ms_userName;
        
        public CToken ()
        {
            ms_bankCode = string.Empty;
            ms_userName = string.Empty;

        }

        public string userName
        {
            get { return ms_userName; }
            set { ms_userName = value; }
        }
        public string bankCode
        {
            get { return ms_bankCode; }
            set { ms_bankCode = value; }
        }
    }
}
