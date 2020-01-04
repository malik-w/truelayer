using Interview.Wajid.Malik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Interview.Wajid.Malik.Services.HttpClients
{
    public class AuthHttpClient : IAuthHttpClient
    {
        private readonly HttpClient httpClient;
        private readonly ClientConfiguration config;

        public AuthHttpClient(HttpClient httpClient, ClientConfiguration config)
        {
            this.httpClient = httpClient;
            this.config = config;
        }

        public async Task<Credentials> GetTokenAsync(string code)
        {
            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("client_id", config.ClientID),
                new KeyValuePair<string, string>("client_secret", config.ClientSecret),
                new KeyValuePair<string, string>("redirect_uri", config.RedirectUri),
                new KeyValuePair<string, string>("code", code)
            };

            var content = new FormUrlEncodedContent(parameters);

            var response = await httpClient.PostAsync("connect/token", content);

            var credentials = await JsonSerializer.DeserializeAsync<Credentials>(await response.Content.ReadAsStreamAsync());

            return credentials;
        }
    }
}
