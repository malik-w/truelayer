using Interview.Wajid.Malik.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Interview.Wajid.Malik.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DBConfiguration dbConfig;

        public TransactionRepository(DBConfiguration dbConfig)
        {
            this.dbConfig = dbConfig;
        }

        public async Task<Dictionary<string, IEnumerable<Transaction>>> GetAsync()
        {
            var transactions = new Dictionary<string, IEnumerable<Transaction>>();
            var data = new List<KeyValuePair<string, Transaction>>();

            using (var conn = new SqlConnection(dbConfig.ConnectionString))
            using (var cmd = new SqlCommand("Transaction_Select", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                await conn.OpenAsync();
                
                using (var rdr = await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        var accountID = (string)rdr["AccountID"];
                        var transaction = new Transaction();
                        transaction.TransactionID = (string)rdr["TransactionID"];
                        transaction.TimeStamp = (DateTime)rdr["TimeStamp"];
                        transaction.Description = (string)rdr["Description"];
                        transaction.Amount = (decimal)rdr["Amount"];
                        transaction.Currency = (string)rdr["Currency"];
                        transaction.TransactionType = (string)rdr["TransactionType"];
                        transaction.TransactionCategory = (string)rdr["TransactionCategory"];
                        data.Add(new KeyValuePair<string, Transaction>(accountID, transaction));
                    }
                }
            }

            var transactionsGroupedByAccount = data.GroupBy(d => d.Key);
            foreach (var account in transactionsGroupedByAccount)
            {
                transactions.Add(account.Key, account.Select(t => t.Value));
            }

            return transactions;
        }

        public async Task SaveAsync(Dictionary<string, IEnumerable<Transaction>> transactions)
        {
            using (var conn = new SqlConnection(dbConfig.ConnectionString))
            using (var cmd = new SqlCommand("Transaction_Delete", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }

            var transactionTable = createTransactionTableType();

            foreach (var account in transactions)
            {
                foreach (var transaction in account.Value)
                {
                    transactionTable.Rows.Add(
                        account.Key,
                        transaction.TransactionID,
                        transaction.TimeStamp,
                        transaction.Description,
                        transaction.Amount,
                        transaction.Currency,
                        transaction.TransactionType,
                        transaction.TransactionCategory
                        );
                }
            }

            using (var sqlBulk = new SqlBulkCopy(dbConfig.ConnectionString))
            {
                sqlBulk.DestinationTableName = "Transactions";
                await sqlBulk.WriteToServerAsync(transactionTable);
            }

        }

        private DataTable createTransactionTableType()
        {
            var table = new DataTable();
            table.Columns.Add("AccountID", typeof(string));
            table.Columns.Add("TransactionID", typeof(string));
            table.Columns.Add("TimeStamp", typeof(DateTime));
            table.Columns.Add("Description", typeof(string));
            table.Columns.Add("Amount", typeof(decimal));
            table.Columns.Add("Currency", typeof(string));
            table.Columns.Add("TransactionType", typeof(string));
            table.Columns.Add("TransactionCategory", typeof(string));
            return table;
        }
    }
}
