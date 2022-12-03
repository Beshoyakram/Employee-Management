using MVCAPP.Models;
using System.ComponentModel.DataAnnotations;

namespace MVCAPP.ViewModels
{
    public class CreateViewModel
    {
        [Required, MaxLength(30, ErrorMessage = "Name can't be excced 30 characters.")]
        [MinLength(2, ErrorMessage = "minnnnn 2")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Office Email")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            , ErrorMessage = "Invaild Email")]
        public string Email { get; set; }
        [Required]
        public Dept? Department { get; set; }
        [Display(Name = "Cover Photo")]
        public IFormFile? Photo { get; set; }
    }
}
