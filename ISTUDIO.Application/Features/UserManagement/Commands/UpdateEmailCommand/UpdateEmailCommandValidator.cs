﻿namespace ISTUDIO.Application.Features.UserManagement.Commands.UpdateEmailCommand;

public class UpdateEmailCommandValidator : AbstractValidator<UpdateEmailCommand>
{
    public UpdateEmailCommandValidator()
    {
        RuleFor(v => v.UserId).NotEmpty();
        RuleFor(v => v.Email)
            .MaximumLength(250)
            .NotEmpty();
    }
}
