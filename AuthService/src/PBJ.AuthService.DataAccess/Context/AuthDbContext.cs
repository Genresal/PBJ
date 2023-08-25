using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;
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

        public DbSet<IdentityResource> IdentityResources { get; set; } = null!;

        public DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; } = null!;

        public DbSet<PersistedGrant> PersistedGrants { get; set; } = null!;

        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; } = null!;

        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
            if (Database.IsRelational())
            {
                Database.Migrate();
            }
        }

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
