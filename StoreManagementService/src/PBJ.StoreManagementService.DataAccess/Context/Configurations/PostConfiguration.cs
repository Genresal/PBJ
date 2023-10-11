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
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnType("date");

            builder.HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserEmail)
                .HasPrincipalKey(u => u.Email)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Post
                {
                    Id = 1,
                    Content = "PostContent1",
                    UserEmail = "unique1@email.com"
                },
                new Post
                {
                    Id = 2,
                    Content = "PostContent2",
                    UserEmail = "unique1@email.com"
                }
            );
        }
    }
}
