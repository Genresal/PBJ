using PBJ.StoreManagementService.Models.Pagination;
using PBJ.StoreManagementService.Models.Post;

namespace PBJ.StoreManagementService.Business.Services.Abstract
{
    public interface IPostService
    {
        Task<PaginationResponseDto<PostDto>> GetPaginatedAsync(int page, int take);

        Task<PaginationResponseDto<PostDto>> GetByUserEmailAsync(string userEmail, int page, int take);

        Task<PostDto> GetAsync(int id);

        Task<PostDto> CreateAsync(CreatePostRequestModel postRequestModel);

        Task<PostDto> UpdateAsync(int id, UpdatePostRequestModel postRequestModel);

        Task<bool> DeleteAsync(int id);
    }
}
