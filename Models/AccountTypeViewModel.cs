using System.ComponentModel.DataAnnotations;

namespace Accounting_Managment_System_Frontend.Models
{
    public class AccountTypeViewModel
    {
        public int? AccountTypeId { get; set; }

        [Required]
        [Display(Name = "Account Type Name")]
        public string Name { get; set; } = string.Empty;

      
    }
}
