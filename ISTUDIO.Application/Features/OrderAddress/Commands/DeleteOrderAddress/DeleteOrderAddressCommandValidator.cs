namespace ISTUDIO.Application.Features.OrderAddress.Commands.DeleteOrderAddress;

public class DeleteOrderAddressCommandValidator : AbstractValidator<DeleteOrderAddressCommand>
{
    public DeleteOrderAddressCommandValidator()
    {
        RuleFor(v=>v.Id)
            .NotEmpty().WithMessage("Id не должен быть пустым.")
           .GreaterThan(0).WithMessage("Id должен быть положительным числом.");
    }
}
