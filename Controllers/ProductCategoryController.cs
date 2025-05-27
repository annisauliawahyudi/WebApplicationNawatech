using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationNawatech.Data;
using WebApplicationNawatech.Models;

namespace WebApplicationNawatech.Controllers
{
    [Authorize]
    public class ProductCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page = 1)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.Find(userId);

            if (user == null) return RedirectToAction("Login");

            int pageSize = 5;

            var categoriesQuery = _context.ProductCategories
                .Where(c => !c.IsDeleted);

            var categories = categoriesQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            int totalItems = categoriesQuery.Count();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            return View(categories);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.Find(userId);

            if (user == null) return RedirectToAction("Login");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductCategory model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.Find(userId);

            if (user == null) return RedirectToAction("Login");

            if (ModelState.IsValid)
            {
                _context.ProductCategories.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.Find(userId);

            if (user == null) return RedirectToAction("Login");

            var category = _context.ProductCategories.FirstOrDefault(c => c.Id == id && !c.IsDeleted);
            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductCategory category)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.Find(userId);

            if (user == null) return RedirectToAction("Login");

            if (ModelState.IsValid)
            {
                var existing = _context.ProductCategories.FirstOrDefault(c => c.Id == category.Id);
                if (existing == null) return NotFound();

                existing.Name = category.Name;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.Find(userId);

            if (user == null) return RedirectToAction("Login");

            var category = _context.ProductCategories.FirstOrDefault(c => c.Id == id && !c.IsDeleted);
            if (category == null) return NotFound();

            category.IsDeleted = true;
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Product successfully deleted.";

            return RedirectToAction("Index");
        }
    }
}
