using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AspApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AspApp.Data
{
    public class DatabaseContext: DbContext
    {
        private readonly IConfiguration configuration;
        public DatabaseContext([NotNullAttribute] DbContextOptions options, IConfiguration configuration) : base (options)
        {
            this.configuration = configuration;

        }

        public DbSet<Department> Departments {get; set;}
        public DbSet<Admin> AdminTable {get; set;}
        public DbSet<Employee> Employees {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                    new Employee()
                    {
                        Id = 1,
                        FirstName = "Ketane",
                        LastName = "Maseloa",
                        Email = "email1@ggmail.com",
                        Status = "Active",
                        Telephone = "4456789002",
                        ManagerId = 4,
                        Password = "Password123#",
                        IsManager = false
                    },
                    new Employee()
                    {
                        Id = 2,
                        FirstName = "Marks",
                        LastName = "Doe",
                        Email = "email2@gmail.com",
                        Status = "Inactive",
                        Telephone = "4456789002",
                        ManagerId = 3,
                        Password = "Password123#",
                        IsManager = false
                    },
                    new Employee()
                    {
                        Id = 3,
                        FirstName = "Smith",
                        LastName = "Walker",
                        Email = "email2@gmail.com",
                        ManagerId = 0,
                        Status = "Active",
                        Telephone = "4456789002",
                        Password = "Password123#",
                        IsManager = true
                    },
                    new Employee()
                    {
                        Id = 4,
                        FirstName = "Lucky",
                        LastName = "Dube",
                        Email = "email3@gmail.com",
                        Status = "Inactive",
                        ManagerId = 0,
                        Telephone = "4456789002",
                        Password = "Password123#",
                        IsManager = true
                    },
                    new Employee()
                    {
                        Id = 5,
                        FirstName = "Raymond",
                        LastName = "Reddd",
                        Email = "email4@gmail.com",
                        Status = "Active",
                        Telephone = "4456789002",
                        ManagerId = 4,
                        Password = "Password123#",
                        IsManager = false
                    },
                    new Employee()
                    {
                        Id = 6,
                        FirstName = "David",
                        LastName = "Ricks",
                        Email = "email5@gmail.com",
                        Status = "Inactive",
                        Telephone = "4456789002",
                        ManagerId = 4,
                        Password = "Password123#",
                        IsManager = false
                    },
                    new Employee()
                    {
                        Id = 7,
                        FirstName = "Riri",
                        LastName = "Paris",
                        Email = "email6@gmail.com",
                        Status = "Active",
                        Telephone = "4456789002",
                        ManagerId = 3,
                        Password = "Password123#",
                        IsManager = false
                    }
            );
            modelBuilder.Entity<Department>().HasData(
                new Department() { Id = 1, Name = "department1", Status = "Active", ManagerId = 3},
                new Department() { Id = 2, Name = "department2", Status = "Inactive", ManagerId = 3},
                new Department() { Id = 3, Name = "department3", Status = "Inactive", ManagerId = 4},
                new Department() { Id = 5, Name = "department5", Status = "Active", ManagerId = 3},
                new Department() { Id = 6, Name = "department6", Status = "Active", ManagerId = 4},
                new Department() { Id = 4, Name = "department4", Status = "Inactive", ManagerId = 4}
            );

            modelBuilder.Entity<Admin>().HasData(
                new Admin() { Id = 1, Username = " hradmin@test.com", Password = "TestPass1234"}
            );

            base.OnModelCreating(modelBuilder);
        }
        
    }
}