using Microsoft.EntityFrameworkCore;
using PBJ.StoreManagementService.DataAccess.Context;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;

namespace PBJ.StoreManagementService.DataAccess.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        { }

        public async Task<List<Comment>> GetPostCommentsAsync(int postId, int amount)
        {
            return await _databaseContext.Comments.AsNoTracking()
                .Where(x => x.PostId == postId).Take(amount).ToListAsync();
        }
    }
}
