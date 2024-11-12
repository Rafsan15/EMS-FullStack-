using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Core.Models;
using System;
namespace EmployeeManagement.Infrastructure
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Department { get; set; }

    }
}
