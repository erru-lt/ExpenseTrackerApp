using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerApp.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 15, ErrorMessage = "Incorrect category name length", MinimumLength = 5)]
        public string Name { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;

        [NotMapped]
        public string IconAndName
        {
            get => $"{Icon} {Name}";
        }
    }
}
