using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Services
{
    public class CollectionService : Controller
    {
        private readonly ApplicationDbContext _context;

        public CollectionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddCollection(Collection collection)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddCollection @id, @name, @description, @image",
                new SqlParameter("@id", collection.idCollection),
                new SqlParameter("@name", collection.nameCollection),
                new SqlParameter("@description", collection.descriptionCollection),
                new SqlParameter("@image", collection.imageCollection)
            );
        }

        public async Task DeleteCollection(string id)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeleteCollection @id",
                new SqlParameter("@id", id)
            );
        }

        public async Task EditCollection(Collection collection)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC EditCollection @id, @name, @description, @image",
                new SqlParameter("@id", collection.idCollection),
                new SqlParameter("@name", collection.nameCollection),
                new SqlParameter("@description", collection.descriptionCollection),
                new SqlParameter("@image", collection.imageCollection)
            );
        }

        public async Task AddToCollection(string idcollection, string idbook)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddToCollection @idcollection, @idbook",
                new SqlParameter("@idcollection", idcollection),
                new SqlParameter("@idbook", idbook)
            );
        }
        public async Task RemoveFromCollection(string idcollection, string idbook)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC RemoveFromCollection @idcollection, @idbook",
                new SqlParameter("@idcollection", idcollection),
                new SqlParameter("@idbook", idbook)
            );
        }

        public async Task<List<Collection>> SearchCollection(string search)
        {
            return await _context.SUUTAP
                .FromSqlRaw("EXEC SearchCollection @search", new SqlParameter("@search", search))
                .ToListAsync();
        }
        //  code mới
        public async Task<Collection?> GetCollectionByIdAsync(string id)
        {
            var param = new SqlParameter("@id", id);
        var collection = await _context.SUUTAP
            .FromSqlRaw("EXEC GetCollectionById @id", param)
            .ToListAsync();
            return collection.FirstOrDefault();
        }
    }
}