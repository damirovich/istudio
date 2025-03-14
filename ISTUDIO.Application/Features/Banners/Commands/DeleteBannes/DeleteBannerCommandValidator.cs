﻿namespace ISTUDIO.Application.Features.Banners.Commands.DeleteBannes;

public class DeleteBannerCommandValidator : AbstractValidator<DeleteBannerCommand>
{
    public DeleteBannerCommandValidator()
    {
        RuleFor(x => x.BannerId)
            .GreaterThan(0).WithMessage("BannerId должен быть больше 0.");
    }
}
