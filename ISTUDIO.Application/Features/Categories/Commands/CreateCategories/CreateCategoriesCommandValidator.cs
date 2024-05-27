namespace ISTUDIO.Application.Features.Categories.Commands.CreateCategories;

public class CreateCategoriesCommandValidator : AbstractValidator<CreateCategoriesCommand>
{
    public CreateCategoriesCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Имя категории обязательно.")
            .MaximumLength(100).WithMessage("Имя категории не должно превышать 100 символов.");

        RuleFor(v => v.Description)
            .NotEmpty().WithMessage("Описание категории обязательно.");

        //RuleFor(command => command.ImageUrl)
        //    .NotEmpty().WithMessage("URL изображения категории обязателен.")
        //    .Must(BeAValidUrl).WithMessage("Недопустимый формат URL изображения.");
    }
}
