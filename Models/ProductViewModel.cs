using System;
using System.ComponentModel.DataAnnotations;

namespace Accounting_Managment_System_Frontend.Models
{
    public class ProductViewModel
    {
        public int? ProductId { get; set; }

        [Required]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        [StringLength(100)]
        public string? SKU { get; set; }

        [Required]
        [StringLength(400)]
        [Display(Name = "Product Name")]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Display(Name = "Sales Account")]
        public int? SalesAccountId { get; set; }

        [Display(Name = "Purchase Account")]
        public int? PurchaseAccountId { get; set; }

        [Display(Name = "Unit Price")]
        [Range(0, double.MaxValue)]
        public decimal? UnitPrice { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Created At")]
        public DateTime? CreatedAt { get; set; }

        // Optional display fields (filled by API if available)
        public string? CompanyName { get; set; }
        public string? SalesAccountName { get; set; }
        public string? PurchaseAccountName { get; set; }
    }
}
