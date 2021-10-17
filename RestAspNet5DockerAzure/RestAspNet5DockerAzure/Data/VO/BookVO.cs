using RestAspNet5DockerAzure.Hypermedia;
using RestAspNet5DockerAzure.Hypermedia.Abstract;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace RestAspNet5DockerAzure.Data.VO
{
    public class BookVO : ISupportsHyperMedia
    {
        [JsonPropertyName("id")]
        [XmlElement("id")]
        public long Id { get; set; }

        [JsonPropertyName("title")]
        [XmlElement("title")]
        public string Title { get; set; }

        [JsonPropertyName("author")]
        [XmlElement("author")]
        public string Author { get; set; }

        //[JsonIgnore]
        [JsonPropertyName("price")]
        [XmlElement("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("launch_date")]
        [XmlElement("launch_date")]
        public DateTime LaunchDate { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}

