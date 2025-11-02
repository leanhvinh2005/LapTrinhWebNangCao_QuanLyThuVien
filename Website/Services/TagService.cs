using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Services
{
    // 1. ĐÃ XÓA ": Controller"
    public class TagService
    {
        private readonly ApplicationDbContext _context;

        public TagService(ApplicationDbContext context)
        {
            _context = context;
        }

        // 2. THÊM HÀM LẤY TẤT CẢ TAG (dùng LINQ)
        // (Giả sử DbSet của bạn là TAG)
        public async Task<List<Tag>> GetTagsAsync()
        {
            return await _context.TAG
                .OrderBy(t => t.nameTag)
                .ToListAsync();
        }

        // 3. THÊM HÀM LẤY 1 TAG (dùng LINQ)
        // (FindAsync là cách nhanh nhất để tìm bằng [Key])
        public async Task<Tag?> GetTagByIdAsync(int id)
        {
            return await _context.TAG.FindAsync(id);
        }

        // SP của bạn không cần idTag, giả sử idTag là tự động tăng (IDENTITY)
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