using RestAspNet5DockerAzure.Hypermedia;
using RestAspNet5DockerAzure.Hypermedia.Abstract;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace RestAspNet5DockerAzure.Data.VO
{
    public class DepartmentVO : ISupportsHyperMedia
    {
        [JsonPropertyName("id")]
        [XmlElement("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        [XmlElement("name")]
        public string Name { get; set; }

        
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}

