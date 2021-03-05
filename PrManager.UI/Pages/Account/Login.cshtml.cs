using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PrManager.DAL;
using PrManager.UI.Extensions;
using PrManager.UI.Models;

namespace PrManager.UI.Pages.Account
{
    public class Login : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty] public AuthViewModel Auth { get; set; }

        public Login(AppDbContext context) => _context = context;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _context.Users.Where(u => u.UserName == Auth.UserName).FirstOrDefaultAsync();
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid credentials");
                return Page();
            }

            var passwordHasher = new PasswordHasher<string>();
            if (passwordHasher.VerifyHashedPassword(null, user.HashedPassword, Auth.Password) !=
                PasswordVerificationResult.Success)
            {
                ModelState.AddModelError(string.Empty, "Invalid credentials");
                return Page();
            }

            // var claimsIdentity = user.Identity();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToPage("/admin/dashboard");
        }
    }
}