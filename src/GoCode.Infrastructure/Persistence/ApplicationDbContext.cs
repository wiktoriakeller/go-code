using GoCode.Domain.Entities;
using GoCode.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoCode.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, string>
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RefreshToken>(eb =>
            {
                eb.HasKey(x => x.Token);

                eb.Property(x => x.Token)
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Answear>(eb =>
            {
                eb.Property(a => a.Content)
                    .IsRequired()
                    .HasMaxLength(500);

                eb.Property(a => a.IsCorrect)
                    .IsRequired();
            });

            modelBuilder.Entity<Question>(eb =>
            {
                eb.Property(q => q.Content)
                    .IsRequired()
                    .HasMaxLength(500);

                eb.Property(q => q.XP)
                    .IsRequired();
            });

            modelBuilder.Entity<Course>(eb =>
            {
                eb.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                eb.Property(c => c.Description)
                    .IsRequired()
                    .HasMaxLength(500);
            });
        }
    }
}
