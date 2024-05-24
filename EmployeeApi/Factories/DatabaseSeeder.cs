using Bogus;
using EmployeeApi.Data;
using EmployeeApi.Entities;
using EmployeeApi.Services;

namespace EmployeeApi.Factories;

public class DatabaseSeeder
{
    private readonly AppDbContext _context;
    private readonly IGroupService _groupService;

    public DatabaseSeeder(AppDbContext context, IGroupService groupService)
    {
        _context = context;
        _groupService = groupService;
    }

    public async Task SeedData()
    {
        if (!_context.Employees.Any())
        {
            var random = new Random();
            var groups = await _groupService.GetAllGroups();
            var groupResponses = groups.ToList();
            var groupIds = groupResponses.Select(g => g.Id).ToList();
            var faker = new Faker<Employee>("id_ID")
                .RuleFor(e => e.Id, f => Guid.NewGuid())
                .RuleFor(e => e.Username, (f, e) => f.Internet.UserName())
                .RuleFor(e => e.EmployeeName, (f, e) => f.Name.FullName())
                .RuleFor(e => e.Email, (f, e) => f.Internet.Email(e.Username))
                .RuleFor(e => e.Address, f => f.Address.FullAddress())
                .RuleFor(e => e.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(e => e.BirthDate, f => f.Date.Past(30, DateTime.Now.AddYears(-18)))
                .RuleFor(e => e.BasicSalary, f => f.Random.Double(3000000, 10000000))
                .RuleFor(e => e.IsActive, f => f.Random.Bool())
                .RuleFor(e => e.GroupId, f => f.PickRandom(Guid.Parse(groupIds[random.Next(0, groupIds.Count)])))
                .RuleFor(e => e.CreatedAt, f => f.Date.Recent());

            var employees = faker.Generate(100);

            _context.Employees.AddRange(employees);
            await _context.SaveChangesAsync();
        }
    }
}