
namespace MVCAPP.Models
{
    public interface IEmployeeRepository
    {
        public Employee GetEmployee(int id);
        public IEnumerable<Employee> AllEmployee();
        public Employee AddEmployee(Employee e);
        public Employee Update(Employee e);
        public Employee Delete(int id);
    }
}
