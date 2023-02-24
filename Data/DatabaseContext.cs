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
        
    }
}