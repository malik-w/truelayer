using System.Text.Json.Serialization;

namespace Interview.Wajid.Malik.Models
{
    public class ClientConfiguration
    {
        [JsonPropertyName("client_id")]
        public string ClientID { get; set; }
        [JsonPropertyName("client_secret")]
        public string ClientSecret { get; set; }
        [JsonPropertyName("redirect_uri")]
        public string RedirectUri { get; set; }
        [JsonPropertyName("auth_link")]
        public string AuthenticationUrl { get; set; }
    }
}
