using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_Razor_Temp.Data;
using Project_Razor_Temp.Models;

namespace Project_Razor_Temp.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _context;
        public Category Category { get; set; }
      
        public DeleteModel(AppDbContext context)
        {
            _context = context;
        }
        public void OnGet(int? id)
        {
            Category = _context.Categories.Find(id);
        }

        public IActionResult OnPost() 
        {
            _context.Categories.Remove(Category);
            TempData["success"] = "category deleted with success!";
            _context.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
