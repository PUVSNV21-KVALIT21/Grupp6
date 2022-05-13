using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hakimslivs.Data;
using hakimslivs.Models;
using Microsoft.AspNetCore.Identity;

namespace hakimslivs.Pages.Admin.OrderManager
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EditModel(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        [BindProperty]
        public Order Order { get; set; }

        public List<SelectListItem> OrderStatuses { get; set; }

        public async Task LoadOrderStatuses()
        {
            OrderStatuses = await _context.OrdersStatuses.Select(os => new SelectListItem
            {
                Value = os.OrderStatusName,
                Text = os.OrderStatusName,
            }).Distinct()
                .ToListAsync();

            SelectListItem none = new SelectListItem
            {
                Value = "Ingen",
                Text = "Ingen"
            };
            OrderStatuses.Insert(0, none);
        }

        public async Task LoadOrder(int? id)
        {
            Order = await _context.Orders.Include(o => o.OrderStatus).FirstOrDefaultAsync(m => m.ID == id);
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await LoadOrderStatuses();

            try
            {
                await LoadOrder(id);
            }
            catch
            {
                return NotFound();
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id, Order order)
        {
            await LoadOrder(id);

            if (!ModelState.IsValid)
            {
                return Page();
            }


            try
            {
                Order.OrderStatus = await _context.OrdersStatuses.FirstAsync(o => o.OrderStatusName == order.OrderStatus.OrderStatusName);
            }
            catch
            {
                Order.OrderStatus = null;
            }

            return RedirectToPage("./Index");
        }
    }
}
