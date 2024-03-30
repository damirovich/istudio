namespace ISTUDIO.Application.Features.Customers.Commands.DeleteCustomers;

public class DeleteCustomersCommandValidator : AbstractValidator<DeleteCustomersCommand>
{
    public DeleteCustomersCommandValidator() 
    {
        RuleFor(v => v.CustomerId).NotEmpty().WithMessage("CustomerId не должен быть пустым.")
            .GreaterThan(0).WithMessage("CustomerId должен быть положительным числом.");
    }
}
