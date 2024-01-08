using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.DataAccess.Data;
using Project.DataAccess.Repository.IRepository;
using Project.Models;

namespace Project_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _context;
        public ProductController(IUnitOfWork context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
           List<Product> products = _context.Product.GetAll().ToList();
            return View(products);
        }

        public IActionResult CreateProduct()
        {
			// Using an item of the class SelectListItem. This will be populated with key-values of the Categories. After this, we will pass the list
			// in the View CreateProduct in a listbox, by using the ViewBag of Projections EF Core.
			IEnumerable<SelectListItem> CategoryList = _context.Category.GetAll().Select(u => new SelectListItem
			{
				// Text and Value are the two string type parameters of the ctor of SelectListItem. We pass them as name and Id of Category EF item
				Text = u.Name,
				Value = u.Id.ToString(),
			});
			// Now we use the ViewBag to pass the list to the view
			ViewBag.CategoryList = CategoryList;
			// Check the HTML code in the View CreateProduct
			return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
			if (product.Title == product.Author.ToString())
			{
				// The parameter string key inside the AddModelError method, is the same as the input field asp-for=""
				ModelState.AddModelError("name", "The Title value must be different from Author value");
			}
			if (ModelState.IsValid)
			{
				_context.Product.Add(product); // Here we use Entity to set the new Product object created
				_context.Save(); // Here we save the changes on the actual database
				TempData["success"] = "new product created with success!"; // Tempdata will show a messagge that confirms the operation's success. To set it, check the Index page
				return RedirectToAction("Index"); // here we want to go back to the Index page of the Category Views, after the action is concluded
			}
			return View();
        }

		public IActionResult EditProduct(int id)
		{
			// The NotFound method is called according to the if statement
			if (id == null || id == 0)
			{
				return NotFound();
			}
			// here we get the element from the db, according to the linq expression.
			Product? productFromDb = _context.Product.Get(c => c.Id == id);
			if (productFromDb == null)
			{
				return NotFound();
			}
			return View(productFromDb); // we pass the value in order to be shown in the next view. Check also the related tag helper in the /Product/Index page
		}

		[HttpPost]
		public IActionResult EditProduct(Product product) 
		{
			if (ModelState.IsValid)
			{
				_context.Product.Update(product);
				_context.Save();
				TempData["success"] = "product updated with success!";
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult DeleteProduct(int? id)
		{
			if(id == null || id == 0)
			{
				return NotFound();
			}
			Product? productFromDb = _context.Product.Get(p => p.Id == id);
			if (productFromDb == null) 
			{
				return NotFound();
			}
			return View(productFromDb);
		}

		[HttpPost, ActionName("DeleteProduct")]
		public IActionResult DeleteProductPOST(int? id)
		{
			Product? product = _context.Product.Get(u => u.Id == id);
			if (product == null)
			{
				return NotFound();
			}
			_context.Product.Remove(product);
			_context.Save();
			TempData["success"] = "category removed with success!";
			return RedirectToAction("Index");
		}
	}
}
