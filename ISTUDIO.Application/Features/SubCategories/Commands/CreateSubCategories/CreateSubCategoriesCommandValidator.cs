namespace ISTUDIO.Application.Features.SubCategories.Commands.CreateSubCategories;

public class CreateSubCategoriesCommandValidator : AbstractValidator<CreateSubCategoriesCommand>
{
    public CreateSubCategoriesCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("Имя Под Категории обязательно.")
            .MaximumLength(100).WithMessage("Имя Под Категории не должно превышать 100 символов.");

        RuleFor(command => command.Description)
            .NotEmpty().WithMessage("Описание Под Категории обязательно.");

        //RuleFor(command => command.ImageUrl)
        //    .NotEmpty().WithMessage("URL изображения категории обязателен.")
        //    .Must(BeAValidUrl).WithMessage("Недопустимый формат URL изображения.");
        RuleFor(v => v.CategoryId).NotEmpty().WithMessage("CategoryId категории не должен быть пустым.")
          .GreaterThan(0).WithMessage("CategoryId  должен быть положительным числом.");
    }
}
