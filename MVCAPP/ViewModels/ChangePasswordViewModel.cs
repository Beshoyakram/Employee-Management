using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [Display(Name = "Current Password")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required]
        [Display(Name = "New password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword",ErrorMessage = "Password annd confimation password do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }

    }
}
