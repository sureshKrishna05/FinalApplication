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

        public string? Packaging { get; set; } // e.g., "10ML"

        public int StockQuantity { get; set; }

        public string? ExpiryDate { get; set; } // e.g., "09/26"

        [Column(TypeName = "decimal(18, 2)")]
        public decimal MRP { get; set; } // Maximum Retail Price

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PTR { get; set; } // Price to Retailer
    }
}