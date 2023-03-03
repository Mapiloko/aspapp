using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AspApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspApp.Data
{
    public class DatabaseContext: IdentityDbContext
    {
        private readonly IConfiguration configuration;
        public DatabaseContext([NotNullAttribute] DbContextOptions options, IConfiguration configuration) : base (options)
        {
            this.configuration = configuration;

        }

        public DbSet<Department> Departments {get; set;}
        public DbSet<Employee> Employees {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Department>().HasData(
            //     new Department() { Id = 1, Name = "dpmt1"},
            //     new Department() { Id = 2, Name = "dpmt2"},
            //     new Department() { Id = 3, Name = "dpmt3"},
            //     new Department() { Id = 5, Name = "dpmt5"},
            //     new Department() { Id = 4, Name = "dpmt4"}

            
            

            base.OnModelCreating(modelBuilder);
        }
        
    }
}