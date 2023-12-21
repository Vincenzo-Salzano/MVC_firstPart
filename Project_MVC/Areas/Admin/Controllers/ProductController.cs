using Microsoft.AspNetCore.Mvc;
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
