using AutoMapper;
using PBJ.StoreManagementService.Api.RequestModels;
using PBJ.StoreManagementService.Business.Dtos;

namespace PBJ.StoreManagementService.Api.Mappers
{
    public class ApiProfile : Profile
    {
        public ApiProfile()
        {
            CreateMap<UserRequestModel, UserDto>();
            CreateMap<PostRequestModel, PostDto>();
            CreateMap<CommentRequestModel, CommentDto>();
            CreateMap<UserFollowerRequestModel, UserFollowersDto>();
        }
    }
}
