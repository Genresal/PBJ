using PBJ.StoreManagementService.Models.Pagination;
using PBJ.StoreManagementService.Models.User;

namespace PBJ.StoreManagementService.Business.Services.Abstract
{
    public interface IUserService
    {
        Task<List<UserDto>> SearchUsersByEmailPartAsync(string emailPart, int take);

        Task<PaginationResponseDto<UserDto>> GetFollowersAsync(string userEmail, int page, int take);

        Task<PaginationResponseDto<UserDto>> GetFollowingsAsync(string followerEmail, int page, int take);

        Task<UserDto> GetAsync(string email);

        Task<UserDto> CreateAsync(UserRequestModel userRequestModel);

        Task<bool> DeleteAsync(int id);
    }
}
