namespace ISTUDIO.Contracts.Features;

public record PaginatedListVM(
    [Required] int PageNumber,
    [Required] int PageSize
    );
