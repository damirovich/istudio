
using Microsoft.AspNetCore.Identity;

namespace ISTUDIO.Infrastructure.Identity;

public static class IdentityResultExtensions
{
    public static Result ToApplicationResult(this IdentityResult result)
    {
        return result.Succeeded
            ? Result.Success()
            : Result.Failure(result.Errors.Select(x => x.Description));
    }
}
