using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Services
{
    public class LibrarianService : Controller
    {
        private readonly ApplicationDbContext _context;

        public LibrarianService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddLibrarian(Librarian librarian)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddLibrarian @role, @date, @iduser",
                new SqlParameter("@role", librarian.roleLibrarian),
                new SqlParameter("@date", librarian.hireLibrarian),
                new SqlParameter("@iduser", librarian.idUser)
            );
        }

        public async Task DeleteLibrarian(int id)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeleteLibrarian @id",
                new SqlParameter("@id", id)
            );
        }

        public async Task EditLibrarian(Librarian librarian)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC EditLibrarian @idlibrarian, @role, @date, @status, @iduser",
                new SqlParameter("@idlibrarian", librarian.idLibrarian),
                new SqlParameter("@role", librarian.roleLibrarian),
                new SqlParameter("@date", librarian.hireLibrarian),
                new SqlParameter("@status", librarian.statusLibrarian),
                new SqlParameter("@iduser", librarian.idUser)
            );
        }
        public async Task<List<Librarian>> SearchLibrarian(string search)
        {
            return await _context.THUTHU
                .FromSqlRaw("EXEC SearchLibrarian @search", new SqlParameter("@search", search))
                .ToListAsync();
        }
    }
}
