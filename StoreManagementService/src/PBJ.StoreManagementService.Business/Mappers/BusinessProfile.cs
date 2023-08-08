using AutoMapper;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.Models.Comment;
using PBJ.StoreManagementService.Models.Pagination;
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
            CreateMap<CreateCommentRequestModel, Comment>()
                .ForMember(x => x.Id, options => options.Ignore());

            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<CreatePostRequestModel, Post>()
                .ForMember(x => x.Id, options => options.Ignore());

            CreateMap<UserFollowers, UserFollowersDto>().ReverseMap();
            CreateMap<UserFollowersRequestModel, UserFollowers>()
                .ForMember(x => x.Id, options => options.Ignore());

            CreateMap<PaginationResponse<User>, PaginationResponseDto<UserDto>>().ReverseMap();
            CreateMap<PaginationResponse<Post>, PaginationResponseDto<PostDto>>().ReverseMap();
            CreateMap<PaginationResponse<Comment>, PaginationResponseDto<CommentDto>>().ReverseMap();
            CreateMap<PaginationResponse<UserFollowers>, PaginationResponseDto<UserFollowersDto>>().ReverseMap();
        }
    }
}
