using Bogus;
using EmployeeApi.Data;
using EmployeeApi.Entities;
using EmployeeApi.Services;

namespace EmployeeApi.Factories;

public class DatabaseSeeder
{
    private readonly AppDbContext _context;
    private readonly DataFactory _factory;

    public DatabaseSeeder(AppDbContext context, DataFactory factory)
    {
        _context = context;
        _factory = factory;
    }

    public async Task SeedData()
    {
        
        if (!_context.Groups.Any())
        {
            var groups = await _factory.GenerateFakeGroup(10);

            _context.Groups.AddRange(groups);
            await _context.SaveChangesAsync();
        }
        
        if (!_context.Employees.Any())
        {
            var employees = await _factory.GenerateFakeEmployee(100);

            _context.Employees.AddRange(employees);
            await _context.SaveChangesAsync();
        }

    }
}