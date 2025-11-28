using System.ComponentModel.DataAnnotations;

namespace Accounting_Managment_System_Frontend.Models
{
    public class UserRoleViewModel
    {
        public int? UserRoleId { get; set; }

        [Required]
        [Display(Name = "User")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Role")]
        public int RoleId { get; set; }

        // Optional: for dropdown display
        public string? UserName { get; set; }
        public string? RoleName { get; set; }
    }
}
