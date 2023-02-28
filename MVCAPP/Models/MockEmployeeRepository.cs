using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace MVCAPP.Models
{
    public class MockEmployeeRepository:IEmployeeRepository
    {
        private List<Employee> _employees;

        public MockEmployeeRepository()
        {
            _employees = new List<Employee>() 
            {
                new Employee(){ Id=1,Name="Beshoy",Email="Beshoy@gmail.com",Department=Dept.IT},
                new Employee(){ Id=2,Name="Mona",Email="Mona@gmail.com",Department=Dept.HR},
                new Employee(){ Id=3,Name="Sayed",Email="Sayed@gmail.com",Department=Dept.IT},
            };

        }

        public Employee AddEmployee(Employee e)
        {
            e.Id = _employees.Max(e => e.Id);
            e.Id++;
            _employees.Add(e);
            return e;
        }

        public IEnumerable<Employee> AllEmployee()
        {
            return _employees;
        }

        public Employee GetEmployee(int id)
        {
            var employee = _employees.FirstOrDefault(n => n.Id == id);
            if (employee != null)
            {
                return employee;
            }
            else { throw new NotImplementedException(); }

        }
        public Employee Update(Employee e)
        {
            var employee = _employees.FirstOrDefault(n => n.Id == e.Id);
            if (employee != null)
            {
                employee.Name=e.Name;
                employee.Email=e.Email;
                employee.Department=e.Department;
                return employee;

            }
            else { throw new NotImplementedException(); }

        }

        public Employee Delete(int id)
        {
            var employee = _employees.FirstOrDefault(n => n.Id == id);
            if (employee != null)
            {
                _employees.Remove(employee);
                return employee;

            }
            else { throw new NotImplementedException(); }
        }

        public IEnumerable<Employee> Search(string SearchTerm)
        {
            if (string.IsNullOrEmpty(SearchTerm))
            {
                return _employees;
            }
            else 
            {
                return _employees.Where(e => e.Name.Contains(SearchTerm) || e.Email.Contains(SearchTerm));
            }
        }

        public int CountDept(Dept dept)
        {
            throw new NotImplementedException();
        }

        public bool FindEmployee(string email)
        {
            throw new NotImplementedException();
        }
    }
}
