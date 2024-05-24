using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EmployeeApi.Entities;

[Table(name:"m_group")]
public class Group
{
    [Key,Column(name:"id")]
    public Guid Id { get; set; }
    
    [Column(name:"group_name", TypeName = "NVarchar(50)")]
    public string GroupName { get; set; } = string.Empty;
    
    [Column(name:"group_description", TypeName = "NVarchar(250)")]
    public string GroupDescription { get; set; } = string.Empty;
    
    public virtual ICollection<Employee>? Employees { get; set; } 
}