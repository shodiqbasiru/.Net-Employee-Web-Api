using EmployeeApi.Entities;
using EmployeeApi.Models.Requests;
using EmployeeApi.Models.Responses;
using EmployeeApi.Repositories;

namespace EmployeeApi.Services.impls;

public class EmployeeService : IEmployeeService
{
    private readonly IRepository<Employee> _repository;
    private readonly IPersistence _persistence;
    private readonly IGroupService _groupService;

    public EmployeeService(IRepository<Employee> repository, IPersistence persistence, IGroupService groupService)
    {
        _repository = repository;
        _persistence = persistence;
        _groupService = groupService;
    }

    public async Task<Employee> CreateEmployee(EmployeeRequest request)
    {
        var group = await _groupService.GetById(request.GroupId);
        var payload = new Employee
        {
            Username = request.Username,
            EmployeeName = request.FirstName + " " + request.LastName,
            Email = request.Email,
            Address = request.Address,
            PhoneNumber = request.PhoneNumber,
            BirthDate = request.BirthDate,
            BasicSalary = request.BasicSalary,
            IsActive = true,
            CreatedAt = DateTime.Now,
            Group = group
        };
        var employee = await _repository.SaveAsync(payload);
        await _persistence.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee> GetById(string id)
    {
        return await _repository.FindByIdAsync(Guid.Parse(id)) ?? throw new Exception("employee not found");
    }

    public async Task<EmployeeResponse> GetEmployeeById(string id)
    {
        var employee = await _repository.FindAsync(employ => employ.Id.Equals(Guid.Parse(id)), new[] { "Group" });
        if (employee is null) throw new Exception("employee not found");
        return GetEmployeeResponse(employee);
    }

    public async Task<IEnumerable<EmployeeResponse>> GetAllEmployees()
    {
        var employees = await _repository.FindAllAsync(new[] { "Group" });
        return employees.Select(GetEmployeeResponse);
    }

    public async Task<Employee> UpdateEmployee(UpdateEmployeeRequest request)
    {
        var employee = await GetById(request.Id);
        var group = await _groupService.GetById(request.GroupId);
        employee.Username = request.Username;
        employee.EmployeeName = request.FirstName + " " + request.LastName;
        employee.Email = request.Email;
        employee.Address = request.Address;
        employee.PhoneNumber = request.PhoneNumber;
        employee.BirthDate = request.BirthDate;
        employee.BasicSalary = request.BasicSalary;
        employee.Group = group;
        var updatedEmployee = _repository.Update(employee);
        await _persistence.SaveChangesAsync();
        return updatedEmployee;
    }

    private static EmployeeResponse GetEmployeeResponse(Employee employee)
    {
        return new EmployeeResponse
        {
            Id = employee.Id,
            Username = employee.Username,
            EmployeeName = employee.EmployeeName,
            Email = employee.Email,
            Address = employee.Address,
            PhoneNumber = employee.PhoneNumber,
            BirthDate = employee.BirthDate.ToString("yyyy-MM-dd"),
            BasicSalary = employee.BasicSalary,
            IsActive = employee.IsActive,
            Group = new GroupResponse
            {
                Id = employee.Group.Id.ToString(),
                GroupName = employee.Group.GroupName,
                GroupDescription = employee.Group.GroupDescription
            },
            CreatedAt = employee.CreatedAt
        };
    }
}