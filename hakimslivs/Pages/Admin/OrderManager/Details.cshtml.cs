using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using hakimslivs.Data;
using hakimslivs.Models;

namespace hakimslivs.Pages.Admin.OrderManager
{
    public class DetailsModel : PageModel
    {
        private readonly hakimslivs.Data.ApplicationDbContext _context;

        public DetailsModel(hakimslivs.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Order Order { get; set; }
        public List<ItemQuantity> ItemQuantityList { get; set; }
        public List<Item> Items { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _context.Orders.Include(o => o.OrderStatus).FirstOrDefaultAsync(m => m.ID == id);
            ItemQuantityList = await _context.ItemQuantities.Include(iq => iq.Item).Where(iq => iq.OrderID == Order.ID).ToListAsync();
            Items = await _context.Items.ToListAsync();

            if (Order == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
