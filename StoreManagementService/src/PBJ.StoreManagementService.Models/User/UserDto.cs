﻿using PBJ.StoreManagementService.Models.Comment;
using PBJ.StoreManagementService.Models.Post;
using PBJ.StoreManagementService.Models.UserFollowers;

namespace PBJ.StoreManagementService.Models.User
{
    public class UserDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Lastname { get; set; }

        public DateTime Birthdate { get; set; }

        public string? Email { get; set; }

        public IReadOnlyCollection<PostDto>? Posts { get; set; }

        public IReadOnlyCollection<CommentDto>? Comments { get; set; }

        public IReadOnlyCollection<UserFollowersDto>? Followings { get; set; }

        public IReadOnlyCollection<UserFollowersDto>? Followers { get; set; }
    }
}