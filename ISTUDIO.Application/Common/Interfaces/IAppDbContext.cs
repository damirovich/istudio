using ISTUDIO.Domain.EntityModel;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ISTUDIO.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<FamilyUserEntity> FamilyUsers { get; set; }
    DbSet<UserImagesEntity> UserImages { get; set; }
    DatabaseFacade Database { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
