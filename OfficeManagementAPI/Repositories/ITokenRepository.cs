using Microsoft.AspNetCore.Identity;

namespace OfficeManagementAPI.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
