using AutoMapper;
using PBJ.StoreManagementService.Business.Constants;
using PBJ.StoreManagementService.Business.Exceptions;
using PBJ.StoreManagementService.Business.Producers.Abstract;
using PBJ.StoreManagementService.Business.Services.Abstract;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;
using PBJ.StoreManagementService.Models.Comment;
using PBJ.StoreManagementService.Models.Pagination;
using Serilog;

namespace PBJ.StoreManagementService.Business.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly IMessageProducer _messageProducer;
        private readonly IUserRepository _userRepository;

        public CommentService(ICommentRepository commentRepository,
            IMapper mapper,
            IUserRepository userRepository,
            IMessageProducer messageProducer)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _messageProducer = messageProducer;
        }

        public async Task<PaginationResponseDto<CommentDto>> GetPaginatedAsync(int page, int take)
        {
            var paginationResponse = await _commentRepository.GetPaginatedAsync(
                page, take, orderBy: x => x.Id);

            return _mapper.Map<PaginationResponseDto<CommentDto>>(paginationResponse);
        }

        public async Task<PaginationResponseDto<CommentDto>> GetByPostIdAsync(int postId, int page, int take)
        {
            var paginationResponse = await _commentRepository.GetPaginatedAsync(
                page, take, x => x.PostId == postId, x => x.Id);

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

            var user = await _userRepository.FirstOrDefaultAsync(x => x.Email == comment.UserEmail);

            await _messageProducer.PublicCommentMessageAsync(user!.Email!, "Hello from sms");

            Log.Information("Created comment: {@comment}", comment);

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

            Log.Information("Updated comment: {@existingComment}", existingComment);

            return _mapper.Map<CommentDto>(existingComment);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingComment = await _commentRepository.GetAsync(id);

            await _commentRepository.DeleteAsync(existingComment);

            Log.Information("Deleted comment: {@existingComment}", existingComment);

            return true;
        }
    }
}
