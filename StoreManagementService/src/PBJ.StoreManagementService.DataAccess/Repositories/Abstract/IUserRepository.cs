using PBJ.StoreManagementService.DataAccess.Entities;

namespace PBJ.StoreManagementService.DataAccess.Repositories.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetAsync(string login);
    }
}
