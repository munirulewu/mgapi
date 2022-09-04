using Newtonsoft.Json;
using SIBLCommon.Common.Entity.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SIBLCommon.SIBLCommon.Common.Entity.Country
{
    [Serializable]
    [DataContract]
    public class CCountryList: ASIBLEntityCollectionBase
    {
        protected List<CCountry> m_liCountryList;
         public CCountryList()
        {
            m_liCountryList = new List<CCountry>();
        }
        [DataMember]
        [JsonProperty("Country")]
        public List<CCountry> CountryList
        {
            get { return m_liCountryList; }
            set { m_liCountryList = value; }
        }
    }
}
