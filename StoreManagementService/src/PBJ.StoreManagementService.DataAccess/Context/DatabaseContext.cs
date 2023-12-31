﻿using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PBJ.StoreManagementService.DataAccess.Entities;

namespace PBJ.StoreManagementService.DataAccess.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<UserFollowers> UserFollowers { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
