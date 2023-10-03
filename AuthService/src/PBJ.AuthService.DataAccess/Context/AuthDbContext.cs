using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.EntityFramework.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PBJ.AuthService.DataAccess.Entities;

namespace PBJ.AuthService.DataAccess.Context
{
    public class AuthDbContext : IdentityDbContext<AuthUser, AuthRole, int>,
        IConfigurationDbContext, IPersistedGrantDbContext
    {
        public DbSet<Client> Clients { get; set; } = null!;

        public DbSet<ApiResource> ApiResources { get; set; } = null!;

        public DbSet<ApiScope> ApiScopes { get; set; } = null!;

        public DbSet<IdentityResource> IdentityResources { get; set; }

        public DbSet<PersistedGrant> PersistedGrants { get; set; }

        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }

        public DbSet<Key> Keys { get; set; }

        public DbSet<ServerSideSession> ServerSideSessions { get; set; }

        public DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }

        public DbSet<IdentityProvider> IdentityProviders { get; set; }

        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        { }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DeviceFlowCodes>().ToTable("DeviceCode").HasKey(x => x.UserCode);
            builder.Entity<PersistedGrant>().ToTable(nameof(PersistedGrant)).HasKey(x => x.Key);
        }
    }
}
