﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MVCAPP.Models
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        

        public DbSet<Employee> employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*For IdentityDBContext class*/
            base.OnModelCreating(modelBuilder);

            #region Seeding Data For Employee table
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "remon",
                    Email = "remon@gmail.com",
                    Department = Dept.IT,
                    PhotoPath = "Male1.png",
                    Phone = 01212222228,
                    Salary = 3150.00,
                    Address = "cairo"
                }, new Employee
                {
                    Id = 2,
                    Name = "Beshoy",
                    Email = "Beshoy@gmail.com",
                    Department = Dept.IT,
                    PhotoPath = "Male2.png",
                    Phone = 01212222228,
                    Salary = 3150.00,
                    Address = "cairo"
                }, new Employee
                {
                    Id = 3,
                    Name = "Mona",
                    Email = "Mona@gmail.com",
                    Department = Dept.IT,
                    PhotoPath = "Female1.png",
                    Phone = 01212222228,
                    Salary = 3150.00,
                    Address = "cairo"
                }, new Employee
                {
                    Id = 4,
                    Name = "Rmay",
                    Email = "Rmay@gmail.com",
                    Department = Dept.IT,
                    PhotoPath = "Male1.png",
                    Phone = 01212222228,
                    Salary = 3150.00,
                    Address = "cairo"
                }, new Employee
                {
                    Id = 5,
                    Name = "Rmay2",
                    Email = "Rmay2@gmail.com",
                    Department = Dept.IT,
                    PhotoPath = "Male2.png",
                    Phone = 01212222228,
                    Salary = 3150.00,
                    Address = "cairo"

                }, new Employee
                {
                    Id = 6,
                    Name = "Rmay3",
                    Email = "Rmay3@gmail.com",
                    Department = Dept.IT,
                    PhotoPath = "Male1.png",
                    Phone = 01212222228,
                    Salary = 3150.00,
                    Address = "cairo"
                },
                new Employee
                {
                    Id = 7,
                    Name = "Rmay4",
                    Email = "Rmay4@gmail.com",
                    Department = Dept.IT,
                    PhotoPath = "Male2.png",
                    Phone = 01212222228,
                    Salary = 3150.00,
                    Address = "cairo"
                },
                new Employee
                {
                    Id = 8,
                    Name = "Reem",
                    Email = "Reem@gmail.com",
                    Department = Dept.IT,
                    PhotoPath = "Female1.png",
                    Phone = 01212222228,
                    Salary = 3150.00,
                    Address = "cairo"
                }

                );
            #endregion

            #region Seeding Data For ApplicationUser table
            string ADMIN_ID = "02174cf0–9412–4cfe-afbf-59f706d72cf6";
            string ROLE_ID = "341743f0-asd2–42de-afbf-59kmkkmk72cf6";

            ApplicationUser admin = new ApplicationUser 
            {
                Id = ADMIN_ID,
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com".ToUpper(),
                EmailConfirmed = true,
                UserName = "admin@gmail.com",
                NormalizedUserName = "admin@gmail.com".ToUpper(),
                City = "Cairo",
                LockoutEnabled = true,
                
            };
            var hasher = new PasswordHasher<ApplicationUser>();
            admin.PasswordHash = hasher.HashPassword(admin, password: "@Admin123");
            modelBuilder.Entity<ApplicationUser>().HasData(admin);


            //Admin Role
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole 
                {
                    Id = ROLE_ID,
                    Name ="Admin",
                    NormalizedName = "Admin".ToUpper(),
                    ConcurrencyStamp = ROLE_ID
                });

            //Connect An admin to Role Admin
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                { RoleId = ROLE_ID, UserId = ADMIN_ID});

            //Give admin Claims
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(
                new IdentityUserClaim<string>
                { Id = 1, UserId = ADMIN_ID, ClaimType = "Create Role", ClaimValue = "true" },
                new IdentityUserClaim<string>
                { Id = 2, UserId = ADMIN_ID, ClaimType = "Edit Role", ClaimValue = "true" },
                new IdentityUserClaim<string>
                { Id = 3, UserId = ADMIN_ID, ClaimType = "Delete Role", ClaimValue = "true" }
                );




            #endregion

            #region  Fleunt API for Employee table
            modelBuilder.Entity<Employee>()
                .Property(p => p.Name)
                .IsRequired().
                HasMaxLength(30);

            modelBuilder.Entity<Employee>()
                .Property(p => p.PhotoPath)
                .HasDefaultValue("No_image.png");

            modelBuilder.Entity<Employee>()
            .Property(p => p.Email)
            .IsRequired();
            
            modelBuilder.Entity<Employee>()
            .Property(p => p.Department)
            .IsRequired();

            modelBuilder.Entity<Employee>()
            .Property(p => p.Phone)
            .IsRequired()
            .HasMaxLength(11);
            #endregion



        }

    }
}
