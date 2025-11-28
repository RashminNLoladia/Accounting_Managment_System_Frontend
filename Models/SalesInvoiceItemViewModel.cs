using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Accounting_Managment_System_Frontend.Models
{
    public class SalesInvoiceItemViewModel
    {
        public int? Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        public decimal Total => Quantity * UnitPrice;
    }

    public class SalesInvoiceViewModel
    {
        public int? Id { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Required]
        [Display(Name = "Invoice Date")]
        public DateTime InvoiceDate { get; set; } = DateTime.Today;

        public string? Remarks { get; set; }

        public bool IsPaid { get; set; } = false;

        public List<SalesInvoiceItemViewModel> Items { get; set; } = new();

        public decimal TotalAmount => Items.Sum(i => i.Total);
    }
}
