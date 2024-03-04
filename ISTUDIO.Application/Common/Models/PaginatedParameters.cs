namespace ISTUDIO.Application.Common.Models;

public class PaginatedParameters
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; } = 5;
}
public class PaginationWithSearchParameters : PaginatedParameters
{
    public string? SearchTerm { get; set; }
}
