using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using UrlShorteningService.Domain;

namespace UrlShorteningService.Infrastructure.Database.EFCore
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UrlInfo>(actionBuilder =>
            {
                actionBuilder.HasKey(x => x.Id);

                actionBuilder.Property(x => x.Id)
                    .ValueGeneratedNever();

                actionBuilder.Property(x => x.OriginalUrl)
                    .IsRequired()
                    .HasMaxLength(2048);

                actionBuilder.Property(x => x.ShortUrl)
                            .IsRequired()
                            .HasMaxLength(100);


            });

            modelBuilder.Entity<AppUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole<Guid>>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");

            modelBuilder.Entity<IdentityUserLogin<Guid>>().HasKey(x => new { 
                x.LoginProvider, 
                x.ProviderKey, 
                x.UserId 
            });
            modelBuilder.Entity<IdentityUserToken<Guid>>().HasKey(x => new { 
                x.UserId, 
                x.LoginProvider, 
                x.Name 
            });
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasKey(x => new { 
                x.UserId, 
                x.RoleId 
            });

            var passwordHasher = new PasswordHasher<AppUser>();
            var user = new AppUser()
            {
                Id = new Guid("fe8e234e-3fb1-4fa5-abc6-8e6aafc98a91"),
                Email = "test123@gmail.com",
                UserName = "test123@gmail.com",
                NormalizedEmail = "TEST123@GMAIL.COM",
                NormalizedUserName = "TEST123@GMAIL.COM"
            };
            user.PasswordHash = passwordHasher.HashPassword(user, "test123");
            modelBuilder.Entity<AppUser>().HasData(
                user

            );
        }
        public DbSet<UrlInfo> UrlInfos { get; set; }
    }
}
