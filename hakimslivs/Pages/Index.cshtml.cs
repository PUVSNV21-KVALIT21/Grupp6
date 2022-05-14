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
        [FromQuery]
        public string SearchTerm { get; set; }

        public async Task OnGetAsync(string currentCategory)
        {
            IQueryable<Item> query = _context.Items.Include(i => i.Category);
            Categories = await _context.Categories.ToListAsync();
            if (!String.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(p => p.Product.ToLower().Contains(SearchTerm.ToLower()) || p.Category.Name.ToLower().Contains(SearchTerm.ToLower()) || p.Description.ToLower().Contains(SearchTerm.ToLower()) || p.Price.ToString().Contains(SearchTerm) || p.Stock.ToString().Contains(SearchTerm));
            }


            if (!String.IsNullOrEmpty(currentCategory))
            {
                query = query.Where(i => i.Category.Name == currentCategory);
            }

            Items = await query.ToListAsync();
        }
    }
}
