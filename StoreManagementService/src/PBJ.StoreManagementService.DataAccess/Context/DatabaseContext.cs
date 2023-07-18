using Microsoft.EntityFrameworkCore;
using PBJ.StoreManagementService.DataAccess.Entities;
using System.Reflection;

namespace PBJ.StoreManagementService.DataAccess.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Following> Followings { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }

        public DbSet<UserFollowing> UserFollowings { get; set; }

        public DbSet<UserSubscription> UserSubscriptions { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
