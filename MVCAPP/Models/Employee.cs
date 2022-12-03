using System.ComponentModel.DataAnnotations;

namespace MVCAPP.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required,MaxLength(30,ErrorMessage ="Name can't be excced 30 characters.")]
        [MinLength(2,ErrorMessage ="minnnnn 2")]
        public string Name { get; set; }
        [Required]
        [Display(Name ="Office Email")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            , ErrorMessage = "Invaild Email")]
        public string Email  { get; set; }
        [Required]
        public Dept? Department { get; set; }
        public string? PhotoPath { get; set; }
    }
}
