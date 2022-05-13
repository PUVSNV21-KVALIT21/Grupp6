using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using hakimslivs.Data;
using hakimslivs.Models;

namespace hakimslivs.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Item> Items { get;set; }
        public IList<Category> Categories { get;set; }
        public string CurrentCategory { get; set; }

        public async Task OnGetAsync(string currentCategory)
        {
            if (!String.IsNullOrEmpty(currentCategory))
            {
                Items = await _context.Items.Include(i => i.Category).Where(i => i.Category.Name == currentCategory).ToListAsync();
            }
            else
            {
                Items = await _context.Items.Include(i => i.Category).ToListAsync();
            }
            Categories = await _context.Categories.ToListAsync();
        }
    }
}
