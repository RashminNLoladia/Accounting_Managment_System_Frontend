using System;
using System.ComponentModel.DataAnnotations;

namespace Accounting_Managment_System_Frontend.Models
{
    public class CustomerViewModel
    {
        public int? CustomerId { get; set; }

        [Required]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }                 // FK to Company

        [Required]
        [StringLength(300)]
        [Display(Name = "Customer Name")]
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

        [StringLength(500)]
        [Display(Name = "Shipping Address")]
        public string? ShippingAddress { get; set; }

        [StringLength(100)]
        [Display(Name = "Customer Code")]
        public string? CustomerCode { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        // CreatedAt is set by server; include for read/display
        [Display(Name = "Created At")]
        public DateTime? CreatedAt { get; set; }
    }
}
