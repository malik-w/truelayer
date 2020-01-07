using Interview.Wajid.Malik.Models;
using System.Threading.Tasks;

namespace Interview.Wajid.Malik.Services.HttpClients
{
    public interface IAuthHttpClient
    {
        Task<Credentials> GetTokenAsync(string code);
    }
}
