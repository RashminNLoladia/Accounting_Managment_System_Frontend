using System;
using System.ComponentModel.DataAnnotations;

namespace Accounting_Managment_System_Frontend.Models
{
    public class BankTransactionViewModel
    {
        public int? BankTransactionId { get; set; }

        [Required]
        [Display(Name = "Bank Account")]
        public int BankAccountId { get; set; }

        [Required]
        [Display(Name = "Transaction Date")]
        public DateTime TransactionDate { get; set; } = DateTime.Today;

        [Required]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Transaction Type")]
        public string TransactionType { get; set; } = "Debit"; // Debit or Credit

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Reference")]
        public string? Reference { get; set; }
    }
}
