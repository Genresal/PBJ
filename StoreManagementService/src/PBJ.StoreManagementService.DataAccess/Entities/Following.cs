﻿using PBJ.StoreManagementService.DataAccess.Entities.Abstract;

namespace PBJ.StoreManagementService.DataAccess.Entities
{
    public class Following : BaseEntity
    {
        public DateTime CreatedAt { get; set; }

        public int FollowingUserId { get; set; }

        public User? FollowingUser { get; set; }

        public IReadOnlyCollection<UserFollowing>? UserFollowings { get; set; }
    }
}
