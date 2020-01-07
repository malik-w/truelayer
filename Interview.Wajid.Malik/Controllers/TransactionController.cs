using Interview.Wajid.Malik.Repositories;
using Interview.Wajid.Malik.Services.HttpClients;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Interview.Wajid.Malik.Controllers
{
    [Route("users/{userID}/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private IDataHttpClient dataHttpClient;
        private ITransactionRepository transactionRepository;

        public TransactionController(IDataHttpClient dataHttpClient, ITransactionRepository transactionRepository)
        {
            this.dataHttpClient = dataHttpClient;
            this.transactionRepository = transactionRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var transactions = await dataHttpClient.GetTransactionsAsync();
            await transactionRepository.SaveAsync(transactions);
            return new ObjectResult(transactions);
        }
    }
}