using System;
using System.Text.Json.Serialization;

namespace Interview.Wajid.Malik.Models
{
    public class Transaction
    {
        [JsonPropertyName("transaction_id")]
        public string TransactionID { get; set; }
        [JsonPropertyName("timestamp")]
        public DateTime TimeStamp { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
        [JsonPropertyName("transaction_type")]
        public string TransactionType { get; set; }
        [JsonPropertyName("transaction_category")]
        public string TransactionCategory { get; set; }
    }
}
