using System.ComponentModel.DataAnnotations;

namespace Accounting_Managment_System_Frontend.Models
{
    public class RoleViewModel
    {
        public int? RoleId { get; set; }

        [Required]
        [Display(Name = "Role Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Description")]
        public string? Description { get; set; }
    }
}
