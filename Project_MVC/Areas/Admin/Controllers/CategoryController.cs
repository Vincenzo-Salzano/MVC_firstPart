using Microsoft.AspNetCore.Mvc;
using Project.DataAccess.Data;
using Project.DataAccess.Repository.IRepository;
using Project.Models;

namespace Project_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        // Thanks to Dipendency Injection, with the next few lines of code, it's possible to use
        // an implementation of ApplicationDbContext class. This is also possible because in Program.cs we
        // set up the related Services
        private readonly IUnitOfWork _context;
        public CategoryController(IUnitOfWork context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            // Here we gather all the data registered in the Category table of the Database and we insert them into a List
            List<Category> categories = _context.Category.GetAll().ToList();
            // next we pass the categories list as parameter of the View method. This will provide the list to the related View!
            return View(categories);
        }

        // The following action is related to the Create View, which will be opened on the click of the "New Category!" button inside the 
        // Category/Index.html View
        public IActionResult Create()
        {
            return View();
        }
        // This is the action needed to Create a category after the submit button is clicked
        // First we set the endpoint of the post method
        [HttpPost]
        public IActionResult Create(Category obj) // Here we pass the Category objet created with the form, as parameter
        {
            // This if statement checks if the Category Name and the display order have the same value
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                // The parameter string key inside the AddModelError method, is the same as the input field asp-for=""
                ModelState.AddModelError("name", "The Category Name value must be different from Display Order value");
            }
            // This if statement checks if the model (Category object) state is valid (Data Annotations are respected). If not, we stay on the
            // create Category page, and the message errors (setted with the tag helpers) will be shown.
            if (ModelState.IsValid)
            {
                _context.Category.Add(obj); // Here we use Entity to set the new Category object created
                _context.Save(); // Here we save the changes on the actual database
                TempData["success"] = "new category created with success!"; // Tempdata will show a messagge that confirms the operation's success. To set it, check the Index page
                return RedirectToAction("Index"); // here we want to go back to the Index page of the Category Views, after the action is concluded
            }
            return View();
        }

        // Edit method for getting the category data element according to the provided id
        public IActionResult Edit(int id)
        {
            // The NotFound method is called according to the if statement
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // here we get the element from the db, according to the linq expression.
            Category? catFromDb = _context.Category.Get(c => c.Id == id);
            if (catFromDb == null)
            {
                return NotFound();
            }
            return View(catFromDb); // we pass the value in order to be shown in the next view. Check also the related tag helper in the /Category/Index page
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            if (obj.Name == obj.DisplayOrder.ToString())
            {

                ModelState.AddModelError("name", "The Category Name value must be different from Display Order value");
            }

            if (ModelState.IsValid)
            {
                // Here we call the Update method instead of Add method
                _context.Category.Update(obj);
                _context.Save();
                TempData["success"] = "category updated with success!";
                return RedirectToAction("Index");
            }
            // The View this time is the Edit View
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? catFromDb = _context.Category.Get(u => u.Id == id);
            if (catFromDb == null)
            {
                return NotFound();
            }
            return View(catFromDb);
        }
        // Here is necessary to define the ActionName, even if the next method has already a name (DeletePOST)
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? category = _context.Category.Get(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Category.Remove(category);
            _context.Save();
            TempData["success"] = "category removed with success!";
            return RedirectToAction("Index");
        }
    }
}
