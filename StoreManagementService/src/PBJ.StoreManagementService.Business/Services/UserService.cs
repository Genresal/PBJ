using AutoMapper;
using PBJ.StoreManagementService.Business.Constants;
using PBJ.StoreManagementService.Business.Exceptions;
using PBJ.StoreManagementService.Business.Services.Abstract;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;
using PBJ.StoreManagementService.Models.Pagination;
using PBJ.StoreManagementService.Models.User;
using Serilog;

namespace PBJ.StoreManagementService.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserFollowersRepository _userFollowersRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository,
            IUserFollowersRepository userFollowersRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _userFollowersRepository = userFollowersRepository;
            _mapper = mapper;
        }

        public async Task<PaginationResponseDto<UserDto>> GetPaginatedAsync(int page, int take)
        {
            var paginationResponse = await _userRepository
                .GetPaginatedAsync(page, take, orderBy: x => x.Id);

            return _mapper.Map<PaginationResponseDto<UserDto>>(paginationResponse);
        }

        public async Task<PaginationResponseDto<UserDto>> GetFollowersAsync(int userId, int page, int take)
        {
            var paginationResponse = await _userRepository.GetPaginatedAsync(page, take, 
                where: x => x.Followings!.Any(uf => uf.UserId == userId),
                orderBy: x => x.Id);

            return _mapper.Map<PaginationResponseDto<UserDto>>(paginationResponse);
        }

        public async Task<PaginationResponseDto<UserDto>> GetFollowingsAsync(int followerId, int page, int take)
        {
            var paginationResponse = await _userRepository.GetPaginatedAsync(page, take,
                where: x => x.Followers!.Any(uf => uf.FollowerId == followerId),
                orderBy: x => x.Id);

            return _mapper.Map<PaginationResponseDto<UserDto>>(paginationResponse);
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

            Log.Information("Created user: {@user}", user);

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

            Log.Information("Updated user: {@existingUser}", existingUser);

            return _mapper.Map<UserDto>(existingUser);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingUser = await _userRepository.GetAsync(id);

            await _userRepository.DeleteAsync(existingUser);

            if (existingUser?.Followers != null)
            {
                await _userFollowersRepository.DeleteRangeAsync(existingUser.Followers);
            }

            if (existingUser?.Followings != null)
            {
                await _userFollowersRepository.DeleteRangeAsync(existingUser.Followings);
            }

            Log.Information("Deleted user: {@existingUser}", existingUser);

            return true;
        }
    }
}
