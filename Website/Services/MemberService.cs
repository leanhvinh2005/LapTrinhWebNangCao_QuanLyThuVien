using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Services
{
    public class MemberService : Controller
    {
        private readonly ApplicationDbContext _context;

        public MemberService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddMember(Member member)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddMember @idcard, @iduser",
                new SqlParameter("@idcard", member.idCard),
                new SqlParameter("@iduser", member.idUser)
            );
        }

        public async Task DeleteMember(int id)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeleteMember @id",
                new SqlParameter("@id", id)
            );
        }

        public async Task EditMember(Member member)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC EditMember @id, @status, @idcard, @iduser",
                new SqlParameter("@id", member.idMember),
                new SqlParameter("@status", member.statusMember),
                new SqlParameter("@idcard", member.idCard),
                new SqlParameter("@iduser", member.idUser)
            );
        }

        public async Task<List<Member>> SearchMember(string search)
        {
            return await _context.DOCGIA
                .FromSqlRaw("EXEC SearchMember @search", new SqlParameter("@search", search))
                .ToListAsync();
        }
    }
}
