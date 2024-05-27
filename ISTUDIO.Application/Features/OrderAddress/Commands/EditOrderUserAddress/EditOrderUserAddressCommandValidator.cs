namespace ISTUDIO.Application.Features.OrderAddress.Commands.EditOrderAddress;

public class EditOrderUserAddressCommandValidator : AbstractValidator<EditOrderUserAddressCommand>
{
    public EditOrderUserAddressCommandValidator()
    {
        RuleFor(x => x.Id)
             .GreaterThan(0).WithMessage("Id должен быть больше 0.");

        RuleFor(x => x.Region)
            .NotEmpty().WithMessage("Регион обязателен для заполнения.")
            .Matches(@"^[а-яА-ЯёЁa-zA-Z\s]+$").WithMessage("Регион должен содержать только буквы.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("Город обязателен для заполнения.")
            .Matches(@"^[а-яА-ЯёЁa-zA-Z\s]+$").WithMessage("Город должен содержать только буквы.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Адрес обязателен для заполнения.")
            .MaximumLength(500).WithMessage("Адрес не должен превышать 500 символов.");

        RuleFor(x => x.Comments)
            .MaximumLength(500).WithMessage("Комментарии не должны превышать 500 символов.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId обязателен для заполнения.")
            .MaximumLength(250).WithMessage("UserId не должен превышать 250 символов.");

    }
}
