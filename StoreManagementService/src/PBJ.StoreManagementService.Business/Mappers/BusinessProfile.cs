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
            CreateMap<FollowingDto, Following>().ReverseMap();
            CreateMap<SubscriptionDto, Subscription>().ReverseMap();
            CreateMap<UserSubscriptionDto, UserSubscription>().ReverseMap();
            CreateMap<UserFollowingDto, UserFollowing>().ReverseMap();
        }
    }
}
