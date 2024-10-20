namespace ISTUDIO.Application.Features.OrderAddress.Commands.CreateOrderUserAddress;

public class CreateOrderUserAddressCommandValidator : AbstractValidator<CreateOrderUserAddressCommand>
{
    public CreateOrderUserAddressCommandValidator()
    {
        RuleFor(x => x.Region)
           .NotEmpty().WithMessage("Регион обязателен для заполнения.")
           .Matches(@"^[а-яА-ЯёЁa-zA-Z\s\-.,]+$").WithMessage("Регион должен содержать только буквы и допустимые символы (-,.,).");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("Город обязателен для заполнения.");
            //.Matches(@"^[а-яА-ЯёЁa-zA-Z\s\-.,]+$").WithMessage("Город должен содержать только буквы.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Адрес обязателен для заполнения.")
            .MaximumLength(200).WithMessage("Адрес не должен превышать 200 символов.");

        RuleFor(x => x.Comments)
            .MaximumLength(500).WithMessage("Комментарии не должны превышать 500 символов.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId обязателен для заполнения.")
            .MaximumLength(250).WithMessage("UserId не должен превышать 250 символов.");
    }
}
