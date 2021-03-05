using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using PrManager.BL.Models;

namespace PrManager.UI.Extensions
{
    public static class ClaimsBuilder
    {
        public static ClaimsIdentity Identity(this User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim("UserName", user.UserName),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim("PublicatorId", user.PublicatorId.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, "User");
            return claimsIdentity;
        }
    }
}