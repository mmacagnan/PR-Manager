using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PrManager.BL.Models;
using PrManager.DAL;
using PrManager.UI.Extensions;
using PrManager.UI.Models;

namespace PrManager.UI.Pages.Account
{
    public class Signup : PageModel
    {
        private readonly AppDbContext _context;
        public Signup(AppDbContext context) => _context = context;

        [BindProperty] public SignUpViewModel SignUp { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            var userExists = await _context.Users
                .Where(x => x.UserName == SignUp.UserName)
                .CountAsync();
            
            if (userExists > 0)
            {
                ModelState.AddModelError("UserName", "This username is already in use");
                return Page();
            }

            var passwordHasher = new PasswordHasher<string>();

            var newUser = new User()
            {
                UserName =  SignUp.UserName,
                Email = SignUp.Email,
                HashedPassword =  passwordHasher.HashPassword(null, SignUp.Password),
                FirstName = SignUp.FirstName,
                LastName = SignUp.LastName
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            var claimsIdentity = newUser.Identity();
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return RedirectToPage("/account/congregation-details");
        }
    }
}