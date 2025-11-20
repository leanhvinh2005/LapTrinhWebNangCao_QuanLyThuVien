using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Services
{
    public class BorrowService : Controller
    {
        private readonly ApplicationDbContext _context;

        public BorrowService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddBorrow(Borrow borrow)
        {
            var idParam = new SqlParameter
            {
                ParameterName = "@idborrow",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddBorrow @idborrow OUTPUT, @idcard, @idlibrarian",
                idParam,
                new SqlParameter("@idcard", borrow.idCard),
                new SqlParameter("@idlibrarian", DBNull.Value)
            );

            int id = (int)idParam.Value;

            return id;
        }

        public async Task DeleteBorrow(int id)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeleteBorrow @id",
                new SqlParameter("@id", id)
            );
        }

        public async Task EditBorrow(int idborrow, string status)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC EditBorrow @idborrow, @status",
                new SqlParameter("@idborrow", idborrow),
                new SqlParameter("@status", status)
            );
        }

        public async Task AddBookToBorrow(int idborrow, string idbook)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddBookToBorrow @idborrow, @idbook",
                new SqlParameter("@idborrow", idborrow),
                new SqlParameter("@idbook", idbook)
            );
        }

        public async Task RemoveBookFromBorrow(int idborrow, string idbook)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC RemoveBookFromBorrow @idborrow, @idbook",
                new SqlParameter("@idborrow", idborrow),
                new SqlParameter("@idbook", idbook)
            );
        }

        public async Task EditBookBorrow(int idborrow, string idbook, string status)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC EditBookBorrow @idborrow, @idbook, @status",
                new SqlParameter("@idborrow", idborrow),
                new SqlParameter("@idbook", idbook),
                new SqlParameter("@status", status)
            );

            var borrows = await _context.JOIN_BOOKBORROW
                .Where(j => j.idBorrow == idborrow)
                .ToListAsync();

            if (borrows.All(b => b.statusBookBorrow == "COMPLETE"))
                await EditBorrow(idborrow, "COMPLETE");
        }

        //HoangTienDat
        public async Task EditBorrowFull(Borrow borrow)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC EditBorrow @idborrow, @idcard, @idlibrarian, @status",
                new SqlParameter("@idborrow", borrow.idBorrow),
                new SqlParameter("@idcard", borrow.idCard),
                new SqlParameter("@idlibrarian", borrow.idLibrarian),
                new SqlParameter("@status", borrow.statusBorrow)
            );
        }
        public async Task<List<Borrow>> GetAllBorrowsAsync()
        {

            return await _context.MUONTRA

                .OrderByDescending(b => b.dateBorrow)
                .ToListAsync();
        }
        public async Task<Borrow?> GetBorrowByIdAsync(int id)
        {
            return await _context.MUONTRA
                .FirstOrDefaultAsync(b => b.idBorrow == id);
        }
    }
}
