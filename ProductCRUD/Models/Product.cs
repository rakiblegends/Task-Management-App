using System.ComponentModel.DataAnnotations;

namespace ProductCRUD.Models
{
    public class Product
    {
        
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Length must not be greater than 100")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Range(1, 1000, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; } = 0;
        [Required]
        [Range(0, 1000, ErrorMessage = "Out of bound")]
        public int StockQuantity { get; set; } = 0;
    }
}
