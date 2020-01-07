using Interview.Wajid.Malik.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interview.Wajid.Malik.Repositories
{
    public interface ITransactionRepository
    {
        Task DeleteAsync();
        Task<Dictionary<string, IEnumerable<Transaction>>> GetAsync();
        Task SaveAsync(Dictionary<string, IEnumerable<Transaction>> transactions);
    }
}
