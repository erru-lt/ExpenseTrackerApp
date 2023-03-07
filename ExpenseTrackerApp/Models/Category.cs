using ExpenseTrackerApp.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string OwnerId { get; set; } = string.Empty;

        [Required]
        [StringLength(maximumLength: 15, ErrorMessage = "Incorrect category name length", MinimumLength = 5)]
        [Display(Name = "Category Name")]
        public string Name { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;

        [Required]
        public CategoryType CategoryType { get; set; }

        [NotMapped]
        public string IconAndName
        {
            get => $"{Icon} {Name}";
        }
    }
}
