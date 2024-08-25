namespace ISTUDIO.Application.Features.Magazines.Commands.DeleteMagazines;

public class DeleteMagazinesCommandValidator : AbstractValidator<DeleteMagazinesCommand>
{
    public DeleteMagazinesCommandValidator()
    {
        RuleFor(v => v.MagazineId).NotEmpty().WithMessage("MagazineId не должен быть пустым.")
           .GreaterThan(0).WithMessage("MagazineId должен быть положительным числом.");
    }
}
