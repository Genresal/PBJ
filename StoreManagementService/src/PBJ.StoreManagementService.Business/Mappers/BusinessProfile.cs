using AutoMapper;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.Models.Comment;
using PBJ.StoreManagementService.Models.Post;
using PBJ.StoreManagementService.Models.User;
using PBJ.StoreManagementService.Models.UserFollowers;

namespace PBJ.StoreManagementService.Business.Mappers
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserRequestModel, User>()
                .ForMember(x => x.Id, options => options.Ignore());

            CreateMap<Comment, CommentDto>();
            CreateMap<CommentRequestModel, Comment>()
                .ForMember(x => x.Id, options => options.Ignore());

            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<PostRequestModel, Post>()
                .ForMember(x => x.Id, options => options.Ignore());

            CreateMap<UserFollowers, UserFollowersDto>().ReverseMap();
            CreateMap<UserFollowersRequestModel, UserFollowers>()
                .ForMember(x => x.Id, options => options.Ignore());
        }
    }
}
