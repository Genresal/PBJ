using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PBJ.StoreManagementService.DataAccess.Entities;

namespace PBJ.StoreManagementService.DataAccess.Context.Configurations
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.ToTable(nameof(Subscription));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.SubscriptionDate)
                .IsRequired()
                .HasColumnType("date");

            builder.HasData(
                new Subscription()
                {
                    Id = 1,
                    SubscribedUserId = 1,
                    SubscriptionDate = DateTime.Now
                },
                new Subscription()
                {
                    Id = 2,
                    SubscribedUserId = 2,
                    SubscriptionDate = DateTime.Now
                }
                );
        }
    }
}
