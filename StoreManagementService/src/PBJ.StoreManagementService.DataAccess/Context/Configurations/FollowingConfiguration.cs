using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PBJ.StoreManagementService.DataAccess.Entities;

namespace PBJ.StoreManagementService.DataAccess.Context.Configurations
{
    public class FollowingConfiguration : IEntityTypeConfiguration<Following>
    {
        public void Configure(EntityTypeBuilder<Following> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnType("date");

            builder.HasData(
                new Following()
                {
                    Id = 1,
                    FollowingUserId = 1,
                    CreatedAt = DateTime.Now
                },
                new Following()
                {
                    Id = 2,
                    FollowingUserId = 2,
                    CreatedAt = DateTime.Now
                }
            );
        }
    }
}
