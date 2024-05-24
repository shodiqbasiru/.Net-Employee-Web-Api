namespace EmployeeApi.Models.Requests;

public class PageRequest
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public string? SortDirection { get; set; }
}