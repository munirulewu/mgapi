using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIBLCommon.SIBLCommon.Common.Entity.MGAPI
{
    public class CAmount
    {
        protected  string svalue { get; set; }
        protected string scurrencyCode { get; set; }

        public CAmount()
        {
            svalue = string.Empty;
            scurrencyCode = string.Empty;

        }

        public string value
        {
            get { return svalue; }
            set { svalue = value; }
        }

        public string currencyCode
        {
            get { return scurrencyCode; }
            set { scurrencyCode = value; }
        }
    }
}
