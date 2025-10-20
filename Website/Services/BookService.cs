using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Website.Areas.User.Controllers;
using Website.Data;
using Website.Models;

namespace Website.Services
{
    public class BookService : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddBook(Book book)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddBook @id, @name, @description, @type, @author, @publisher, @date, @format, @note, @image",
                new SqlParameter("@id", book.idBook),
                new SqlParameter("@name", book.nameBook),
                new SqlParameter("@description", book.descriptionBook),
                new SqlParameter("@type", book.typeBook),
                new SqlParameter("@author", book.authorBook),
                new SqlParameter("@publisher", book.publisherBook),
                new SqlParameter("@date", book.dateBook),
                new SqlParameter("@format", book.formatBook),
                new SqlParameter("@note", book.noteBook),
                new SqlParameter("@image", book.imageBook)
            );
        }

        public async Task DeleteBook(string id)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeleteBook @id",
                new SqlParameter("@id", id)
            );
        }

        public async Task EditBook(Book book)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC EditBook @id, @name, @description, @type, @author, @publisher, @date, @format, @note, @status, @image",
                new SqlParameter("@id", book.idBook),
                new SqlParameter("@name", book.nameBook),
                new SqlParameter("@description", book.descriptionBook),
                new SqlParameter("@type", book.typeBook),
                new SqlParameter("@author", book.authorBook),
                new SqlParameter("@publisher", book.publisherBook),
                new SqlParameter("@date", book.dateBook),
                new SqlParameter("@format", book.formatBook),
                new SqlParameter("@note", book.noteBook),
                new SqlParameter("@image", book.statusBook),
                new SqlParameter("@image", book.imageBook)
            );
        }

        public async Task<List<Book>> SearchBook(string search)
        {
            return await _context.SACH
                .FromSqlRaw("EXEC SearchBook @search", new SqlParameter("@search", search))
                .ToListAsync();
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _context.SACH
                .FromSqlRaw("SELECT * FROM SACH ORDER BY idBook")
                .ToListAsync();
        }
    }
}
