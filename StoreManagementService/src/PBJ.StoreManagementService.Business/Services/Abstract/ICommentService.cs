using PBJ.StoreManagementService.Models.Comment;

namespace PBJ.StoreManagementService.Business.Services.Abstract
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetAmountAsync(int amount);

        Task<CommentDto> GetAsync(int id);

        Task<CommentDto> CreateAsync(CreateCommentRequestModel commentRequestModel);

        Task<CommentDto> UpdateAsync(int id, UpdateCommentRequestModel commentRequestModel);

        Task<bool> DeleteAsync(int id);
    }
}
