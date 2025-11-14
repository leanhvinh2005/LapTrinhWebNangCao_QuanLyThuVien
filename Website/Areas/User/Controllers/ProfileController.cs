using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Website.Data;
using Website.Models;
using Website.Models.ViewModels;
using Website.Services;

namespace Website.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserService _userService;
        private readonly CardService _cardService;
        private readonly MemberService _memberService;

        public ProfileController(ApplicationDbContext context, UserService userService, CardService cardService, MemberService memberService)
        {
            _context = context;
            _userService = userService;
            _cardService = cardService;
            _memberService = memberService;
        }

        [Route("/Profile")]
        public async Task<IActionResult> Profile()
        {
            var userid = User.FindFirst("UserId").Value;

            var member = await _context.DOCGIA
                .FirstOrDefaultAsync(m => m.idUser == int.Parse(userid));

            var card = await _context.THETHUVIEN
                .FirstOrDefaultAsync(c => c.idCard == member.idCard);

            var user = await _context.ACCOUNT_USER
                .FirstOrDefaultAsync(u => u.idUser == int.Parse(userid));

            ProfileInfo profile = new ProfileInfo
            {
                User = user,
                Card = card
            };

            return View(profile);
        }
    }
}
