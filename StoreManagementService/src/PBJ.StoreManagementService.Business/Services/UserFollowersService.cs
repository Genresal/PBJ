﻿using AutoMapper;
using PBJ.StoreManagementService.Business.Constants;
using PBJ.StoreManagementService.Business.Exceptions;
using PBJ.StoreManagementService.Business.Services.Abstract;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;
using PBJ.StoreManagementService.Models.Pagination;
using PBJ.StoreManagementService.Models.UserFollowers;
using Serilog;

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

        public async Task<PaginationResponseDto<UserFollowersDto>> GetPaginatedAsync(int page, int take)
        {
            var paginationResponse = await _userFollowersRepository
                .GetPaginatedAsync(page, take, orderBy: x => x.Id);

            return _mapper.Map<PaginationResponseDto<UserFollowersDto>>(paginationResponse);
        }

        public async Task<UserFollowersDto> GetAsync(string userEmail, string followerEmail)
        {
            var userFollower = await _userFollowersRepository
                .FirstOrDefaultAsync(x => x.UserEmail == userEmail && x.FollowerEmail == followerEmail);

            if (userFollower == null)
            {
                throw new NotFoundException(ExceptionMessages.USERFOLLOWER_NOT_FOUND_MESSAGE);
            }

            return _mapper.Map<UserFollowersDto>(userFollower);
        }

        public async Task<UserFollowersDto> CreateAsync(UserFollowersRequestModel userFollowersRequestModel)
        {
            var userFollower = _mapper.Map<UserFollowers>(userFollowersRequestModel);

            await _userFollowersRepository.CreateAsync(userFollower);

            Log.Information("Created userFollower: {@userFollower}", userFollower);

            return _mapper.Map<UserFollowersDto>(userFollower);
        }

        public async Task<bool> DeleteAsync(UserFollowersRequestModel requestModel)
        {
            var existingUserFollower = await _userFollowersRepository
                .FirstOrDefaultAsync(x => x.UserEmail == requestModel.UserEmail && x.FollowerEmail == requestModel.FollowerEmail);

            await _userFollowersRepository.DeleteAsync(existingUserFollower);

            Log.Information("Deleted userFollower: {@existingUserFollower}", existingUserFollower);

            return true;
        }
    }
}
