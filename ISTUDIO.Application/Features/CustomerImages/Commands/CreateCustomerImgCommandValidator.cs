using ISTUDIO.Application.Features.ModelsDTO;

namespace ISTUDIO.Application.Features.CustomerImages.Commands;

public class CreateCustomerImgCommandValidator : AbstractValidator<CreateCustomerImgCommand>
{
    public CreateCustomerImgCommandValidator()
    {
        RuleFor(v => v.CustomerImages).NotEmpty().WithMessage("Список изображений не должен быть пустым");
        RuleForEach(v => v.CustomerImages).SetValidator(new CustomerImagesDTOValidator());
    }

}
public class CustomerImagesDTOValidator : AbstractValidator<CustomerImagesDTO>
{
    public CustomerImagesDTOValidator()
    {
        RuleFor(v => v.Url).NotEmpty().WithMessage("URL изображения обязателен");
        // Добавьте другие правила валидации по мере необходимости
    }
}