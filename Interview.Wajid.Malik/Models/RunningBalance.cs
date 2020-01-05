using System.Text.Json.Serialization;

namespace Interview.Wajid.Malik.Models
{
    public class RunningBalance
    {
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }
}
