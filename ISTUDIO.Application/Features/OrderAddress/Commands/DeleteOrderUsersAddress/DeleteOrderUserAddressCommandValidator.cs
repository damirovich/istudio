namespace ISTUDIO.Application.Features.OrderAddress.Commands.DeleteOrderAddress;

public class DeleteOrderUserAddressCommandValidator : AbstractValidator<DeleteOrderUserAddressCommand>
{
    public DeleteOrderUserAddressCommandValidator()
    {
        RuleFor(v=>v.Id)
            .NotEmpty().WithMessage("Id не должен быть пустым.")
           .GreaterThan(0).WithMessage("Id должен быть положительным числом.");
    }
}
