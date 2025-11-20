using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Website.Data;
using Website.Models;
using Website.Services;
using Website.Services.Other;

namespace Website.Areas.User.Controllers
{
    [Area("User")]
    public class RegisterCardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CardService _cardService;
        private readonly EmailService _emailService;

        public RegisterCardController(ApplicationDbContext context, CardService cardService, EmailService emailService)
        {
            _context = context;
            _cardService = cardService;
            _emailService = emailService;
        }

        [Route("/RegisterCard")]
        public IActionResult RegisterCard()
        {
            return View();
        }

        [HttpPost("/RegisterCard")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterCard(Card card)
        {
            card.idCard = _cardService.GenerateCardID();
            card.statusCard = "CREATED";

            ModelState.Remove(nameof(card.idCard));
            ModelState.Remove(nameof(card.statusCard));

            if (!ModelState.IsValid)
            {
                Console.WriteLine("Model invalid:");
                foreach (var err in ModelState.Values.SelectMany(v => v.Errors))
                    Console.WriteLine(err.ErrorMessage);
                return View(card);
            }

            await _cardService.AddCard(card);
            await _emailService.SendEmail(
                card.emailCard,
                "Card ID",
                $"Your newly created card comes with the following ID: {card.idCard}");

            TempData["Message"] = "Đăng ký thẻ thư viện thành công. Xin kiểm tra email để nhận ID";

            return RedirectToAction("Home", "Home");    
        }
    }
}
