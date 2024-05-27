namespace ISTUDIO.Application.Features.Banners.Commands.CreateBanners;

public class CreateBannersCommandValidator : AbstractValidator<CreateBannersCommand>
{
    public CreateBannersCommandValidator()
    {
        RuleFor(x => x.PhotoBanner)
            .NotEmpty().WithMessage("PhotoBanner обязательный.");

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 1).WithMessage("Статус должен быть 0 или 1.");
    }
    
}
