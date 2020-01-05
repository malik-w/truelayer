using Interview.Wajid.Malik.Models;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Interview.Wajid.Malik.Services
{
    public class AuthService : IAuthService
    {
        private readonly ClientConfiguration config;
        private Credentials credentials;

        public AuthService(ClientConfiguration config)
        {
            this.config = config;
        }

        public string AuthenticationUrl
        {
            get { return config.AuthenticationUrl; }
        }

        public bool IsAuthenticated
        {
            get { return credentials != null; }
        }

        public AuthenticationHeaderValue GetAuthenticationHeader()
        {
            return new AuthenticationHeaderValue("Bearer", credentials.AccessToken);
        }

        public async Task SaveCredentialsAsync(Credentials credentials)
        {
            this.credentials = credentials;
        }
    }
}
