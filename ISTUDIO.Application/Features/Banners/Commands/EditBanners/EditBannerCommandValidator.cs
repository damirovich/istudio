namespace ISTUDIO.Application.Features.Banners.Commands.EditBanners;

public class EditBannerCommandValidator : AbstractValidator<EditBannerCommand>
{
    public EditBannerCommandValidator()
    {
        RuleFor(x => x.BannerId)
            .GreaterThan(0).WithMessage("BannerId должен быть больше 0.");

        RuleFor(x => x.PhotoBanner)
            .NotEmpty().When(x => x.PhotoBanner != null).WithMessage("PhotoBanner обязательный.");

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 1).WithMessage("Статус должен быть 0 или 1");
    }
}
