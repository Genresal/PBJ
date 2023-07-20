using PBJ.StoreManagementService.Business.Dtos;

namespace PBJ.StoreManagementService.Business.Services.Abstract
{
    public interface IPostService
    {
        Task<List<PostDto>> GetAmountAsync(int amount);

        Task<List<PostDto>> GetUserPostsAsync(int userId, int amount);

        Task<PostDto> GetAsync(int id);

        Task<PostDto> CreateAsync(PostDto postDto);

        Task<PostDto> UpdateAsync(int id, PostDto postDto);

        Task<bool> DeleteAsync(int id);
    }
}
