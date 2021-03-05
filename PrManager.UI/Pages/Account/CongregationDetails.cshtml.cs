using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PrManager.BL.Models;
using PrManager.DAL;
using PrManager.UI.Extensions;
using PrManager.UI.Models;

namespace PrManager.UI.Pages.Account
{
    public class CongregationDetails : PageModel
    {
        private readonly AppDbContext _context;

        public CongregationDetails(AppDbContext context) => _context = context;

        [BindProperty] public CongregationDetailViewModel CongregationDetail { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var currentUserId = HttpContext.User.Identity?.Name;
            var congId = await _context.Congregations
                .Where(x => x.CreatorId == Convert.ToInt32(currentUserId))
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            if (congId > 0)
            {

                var userIdt = HttpContext.User.Identity;
                var newPublicator = new Publicator()
                {
                    CongregationId = congId,
                    Email = userIdt?.GetEmail(),
                    FirstName = userIdt.GetFirstName(),
                    LastName = userIdt.GetLastName()
                };

                await _context.Publicators.AddAsync(newPublicator);
                await _context.SaveChangesAsync();

                string query = $"UPDATE users SET PublicatorId = '{newPublicator.Id}' WHERE Id = '{userIdt?.Name}'";
                await _context.Database.ExecuteSqlRawAsync(query);
                
                User.AddKey("PublicatorId", newPublicator.Id.ToString());

                return RedirectToPage("/admin/dashboard");

            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            var congregationTaken = await _context
                .Congregations
                .Where(x =>
                    x.CongregationName == CongregationDetail.CongregationName
                    && x.CongregationNumber == CongregationDetail.CongregationNumber
                )
                .CountAsync();

            if (congregationTaken > 0)
            {
                ModelState.AddModelError(string.Empty, "Already exists a congregation with those details");
                return Page();
            }

            var userIdt = HttpContext.User.Identity;

            var newPublicator = new Publicator()
            {
                Congregation = new Congregation()
                {
                    CreatorId = Convert.ToInt32(userIdt?.Name),
                    CongregationName = CongregationDetail.CongregationName,
                    CongregationNumber = CongregationDetail.CongregationNumber
                },
                FirstName = userIdt.GetFirstName(),
                LastName = userIdt.GetLastName(),
                Email = userIdt.GetEmail()
            };
            
            await _context.Publicators.AddAsync(newPublicator);
            await _context.SaveChangesAsync();
            
            string query = $"UPDATE users SET PublicatorId = '{newPublicator.Id}' WHERE Id = '{userIdt?.Name}'";
            await _context.Database.ExecuteSqlRawAsync(query);
            return RedirectToPage("/admin/dashboard");
        }
    }
}