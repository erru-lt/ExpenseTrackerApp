using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerApp.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required]
        public double Amount { get; set; }

        [StringLength(200)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DisplayName("Date")]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [NotMapped]
        public string CategoryName
        {
            get => Category == null 
                ? " " 
                : $"{Category.Icon} {Category.Name}";
        }

        [NotMapped]
        public string FormattedAmount
        {
            get => Amount.ToString("C");
        }
    }
}
