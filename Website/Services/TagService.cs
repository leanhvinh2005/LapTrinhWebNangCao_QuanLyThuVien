using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Services
{
    public class TagService : Controller
    {
        private readonly ApplicationDbContext _context;

        public TagService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddTag(Tag tag)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddTag @name, @type",
                new SqlParameter("@name", tag.nameTag),
                new SqlParameter("@type", tag.typeTag)
            );
        }

        public async Task DeleteTag(int id)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeleteTag @id",
                new SqlParameter("@id", id)
            );
        }

        public async Task EditTag(Tag tag)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC EditTag @id, @name, @type",
                new SqlParameter("@id", tag.idTag),
                new SqlParameter("@name", tag.nameTag),
                new SqlParameter("@type", tag.typeTag)
            );
        }
    }
}
