using PBJ.StoreManagementService.Models.Post;

namespace PBJ.StoreManagementService.Business.Services.Abstract
{
    public interface IPostService
    {
        Task<List<PostDto>> GetAmountAsync(int amount);

        Task<List<PostDto>> GetUserPostsAsync(int userId, int amount);

        Task<PostDto> GetAsync(int id);

        Task<PostDto> CreateAsync(PostRequestModel postRequestModel);

        Task<PostDto> UpdateAsync(int id, PostRequestModel postRequestModel);

        Task<bool> DeleteAsync(int id);
    }
}
