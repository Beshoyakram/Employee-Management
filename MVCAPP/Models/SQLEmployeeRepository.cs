using System.Linq;

namespace MVCAPP.Models
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext context;

        public SQLEmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Employee AddEmployee(Employee e)
        {
            context.employees.Add(e);
            context.SaveChanges();
            return e;
        }

        public IEnumerable<Employee> AllEmployee()
        {
            return context.employees;
        }

        public int CountDept(Dept dept)
        {
            var count = context.employees.Where(e=>e.Department == dept).Count();
            return count;
        }

        public Employee Delete(int id)
        {
            var emp = context.employees.Find(id);
            if (emp != null)
            {
                context.employees.Remove(emp);
                context.SaveChanges();
                return emp;
            }
            else { throw new NotImplementedException(); }
            
        }

        public bool FindEmployee(string email)
        {
            Employee emp = context.employees.Where(e => e.Email == email).FirstOrDefault();
            if (emp != null) { return true; }
            else { return false; }
        }

        public Employee GetEmployee(int id)
        {
            var emp = context.employees.Find(id);
            if (emp != null) { return emp; }
            else {return null; }
        }

        public IEnumerable<Employee> Search(string SearchTerm)
        {
            if (string.IsNullOrEmpty(SearchTerm))
            {
                return context.employees;
            }
            else
            {
                return context.employees.Where(e => e.Name
                .Contains(SearchTerm) ||
                e.Email.Contains(SearchTerm) ||
                e.Phone.ToString().Contains(SearchTerm)); 
                
            }
        }

        public Employee Update(Employee e)
        {
            //I catch the employee like e from db
            var emplyee = context.employees.Attach(e);
            //make changes on it
            emplyee.State=Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            //return the updated record
            return e;

            
        }
    }
}
