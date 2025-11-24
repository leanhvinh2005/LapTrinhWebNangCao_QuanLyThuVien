using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Services
{
    public class ChapterService : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChapterService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddChapter(Chapter chapter)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddChapter @id, @number, @title, @content",
                new SqlParameter("@id", chapter.idChapter),
                new SqlParameter("@number", chapter.numberChapter),
                new SqlParameter("@title", chapter.titleChapter),
                new SqlParameter("@content", chapter.contentChapter)
            );
        }

        public async Task DeleteChapter(string id)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeleteChapter @id",
                new SqlParameter("@id", id)
            );
        }

        public async Task EditChapter(Chapter chapter)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC EditChapter @id, @number, @title, @content",
                new SqlParameter("@id", chapter.idChapter),
                new SqlParameter("@number", chapter.numberChapter),
                new SqlParameter("@title", chapter.titleChapter),
                new SqlParameter("@content", chapter.contentChapter)
            );
        }

        public async Task<List<Chapter>> SearchChapter(string search)
        {
            return await _context.TRANG
                .FromSqlRaw("EXEC SearchChapter @search", new SqlParameter("@search", search))
                .ToListAsync();
        }
        // Hoàng Tiến Đạt
        public async Task<Chapter?> GetChapterById(string id)
        {
            return await _context.TRANG.FindAsync(id);
        }
    }
}
