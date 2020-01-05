using Interview.Wajid.Malik.Models;
using Interview.Wajid.Malik.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interview.Wajid.Malik.Controllers
{
    [Route("users/{userID}/transactions/categories")]
    [ApiController]
    public class TransactionCategoryController : ControllerBase
    {
        private readonly ITransactionRepository transactionRepository;

        public TransactionCategoryController(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }

        [HttpGet("stats")]
        public async Task<ActionResult<Dictionary<string, TransactionCategoryStats>>> GetStats()
        {
            var result = new Dictionary<string, TransactionCategoryStats>();

            var transactions = await transactionRepository.GetAsync();

            var flattenedTransactions = transactions.SelectMany(kv => kv.Value);
            var transactionsByCategory = flattenedTransactions.GroupBy(t => t.TransactionCategory);

            foreach (var category in transactionsByCategory)
            {
                var stats = new TransactionCategoryStats();
                stats.MinAmount = category.Min(t => t.Amount);
                stats.MaxAmount = category.Max(t => t.Amount);
                stats.AverageAmount = category.Average(t => t.Amount);
                result.Add(category.Key, stats);
            }

            return result;
        }
    }
}
