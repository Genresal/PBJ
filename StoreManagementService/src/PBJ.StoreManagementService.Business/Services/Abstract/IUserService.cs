using PBJ.StoreManagementService.Models.User;

namespace PBJ.StoreManagementService.Business.Services.Abstract
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAmountAsync(int amount);

        Task<List<UserDto>> GetFollowersAsync(int userId, int amount);

        Task<List<UserDto>> GetFollowingsAsync(int userId, int amount);

        Task<UserDto> GetAsync(int id);

        Task<UserDto> GetAsync(string login);

        Task<UserDto> CreateAsync(UserRequestModel userRequestModel);

        Task<UserDto> UpdateAsync(int id, UserRequestModel userRequestModel);

        Task<bool> DeleteAsync(int id);
    }
}
