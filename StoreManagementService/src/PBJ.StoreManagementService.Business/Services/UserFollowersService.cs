using AutoMapper;
using PBJ.StoreManagementService.Business.Constants;
using PBJ.StoreManagementService.Business.Dtos;
using PBJ.StoreManagementService.Business.Exceptions;
using PBJ.StoreManagementService.Business.Services.Abstract;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;

namespace PBJ.StoreManagementService.Business.Services
{
    public class UserFollowersService : IUserFollowersService
    {
        private readonly IUserFollowersRepository _userFollowersRepository;
        private readonly IMapper _mapper;

        public UserFollowersService(IUserFollowersRepository userFollowersRepository,
            IMapper mapper)
        {
            _userFollowersRepository = userFollowersRepository;
            _mapper = mapper;
        }

        public async Task<List<UserFollowersDto>> GetAmountAsync(int amount)
        {
            var userFollowers = await _userFollowersRepository.GetAmountAsync(amount);

            if (userFollowers.Count == 0)
            {
                throw new NotFoundException(ExceptionMessages.USERFOLLOWERS_EMPTY_MESSAGE);
            }

            return await Task.FromResult(_mapper.Map<List<UserFollowersDto>>(userFollowers));
        }

        public async Task<UserFollowersDto> GetAsync(int id)
        {
            var userFollower = await _userFollowersRepository.GetAsync(id);

            if (userFollower == null)
            {
                throw new NotFoundException(ExceptionMessages.USERFOLLOWER_NOT_FOUND_MESSAGE;
            }

            return await Task.FromResult(_mapper.Map<UserFollowersDto>(userFollower));
        }

        public async Task<bool> CreateAsync(UserFollowersDto userFollowersDto)
        {
            await _userFollowersRepository.CreateAsync(_mapper.Map<UserFollowers>(userFollowersDto));

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingUserFollower = await _userFollowersRepository.GetAsync(id);

            if (existingUserFollower == null)
            {
                throw new NotFoundException(ExceptionMessages.USERFOLLOWER_NOT_FOUND_MESSAGE);
            }

            await _userFollowersRepository.DeleteAsync(existingUserFollower);

            return await Task.FromResult(true);
        }
    }
}
