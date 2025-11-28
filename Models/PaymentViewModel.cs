using System;
using System.ComponentModel.DataAnnotations;

namespace Accounting_Managment_System_Frontend.Models
{
    public class PaymentViewModel
    {
        public int? PaymentId { get; set; }

        [Required]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        [Required]
        [Display(Name = "Payment Number")]
        public string PaymentNumber { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; } = "Receipt"; // Receipt, Payment

        [Display(Name = "Method")]
        public string? Method { get; set; } // Bank Transfer, Cash, Card

        [Display(Name = "Reference")]
        public string? Reference { get; set; }

        [Display(Name = "Related Invoice")]
        public int? RelatedInvoiceId { get; set; }

        [Display(Name = "Related Bill")]
        public int? RelatedBillId { get; set; }

        [Display(Name = "Bank Account")]
        public int? BankAccountId { get; set; }

        [Display(Name = "Created At")]
        public DateTime? CreatedAt { get; set; }

        [Display(Name = "Posted At")]
        public DateTime? PostedAt { get; set; }
    }
}
