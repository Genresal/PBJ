using PBJ.StoreManagementService.Business.Dtos;

namespace PBJ.StoreManagementService.Business.Services.Abstract
{
    public interface IPostService
    {
        Task<List<PostDto>> GetAmountAsync(int amount);

        Task<PostDto> GetAsync(int id);

        Task<bool> CreateAsync(PostDto postDto);

        Task<bool> UpdateAsync(int id, PostDto postDto);

        Task<bool> DeleteAsync(int id);
    }
}
