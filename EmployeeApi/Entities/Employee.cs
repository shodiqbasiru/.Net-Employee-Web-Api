using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeApi.Entities;

[Table(name:"m_employee")]
public class Employee
{
    [Key,Column(name:"id")]
    public Guid Id { get; set; }
    
    [Column(name: "username", TypeName = "NVarchar(50)")]
    public string Username { get; set; } = string.Empty;
    
    [Column(name:"employee_name",TypeName = "NVarchar(100)")]
    public string EmployeeName { get; set; } = string.Empty;
    
    [Column(name: "email", TypeName = "NVarchar(50)")]
    public string? Email { get; set; }
    
    [Column(name:"address",TypeName = "NVarchar(250)")]
    public string? Address { get; set; }
    
    [Column(name:"phone_number",TypeName = "NVarchar(14)")]
    public string? PhoneNumber { get; set; }
    
    [Column(name:"birth_date")]
    public DateTime BirthDate  { get; set; }
    
    [Column(name:"basic_salary")]
    public double BasicSalary { get; set; }
    
    [Column(name: "is_active")]
    public bool IsActive { get; set; }
    
    [Column(name:"group_id")]
    public Guid GroupId { get; set; }
    
    [Column(name: "created_at")]
    public DateTime CreatedAt { get; set; }


    public Group Group { get; set; }
}