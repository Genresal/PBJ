﻿using AutoMapper;
using PBJ.StoreManagementService.Business.Constants;
using PBJ.StoreManagementService.Business.Exceptions;
using PBJ.StoreManagementService.Business.Services.Abstract;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;
using PBJ.StoreManagementService.Models.User;

namespace PBJ.StoreManagementService.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository,
            IPostRepository postRepository,
            ICommentRepository commentRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> GetAmountAsync(int amount)
        {
            var users = await _userRepository.GetAmountAsync(amount);

            if (users.Count == 0)
            {
                throw new NotFoundException(ExceptionMessages.USERS_EMPTY_MESSAGE);
            }

            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<List<UserDto>> GetFollowersAsync(int userId, int amount)
        {
            var followers = await _userRepository.GetFollowersAsync(userId, amount);

            if (followers.Count == 0)
            {
                throw new NotFoundException(ExceptionMessages.POST_NOT_FOUND_MESSAGE);
            }

            return _mapper.Map<List<UserDto>>(followers);
        }

        public async Task<List<UserDto>> GetFollowingsAsync(int followerId, int amount)
        {
            var followers = await _userRepository.GetFollowingsAsync(followerId, amount);

            if (followers.Count == 0)
            {
                throw new NotFoundException(ExceptionMessages.USERS_EMPTY_MESSAGE);
            }

            return _mapper.Map<List<UserDto>>(followers);
        }

        public async Task<UserDto> GetAsync(int id)
        {
            var user = await _userRepository.GetAsync(id);

            if (user == null)
            {
                throw new NotFoundException(ExceptionMessages.USER_NOT_FOUND_MESSAGE);
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetAsync(string email)
        {
            var user = await _userRepository.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
            {
                throw new NotFoundException(ExceptionMessages.USER_NOT_FOUND_MESSAGE);
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateAsync(UserRequestModel userRequestModel)
        {
            var existingUser = await _userRepository
                .FirstOrDefaultAsync(x => x.Email == userRequestModel.Email);

            if (existingUser != null)
            {
                throw new AlreadyExistsException(ExceptionMessages.USER_ALREADY_EXISTS_MESSAGE);
            }

            var user = _mapper.Map<User>(userRequestModel);

            await _userRepository.CreateAsync(user);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateAsync(int id, UserRequestModel userRequestModel)
        {
            var existingUser = await _userRepository.GetAsync(id);

            if (existingUser == null)
            {
                throw new NotFoundException(ExceptionMessages.USER_NOT_FOUND_MESSAGE);
            }

            existingUser = _mapper.Map<User>(userRequestModel);

            existingUser.Id = id;

            await _userRepository.UpdateAsync(existingUser);

            return _mapper.Map<UserDto>(existingUser);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingUser = await _userRepository.GetAsync(id);

            if (existingUser == null)
            {
                throw new NotFoundException(ExceptionMessages.USER_NOT_FOUND_MESSAGE);
            }

            await _userRepository.DeleteAsync(existingUser);

            await _postRepository.DeleteRangeAsync(_mapper.Map<List<Post>>(existingUser.Posts));

            foreach (var post in existingUser.Posts)
            {
                await _commentRepository.DeleteRangeAsync(
                    _mapper.Map<List<Comment>>(post.Comments));
            }

            return true;
        }
    }
}
