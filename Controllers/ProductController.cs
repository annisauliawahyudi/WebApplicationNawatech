using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationNawatech.Data;
using WebApplicationNawatech.Models;
using WebApplicationNawatech.ViewModels;

namespace WebApplicationNawatech.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.Find(userId);

            if (user == null) return RedirectToAction("Login");

            int pageSize = 5;

            var productsQuery = _context.Products
                .Include(p => p.ProductCategory)
                .Where(p => !p.IsDeleted);

            var products = productsQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            int totalItems = productsQuery.Count();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            return View(products);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.Find(userId);

            if (user == null) return RedirectToAction("Login");

            ViewBag.Categories = _context.ProductCategories
                .Where(c => !c.IsDeleted)
                .ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateProductViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.Find(userId);

            if (user == null) return RedirectToAction("Login");

            if (ModelState.IsValid)
            {
                string imageUrl = "";

                if (model.ImageFile != null)
                {
                    var fileName = $"{Guid.NewGuid()}_{model.ImageFile.FileName}";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.ImageFile.CopyTo(stream);
                    }

                    imageUrl = "/uploads/" + fileName;
                }

                var product = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    ProductCategoryId = model.ProductCategoryId,
                    ImageUrl = imageUrl
                };

                _context.Products.Add(product);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Categories = _context.ProductCategories
                .Where(c => !c.IsDeleted)
                .ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.Find(userId);

            if (user == null) return RedirectToAction("Login");

            var product = _context.Products.FirstOrDefault(p => p.Id == id && !p.IsDeleted);
            if (product == null) return NotFound();

            var model = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ProductCategoryId = product.ProductCategoryId,
                ExistingImageUrl = product.ImageUrl
            };

            ViewBag.Categories = _context.ProductCategories
                .Where(c => !c.IsDeleted)
                .ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.Find(userId);

            if (user == null) return RedirectToAction("Login");

            var product = _context.Products.FirstOrDefault(p => p.Id == model.Id && !p.IsDeleted);
            if (product == null) return NotFound();

            // Update hanya jika field tidak null/kosong
            if (!string.IsNullOrEmpty(model.Name))
                product.Name = model.Name;

            if (model.Price.HasValue)
                product.Price = model.Price.Value;

            if (model.ProductCategoryId.HasValue)
                product.ProductCategoryId = model.ProductCategoryId.Value;

            if (model.Image != null)
            {
                var fileName = $"{Guid.NewGuid()}_{model.Image.FileName}";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(stream);
                }

                product.ImageUrl = "/uploads/" + fileName;
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.Find(userId);

            if (user == null) return RedirectToAction("Login");

            var product = _context.Products.FirstOrDefault(p => p.Id == id && !p.IsDeleted);
            if (product == null) return NotFound();

            product.IsDeleted = true;
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Product successfully deleted.";

            return RedirectToAction("Index");
        }



    }
}
