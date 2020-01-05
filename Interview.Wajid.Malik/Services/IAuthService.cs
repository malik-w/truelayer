using Interview.Wajid.Malik.Models;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Interview.Wajid.Malik.Services
{
    public interface IAuthService
    {
        string AuthenticationUrl { get; }
        bool IsAuthenticated { get; }
        AuthenticationHeaderValue GetAuthenticationHeader();
        Task SaveCredentialsAsync(Credentials credentials);
    }
}
