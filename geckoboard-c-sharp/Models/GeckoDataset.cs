using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace geckoboardcsharp.Models
{
    [DataContract(Namespace = ""), XmlRoot("root", Namespace = "")]
    public class GeckoDataset
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

       
    }
}
