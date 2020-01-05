using Interview.Wajid.Malik.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interview.Wajid.Malik.Services.HttpClients
{
    public interface IDataHttpClient
    {
        Task<Dictionary<string, IEnumerable<Transaction>>> GetTransactionsAsync();
    }
}
