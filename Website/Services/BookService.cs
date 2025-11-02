using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Website.Data;
using Website.Models;

namespace Website.Services
{
    // Đã xóa ": Controller"
    public class BookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hàm tôi đã thêm (cho Edit/Delete)
        public async Task<Book?> GetBookById(string id)
        {
            var book = await _context.SACH
                .FromSqlRaw("EXEC GetBookById @id", new SqlParameter("@id", id))
                .ToListAsync();
            return book.FirstOrDefault();
        }

        public async Task AddBook(Book book)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddBook @id, @name, @description, @author, @publisher, @date, @format, @note, @image",
                new SqlParameter("@id", book.idBook),
                new SqlParameter("@name", book.nameBook),
                new SqlParameter("@description", book.descriptionBook),
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

        // Hàm tôi đã sửa lỗi copy-paste
        public async Task EditBook(Book book)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC EditBook @id, @name, @description, @author, @publisher, @date, @format, @note, @status, @image",
                new SqlParameter("@id", book.idBook),
                new SqlParameter("@name", book.nameBook),
                new SqlParameter("@description", book.descriptionBook),
                new SqlParameter("@author", book.authorBook),
                new SqlParameter("@publisher", book.publisherBook),
                new SqlParameter("@date", book.dateBook),
                new SqlParameter("@format", book.formatBook),
                new SqlParameter("@note", book.noteBook),
                new SqlParameter("@status", book.statusBook), // <-- Đã sửa
                new SqlParameter("@image", book.imageBook)
            );
        }

        public async Task<List<Book>> SearchBook(string search)
        {
            return await _context.SACH
                .FromSqlRaw("EXEC SearchBook @search", new SqlParameter("@search", search))
                .ToListAsync();
        }

        // *** HÀM BỊ THIẾU CỦA BẠN (ĐÃ THÊM LẠI) ***
        public async Task<List<Book>> FilterBook(List<int> tagids)
        {
            var table = new DataTable();
            table.Columns.Add("Id", typeof(int));
            foreach (var id in tagids)
                table.Rows.Add(id);

            var param = new SqlParameter("@TagIds", table)
            {
                TypeName = "IntList", // Đảm bảo bạn đã tạo TYPE IntList trong SQL
                SqlDbType = SqlDbType.Structured
            };

            return await _context.SACH
                .FromSqlRaw("EXEC FilterBook @TagIds", param)
                .ToListAsync();
        }

        // *** HÀM BỊ THIẾU CỦA BẠN (ĐÃ THÊM LẠI) ***
        public async Task<List<Book>> GetAllBooks()
        {
            return await _context.SACH
                .FromSqlRaw("SELECT * FROM SACH ORDER BY idBook")
                .ToListAsync();
        }
    }
}