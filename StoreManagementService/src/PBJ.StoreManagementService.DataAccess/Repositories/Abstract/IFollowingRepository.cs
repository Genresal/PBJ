using PBJ.StoreManagementService.DataAccess.Entities;

namespace PBJ.StoreManagementService.DataAccess.Repositories.Abstract
{
    public interface IFollowingRepository : IRepository<Following>
    {
        Task<List<Following>> GetUserFollowingsAsync(int userId, int amount);
    }
}
