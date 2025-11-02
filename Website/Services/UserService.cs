using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Services
{
    // 1. ĐÃ XÓA ": Controller"
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        // 2. THÊM HÀM NÀY (Bắt buộc cho Edit/Delete)
        public async Task<User?> GetUserById(int id)
        {
            // (Giả sử bạn có SP tên là GetUserById)
            var user = await _context.ACCOUNT_USER
                .FromSqlRaw("EXEC GetUserById @id", new SqlParameter("@id", id))
                .ToListAsync();
            return user.FirstOrDefault(); // Lấy người dùng đầu tiên hoặc null
        }

        public async Task<int> AddUser(User user)
        {
            var idParam = new SqlParameter
            {
                ParameterName = "@id",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddUser @id OUTPUT, @name, @email, @password",
                idParam,
                new SqlParameter("@name", user.nameUser),
                new SqlParameter("@email", user.emailUser),
                new SqlParameter("@password", user.passwordUser)
            );

            int id = (int)idParam.Value;

            return id;
        }

        public async Task DeleteUser(int id)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeleteUser @id",
                new SqlParameter("@id", id)
            );
        }

        public async Task EditUser(User user)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC EditUser @id, @name, @email, @password",
                new SqlParameter("@id", user.idUser),
                new SqlParameter("@name", user.nameUser),
                new SqlParameter("@email", user.emailUser),
                new SqlParameter("@password", user.passwordUser)
            );
        }

        public async Task<List<User>> SearchUser(string search)
        {
            return await _context.ACCOUNT_USER
                .FromSqlRaw("EXEC SearchUser @search", new SqlParameter("@search", search))
                .ToListAsync();
        }
    }
}