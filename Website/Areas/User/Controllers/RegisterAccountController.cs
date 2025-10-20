using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Website.Data;
using Website.Models;
using Website.Services;

namespace Website.Areas.User.Controllers
{
    [Area("User")]
    public class RegisterAccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserService _userService;
        private readonly MemberService _memberService;

        public RegisterAccountController(ApplicationDbContext context, UserService userService, MemberService memberService)
        {
            _context = context;
            _userService = userService;
            _memberService = memberService;
        }

        [Route("/RegisterAccount")]
        public IActionResult RegisterAccount()
        {
            return View();
        }

        [HttpPost("/RegisterAccount")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAccount(Website.Models.User user, string cardid)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Model invalid:");
                foreach (var err in ModelState.Values.SelectMany(v => v.Errors))
                    Console.WriteLine(err.ErrorMessage);
                return View(user);
            }

            var members = await _memberService.SearchMember(cardid);
            var users = await _userService.SearchUser(user.emailUser);

            if (users.Count > 0)
            {
                ModelState.AddModelError(string.Empty, "This email already has an account");
                return View(user);
            }

            if (members.Count > 0)
            {
                ModelState.AddModelError(string.Empty, "This card is already registered to an account");
                return View(user);
            }

            int iduser = await _userService.AddUser(user);
            Member member = new Member
            {
                idMember = 0,
                statusMember = "PLACEHOLDER",
                idCard = cardid,
                idUser = iduser
            };
            await _memberService.AddMember(member);

            return RedirectToAction("Home", "Home");
        }
    }
}
