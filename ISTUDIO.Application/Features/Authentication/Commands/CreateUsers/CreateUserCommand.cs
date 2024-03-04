namespace ISTUDIO.Application.Features.Authentication.Commands.CreateUsers;

public class CreateUserCommand : IRequest<string>
{
    public string? FullName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public List<string>? Roles { get; set; } = new List<string>() { "Anonymous" };
}
