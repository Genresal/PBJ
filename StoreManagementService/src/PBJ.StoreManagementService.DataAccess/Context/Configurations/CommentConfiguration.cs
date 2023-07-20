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

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.PostId)
                .IsRequired();

            builder.HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(
                new Comment()
                {
                    Id = 1,
                    Content = "CommentContent1",
                    CreatedAt = DateTime.Now,
                    UserId = 1,
                    PostId = 2
                },
                new Comment()
                {
                    Id = 2,
                    Content = "CommentContent2",
                    CreatedAt = DateTime.Now,
                    UserId = 2,
                    PostId = 1
                }
                );
        }
    }
}
