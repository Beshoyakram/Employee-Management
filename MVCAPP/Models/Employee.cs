using System.ComponentModel.DataAnnotations;

namespace MVCAPP.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [MinLength(4, ErrorMessage = "Minimum legth must be 4 characters")]
        public string Name { get; set; }
     
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Invaild Email")]
        public string Email  { get; set; }
        public Dept? Department { get; set; }
        public string? PhotoPath { get; set; }
        public int Phone { get; set; }
        public double Salary { get; set; }
        public string Address { get; set; }


    }
}
