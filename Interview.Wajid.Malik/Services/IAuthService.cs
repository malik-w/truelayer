using Interview.Wajid.Malik.Models;
using System.Threading.Tasks;

namespace Interview.Wajid.Malik.Services
{
    public interface IAuthService
    {
        string AuthenticationUrl { get; }
        bool IsAuthenticated { get; }
        Task SaveCredentialsAsync(Credentials credentials);
    }
}
