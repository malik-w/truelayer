using System.Text.Json.Serialization;

namespace Interview.Wajid.Malik.Models
{
    public class TransactionMetadata
    {
        [JsonPropertyName("bank_transaction_id")]
        public string BankTransactionID { get; set; }
        [JsonPropertyName("provider_transaction_category")]
        public string ProviderTransactionCategory { get; set; }
    }
}
