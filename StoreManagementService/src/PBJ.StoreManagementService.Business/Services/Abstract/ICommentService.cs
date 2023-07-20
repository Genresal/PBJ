using PBJ.StoreManagementService.Business.Dtos;

namespace PBJ.StoreManagementService.Business.Services.Abstract
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetAmountAsync(int amount);

        Task<CommentDto> GetAsync(int id);

        Task<CommentDto> CreateAsync(CommentDto commentDto);

        Task<CommentDto> UpdateAsync(int id, CommentDto commentDto);

        Task<bool> DeleteAsync(int id);
    }
}
