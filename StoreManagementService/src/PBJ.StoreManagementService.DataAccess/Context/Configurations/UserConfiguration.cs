using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PBJ.StoreManagementService.DataAccess.Entities;

namespace PBJ.StoreManagementService.DataAccess.Context.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnType("nchar");

            builder.Property(x => x.Surname)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnType("nchar");

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnType("nchar");

            builder.Property(x => x.BirthDate)
                .IsRequired(false)
                .HasColumnType("date");

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nchar");

            builder.HasOne(u => u.Subscription)
                .WithOne(s => s.SubscribedUser)
                .HasForeignKey<Subscription>(s => s.SubscribedUserId);

            builder.HasOne(u => u.Following)
                .WithOne(s => s.FollowingUser)
                .HasForeignKey<Following>(s => s.FollowingUserId);

            builder.HasData(
                new User()
                {
                    Id = 1,
                    Name = "Name1",
                    Surname = "Surname1",
                    LastName = "Lastname1",
                    BirthDate = DateTime.Now,
                    Email = "login1"
                },
                new User()
                {
                    Id = 2,
                    Name = "Name2",
                    Surname = "Surname2",
                    LastName = "Lastname2",
                    BirthDate = DateTime.Now,
                    Email = "login2"
                }
            );
        }
    }
}
