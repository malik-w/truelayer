using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Interview.Wajid.Malik.Models
{
    public class AccountsResponse
    {
        [JsonPropertyName("results")]
        public IEnumerable<Account> Accounts { get; set; }
    }
}
