using ExpenseTrackerApp.Models;

namespace ExpenseTrackerApp.Services.DropdownService
{
    public interface IDropdownService
    {
        Task<CategoryDropdown> GetCategoryDropdownValues();
    }
}