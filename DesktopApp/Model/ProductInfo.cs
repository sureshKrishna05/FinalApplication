using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesktopApp.Model
{
    public class ProductInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; } = string.Empty;

        public string? BatchNo { get; set; }
        public string? HSNCode { get; set; }
        public string? Packaging { get; set; }
        public int StockQuantity { get; set; }
        public string? ExpiryDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal MRP { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PTR { get; set; }

        // Added Column attribute for consistency
        [Column(TypeName = "decimal(18, 2)")]
        public decimal COST { get; set; } // Actual Cost

        public string? ProductCategory { get; set; } // type of product
    }
}