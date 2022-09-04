using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIBLCommon.SIBLCommon.Common.Entity.MGAPI
{
    [Serializable]
   public class CAdditionData
    {
        string sKey;
        string sValue;
        public CAdditionData()
        {
            sKey = string.Empty;
            sValue = string.Empty;
        }

        public string value
        {
            get { return sValue; }
            set { sValue = value; }
        }

        public string key
        {
            get { return sKey; }
            set { sKey = value; }
        }
    }
}
