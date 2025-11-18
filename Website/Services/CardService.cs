using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Services
{
    public class CardService : Controller
    {
        private readonly ApplicationDbContext _context;

        public CardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddCard(Card card)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddCard @id, @name, @email, @address, @phone, @date",
                new SqlParameter("@id", card.idCard),
                new SqlParameter("@name", card.nameCard),
                new SqlParameter("@email", card.emailCard),
                new SqlParameter("@address", card.addressCard),
                new SqlParameter("@phone", card.phoneCard),
                new SqlParameter("@date", card.dateCard)
            );
        }

        public async Task DeleteCard(string id)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeleteCard @id",
                new SqlParameter("@id", id)
            );
        }

        public async Task EditCard(Card card)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC EditCard @id, @name, @email, @address, @phone, @date, @status",
                new SqlParameter("@id", card.idCard),
                new SqlParameter("@name", card.nameCard),
                new SqlParameter("@email", card.emailCard),
                new SqlParameter("@address", card.addressCard),
                new SqlParameter("@phone", card.phoneCard),
                new SqlParameter("@date", card.dateCard),
                new SqlParameter("@status", card.statusCard)
            );
        }

        public async Task<List<Card>> SearchCard(string search)
        {
            return await _context.THETHUVIEN
                .FromSqlRaw("EXEC SearchCard @search", new SqlParameter("@search", search))
                .ToListAsync();
        }

        public string GenerateCardID()
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();

            char firstChar = letters[random.Next(letters.Length)];

            var rest = new char[11];
            for (int i = 0; i < 11; i++)
            {
                rest[i] = chars[random.Next(chars.Length)];
            }

            return firstChar + new string(rest);
        }
        // code mới 
        public async Task<Card?> GetCardByIdAsync(string id)
        {
            return await _context.THETHUVIEN
                .FirstOrDefaultAsync(c => c.idCard == id);
        }
    }
}
