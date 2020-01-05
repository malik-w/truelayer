using Interview.Wajid.Malik.Controllers;
using Interview.Wajid.Malik.Models;
using Interview.Wajid.Malik.Repositories;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interview.Wajid.Malik.Tests.Controllers
{
    [TestFixture]
    public class TransactionCategoryControllerTests
    {
        TransactionCategoryController sut;
        Mock<ITransactionRepository> mockTransactionRepository;

        [SetUp]
        public void SetUp()
        {
            mockTransactionRepository = new Mock<ITransactionRepository>();
            mockTransactionRepository.Setup(m => m.GetAsync()).ReturnsAsync(new Dictionary<string, IEnumerable<Transaction>>());
            sut = new TransactionCategoryController(mockTransactionRepository.Object);
        }

        public class GetStats : TransactionCategoryControllerTests
        {
            private bool compareStats(TransactionCategoryStats stats, decimal min, decimal max, decimal average)
            {
                return stats != null
                    && stats.MinAmount == min
                    && stats.MaxAmount == max
                    && stats.AverageAmount == average;
            }

            [Test]
            public async Task ShouldReturnEmptyObject_WhenNoTransactionsExist()
            {
                var result = await sut.GetStats();

                Assert.Zero(result.Value.Count);
            }

            [Test]
            public async Task ShouldReturnTransactionAmount_WhenSingleTransactionExists()
            {
                var transaction = new Transaction() { TransactionCategory = "DIRECT_DEBIT", Amount = 10m };
                var transactions = new Dictionary<string, IEnumerable<Transaction>>()
                {
                    { "AccountID1", new List<Transaction>() { transaction } }
                };
                mockTransactionRepository.Setup(m => m.GetAsync()).ReturnsAsync(transactions);

                var result = await sut.GetStats();

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(1, result.Value.Count);
                    Assert.AreEqual("DIRECT_DEBIT", result.Value.First().Key);
                    Assert.IsTrue(compareStats(result.Value.First().Value, 10m, 10m, 10m));
                });
            }

            [Test]
            public async Task ShouldCalculateStats_WhenASingleCategoryExists()
            {
                var transactions = new List<Transaction>()
                {
                    new Transaction() { TransactionCategory = "DIRECT_DEBIT", Amount = 20m },
                    new Transaction() { TransactionCategory = "DIRECT_DEBIT", Amount = -30m }
                };
                var accountTransactions = new Dictionary<string, IEnumerable<Transaction>>()
                {
                    { "AccountID1", transactions }
                };
                mockTransactionRepository.Setup(m => m.GetAsync()).ReturnsAsync(accountTransactions);

                var result = await sut.GetStats();

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(1, result.Value.Count, 1);
                    Assert.AreEqual("DIRECT_DEBIT", result.Value.First().Key);
                    Assert.IsTrue(compareStats(result.Value.First().Value, -30m, 20m, -5m));
                });
            }

            [Test]
            public async Task ShouldCalculateStats_WhenMultipleCategoriesExist()
            {
                var transactions = new List<Transaction>()
                {
                    new Transaction() { TransactionCategory = "DIRECT_DEBIT", Amount = 20m },
                    new Transaction() { TransactionCategory = "TRANSFER", Amount = -30m }
                };
                var accountTransactions = new Dictionary<string, IEnumerable<Transaction>>()
                {
                    { "AccountID1", transactions }
                };
                mockTransactionRepository.Setup(m => m.GetAsync()).ReturnsAsync(accountTransactions);

                var result = await sut.GetStats();

                Assert.AreEqual(2, result.Value.Count);
            }

            [Test]
            public async Task ShouldCalculateStats_WhenMultipleAccountsExist()
            {
                var transaction1 = new Transaction() { TransactionCategory = "DIRECT_DEBIT", Amount = 20m };
                var transaction2 = new Transaction() { TransactionCategory = "DIRECT_DEBIT", Amount = -30m };
                var accountTransactions = new Dictionary<string, IEnumerable<Transaction>>()
                {
                    { "AccountID1", new List<Transaction>() { transaction1 } },
                    { "AccountID2", new List<Transaction>() { transaction2 } }
                };
                mockTransactionRepository.Setup(m => m.GetAsync()).ReturnsAsync(accountTransactions);

                var result = await sut.GetStats();

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(1, result.Value.Count);
                    Assert.AreEqual("DIRECT_DEBIT", result.Value.First().Key);
                    Assert.IsTrue(compareStats(result.Value.First().Value, -30m, 20m, -5m));
                });
            }

            [Test]
            public async Task ShouldCalculateStats_WhenMultipleCategoriesExistAcrossMultipleAccounts()
            {
                var accTransactions1 = new List<Transaction>()
                {
                    new Transaction() { TransactionCategory = "DIRECT_DEBIT", Amount = 10m },
                    new Transaction() { TransactionCategory = "DIRECT_DEBIT", Amount = 20m },
                    new Transaction() { TransactionCategory = "TRANSFER", Amount = -50m },
                    new Transaction() { TransactionCategory = "CASH", Amount = 10m }
                };
                var accTransactions2 = new List<Transaction>()
                {
                    new Transaction() { TransactionCategory = "DIRECT_DEBIT", Amount = 30m },
                    new Transaction() { TransactionCategory = "TRANSFER", Amount = 30m },
                };
                var accountTransactions = new Dictionary<string, IEnumerable<Transaction>>()
                {
                    { "AccountID1", accTransactions1 },
                    { "AccountID2", accTransactions2 }
                };
                mockTransactionRepository.Setup(m => m.GetAsync()).ReturnsAsync(accountTransactions);

                var result = await sut.GetStats();

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(3, result.Value.Count);
                    Assert.IsTrue(compareStats(result.Value["DIRECT_DEBIT"], 10m, 30m, 20m));
                    Assert.IsTrue(compareStats(result.Value["TRANSFER"], -50m, 30m, -10m));
                    Assert.IsTrue(compareStats(result.Value["CASH"], 10m, 10m, 10m));
                });
            }
        }
    }
}
