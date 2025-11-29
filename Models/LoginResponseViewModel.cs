namespace Accounting_Managment_System_Frontend.Models
{
    public class LoginResponseViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public string? Token { get; set; }
    }
}
