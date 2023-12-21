using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_Razor_Temp.Data;
using Project_Razor_Temp.Models;

namespace Project_Razor_Temp.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        public List<Category> CategoryList { get; set; }

        public IndexModel(AppDbContext context)
        {
            _context= context;
        }
        public void OnGet()
        {
            CategoryList = _context.Categories.ToList();
        }
    }
}
