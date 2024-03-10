namespace ISTUDIO.Application.Features.UserManagement.Commands.CreateUsers;
using ISTUDIO.Domain.EntityModel;

using ResModel = Result;
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResModel>
{
    private readonly IIdentityService _identityService;
    private readonly IAppDbContext _appDbContext;
    public CreateUserCommandHandler(IIdentityService identityService, IAppDbContext appDbContext) =>
                            (_identityService, _appDbContext) = (identityService, appDbContext);

    public async Task<ResModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _identityService.CreateUserAsync(request.FullName!, request.UserName!, request.Email!, request.Password!);

            if (!result.Result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, result.Result.Errors);
                throw new Exception($"Unable to create {request.UserName}.{Environment.NewLine}{errors}");
            }

            // Сохраняем изображения клиента
            foreach (var image in request.Images)
            {
                var userImage = new UserImagesEntity
                {
                    TypeImg = image.Type,
                    Url = image.Url,
                    Name = image.Name,
                    ContentType = image.ContentType,
                    UsersId = result.UserId // Привязываем изображение к пользователю
                };
                _appDbContext.UserImages.Add(userImage);
            }

            // Сохраняем данные о родственниках
            foreach (var familyMember in request.FamilyMembers)
            {
                var familyUser = new FamilyUserEntity
                {
                    FullName = familyMember.FullName,
                    PIN = familyMember.PIN,
                    PhoneNumber = familyMember.PhoneNumber,
                    PlaceOfWork = familyMember.PlaceOfWork,
                    RelationDegreeClient = familyMember.RelationDegreeClient,
                    UsersId = result.UserId // Привязываем родственника к пользователю
                };
                _appDbContext.FamilyUsers.Add(familyUser);
            }

            await _appDbContext.SaveChangesAsync(cancellationToken);

            var addUserToRole = await _identityService.AddToRolesAsync(result.UserId, request.Roles!);
            if (!addUserToRole.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, result.Result.Errors);
                throw new Exception($"Unable to add {request.UserName} to assigned role/s.{Environment.NewLine}{errors}");
            }
            return ResModel.Success();
        }
        catch (Exception ex)
        {
            return ResModel.Failure(new[] { ex.InnerException?.Message ?? ex.Message });
        }
    }
}