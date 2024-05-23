namespace ISTUDIO.Application.Features.Banners.Commands.CreateBanners;

public class CreateBannersCommandValidator : AbstractValidator<CreateBannersCommand>
{
    public CreateBannersCommandValidator()
    {
        RuleFor(x => x.PhotoBanner)
            .NotEmpty().WithMessage("PhotoBanner is required.");

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 1).WithMessage("Status must be either 0 or 1.");

        //RuleFor(x => x.CategoryId)
        //    .GreaterThan(0).When(x => x.CategoryId.HasValue).WithMessage("CategoryId must be greater than 0 if provided.");

        //RuleFor(x => x.DiscountId)
        //    .GreaterThan(0).When(x => x.DiscountId.HasValue).WithMessage("DiscountId must be greater than 0 if provided.");

        //RuleFor(x => x.ProductId)
        //    .GreaterThan(0).When(x => x.ProductId.HasValue).WithMessage("ProductId must be greater than 0 if provided.");
    }
    
}
