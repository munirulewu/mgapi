using SIBLCommon.Common.Entity.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SIBLCommon.SIBLCommon.Common.Entity.MGAPI
{
    [Serializable]
    [DataContract]
    public class CTransaction 
    {
        CATransaction oTransaction = new CATransaction();
        string ms_accountCode;
        string ms_accountNumber;
        string ms_OperationType;
        string ms_mesage;
        string ms_responseCode;
        public CTransaction()
        {
            oTransaction = new CATransaction();
            ms_OperationType = string.Empty;
            ms_accountCode = string.Empty;
            ms_accountNumber = string.Empty;
            ms_responseCode = string.Empty;
        }
        [DataMember]
        public CATransaction transaction
        {
            get { return oTransaction; }
            set { oTransaction = value; }
        }

        [DataMember]
        public string accountCode
        {
            get { return ms_accountCode; }
            set { ms_accountCode = value; }
        }
        /// <summary>
        /// Account Number
        /// </summary>
        [DataMember]
        public string accountNumber
        {
            get { return ms_accountNumber; }
            set { ms_accountNumber = value; }
        }

        public string message
        {
            get { return ms_mesage; }
            set { ms_mesage = value; }
        }

        public string responseCode
        {
            get { return ms_responseCode; }
            set { ms_responseCode = value; }
        } 

    }
}
