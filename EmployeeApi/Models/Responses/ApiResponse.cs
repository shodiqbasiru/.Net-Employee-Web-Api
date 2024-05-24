namespace EmployeeApi.Models.Responses;

public class ApiResponse<T>
{
    public int StatusCode { get; set; }
    public string? Status { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
}