using Interview.Wajid.Malik.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interview.Wajid.Malik.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private Dictionary<string, IEnumerable<Transaction>> transactions = new Dictionary<string, IEnumerable<Transaction>>();

        public async Task<Dictionary<string, IEnumerable<Transaction>>> GetAsync()
        {
            return transactions;
        }

        public async Task SaveAsync(Dictionary<string, IEnumerable<Transaction>> transactions)
        {
            this.transactions = transactions;
        }
    }
}
