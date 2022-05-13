using hakimslivs.Data;
using hakimslivs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Threading.Tasks;

namespace hakimslivs.Pages.Checkout
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public IndexModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public ApplicationUser IdentityUser { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IdentityUser = await _userManager.FindByIdAsync(UserID);

            return Page();
        }
    }
}