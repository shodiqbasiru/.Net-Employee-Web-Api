namespace EmployeeApi.Models.Requests;

public class UpdateEmployeeRequest
{
    public string Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime BirthDate  { get; set; }
    public double BasicSalary { get; set; }
    public string GroupId { get; set; }
}