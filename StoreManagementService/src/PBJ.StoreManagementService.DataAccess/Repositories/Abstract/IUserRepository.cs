﻿using PBJ.StoreManagementService.DataAccess.Entities;

namespace PBJ.StoreManagementService.DataAccess.Repositories.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        Task<PaginationResponse<User>> GetFollowersAsync(string userEmail, int page, int take);

        Task<PaginationResponse<User>> GetFollowingsAsync(string followerEmail, int page, int take);
    }
}
