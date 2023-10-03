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
                .WithMany(u => u.Followers)
                .HasForeignKey(uf => uf.UserEmail)
                .HasPrincipalKey(u => u.Email)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(uf => uf.Follower)
                .WithMany(u => u.Followings)
                .HasForeignKey(uf => uf.FollowerEmail)
                .HasPrincipalKey(u => u.Email)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(
                new UserFollowers
                {
                    Id = 1,
                    UserEmail = "unique1@email.com",
                    FollowerEmail = "unique2@email.com"
                },
                new UserFollowers
                {
                    Id = 2,
                    UserEmail = "unique2@email.com",
                    FollowerEmail = "unique1@email.com"
                }
                );
        }
    }
}
