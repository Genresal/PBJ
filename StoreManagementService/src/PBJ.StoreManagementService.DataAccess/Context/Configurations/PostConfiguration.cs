using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PBJ.StoreManagementService.DataAccess.Entities;

namespace PBJ.StoreManagementService.DataAccess.Context.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Content)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("nchar");

            builder.Property(x => x.PostDate)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(
                new Post()
                {
                    Id = 1,
                    Content = "PostContent1",
                    PostDate = DateTime.Now,
                    UserId = 1
                },
                new Post()
                {
                    Id = 2,
                    Content = "PostContent2",
                    PostDate = DateTime.Now,
                    UserId = 2
                }
                );
        }
    }
}
