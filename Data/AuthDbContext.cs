using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Notes.Identity.Models;

namespace Notes.Identity.Data
{
    public class AuthDbContext : IdentityDbContext<User>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(e => e.ToTable("Users"));
            builder.Entity<IdentityRole>(e => e.ToTable(name: "Roles"));
            builder.Entity<IdentityUserRole<string>>(e => e.ToTable("UserRoles"));
            builder.Entity<IdentityUserClaim<string>>(e => e.ToTable("UserClaims"));
            builder.Entity<IdentityUserLogin<string>>(e => e.ToTable("UserLogins"));
            builder.Entity<IdentityUserToken<string>>(e => e.ToTable("UserTokens"));
            builder.Entity<IdentityRoleClaim<string>>(e => e.ToTable("RoleClaims"));

            builder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
