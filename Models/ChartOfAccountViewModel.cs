using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Accounting_Managment_System_Frontend.Models
{
    public class ChartOfAccountViewModel
    {
        public int? AccountId { get; set; }

        [Required]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        [Display(Name = "Parent Account")]
        public int? ParentAccountId { get; set; }

        [Display(Name = "Account Number")]
        public string? AccountNumber { get; set; }

        [Required]
        [Display(Name = "Account Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Account Type")]
        public int AccountTypeId { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Created At")]
        public DateTime? CreatedAt { get; set; }

        // Dropdowns
        public IEnumerable<KeyValuePair<int, string>> CompanyList { get; set; } = new List<KeyValuePair<int, string>>();
        public IEnumerable<KeyValuePair<int, string>> AccountTypeList { get; set; } = new List<KeyValuePair<int, string>>();
        public IEnumerable<KeyValuePair<int, string>> ParentAccountList { get; set; } = new List<KeyValuePair<int, string>>();

        // Display names for Index view
        public string? CompanyName { get; set; }
        public string? AccountTypeName { get; set; }
        public string? ParentAccountName { get; set; }
    }
}
