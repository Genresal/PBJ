﻿using PBJ.StoreManagementService.Business.Dtos;

namespace PBJ.StoreManagementService.Business.Services.Abstract
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAmountAsync(int amount);

        Task<UserDto> GetAsync(int id);

        Task<UserDto> GetAsync(string login);

        Task<bool> CreateAsync(UserDto userDto);

        Task<bool> UpdateAsync(int id, UserDto userDto);

        Task<bool> DeleteAsync(int id);
    }
}