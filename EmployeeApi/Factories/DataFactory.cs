using Bogus;
using EmployeeApi.Entities;
using EmployeeApi.Services;

namespace EmployeeApi.Factories;

public class DataFactory
{
    private readonly IGroupService _groupService;

    public DataFactory(IGroupService groupService)
    {
        _groupService = groupService;
    }
    
    public async Task<List<Employee>> GenerateFakeEmployee(int count)
    {
        var random = new Random();
        var groups = await _groupService.GetAllGroups();
        var groupResponses = groups.ToList();
        var groupIds = groupResponses.Select(g => g.Id).ToList();
        var faker = new Faker<Employee>("id_ID")
            .RuleFor(e => e.Id, f => Guid.NewGuid())
            .RuleFor(e => e.Username, (f, e) => f.Internet.UserName().ToLower())
            .RuleFor(e => e.EmployeeName, (f, e) => f.Name.FullName())
            .RuleFor(e => e.Email, (f, e) => f.Internet.Email(e.Username))
            .RuleFor(e => e.Address, f => f.Address.FullAddress())
            .RuleFor(e => e.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(e => e.BirthDate, f => f.Date.Past(30, DateTime.Now.AddYears(-18)))
            .RuleFor(e => e.BasicSalary, f => f.PickRandom(Salaries()))
            .RuleFor(e => e.IsActive, f => f.Random.Bool())
            .RuleFor(e => e.GroupId, f => f.PickRandom(Guid.Parse(groupIds[random.Next(0, groupIds.Count)])))
            .RuleFor(e => e.CreatedAt, f => f.Date.Recent());

        return faker.Generate(count);
    }

    public Task<List<Group>> GenerateFakeGroup(int count)
    {
        var faker = new Faker<Group>("id_ID")
                .RuleFor(g => g.Id, f => Guid.NewGuid())
                .RuleFor(g => g.GroupName, f => f.Commerce.Department())
                .RuleFor(g => g.GroupDescription, f => f.Lorem.Sentence(3));
        
        return Task.FromResult(faker.Generate(count));
    }
    
    private static List<Double> Salaries()
    {
        return new List<double>
        {
            3000000,
            4000000,
            5000000,
            6000000,
            7000000,
            8000000,
            9000000,
            10000000
        };
    }
}