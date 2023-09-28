using AutoMapper;
using PBJ.StoreManagementService.Business.Constants;
using PBJ.StoreManagementService.Business.Exceptions;
using PBJ.StoreManagementService.Business.Services.Abstract;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;
using PBJ.StoreManagementService.Models.Pagination;
using PBJ.StoreManagementService.Models.Post;
using Serilog;

namespace PBJ.StoreManagementService.Business.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository,
            IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<PaginationResponseDto<PostDto>> GetPaginatedAsync(int page, int take)
        {
            var paginationResponse = await _postRepository.GetPaginatedAsync(
                page, take, orderBy: x => x.Id);

            return _mapper.Map<PaginationResponseDto<PostDto>>(paginationResponse);
        }

        public async Task<PaginationResponseDto<PostDto>> GetByUserEmailAsync(string userEmail, int page, int take)
        {
            var paginationResponse = await _postRepository.GetPaginatedAsync(
                page, take, where: x => x.UserEmail == userEmail, orderBy: x => x.Id);

            return _mapper.Map<PaginationResponseDto<PostDto>>(paginationResponse);
        }

        public async Task<PostDto> GetAsync(int id)
        {
            var post = await _postRepository.GetAsync(id);

            if (post == null)
            {
                throw new NotFoundException(ExceptionMessages.POST_NOT_FOUND_MESSAGE);
            }

            return _mapper.Map<PostDto>(post);
        }

        public async Task<PostDto> CreateAsync(CreatePostRequestModel postRequestModel)
        {
            var post = _mapper.Map<Post>(postRequestModel);

            await _postRepository.CreateAsync(post);

            Log.Information("Created post: {@post}", post);

            return _mapper.Map<PostDto>(post);
        }

        public async Task<PostDto> UpdateAsync(int id, UpdatePostRequestModel postRequestModel)
        {
            var existingPost = await _postRepository.GetAsync(id);

            if (existingPost == null)
            {
                throw new NotFoundException(ExceptionMessages.POST_NOT_FOUND_MESSAGE);
            }

            existingPost.Content = postRequestModel.Content;

            await _postRepository.UpdateAsync(existingPost);

            Log.Information("Updated post: {@existingPost}", existingPost);

            return _mapper.Map<PostDto>(existingPost);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingPost = await _postRepository.GetAsync(id);

            await _postRepository.DeleteAsync(existingPost);

            Log.Information("Deleted post: {@existingPost}", existingPost);

            return true;
        }
    }
}
