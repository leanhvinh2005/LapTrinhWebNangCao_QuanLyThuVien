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

        public async Task<int> AddBorrow(Borrow borrow, List<Book> books)
        {
            var idParam = new SqlParameter
            {
                ParameterName = "@idborrow",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddBorrow @idborrow OUTPUT, @idcard, @idlibrarian",
                new SqlParameter("@idborrow", borrow.idBorrow),
                new SqlParameter("@idcard", borrow.idCard),
                new SqlParameter("@idlibrarian", borrow.idLibrarian)
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

        public async Task EditBorrow(Borrow borrow)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC EditBorrow @idborrow, @idcard, @idlibrarian, @status",
                new SqlParameter("@idborrow", borrow.idBorrow),
                new SqlParameter("@idcard", borrow.idCard),
                new SqlParameter("@idlibrarian", borrow.idLibrarian),
                new SqlParameter("@status", borrow.statusBorrow)
            );
        }

        public async Task AddBookToBorrow(BookBorrow bookBorrow)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddBookToBorrow @idborrow, @idbook",
                new SqlParameter("@idborrow", bookBorrow.idBorrow),
                new SqlParameter("@idbook", bookBorrow.idBook)
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
    }
}
