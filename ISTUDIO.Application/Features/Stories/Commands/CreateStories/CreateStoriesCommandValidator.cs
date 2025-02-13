namespace ISTUDIO.Application.Features.Stories.Commands.CreateStories;

public class CreateStoriesCommandValidator : AbstractValidator<CreateStoriesCommand>
{
    public CreateStoriesCommandValidator()
    {
        RuleFor(x => x.IconPhoto)
            .NotEmpty()
            .WithMessage("Иконка обязательна для загрузки.")
            .Must(photo => photo.Length > 0)
            .WithMessage("Иконка не может быть пустой.");

        RuleFor(x => x.CreatedAt)
            .NotEmpty()
            .WithMessage("Дата создания не может быть пустой.");

        RuleFor(x => x.ExpireAt)
            .NotEmpty()
            .WithMessage("Дата истечения не может быть пустой.")
            .GreaterThan(x => x.CreatedAt)
            .WithMessage("Дата истечения должна быть позже даты создания.");
    }
}
