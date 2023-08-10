using AutoMapper;
using PBJ.StoreManagementService.Business.Constants;
using PBJ.StoreManagementService.Business.Exceptions;
using PBJ.StoreManagementService.Business.Services.Abstract;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;
using PBJ.StoreManagementService.Models.Comment;
using PBJ.StoreManagementService.Models.Pagination;

namespace PBJ.StoreManagementService.Business.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository,
            IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<PaginationResponseDto<CommentDto>> GetPaginatedAsync(int page, int take)
        {
            var paginationResponse = await _commentRepository.GetPaginatedAsync(
                page, take, orderBy: x => x.Id);

            return _mapper.Map<PaginationResponseDto<CommentDto>>(paginationResponse);
        }

        public async Task<PaginationResponseDto<CommentDto>> GetByPostIdAsync(int postId, int page, int take)
        {
            var paginationResponse = await _commentRepository.GetPaginatedAsync<int>(
                page, take, where: x => x.PostId == postId, orderBy: x => x.Id);

            return _mapper.Map<PaginationResponseDto<CommentDto>>(paginationResponse);
        }

        public async Task<CommentDto> GetAsync(int id)
        {
            var comment = await _commentRepository.GetAsync(id);

            if (comment == null)
            {
                throw new NotFoundException(ExceptionMessages.COMMENT_NOT_FOUND_MESSAGE);
            }

            return _mapper.Map<CommentDto>(comment);
        }

        public async Task<CommentDto> CreateAsync(CreateCommentRequestModel commentRequestModel)
        {
            var comment = _mapper.Map<Comment>(commentRequestModel);

            await _commentRepository.CreateAsync(comment);

            return _mapper.Map<CommentDto>(comment);
        }

        public async Task<CommentDto> UpdateAsync(int id, UpdateCommentRequestModel commentRequestModel)
        {
            var existingComment = await _commentRepository.GetAsync(id);

            if (existingComment == null)
            {
                throw new NotFoundException(ExceptionMessages.COMMENT_NOT_FOUND_MESSAGE);
            }

            existingComment.Content = commentRequestModel.Content;

            await _commentRepository.UpdateAsync(existingComment);

            return _mapper.Map<CommentDto>(existingComment);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingComment = await _commentRepository.GetAsync(id);

            await _commentRepository.DeleteAsync(existingComment);

            return true;
        }
    }
}
