using PBJ.StoreManagementService.Models.Comment;
using PBJ.StoreManagementService.Models.Pagination;

namespace PBJ.StoreManagementService.Business.Services.Abstract
{
    public interface ICommentService
    {
        Task<PaginationResponseDto<CommentDto>> GetPaginatedAsync(int page, int take);

        Task<PaginationResponseDto<CommentDto>> GetByPostIdAsync(int postId, int page, int take);

        Task<CommentDto> GetAsync(int id);

        Task<CommentDto> CreateAsync(CreateCommentRequestModel commentRequestModel);

        Task<CommentDto> UpdateAsync(int id, UpdateCommentRequestModel commentRequestModel);

        Task<bool> DeleteAsync(int id);
    }
}