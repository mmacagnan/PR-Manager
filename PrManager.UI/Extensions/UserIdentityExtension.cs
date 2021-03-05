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

        public static string GetEmail(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity) identity).FindFirst(ClaimTypes.Email);
            return (claim != null) ? claim.Value : string.Empty;
        }
        
        public static string GetFirstName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity) identity).FindFirst(ClaimTypes.GivenName);
            return (claim != null) ? claim.Value : string.Empty;
        }
        
        public static string GetLastName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity) identity).FindFirst(ClaimTypes.Surname);
            return (claim != null) ? claim.Value : string.Empty;
        }
        
        /// <summary>
        /// Add new key or update key from current principal claims
        /// </summary>
        /// <param name="currentPrincipal">the current user context</param>
        /// <param name="key">new key to add or update</param>
        /// <param name="value">value to add to key</param>
        public static void AddKey(this IPrincipal currentPrincipal, string key, string value)
        {
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
            {
                var identity = currentPrincipal.Identity as ClaimsIdentity;
                var existingClaim = identity?.FindFirst(key);
                if (existingClaim != null)
                {
                    identity.RemoveClaim(existingClaim);
                }
                identity?.AddClaim(new Claim(key, value));
            }
            
        }
    }
}