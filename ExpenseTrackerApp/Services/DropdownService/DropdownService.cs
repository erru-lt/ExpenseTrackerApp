using ExpenseTrackerApp.Data;
using ExpenseTrackerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApp.Services.DropdownService
{
    public class DropdownService : IDropdownService
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContext;

        public DropdownService(AppDbContext dbContext, IHttpContextAccessor httpContext)
        {
            _dbContext = dbContext;
            _httpContext = httpContext;
        }

        public async Task<CategoryDropdown> GetCategoryDropdownValues()
        {
            var userId = GetUserId();

            var categoryDropdown = new CategoryDropdown()
            {
                Categories = await _dbContext.Categories.Where(c => c.OwnerId == userId || c.OwnerId == string.Empty).OrderBy(c => c.Id).ToListAsync()
            };

            return categoryDropdown;
        }

        private string? GetUserId()
        {
            return _httpContext.HttpContext?.User.Identity?.Name;
        }
    }
}
