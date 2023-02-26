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
                        ManagerId = 10,
                        Password = "Password123#",
                        IsManager = false
                    },
                    new Employee()
                    {
                        Id = 8,
                        FirstName = "Thandeka",
                        LastName = "Keeper",
                        Email = "email1@ggmail.com",
                        Status = "Active",
                        Telephone = "4456789002",
                        ManagerId = 10,
                        Password = "Password123#",
                        IsManager = false
                    },
                    new Employee()
                    {
                        Id = 9,
                        FirstName = "Pro",
                        LastName = "Steve",
                        Email = "email2@gmail.com",
                        Status = "Inactive",
                        Telephone = "4456789002",
                        ManagerId = 10,
                        Password = "Password123#",
                        IsManager = false
                    },
                    new Employee()
                    {
                        Id = 10,
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
                        Id = 11,
                        FirstName = "Ompha",
                        LastName = "Fortunate",
                        Email = "email3@gmail.com",
                        Status = "Inactive",
                        ManagerId = 0,
                        Telephone = "4456789002",
                        Password = "Password123#",
                        IsManager = true
                    },
                    new Employee()
                    {
                        Id = 12,
                        FirstName = "Raymond",
                        LastName = "Reddd",
                        Email = "email4@gmail.com",
                        Status = "Active",
                        Telephone = "4456789002",
                        ManagerId = 11,
                        Password = "Password123#",
                        IsManager = false
                    },
                    new Employee()
                    {
                        Id = 13,
                        FirstName = "Milly",
                        LastName = "Thwala",
                        Email = "email5@gmail.com",
                        Status = "Inactive",
                        Telephone = "4456789002",
                        ManagerId = 11,
                        Password = "Password123#",
                        IsManager = false
                    },
                    new Employee()
                    {
                        Id = 14,
                        FirstName = "Selunathi",
                        LastName = "Muzex",
                        Email = "email6@gmail.com",
                        Status = "Active",
                        Telephone = "4456789002",
                        ManagerId = 17,
                        Password = "Password123#",
                        IsManager = false
                    },
                    new Employee()
                    {
                        Id = 15,
                        FirstName = "Phindile",
                        LastName = "Sendisoa",
                        Email = "email1@ggmail.com",
                        Status = "Active",
                        Telephone = "4456789002",
                        ManagerId = 17,
                        Password = "Password123#",
                        IsManager = false
                    },
                    new Employee()
                    {
                        Id = 16,
                        FirstName = "Mjeja",
                        LastName = "Revent",
                        Email = "email2@gmail.com",
                        Status = "Inactive",
                        Telephone = "4456789002",
                        ManagerId = 17,
                        Password = "Password123#",
                        IsManager = false
                    },
                    new Employee()
                    {
                        Id = 17,
                        FirstName = "Letor",
                        LastName = "Izzu",
                        Email = "email2@gmail.com",
                        ManagerId = 0,
                        Status = "Active",
                        Telephone = "4456789002",
                        Password = "Password123#",
                        IsManager = true
                    },
                    new Employee()
                    {
                        Id = 18,
                        FirstName = "Nikita",
                        LastName = "Dudu",
                        Email = "email3@gmail.com",
                        Status = "Inactive",
                        ManagerId = 0,
                        Telephone = "4456789002",
                        Password = "Password123#",
                        IsManager = true
                    },
                    new Employee()
                    {
                        Id = 19,
                        FirstName = "Phase",
                        LastName = "Way",
                        Email = "email4@gmail.com",
                        Status = "Active",
                        Telephone = "4456789002",
                        ManagerId = 18,
                        Password = "Password123#",
                        IsManager = false
                    },
                    new Employee()
                    {
                        Id = 20,
                        FirstName = "Rose",
                        LastName = "Pink",
                        Email = "email5@gmail.com",
                        Status = "Inactive",
                        Telephone = "4456789002",
                        ManagerId = 18,
                        Password = "Password123#",
                        IsManager = false
                    },
                    new Employee()
                    {
                        Id = 21,
                        FirstName = "Khanya",
                        LastName = "Phonm",
                        Email = "email6@gmail.com",
                        Status = "Active",
                        Telephone = "4456789002",
                        ManagerId = 18,
                        Password = "Password123#",
                        IsManager = false
                    }
            );
            modelBuilder.Entity<Department>().HasData(
                new Department() { Id = 1, Name = "dpmt1", Status = "Active", ManagerId = 3},
                new Department() { Id = 2, Name = "dpmt2", Status = "Inactive", ManagerId = 3},
                new Department() { Id = 3, Name = "dpmt3", Status = "Inactive", ManagerId = 4},
                new Department() { Id = 5, Name = "dpmt5", Status = "Active", ManagerId = 3},
                new Department() { Id = 6, Name = "dpmt6", Status = "Active", ManagerId = 4},
                new Department() { Id = 7, Name = "dpmt7", Status = "Inactive", ManagerId = 4},
                new Department() { Id = 8, Name = "dpmt8", Status = "Active", ManagerId = 10},
                new Department() { Id = 9, Name = "dpmt9", Status = "Inactive", ManagerId = 11},
                new Department() { Id = 10, Name = "dpmt10", Status = "Inactive", ManagerId = 11},
                new Department() { Id = 11, Name = "dpmt11", Status = "Active", ManagerId = 11},
                new Department() { Id = 12, Name = "dpmt12", Status = "Active", ManagerId = 10},
                new Department() { Id = 13, Name = "dpmt13", Status = "Inactive", ManagerId = 10},
                new Department() { Id = 14, Name = "dpmt14", Status = "Active", ManagerId = 17},
                new Department() { Id = 15, Name = "dpmt15", Status = "Inactive", ManagerId = 17},
                new Department() { Id = 16, Name = "dpmt16", Status = "Inactive", ManagerId = 17},
                new Department() { Id = 17, Name = "dpmt17", Status = "Active", ManagerId = 18},
                new Department() { Id = 18, Name = "dpmt18", Status = "Active", ManagerId = 18},
                new Department() { Id = 19, Name = "dpmt19", Status = "Inactive", ManagerId = 18}
            );

            modelBuilder.Entity<Admin>().HasData(
                new Admin() { Id = 1, Username = " hradmin@test.com", Password = "TestPass1234"}
            );

            base.OnModelCreating(modelBuilder);
        }
        
    }
}