using PBJ.StoreManagementService.DataAccess.Entities;

namespace PBJ.StoreManagementService.DataAccess.Repositories.Abstract
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<List<Post>> GetUserPostsAsync(int userId, int amount);
    }
}
