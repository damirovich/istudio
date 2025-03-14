﻿namespace ISTUDIO.Application.Features.UserManagement.Commands.DeleteUser;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(v => v.UserId).NotEmpty().WithMessage("UserId не должен быть пустым");
    }
}
