using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIBLCommon.SIBLCommon.Common.Entity.MGAPI
{
    public class CMgBkashSuccess
    {
        string smessage ;
        public CMgBkashSuccess()
        {
            smessage = "";
        }

        public string message
        {
            get { return smessage; }
            set { smessage = value; }
        }
    }
}
