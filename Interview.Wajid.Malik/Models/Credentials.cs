using System;
using System.Text.Json.Serialization;

namespace Interview.Wajid.Malik.Models
{
    public class Credentials
    {
        private DateTime creationDate = DateTime.Now;

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        [JsonPropertyName("expires_in")]
        public int ExpirySeconds { get; set; }
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        public DateTime ExpiryDate { get { return creationDate.AddSeconds(ExpirySeconds); } }
    }
}
