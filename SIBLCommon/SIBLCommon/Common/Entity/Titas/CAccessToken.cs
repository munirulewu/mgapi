using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIBLCommon.Common.Entity.Bases;

namespace SIBLCommon.Common.Entity.Titas
{    
    [Serializable]
    public class CAccessToken : ASIBLEntityBase
    {
        protected string saccess_token;
        protected string sexpires_in;
        protected string stoken_type;
        protected string sCreator;
        protected string sCreationTime;
        protected string sScope;
        protected string sTokenDate;
        protected string sMeterType;


        public CAccessToken()
        {
            saccess_token = string.Empty;
            sexpires_in = string.Empty;
            token_type = string.Empty;
            sCreator = string.Empty;
            sCreationTime = string.Empty;
            sScope = string.Empty;
            sTokenDate = string.Empty;
            sMeterType = string.Empty;

        }


        public string access_token
        {
            get { return saccess_token; }
            set { saccess_token = value; }
        }

        public string expires_in
        {
            get { return sexpires_in; }
            set { sexpires_in = value; }
        }

        public string token_type
        {
            get { return stoken_type; }
            set { stoken_type = value; }
        }


        public string Creator
        {
            get { return sCreator; }
            set { sCreator = value; }
        }
        public string CreationTime
        {
            get { return sCreationTime; }
            set { sCreationTime = value; }
        }
        public string Scope
        {
            get { return sScope; }
            set { sScope = value; }
        }

        public string TokenDate
        {
            get { return sTokenDate; }
            set { sTokenDate = value; }
        }

        public string MeterType
        {
            get { return sMeterType; }
            set { sMeterType = value; }
        }


    }
}
