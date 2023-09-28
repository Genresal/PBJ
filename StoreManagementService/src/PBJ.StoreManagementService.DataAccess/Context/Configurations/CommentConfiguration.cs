using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PBJ.StoreManagementService.DataAccess.Entities;

namespace PBJ.StoreManagementService.DataAccess.Context.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Content)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nchar");

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnType("date");

            builder.HasOne(p => p.User)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.UserEmail)
                .HasPrincipalKey(u => u.Email)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Comment
                {
                    Id = 1,
                    Content = "CommentContent1",
                    UserEmail = "unique1@email.com",
                    PostId = 2
                },
                new Comment
                {
                    Id = 2,
                    Content = "CommentContent2",
                    UserEmail = "unique2@email.com",
                    PostId = 1
                }
                );
        }
    }
}
