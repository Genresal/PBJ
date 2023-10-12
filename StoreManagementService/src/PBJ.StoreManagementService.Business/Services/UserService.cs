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

        public async Task<List<UserDto>> SearchUsersByEmailPartAsync(string emailPart, int take)
        {
            Log.Information("Email Part {email}", emailPart);

            var paginationResponse = await _userRepository
                .GetPaginatedAsync(1, take,
                    where: x => x.Email.ToLower().Contains(emailPart.ToLower()),
                    orderBy: x => x.Id);

            Log.Information("{items}", paginationResponse.Items);
            

            return _mapper.Map<List<UserDto>>(paginationResponse.Items);
        }

        public async Task<PaginationResponseDto<UserDto>> GetFollowersAsync(string userEmail, int page, int take)
        {
            var paginationResponse = await _userRepository.GetFollowersAsync(userEmail, page, take);

            var paginationResponseDto = _mapper.Map<PaginationResponseDto<UserDto>>(paginationResponse);

            foreach (var userDto in paginationResponseDto.Items)
            {
                if (userDto.Followers!.Any(x => x.FollowerEmail == userEmail))
                {
                    userDto.IsFollowingRequestUser = true;
                }
            }

            return paginationResponseDto;
        }

        public async Task<PaginationResponseDto<UserDto>> GetFollowingsAsync(string followerEmail, int page, int take)
        {
            var paginationResponse = await _userRepository.GetPaginatedAsync(page, take,
                where: x => x.Followers!.Any(uf => uf.FollowerEmail == followerEmail),
                orderBy: x => x.Id);

            var paginationResponseDto = _mapper.Map<PaginationResponseDto<UserDto>>(paginationResponse);

            paginationResponseDto.Items.ToList().ForEach(x => x.IsFollowingRequestUser = true);

            return paginationResponseDto;
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
