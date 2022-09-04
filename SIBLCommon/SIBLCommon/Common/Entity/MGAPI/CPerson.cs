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
    public class CPerson
    {
        protected string ms_firstName;
        protected string ms_middleName;
        protected string ms_lastName;
        protected string ms_SecondLastName;
        protected string ms_CountryCode;
        protected string ms_mobileNo;
        protected string ms_Address;
        protected string ms_fullName;
        public CPerson()
        {
            ms_firstName = string.Empty;
            ms_middleName = string.Empty;
            ms_lastName = string.Empty;
            ms_SecondLastName=string.Empty;
            ms_CountryCode = string.Empty;
            ms_mobileNo = string.Empty;
            ms_Address = string.Empty;
            ms_fullName = string.Empty;
        }
        [DataMember]
        public string firstName
        {
            get { return ms_firstName; }
            set { ms_firstName = value; }
        }
          [DataMember]
        public string middleName
        {
            get { return ms_middleName; }
            set { ms_middleName = value; }
        }
          [DataMember]
        public string lastName
        {
            get { return ms_lastName; }
            set { ms_lastName = value; }
        }
        [DataMember]
        public string SecondLastName
        {
            get { return ms_SecondLastName; }
            set { ms_SecondLastName = value; }
        }
        public string CountryCode
        {
            get { return ms_CountryCode; }
            set { ms_CountryCode = value; }
        }
        public string mobileNo
        {
            get { return ms_mobileNo; }
            set { ms_mobileNo = value; }
        }
        public string address
        {
            get { return ms_Address; }
            set { ms_Address = value; }
        }
        public string fullName
        {
            get { return ms_fullName; }
            set { ms_fullName = value; }
        }
    }
}
