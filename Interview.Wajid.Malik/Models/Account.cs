using System.Text.Json.Serialization;

namespace Interview.Wajid.Malik.Models
{
    public class Account
    {
        [JsonPropertyName("account_id")]
        public string AccountID { get; set; }
    }
}
