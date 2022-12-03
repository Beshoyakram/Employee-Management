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

        public Employee GetEmployee(int id)
        {
            var emp = context.employees.Find(id);
            if (emp != null) { return emp; }
            else {return null; }
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
