using PBJ.StoreManagementService.DataAccess.Entities;

namespace PBJ.StoreManagementService.DataAccess.Repositories.Abstract
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<List<Comment>> GetPostCommentsAsync(int postId, int amount);
    }
}
