using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
