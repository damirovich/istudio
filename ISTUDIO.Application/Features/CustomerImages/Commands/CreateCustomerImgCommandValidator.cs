using ISTUDIO.Application.Features.ModelsDTO;

namespace ISTUDIO.Application.Features.CustomerImages.Commands;

public class CreateCustomerImgCommandValidator : AbstractValidator<CreateCustomerImgCommand>
{
    public CreateCustomerImgCommandValidator()
    {
        RuleFor(x => x.CustomerImages).NotEmpty().WithMessage("Список изображений не должен быть пустым");
        RuleForEach(x => x.CustomerImages).SetValidator(new CustomerImagesDTOValidator());
    }

}
public class CustomerImagesDTOValidator : AbstractValidator<CustomerImagesDTO>
{
    public CustomerImagesDTOValidator()
    {
        RuleFor(x => x.Url).NotEmpty().WithMessage("URL изображения обязателен");
        // Добавьте другие правила валидации по мере необходимости
    }
}