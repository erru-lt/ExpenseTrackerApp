using ExpenseTrackerApp.Data;
using ExpenseTrackerApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExpenseTrackerApp.Services.TransactionService
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public TransactionService(AppDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            var userId = GetUserId();
            var transactions = await _context.Transactions.Where(t => t.OwnerId == userId).Include(t => t.Category).ToListAsync();
            return transactions;
        }


        public async Task<Transaction> GetTransactionByIdAsync(int id)
        {
            var transaction = await _context.Transactions.Include(t => t.Category).SingleOrDefaultAsync(t => t.Id == id);
            return transaction;
        }

        public async Task AddNewTransactionAsync(Transaction transaction)
        {
            var userId = GetUserId();
            transaction.OwnerId = userId;

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
        }

        public async Task DeleteTransactionAsync(int id)
        {
            Transaction? transaction = await GetTransactionByIdAsync(id);
            _context.Remove(transaction);
            await _context.SaveChangesAsync();
        }
        
        private string? GetUserId()
        {
            return _httpContext?.HttpContext?.User?.Identity?.Name;
        }
    }
}
