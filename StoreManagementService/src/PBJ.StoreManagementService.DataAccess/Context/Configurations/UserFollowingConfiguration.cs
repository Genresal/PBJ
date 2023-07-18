using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PBJ.StoreManagementService.DataAccess.Entities;

namespace PBJ.StoreManagementService.DataAccess.Context.Configurations
{
    public class UserFollowingConfiguration : IEntityTypeConfiguration<UserFollowing>
    {
        public void Configure(EntityTypeBuilder<UserFollowing> builder)
        {
            builder.ToTable(nameof(UserFollowing));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(uf => uf.User)
                .WithMany(u => u.UserFollowings)
                .HasForeignKey(u => u.Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(uf => uf.Following)
                .WithMany(f => f.UserFollowings)
                .HasForeignKey(f => f.FollowingId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(
                new UserFollowing()
                {
                    Id = 1,
                    UserId = 1,
                    FollowingId = 1,
                },
                new UserFollowing()
                {
                    Id = 2,
                    UserId = 2,
                    FollowingId = 2
                }
                );
        }
    }
}
