using System;
using System.ComponentModel.DataAnnotations;

namespace Accounting_Managment_System_Frontend.Models
{
    public class VendorViewModel
    {
        public int? VendorId { get; set; }

        [Required]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        [Required]
        [StringLength(300)]
        [Display(Name = "Vendor Name")]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        [Display(Name = "Contact Name")]
        public string? ContactName { get; set; }

        [EmailAddress]
        [StringLength(200)]
        public string? Email { get; set; }

        [StringLength(50)]
        public string? Phone { get; set; }

        [StringLength(500)]
        [Display(Name = "Billing Address")]
        public string? BillingAddress { get; set; }

        [StringLength(100)]
        [Display(Name = "Vendor Code")]
        public string? VendorCode { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Created At")]
        public DateTime? CreatedAt { get; set; }
    }
}
