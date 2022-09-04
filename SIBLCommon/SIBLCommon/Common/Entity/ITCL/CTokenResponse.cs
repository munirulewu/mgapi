/*
 * File name            : CTokenResponse.cs
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
    public class CTokenResponse
    {
        string ms_bankCode;
        string ms_keyID;
        string ms_key1;
        string ms_key2;
        CAccDepositResponse oResponse;
        public CTokenResponse()
        {
            ms_bankCode = string.Empty;
            ms_keyID = string.Empty;
            ms_key1 = string.Empty;
            ms_key2 = string.Empty;
            oResponse = new CAccDepositResponse();
        }

        public CAccDepositResponse OutParameter
        {
            get { return oResponse; }
            set { oResponse = value; }
        }

        public string bankCode
        {
            get { return ms_bankCode; }
            set { ms_bankCode = value; }
        }
        public string keyID
        {
            get { return ms_keyID; }
            set { ms_keyID = value; }
        }
        public string key1
        {
            get { return ms_key1; }
            set { ms_key1 = value; }
        }
        public string key2
        {
            get { return ms_key2; }
            set { ms_key2 = value; }
        }

    }
}
