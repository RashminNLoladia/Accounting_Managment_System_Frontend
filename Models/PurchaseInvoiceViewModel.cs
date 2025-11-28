using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Accounting_Managment_System_Frontend.Models
{
    public class PurchaseInvoiceViewModel
    {
        public int? BillId { get; set; }

        [Required]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        [Required]
        [Display(Name = "Bill Number")]
        public string BillNumber { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Invoice Date")]
        public DateTime BillDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Vendor")]
        public int VendorId { get; set; }

        [Required]
        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }

        [Display(Name = "Sub Total")]
        public decimal SubTotal { get; set; } = 0;

        [Display(Name = "Tax Total")]
        public decimal TaxTotal { get; set; } = 0;

        [Display(Name = "Total")]
        public decimal Total { get; set; } = 0;

        [Display(Name = "Status")]
        public string Status { get; set; } = "Draft"; // Draft, Posted, Paid

        [Display(Name = "Due Date")]
        public DateTime? DueDate { get; set; }

        [Display(Name = "Created At")]
        public DateTime? CreatedAt { get; set; }

        [Display(Name = "Posted At")]
        public DateTime? PostedAt { get; set; }

        public List<BillItemViewModel> Items { get; set; } = new();
    }

    public class BillItemViewModel
    {
        public int? BillItemId { get; set; }

        [Required]
        [Display(Name = "Product")]
        public int? ProductId { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Required]
        public decimal Quantity { get; set; } = 1;

        [Required]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; } = 0;

        [Display(Name = "Line Total")]
        public decimal LineTotal => Quantity * UnitPrice;

        [Display(Name = "Tax Rate")]
        public int? TaxRateId { get; set; }
    }
}
