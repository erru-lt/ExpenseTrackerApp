using ExpenseTrackerApp.Data;
using ExpenseTrackerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApp.Services.TransactionService
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _context;

        public TransactionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            var transactions = await _context.Transactions.Include(t => t.Category).ToListAsync();
            return transactions;
        }

        public async Task<Transaction> GetTransactionByIdAsync(int id)
        {
            var transaction = await _context.Transactions.Include(t => t.Category).SingleOrDefaultAsync(t => t.Id == id);
            return transaction;
        }

        public async Task AddNewTransactionAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            var dbTransaction = await GetTransactionByIdAsync(transaction.Id);

            if (transaction != null)
            {
                dbTransaction.Description = transaction.Description;
                dbTransaction.Amount = transaction.Amount;

                await _context.SaveChangesAsync();
            }
            //_context.Attach(transaction).State = EntityState.Modified;
        }

        public async Task DeleteTransactionAsync(int id)
        {
            Transaction? transaction = await GetTransactionByIdAsync(id);
            _context.Remove(transaction);
            await _context.SaveChangesAsync();
        }
    }
}
