using EmployeeApi.Entities;
using EmployeeApi.Models.Requests;
using EmployeeApi.Models.Responses;

namespace EmployeeApi.Services;

public interface IEmployeeService
{
    Task<Employee> CreateEmployee(EmployeeRequest request);
    Task<Employee> GetById(string id);
    Task<EmployeeResponse> GetEmployeeById(string id);
    Task<IEnumerable<EmployeeResponse>> GetAllEmployees();
    Task<PageResult<EmployeeResponse>> GetAllEmployees(PageRequest request);
    Task<Employee> UpdateEmployee(UpdateEmployeeRequest request);
}