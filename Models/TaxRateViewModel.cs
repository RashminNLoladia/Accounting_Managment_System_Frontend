using System.ComponentModel.DataAnnotations;

namespace Accounting_Managment_System_Frontend.Models
{
    public class TaxRateViewModel
    {
        public int? TaxRateId { get; set; }

        [Required]
        [Display(Name = "Tax Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Rate (Fraction)")]
        [Range(0, 1)]
        public decimal Rate { get; set; } // example: 0.18 for 18%

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;
    }
}
