/*
 * File name            : CAccDepositResponse.cs
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
    public class CAccDepositResponse
    {
        string ms_AuthResponseCode;
        string ms_Info;
        string ms_Response;
        string ms_TranId;
        public CAccDepositResponse()
        {
            ms_AuthResponseCode = string.Empty;
            ms_Info = string.Empty;
            ms_Response = string.Empty;
            ms_TranId = string.Empty;

        }

        public string TranId
        {
            get { return ms_TranId; }
            set { ms_TranId = value; }
        }
        public string AuthResponseCode
        {
            get { return ms_AuthResponseCode; }
            set { ms_AuthResponseCode = value; }
        }
        public string Info
        {
            get { return ms_Info; }
            set { ms_Info = value; }
        }
        public string Response
        {
            get { return ms_Response; }
            set { ms_Response = value; }
        }

    }
}
