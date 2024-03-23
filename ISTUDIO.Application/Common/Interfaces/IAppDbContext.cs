using ISTUDIO.Domain.EntityModel;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ISTUDIO.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<FamilyCustomersEntity> FamilyUsers { get; set; }
    DbSet<CustomerImagesEntity> UserImages { get; set; }
    DbSet<SmsNikitaRequest> SmsNikitaRequests { get; set; }
    DbSet<SmsNikitaResponse> SmsNikitaResponses { get; set; }
    DbSet<SmsNikitaStatus> SmsNikitaStatuses { get; set; }
    DbSet<CategoryEntity> Categories { get; set; }
    DbSet<CustomersEntity> Customers { get; set; }
    DbSet<CustomerImagesEntity> CustomerImages { get; set; }
    
    DatabaseFacade Database { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
