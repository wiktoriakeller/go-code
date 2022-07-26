﻿using GoCode.Domain.Entities;
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

            modelBuilder.Entity<Course>(eb =>
            {
                eb.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(1000);

                eb.Property(c => c.Description)
                    .IsRequired()
                    .HasMaxLength(2000);

                eb.Property(c => c.XP)
                    .IsRequired();

                eb.Property(c => c.PassPercentTreshold)
                    .IsRequired();
            });

            modelBuilder.Entity<Question>(eb =>
            {
                eb.Property(q => q.Content)
                    .IsRequired()
                    .HasMaxLength(2000);
            });

            modelBuilder.Entity<Answear>(eb =>
            {
                eb.Property(a => a.Content)
                    .IsRequired()
                    .HasMaxLength(2000);

                eb.Property(a => a.IsCorrect)
                    .IsRequired();
            });

            modelBuilder.Entity<Course>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Question>()
                .HasIndex(q => new { q.Content, q.CourseId })
                .IsUnique();

            modelBuilder.Entity<Answear>()
                .HasIndex(a => new { a.Content, a.QuestionId })
                .IsUnique();
        }
    }
}
