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

public class PaginatedParametersValidator : AbstractValidator<PaginatedParameters>
{
    public PaginatedParametersValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("PageNumber должен быть больше нуля.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("PageSize должен быть больше нуля.")
            .LessThanOrEqualTo(100).WithMessage("PageSize не должен превышать 100.");
    }
}

public class PaginationWithSearchParametersValidator : AbstractValidator<PaginationWithSearchParameters>
{
    public PaginationWithSearchParametersValidator()
    {
        Include(new PaginatedParametersValidator());

        RuleFor(x => x.SearchTerm)
            .MaximumLength(100).WithMessage("SearchTerm не должен превышать 250 символов.");
    }
}