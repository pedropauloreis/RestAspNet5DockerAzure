using RestAspNet5DockerAzure.Hypermedia;
using RestAspNet5DockerAzure.Hypermedia.Abstract;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace RestAspNet5DockerAzure.Data.VO
{
    public class UserVO : ISupportsHyperMedia
    {
        [JsonPropertyName("id")]
        [XmlElement("id")]
        public long Id { get; set; }

        [JsonPropertyName("user_name")]
        [XmlElement("user_name")]
        public string UserName { get; set; }

        [JsonPropertyName("full_name")]
        [XmlElement("full_name")]
        public string FullName { get; set; }

        [JsonPropertyName("password")]
        [XmlElement("password")]
        public string Password { get; set; }

        [JsonPropertyName("refresh_token")]
        [XmlElement("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("refresh_token_expiry_time")]
        [XmlElement("refresh_token_expiry_time")]
        public DateTime RefreshTokenExpiryTime { get; set; }

        [JsonPropertyName("enabled")]
        [XmlElement("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("roles")]
        [XmlElement("roles")]
        public List<RoleVO> Roles { get; set; }


        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}

