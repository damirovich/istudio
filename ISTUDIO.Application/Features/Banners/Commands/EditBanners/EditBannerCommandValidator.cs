namespace ISTUDIO.Application.Features.Banners.Commands.EditBanners;

public class EditBannerCommandValidator : AbstractValidator<EditBannerCommand>
{
    public EditBannerCommandValidator()
    {
        RuleFor(x => x.BannerId)
            .GreaterThan(0).WithMessage("BannerId must be greater than 0.");

        RuleFor(x => x.PhotoBanner)
            .NotEmpty().When(x => x.PhotoBanner != null).WithMessage("PhotoBanner must be a valid image if provided.");

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
