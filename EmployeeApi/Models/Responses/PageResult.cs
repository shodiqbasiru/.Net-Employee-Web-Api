namespace EmployeeApi.Models.Responses;

public class PageResult<T>
{
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
    public IEnumerable<T> Content { get; set; } = new List<T>();
}