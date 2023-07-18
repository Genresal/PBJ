using Microsoft.EntityFrameworkCore;
using PBJ.StoreManagementService.DataAccess.Context;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;

namespace PBJ.StoreManagementService.DataAccess.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        { }

        public async Task<User> GetAsync(string login)
        {
            return await _databaseContext.Users.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Login == login);
        }
    }
}
