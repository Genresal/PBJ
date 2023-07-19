using PBJ.StoreManagementService.Business.Dtos;

namespace PBJ.StoreManagementService.Business.Services.Abstract
{
    public interface IFollowingService
    {
        Task<List<FollowingDto>> GetAmountAsync(int amount);

        Task<List<FollowingDto>> GetUserFollowingsAsync(int userId, int amount);

        Task<FollowingDto> GetAsync(int id);

        Task<bool> CreateAsync(FollowingDto followingDto);

        Task<bool> UpdateAsync(int id, FollowingDto followingDto);

        Task<bool> DeleteAsync(int id);
    }
}
