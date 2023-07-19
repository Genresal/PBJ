using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PBJ.StoreManagementService.DataAccess.Entities;

namespace PBJ.StoreManagementService.DataAccess.Context.Configurations
{
    public class UserFollowersConfiguration : IEntityTypeConfiguration<UserFollowers>
    {
        public void Configure(EntityTypeBuilder<UserFollowers> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(uf => uf.User)
                .WithMany(u => u.Followings)
                .HasForeignKey(uf => uf.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(uf => uf.Follower)
                .WithMany(u => u.Followers)
                .HasForeignKey(uf => uf.FollowerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(
                new UserFollowers()
                {
                    Id = 1,
                    UserId = 1,
                    FollowerId = 2
                },
                new UserFollowers()
                {
                    Id = 2,
                    UserId = 2,
                    FollowerId = 1
                }
                );
        }
    }
}
