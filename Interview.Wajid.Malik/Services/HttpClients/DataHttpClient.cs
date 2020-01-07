using Interview.Wajid.Malik.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Interview.Wajid.Malik.Services.HttpClients
{
    public class DataHttpClient : IDataHttpClient
    {
        private readonly HttpClient httpClient;
        private readonly IAuthService authService;

        public DataHttpClient(HttpClient httpClient, IAuthService authService)
        {
            this.httpClient = httpClient;
            this.authService = authService;

            this.httpClient.DefaultRequestHeaders.Authorization = this.authService.GetAuthenticationHeader();
        }

        public async Task<Dictionary<string, IEnumerable<Transaction>>> GetTransactionsAsync()
        {
            var result = new Dictionary<string, IEnumerable<Transaction>>();

            var accHttpResponse = await httpClient.GetAsync("accounts");            
            var accountsResponse = await JsonSerializer.DeserializeAsync<AccountsResponse>(await accHttpResponse.Content.ReadAsStreamAsync());

            foreach (var account in accountsResponse.Accounts)
            {
                var transactionsHttpResponse = await httpClient.GetAsync($"accounts/{account.AccountID}/transactions");
                var transactionsResponse = await JsonSerializer.DeserializeAsync<TransactionsResponse>(await transactionsHttpResponse.Content.ReadAsStreamAsync());
                result.Add(account.AccountID, transactionsResponse.Transactions);
            }

            return result; 
        }
    }
}
