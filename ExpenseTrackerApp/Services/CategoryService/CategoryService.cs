using ExpenseTrackerApp.Data;
using ExpenseTrackerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApp.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }
        
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public async Task AddNewCategoryAsync(Category category)
        {
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
    }
}
