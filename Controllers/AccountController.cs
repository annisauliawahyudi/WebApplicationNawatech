using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplicationNawatech.Data;
using WebApplicationNawatech.Models;
using WebApplicationNawatech.ViewModels;

namespace WebApplicationNawatech.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        //POST: /Account/Register
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Email = model.Email,
                    Password = model.Password,
                    Name = model.Name,
                    ProfilePicture = ""
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Registration successful! Please login.";

                return RedirectToAction("Login");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            System.Diagnostics.Debug.WriteLine("Mencoba render view Login");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            string? returnUrl = Request.Form["returnUrl"];

            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    // Simpan ke session (opsional)
                    HttpContext.Session.SetInt32("UserId", user.Id);
                    HttpContext.Session.SetString("UserName", user.Name);
                    HttpContext.Session.SetString("UserPhoto", user.ProfilePicture ?? "");

                    // 👇 Autentikasi Cookie — WAJIB supaya [Authorize] berfungsi
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserId", user.Id.ToString())
            };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    TempData["SuccessMessage"] = $"Welcome, {user.Name}!";

                    // Redirect ke halaman tujuan (returnUrl) jika ada
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    // Redirect default
                    return RedirectToAction("Index", "Product");
                }

                ModelState.AddModelError(string.Empty, "Invalid email or password");
            }

            return View(model);
        }



        public IActionResult ListUsers()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var users = _context.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public IActionResult EditProfile()
        {
            // Ambil user yang sedang login
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.Find(userId);

            if (user == null) return RedirectToAction("Login");

            var model = new EditProfileViewModel
            {
                Name = user.Name
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditProfile(EditProfileViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.Find(userId);

            if (user == null) return RedirectToAction("Login");

            if (ModelState.IsValid)
            {
                user.Name = model.Name;

                if (model.ProfilePicture != null)
                {
                    // Simpan gambar ke folder wwwroot/uploads
                    var fileName = $"{Guid.NewGuid()}_{model.ProfilePicture.FileName}";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.ProfilePicture.CopyTo(stream);
                    }

                    user.ProfilePicture = "/uploads/" + fileName;
                }

                _context.SaveChanges();

                HttpContext.Session.SetString("UserName", user.Name);
                HttpContext.Session.SetString("UserPhoto", user.ProfilePicture ?? "");

                TempData["SuccessMessage"] = "Profile updated successfully!";

                return RedirectToAction("Index", "Product");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();

            TempData["LogoutMessage"] = "You have logged out successfully.";

            return RedirectToAction("Index", "Home");
        }

    }
}
