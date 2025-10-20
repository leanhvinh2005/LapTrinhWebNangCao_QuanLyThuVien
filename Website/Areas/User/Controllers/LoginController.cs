using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using Website.Data;
using Website.Models;
using Website.Services;

namespace Website.Areas.User.Controllers
{
    [Area("User")]
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserService _userService;
        private readonly LibrarianService _librarianService;

        public LoginController(ApplicationDbContext context, UserService userService, LibrarianService librarianService)
        {
            _context = context;
            _userService = userService;
            _librarianService = librarianService;
        }

        [Route("/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("/Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Website.Models.User user)
        {
            user.nameUser = "";

            ModelState.Remove(nameof(user.nameUser));

            if (!ModelState.IsValid)
            {
                Console.WriteLine("Model invalid:");
                foreach (var err in ModelState.Values.SelectMany(v => v.Errors))
                    Console.WriteLine(err.ErrorMessage);
                return View(user);
            }

            var users = await _context.ACCOUNT_USER
                .FirstOrDefaultAsync(u => u.emailUser == user.emailUser);

            if (users == null || users.passwordUser != user.passwordUser)
            {
                ModelState.AddModelError(string.Empty, "Please double check your email and password");
                return View(user);
            }

            var librarians = await _librarianService.SearchLibrarian(user.idUser.ToString());
            string role = "User";
            if (librarians.Count > 0)
                role = "Admin";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.nameUser),
                new Claim(ClaimTypes.Email, user.emailUser),
                new Claim("UserId", user.idUser.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
            
            if (role == "Admin")
                return RedirectToAction("Dashboard", "Dashboard");
            return RedirectToAction("Home", "Home");
        }

        [HttpPost("/Logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Login");
        }
    }
}
