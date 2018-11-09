using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace geckoboardcsharp.Models
{
    public class ConfigurationDataset
    {
        [DataMember(Name = "fields", IsRequired = false), XmlElement("fields"), JsonProperty("fields")]
        public Dictionary<string, DatasetField> Fields { get; set; }
        public bool ShouldSerializeFields() { return (Fields != null) && (Fields.Count > 0); }

        [DataMember(Name = "data", IsRequired = false), XmlElement("data"), JsonProperty("data")]
        public List<Dictionary<string, object>> Data { get; set; }
        public bool ShouldSerializeData() { return (Data != null) && (Data.Count > 0); }

        [DataMember(Name = "unique_by", IsRequired = false), XmlElement("unique_by"), JsonProperty("unique_by")]
        public List<string> UniqueBy { get; set; }
        public bool ShouldSerializeUniqueBy() { return (UniqueBy != null) && (UniqueBy.Count > 0); }
        [DataMember(Name = "datasetname", IsRequired = true), XmlElement("datasetname"), JsonProperty("datasetname")]
        public string DataSetName { get; set; }

        [DataMember(Name = "datastatement", IsRequired = true), XmlElement("datastatement"), JsonProperty("datastatement")]
        public string DataStatement { get; set; }

        [DataMember(Name = "pollinterval", IsRequired = true), XmlElement("pollinterval"), JsonProperty("pollinterval")]
        public int PollInterval { get; set; }

        [DataMember(Name = "geckoboadapikey", IsRequired = true), XmlElement("geckoboadapikey"), JsonProperty("geckoboadapikey")]
        public string GeckoBoadApiKey { get; set; }

        [DataMember(Name = "snowflakeconnection", IsRequired = true), XmlElement("snowflakeconnection"), JsonProperty("snowflakeconnection")]
        public string SnowflakeConnection { get; set; }
    }
}
