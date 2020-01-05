using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Interview.Wajid.Malik.Models
{
    public class TransactionsResponse
    {
        [JsonPropertyName("results")]
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
