using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace geckoboardcsharp.Models
{
    public class DatasetField : IDatasetField
    {
        [JsonConstructor]
        public DatasetField(DatasetFieldType type, string name = null, string currencyCode = null, string defaultValue = null)
        {
            this.Type = type;
            this.Name = name;
            this.CurrencyCode = currencyCode;
            Optional = false;
            DefaultValue = defaultValue;
        }

        [DataMember(Name = "name", IsRequired = true), XmlElement("name"), JsonProperty("name")]
        public string Name { get; set; }
        public bool ShouldSerializeName() { return !string.IsNullOrEmpty(Name); }

        [DataMember(Name = "type", IsRequired = true), XmlElement("type"), JsonProperty("type"), JsonConverter(typeof(StringEnumConverter))]
        public DatasetFieldType Type { get; set; }

        [DataMember(Name = "currency_code", IsRequired = false), XmlElement("currency_code"), JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }
        public bool ShouldSerializeCurrencyCode() { return !string.IsNullOrEmpty(CurrencyCode); }

        [DataMember(Name = "optional", IsRequired = false), XmlElement("optional"), JsonProperty("optional")]
        public bool? Optional { get; set; }
        public bool ShouldSerializeOptional() { return Optional.HasValue; }

        [DataMember(Name = "defaultvalue", IsRequired = false), XmlElement("defaultvalue"), JsonProperty("defaultvalue")]
        public string DefaultValue { get; set; }
        public bool ShouldSerializeDefaultValue() { return !string.IsNullOrEmpty(DefaultValue); }
    }
}
