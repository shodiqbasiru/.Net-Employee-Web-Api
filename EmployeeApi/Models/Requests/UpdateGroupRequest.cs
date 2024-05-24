namespace EmployeeApi.Models.Requests;

public class UpdateGroupRequest
{
    public string Id { get; set; }
    public string GroupName { get; set; } = string.Empty;
    public string GroupDescription { get; set; } = string.Empty;
}