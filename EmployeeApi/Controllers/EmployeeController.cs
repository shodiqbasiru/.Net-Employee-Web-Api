using System.Net;
using EmployeeApi.Entities;
using EmployeeApi.Models.Requests;
using EmployeeApi.Models.Responses;
using EmployeeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeeController(IEmployeeService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeRequest request)
    {
        var employee = await _service.CreateEmployee(request);
        var response = new ApiResponse<Employee>
        {
            StatusCode = (int)HttpStatusCode.Created,
            Status = HttpStatusCode.Created.ToString(),
            Message = "Employee created successfully",
            Data = employee
        };

        return Created($"/api/employees", response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeById(string id)
    {
        var employee = await _service.GetEmployeeById(id);
        var response = new ApiResponse<EmployeeResponse>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employee found successfully",
            Data = employee
        };

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEmployees()
    {
        var employees = await _service.GetAllEmployees();
        var response = new ApiResponse<IEnumerable<EmployeeResponse>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employees found successfully",
            Data = employees
        };

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeRequest request)
    {
        var employee = await _service.UpdateEmployee(request);
        var response = new ApiResponse<Employee>
        {
            StatusCode = (int)HttpStatusCode.Created,
            Status = HttpStatusCode.Created.ToString(),
            Message = "Employee updated successfully",
            Data = employee
        };

        return Created($"/api/employees/{employee.Id}", response);
    }
}