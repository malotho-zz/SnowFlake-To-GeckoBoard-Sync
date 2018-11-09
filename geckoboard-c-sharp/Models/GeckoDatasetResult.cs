using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace geckoboardcsharp.Models
{
    public class GeckoDatasetResult : GeckoDataset
    {
        [DataMember(Name = "id", IsRequired = false), XmlElement("id"), JsonProperty("id")]
        public string Id { get; set; }

        [DataMember(Name = "created_at", IsRequired = false), XmlElement("created_at"), JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "updated_at", IsRequired = false), XmlElement("updated_at"), JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
