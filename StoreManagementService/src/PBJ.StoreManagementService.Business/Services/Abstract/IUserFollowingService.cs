using PBJ.StoreManagementService.Business.Dtos;

namespace PBJ.StoreManagementService.Business.Services.Abstract
{
    public interface IUserFollowingService
    {
        Task<List<UserFollowingDto>> GetAmountAsync(int amount);

        Task<UserFollowingDto> GetAsync(int id);

        Task<bool> CreateAsync(UserFollowingDto userFollowingDto);

        Task<bool> UpdateAsync(int id, UserFollowingDto userFollowingDto);

        Task<bool> DeleteAsync(int id);
    }
}
