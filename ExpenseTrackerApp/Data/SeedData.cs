using ExpenseTrackerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApp.Data
{
    public class SeedData
    {
        public static async Task SeedCategories(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                context.Database.EnsureCreated();

                if (context.Categories.Any() == false)
                {
                    InsertCategories(context);
                    await context.SaveChangesAsync();
                }
            }
        }

        private static void InsertCategories(AppDbContext context)
        {
            context.Categories.AddRange(new List<Category>()
                    {
                        new Category()
                        {
                            Name = "Food",
                            Icon = "🍔",
                        },
                        new Category()
                        {
                            Name = "Travel",
                            Icon = "✈️",
                        },
                        new Category()
                        {
                            Name = "Entertainment",
                            Icon = "🍾",
                        }
                    });
        }
    }
}
