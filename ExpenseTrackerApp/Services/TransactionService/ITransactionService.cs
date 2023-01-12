using ExpenseTrackerApp.Models;

namespace ExpenseTrackerApp.Services.TransactionService
{
    public interface ITransactionService
    {
        Task AddNewTransactionAsync(Transaction transaction);
        Task DeleteTransactionAsync(int id);
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
        Task<Transaction> GetTransactionByIdAsync(int id);
        Task UpdateTransactionAsync(Transaction transaction);
    }
}