using AutoMapper;
using PBJ.StoreManagementService.Business.Dtos;
using PBJ.StoreManagementService.DataAccess.Entities;

namespace PBJ.StoreManagementService.Business.Mappers
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<CommentDto, Comment>().ReverseMap();
            CreateMap<PostDto, Post>().ReverseMap();
            CreateMap<UserFollowersDto, UserFollowers>().ReverseMap();
        }
    }
}
