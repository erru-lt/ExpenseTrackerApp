namespace ExpenseTrackerApp.Models
{
    public class CategoryDropdown
    {
        public List<Category> Categories { get; set; }

        public CategoryDropdown()
        {
            Categories = new List<Category>();
        }
    }
}
