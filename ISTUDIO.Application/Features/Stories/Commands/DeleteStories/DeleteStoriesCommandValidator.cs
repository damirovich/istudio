namespace ISTUDIO.Application.Features.Stories.Commands.DeleteStories;

public class DeleteStoriesCommandValidator : AbstractValidator<DeleteStoriesCommand>
{
    public DeleteStoriesCommandValidator()
    {
        RuleFor(x => x.StoriesId)
            .GreaterThan(0)
            .WithMessage("StoriesId сторис должен быть больше 0.");
    }
}
