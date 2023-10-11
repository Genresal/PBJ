using PBJ.StoreManagementService.Models.Pagination;
using PBJ.StoreManagementService.Models.UserFollowers;

namespace PBJ.StoreManagementService.Business.Services.Abstract
{
    public interface IUserFollowersService
    {
        Task<PaginationResponseDto<UserFollowersDto>> GetPaginatedAsync(int page, int take);

        Task<UserFollowersDto> GetAsync(int id);

        Task<UserFollowersDto> CreateAsync(UserFollowersRequestModel userFollowersRequestModel);

        Task<bool> DeleteAsync(UserFollowersRequestModel requestModel);
    }
}
