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

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
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

            var realUser = await _context.ACCOUNT_USER
                .FirstOrDefaultAsync(u => u.emailUser == user.emailUser);

            if (realUser == null || realUser.passwordUser != user.passwordUser)
            {
                ModelState.AddModelError(string.Empty, "Please double check your email and password");
                return View(user);
            }

            var librarians = await _context.THUTHU
                .Where(u => u.idUser == realUser.idUser)
                .ToListAsync();
            string role = "User";
            if (librarians.Count > 0)
                role = "Admin";

            var claims = new List<Claim>
            {
                new Claim("UserId", realUser.idUser.ToString()),
                new Claim(ClaimTypes.Name, realUser.nameUser),
                new Claim(ClaimTypes.Email, realUser.emailUser),
                new Claim(ClaimTypes.Role, role)
            };

            if (role == "User")
            {
                var member = await _context.DOCGIA
                    .FirstOrDefaultAsync(u => u.idUser == realUser.idUser);
                claims.Add(new Claim("CardId", member.idCard));
            }  

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            TempData["Message"] = "Đăng nhập tài khoản thành công";

            if (role == "Admin")
                return Redirect("/Dashboard");
            return RedirectToAction("Home", "Home");
        }

        [HttpPost("/Logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Clear();

            TempData["Message"] = "Đăng xuất tài khoản thành công";

            return RedirectToAction("Login", "Login");
        }
    }
}
