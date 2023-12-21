using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_Razor_Temp.Data;
using Project_Razor_Temp.Models;

namespace Project_Razor_Temp.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public Category Category { get; set; }
        public EditModel(AppDbContext context)
        {
            _context= context;
        }
        public void OnGet(int? id)
        {
            Category =_context.Categories.Find(id);
        }

        public IActionResult OnPost()
        {
            _context.Categories.Update(Category);
            TempData["success"] = "category updated with success!";
            _context.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
