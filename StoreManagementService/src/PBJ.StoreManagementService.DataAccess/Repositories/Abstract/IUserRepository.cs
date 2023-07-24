using PBJ.StoreManagementService.DataAccess.Entities;

namespace PBJ.StoreManagementService.DataAccess.Repositories.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<User>> GetFollowersAsync(int userId, int amount);

        Task<List<User>> GetFollowingsAsync(int followerId, int amount);
    }
}
