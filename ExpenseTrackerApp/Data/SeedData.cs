using ExpenseTrackerApp.Enum;
using ExpenseTrackerApp.Models;
using Microsoft.AspNetCore.Identity;
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

        public static async Task EnsureRole(IServiceProvider serviceProvider, string roleName)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if(await roleManager.RoleExistsAsync(roleName) == false)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
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
                            CategoryType = CategoryType.Expense,
                        },
                        new Category()
                        {
                            Name = "Travel",
                            Icon = "✈️",
                            CategoryType = CategoryType.Expense,
                        },
                        new Category()
                        {
                            Name = "Entertainment",
                            Icon = "🍾",
                            CategoryType = CategoryType.Expense,
                        },
                        new Category()
                        {
                            Name = "Salary",
                            Icon = "💸",
                            CategoryType= CategoryType.Income,
                        }
                    });
        }
    }
}
