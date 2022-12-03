using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MVCAPP.Models
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
            
        }

        public DbSet<Employee> employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*For IdentityDBContext class*/
            base.OnModelCreating(modelBuilder);
            

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "remon",
                    Email = "remon@gmail.com",
                    Department = Dept.IT
                },
                new Employee
                {
                    Id = 2,
                    Name = "Beshoy",
                    Email = "beshoy@gmail.com",
                    Department = Dept.IT
                }

                );
        }

    }
}
