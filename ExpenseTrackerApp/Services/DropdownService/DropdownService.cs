using ExpenseTrackerApp.Data;
using ExpenseTrackerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApp.Services.DropdownService
{
    public class DropdownService : IDropdownService
    {
        private readonly AppDbContext _dbContext;

        public DropdownService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CategoryDropdown> GetCategoryDropdownValues()
        {
            var categoryDropdown = new CategoryDropdown()
            {
                Categories = await _dbContext.Categories.OrderBy(c => c.Id).ToListAsync()
            };

            return categoryDropdown;
        }
    }
}
