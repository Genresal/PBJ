using AutoMapper;
using PBJ.StoreManagementService.Business.Constants;
using PBJ.StoreManagementService.Business.Dtos;
using PBJ.StoreManagementService.Business.Exceptions;
using PBJ.StoreManagementService.Business.Services.Abstract;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;

namespace PBJ.StoreManagementService.Business.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository,
            ICommentRepository commentRepository,
            IMapper mapper)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<List<PostDto>> GetAmountAsync(int amount)
        {
            var posts = await _postRepository.GetAmountAsync(amount);

            if (posts.Count == 0)
            {
                throw new NotFoundException(ExceptionMessages.POSTS_EMPTY_MESSAGE);
            }

            return _mapper.Map<List<PostDto>>(posts);
        }

        public async Task<List<PostDto>> GetUserPostsAsync(int userId, int amount)
        {
            var posts = await _postRepository.GetUserPostsAsync(userId, amount);

            if (posts.Count == 0)
            {
                throw new NotFoundException(ExceptionMessages.POST_NOT_FOUND_MESSAGE);
            }

            return _mapper.Map<List<PostDto>>(posts);
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

        public async Task<PostDto> CreateAsync(PostDto postDto)
        {
            await _postRepository.CreateAsync(_mapper.Map<Post>(postDto));

            return postDto;
        }

        public async Task<PostDto> UpdateAsync(int id, PostDto postDto)
        {
            var existingPost = await _postRepository.GetAsync(id);

            if (existingPost == null)
            {
                throw new NotFoundException(ExceptionMessages.POST_NOT_FOUND_MESSAGE);
            }

            existingPost = _mapper.Map<Post>(postDto);

            existingPost.Id = id;

            await _postRepository.UpdateAsync(existingPost);

            return _mapper.Map<PostDto>(existingPost);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingPost = await _postRepository.GetAsync(id);

            if (existingPost == null)
            {
                throw new NotFoundException(ExceptionMessages.POST_NOT_FOUND_MESSAGE);
            }

            await _postRepository.DeleteAsync(existingPost);

            await _commentRepository.DeleteRangeAsync(_mapper.Map<List<Comment>>(existingPost.Comments));

            return true;
        }
    }
}
