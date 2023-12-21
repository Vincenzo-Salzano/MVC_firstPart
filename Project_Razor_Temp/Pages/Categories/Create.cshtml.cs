using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_Razor_Temp.Data;
using Project_Razor_Temp.Models;

namespace Project_Razor_Temp.Pages.Categories
{
    [BindProperties]  // This Data Annotation binds all the properties to the OnPost method (example the Category prop)
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        public Category Category { get; set; }

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            _context.Categories.Add(Category);
            TempData["success"] = "new category created with success!";
            _context.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
