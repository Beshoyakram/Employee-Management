using MVCAPP.Models;

namespace EmployeeManagement.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Employee> Employees { get; set; }
        public string SerachTerm { get; set; }
    }
}
