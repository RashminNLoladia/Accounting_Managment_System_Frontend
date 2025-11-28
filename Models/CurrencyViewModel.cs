using System;
using System.ComponentModel.DataAnnotations;

namespace Accounting_Managment_System_Frontend.Models
{
    public class CurrencyViewModel
    {
        public int? CurrencyId { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; } = string.Empty;

        [Display(Name = "Name")]
        public string? Name { get; set; }

        [Display(Name = "Symbol")]
        public string? Symbol { get; set; }

        [Required]
        [Display(Name = "Decimal Places")]
        public byte DecimalPlaces { get; set; } = 2;

      
    }
}
