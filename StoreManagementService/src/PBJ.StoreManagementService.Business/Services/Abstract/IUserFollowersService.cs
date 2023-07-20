using PBJ.StoreManagementService.Business.Dtos;

namespace PBJ.StoreManagementService.Business.Services.Abstract
{
    public interface IUserFollowersService
    {
        Task<List<UserFollowersDto>> GetAmountAsync(int amount);

        Task<UserFollowersDto> GetAsync(int id);

        Task<UserFollowersDto> CreateAsync(UserFollowersDto userFollowersDto);

        Task<bool> DeleteAsync(int id);
    }
}
