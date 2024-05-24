using EmployeeApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Data;

public class AppDbContext : DbContext
{
    public DbSet<Group> Groups { get; set; }
    public DbSet<Employee> Employees { get; set; }

    protected AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}