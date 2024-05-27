namespace ISTUDIO.Application.Features.UserManagement.Commands.UpdateUserPhotoProfile;

public class UpdateUserPhotoProfileCommandValidator : AbstractValidator<UpdateUserPhotoProfileCommand>
{
    public UpdateUserPhotoProfileCommandValidator()
    {
        RuleFor(v => v.UserId).NotEmpty().WithMessage("UserId не должно быть пустым.");
        RuleFor(v => v.PhotoUrl).NotEmpty().WithMessage("PhotoUrl должен быть пустым.");
    }
}
