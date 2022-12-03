using System.ComponentModel.DataAnnotations;

namespace MVCAPP.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
