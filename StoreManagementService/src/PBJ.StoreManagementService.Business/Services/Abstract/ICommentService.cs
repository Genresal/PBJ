using PBJ.StoreManagementService.Models.Comment;

namespace PBJ.StoreManagementService.Business.Services.Abstract
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetAmountAsync(int amount);

        Task<CommentDto> GetAsync(int id);

        Task<bool> CreateAsync(CommentRequestModel commentRequestModel);

        Task<bool> UpdateAsync(int id, CommentRequestModel commentRequestModel);

        Task<bool> DeleteAsync(int id);
    }
}
