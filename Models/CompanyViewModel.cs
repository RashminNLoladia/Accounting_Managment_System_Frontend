using System;
using System.ComponentModel.DataAnnotations;

namespace Accounting_Managment_System_Frontend.Models
{
    public class CompanyViewModel
    {
        public int? CompanyId { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Short Name")]
        public string? ShortName { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
