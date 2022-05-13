using hakimslivs.Data;
using hakimslivs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace hakimslivs.Pages
{
    [Authorize(Roles = "SuperAdmin, Admin, Moderator, Basic")]
    public class InvoiceModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public InvoiceModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public List<IdentityRole> Roles { get; set; }
        public ApplicationUser IdentityUser { get; set; }
        public Order Order { get; set; }
        public List<ItemQuantity> ItemQuantities { get; set; }
        public decimal TotalSum { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Roles = await _roleManager.Roles.ToListAsync();
            var UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IdentityUser = await _userManager.FindByIdAsync(UserID);
            Thread.Sleep(5000);
            Order = _context.Orders.Include(o => o.User).Where(o => o.User == IdentityUser).OrderByDescending(o => o.OrderDate).First();
            if (Order == null)
            {
                return NotFound();
            }
            ItemQuantities = _context.ItemQuantities.Include(i => i.Order).Include(i => i.Item).Where(i => i.Order == Order).ToList();

            return Page();
        }
    }
}
