namespace ISTUDIO.Application.Features.Authentication.Commands.RefreshJWT;

public class RefreshJWTCommandValidator : AbstractValidator<RefreshJWTCommand>
{
    public RefreshJWTCommandValidator()
    {
        RuleFor(v => v.AccessToken).NotEmpty();
        RuleFor(v => v.RefreshToken).NotEmpty();
    }
}
