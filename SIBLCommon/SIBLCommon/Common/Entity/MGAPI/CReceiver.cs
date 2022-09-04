using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIBLCommon.SIBLCommon.Common.Entity.MGAPI
{
    public class CReceiver
    {
        CPerson oReceiver;
        public CReceiver()
        {
            oReceiver = new CPerson();
        }

        public CPerson person
        {
            get { return oReceiver; }
            set { oReceiver = value; }
        }
    }
}
