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

            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nchar");

            builder.HasData(
                new User
                {
                    Id = 1,
                    Email = "unique1@email.com"
                },
                new User
                {
                    Id = 2,
                    Email = "unique2@email.com"
                }
            );
        }
    }
}
