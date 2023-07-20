﻿using PBJ.StoreManagementService.DataAccess.Entities;

namespace PBJ.StoreManagementService.DataAccess.Repositories.Abstract
{
    public interface IUserFollowersRepository : IRepository<UserFollowers>
    {
        Task DeleteRangeAsync(IReadOnlyCollection<UserFollowers> collection);
    }
}
