using PBJ.StoreManagementService.Business.Dtos;

namespace PBJ.StoreManagementService.Business.Services.Abstract
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetAmountAsync(int amount);

        Task<List<CommentDto>> GetPostCommentsAsync(int postId, int amount);

        Task<CommentDto> GetAsync(int id);

        Task<bool> CreateAsync(CommentDto commentDto);

        Task<bool> UpdateAsync(int id, CommentDto commentDto);

        Task<bool> DeleteAsync(int id);
    }
}
