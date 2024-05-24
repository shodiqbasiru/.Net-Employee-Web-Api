using EmployeeApi.Entities;

namespace EmployeeApi.Models.Responses;

public class EmployeeResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string BirthDate  { get; set; }
    public double BasicSalary { get; set; }
    public bool IsActive { get; set; }
    public GroupResponse? Group { get; set; }
    public DateTime CreatedAt { get; set; }
}