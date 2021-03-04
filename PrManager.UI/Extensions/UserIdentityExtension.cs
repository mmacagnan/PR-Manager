using System.Security.Claims;
using System.Security.Principal;

namespace PrManager.UI.Extensions
{
    public static class UserIdentityExtension
    {
        public static string GetPublicatorId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity) identity).FindFirst("PublicatorId");
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}