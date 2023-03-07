using ExpenseTrackerApp.Data;
using ExpenseTrackerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApp.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public CategoryService(AppDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            var userId = GetUserId();
            var categories = await _context.Categories.Where(c => c.OwnerId == userId || c.OwnerId == string.Empty).ToListAsync();
            return categories;
        }
        
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public async Task AddNewCategoryAsync(Category category)
        {
            var userId = GetUserId();
            category.OwnerId = userId;

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            var dbCategory = await GetCategoryByIdAsync(category.Id);

            if(dbCategory != null)
            {
                dbCategory.CategoryType = category.CategoryType;
                dbCategory.Icon = category.Icon;
                dbCategory.Name = category.Name;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await GetCategoryByIdAsync(id);

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        private string? GetUserId()
        {
            return _httpContext.HttpContext?.User.Identity?.Name;
        }
    }
}
