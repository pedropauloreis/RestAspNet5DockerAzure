using RestAspNet5DockerAzure.Hypermedia;
using RestAspNet5DockerAzure.Hypermedia.Abstract;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace RestAspNet5DockerAzure.Data.VO
{
    public class PersonVO : ISupportsHyperMedia
    {
        [JsonPropertyName("id")]
        [XmlElement("id")]
        public long Id { get; set; }

        [JsonPropertyName("first_name")]
        [XmlElement("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        [XmlElement("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("address")]
        [XmlElement("address")]
        public string Address { get; set; }

        //[JsonIgnore]
        [JsonPropertyName("gender")]
        [XmlElement("gender")]
        public string Gender { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
