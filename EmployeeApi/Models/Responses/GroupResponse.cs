namespace EmployeeApi.Models.Responses;

public class GroupResponse
{
    public string Id { get; set; } = string.Empty;
    public string GroupName { get; set; } = string.Empty;
    public string GroupDescription { get; set; } = string.Empty;

    public List<EmployeeResponse>? EmployeeResponses { get; set; }
}