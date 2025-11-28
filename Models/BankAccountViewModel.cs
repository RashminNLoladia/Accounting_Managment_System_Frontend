using System;
using System.ComponentModel.DataAnnotations;

namespace Accounting_Managment_System_Frontend.Models
{
    public class BankAccountViewModel
    {
        public int? BankAccountId { get; set; }

        [Required]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        [Required]
        [Display(Name = "Bank Name")]
        public string BankName { get; set; } = string.Empty;

        [Display(Name = "Account Number")]
        public string? AccountNumber { get; set; }

        [Display(Name = "IFSC")]
        public string? IFSC { get; set; }

        [Required]
        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }

        [Required]
        [Display(Name = "Opening Balance")]
        public decimal OpeningBalance { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;
    }
}
