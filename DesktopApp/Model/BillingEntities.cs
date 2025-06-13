using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesktopApp.Models
{

    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }

        [Required]
        public string InvoiceNo { get; set; } = string.Empty;

        [Required]
        public DateTime InvoiceDate { get; set; }

        // Foreign Key to Party
        [Required]
        public int PartyId { get; set; }

        [ForeignKey("PartyId")]
        public Party Party { get; set; } = null!;

        public List<Product> Products { get; set; } = new();
    }

    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Unit { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        // Foreign key
        public int InvoiceId { get; set; }

        [ForeignKey("InvoiceId")]
        public Invoice Invoice { get; set; } = null!;
    }

    public class Party
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
