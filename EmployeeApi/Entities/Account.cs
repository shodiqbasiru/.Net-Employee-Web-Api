using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeApi.Entities;

[Table(name:"m_account")]
public class Account
{
    [Key, Column(name: "id")]
    public Guid Id { get; set; }
    
    [Column(name: "username", TypeName = "NVarchar(50)")]
    public string Username { get; set; } = string.Empty;
    
    [Column(name: "email", TypeName = "NVarchar(50)")]
    public string? Email { get; set; }
    
    [Column(name: "password", TypeName = "NVarchar(50)")]
    public string Password { get; set; } = string.Empty;
    
    [Column(name: "is_active")]
    public bool IsActive { get; set; }
    
    [Column(name: "created_at")]
    public DateTime CreatedAt { get; set; }

    public virtual Employee Employee { get; set; }
}