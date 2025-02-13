namespace ISTUDIO.Application.Features.Stories.Commands.EditStories;

public class EditStoriesCommandValidator : AbstractValidator<EditStoriesCommand>
{
    public EditStoriesCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id сторис должен быть больше 0.");

        RuleFor(x => x.ExpireAt)
            .NotEmpty()
            .WithMessage("Дата истечения не может быть пустой.")
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Дата истечения должна быть позже текущей даты.");

        RuleFor(x => x.IconPhoto)
            .Must(icon => icon == null || icon.Length > 0)
            .WithMessage("Если передана иконка, она не может быть пустой.");
    }
}
