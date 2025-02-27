using Microsoft.AspNetCore.Identity;

namespace ISTUDIO.Infrastructure.Identity;

public class RolePermissionEntity
{
    public string RoleId { get; set; } // IdentityRole.Id
    public IdentityRole Role { get; set; }

    public int PermissionId { get; set; } // PermissionEntity.Id
    public PermissionEntity Permission { get; set; }
}
