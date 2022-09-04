using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIBLCommon.SIBLCommon.Common.Entity.MGAPI
{
    public class CSender
    {
        CPerson oSender;
        public CSender()
        {
            oSender = new CPerson();
        }

        public CPerson person
        {
            get { return oSender; }
            set { oSender = value; }
        }
    }
}
