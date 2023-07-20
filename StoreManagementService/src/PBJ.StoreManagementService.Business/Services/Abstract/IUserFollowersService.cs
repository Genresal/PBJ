﻿using PBJ.StoreManagementService.Models.UserFollowers;

namespace PBJ.StoreManagementService.Business.Services.Abstract
{
    public interface IUserFollowersService
    {
        Task<List<UserFollowersDto>> GetAmountAsync(int amount);

        Task<UserFollowersDto> GetAsync(int id);

        Task<UserFollowersDto> CreateAsync(UserFollowersRequestModel userFollowersRequestModel);

        Task<bool> DeleteAsync(int id);
    }
}
