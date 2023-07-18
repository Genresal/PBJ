using Microsoft.EntityFrameworkCore;
using PBJ.StoreManagementService.DataAccess.Context;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;

namespace PBJ.StoreManagementService.DataAccess.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        { }

        public async Task<List<Post>> GetUserPostsAsync(int userId, int amount)
        {
            return await _databaseContext.Posts.AsNoTracking()
                .Where(x => x.UserId == userId).Take(amount).ToListAsync();
        }
    }
}
