
        /*
 * File name            : CResponse.cs
 * Author               : Munirul Islam
 * Date                 : April 23, 2020
 * Version              : 1.0
 *
 * Description          : Entity Class for CResponse
 *
 * Modification history :
 * Name                         Date                            Desc
 * 
 * 
 * Copyright (c) 2020: SOCIAL ISLAMI BANK LIMITED
 */

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SIBLCommon.Common.Entity.Bases;
using SIBLCommon.SIBLCommon.Common.Entity.Country;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SIBLCommon.SIBLCommon.Common.Entity.RemitGlobalAPI
{
    public class SingleOrArrayConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(List<T>));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Array)
            {
                return token.ToObject<List<T>>();
            }
            return new List<T> { token.ToObject<T>() };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            List<T> list = (List<T>)value;
            if (list.Count == 1)
            {
                value = list[0];
            }
            serializer.Serialize(writer, value);
        }

        public override bool CanWrite
        {
            get { return true; }
        }
    }

    [Serializable]
    [DataContract]
    public class CResponseDAS
    {

        #region Protectd Member

        protected string m_sresponseCode;
        protected string m_sresponseMessage;
        protected string ms_MoreData;
        protected string ms_TotalRecord;

        #endregion


        #region Constructor
        public CResponseDAS()
            : base()
        {
            Initialization();
        }
        #endregion Constructor

        #region Initialization
        protected void Initialization()
        {

            m_sresponseCode = string.Empty;
            m_sresponseMessage = string.Empty;
            ms_MoreData = string.Empty;
            ms_TotalRecord = string.Empty;
        }
        #endregion Initialization

        #region public Member


        [DataMember]
        [Display(Order = 1)]
        public string ResponseCode
        {
            get { return m_sresponseCode; }
            set { m_sresponseCode = value; }
        }
        [DataMember]
        [Display(Order = 2)]
        public string ResponseMessage
        {
            get { return m_sresponseMessage; }
            set { m_sresponseMessage = value; }
        }

        [DataMember]
        public string MoreData
        {
            get { return ms_MoreData; }
            set { ms_MoreData = value; }
        }
        [DataMember]
        public string TotalRecord
        {
            get { return ms_TotalRecord; }
            set { ms_TotalRecord = value; }
        }

      //  [DataMember]
      ////  [JsonProperty("CountryList")]
      // // [JsonConverter(typeof(SingleOrArrayConverter<CCountry>))]
      //  public CCountryList CountryList
      //  {
      //      get { return oCountryList; }
      //      set { oCountryList = value; }
      //  }

       
        #endregion
    }

}
