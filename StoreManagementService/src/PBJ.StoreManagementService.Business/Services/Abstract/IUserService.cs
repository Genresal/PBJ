using PBJ.StoreManagementService.Models.Pagination;
using PBJ.StoreManagementService.Models.User;

namespace PBJ.StoreManagementService.Business.Services.Abstract
{
    public interface IUserService
    {
        Task<PaginationResponseDto<UserDto>> GetPaginatedAsync(int page, int take);

        Task<PaginationResponseDto<UserDto>> GetFollowersAsync(int userId, int page, int take);

        Task<PaginationResponseDto<UserDto>> GetFollowingsAsync(int userId, int page, int take);

        Task<UserDto> GetAsync(int id);

        Task<UserDto> GetAsync(string login);

        Task<UserDto> CreateAsync(UserRequestModel userRequestModel);

        Task<UserDto> UpdateAsync(int id, UserRequestModel userRequestModel);

        Task<bool> DeleteAsync(int id);
    }
}
