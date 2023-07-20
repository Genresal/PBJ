using PBJ.StoreManagementService.Business.Dtos;

namespace PBJ.StoreManagementService.Business.Services.Abstract
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAmountAsync(int amount);

        Task<List<UserDto>> GetFollowersAsync(int userId, int amount);

        Task<List<UserDto>> GetFollowingsAsync(int userId, int amount);

        Task<UserDto> GetAsync(int id);

        Task<UserDto> GetAsync(string login);

        Task<UserDto> CreateAsync(UserDto userDto);

        Task<UserDto> UpdateAsync(int id, UserDto userDto);

        Task<bool> DeleteAsync(int id);
    }
}
