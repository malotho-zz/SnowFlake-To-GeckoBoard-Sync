using System;
using System.IO;
using geckoboardcsharp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Geckonet.Core.Serialization
{
   public class DatasetFieldConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(DatasetField));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);

            string Name = jo["name"].ToString();
            DatasetFieldType DatasetFieldType =(DatasetFieldType) Enum.Parse(typeof(DatasetFieldType), jo["type"]?.ToString(), true);
            string currency_code = (string)jo["currency_code"];
            string defaultvalue = (string)jo["defaultvalue"];
            DatasetField result = new DatasetField(DatasetFieldType, Name, currency_code, defaultvalue);


            return result;
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
