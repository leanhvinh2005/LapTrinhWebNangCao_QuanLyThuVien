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
        private readonly CardService _cardController;
        private readonly EmailService _emailService;

        public RegisterCardController(ApplicationDbContext context, CardService cardController, EmailService emailService)
        {
            _context = context;
            _cardController = cardController;
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
            card.idCard = _cardController.GenerateCardID();
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

            await _cardController.AddCard(card);
            await _emailService.SendEmail(
                card.emailCard,
                "Card ID",
                $"Your newly created card comes with the following ID: {card.idCard}");

            return RedirectToAction("Home", "Home");    
        }
    }
}
