using System;
using Microsoft.EntityFrameworkCore;
using Student.Public.Domain.Authentication;

namespace Student.Public.Infrastructure.Authentication
{
    public sealed class AuthenticationDbContext : DbContext
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options) { }

        internal DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(b =>
            {
                b.ToTable(nameof(User));

                b.HasIndex(g => g.Id)
                    .IsUnique();

                b.HasKey(g => g.Id);
                b.Property(g => g.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedNever()
                    .IsRequired();

                b.Property(g => g.Password);
                b.Property(g => g.Login);

                b.Property(g => g.Status)
                    .HasColumnName("Status")
                    .HasConversion<String>()
                    .HasMaxLength(32)
                    .IsRequired();

                b.Ignore(g => g.IsActive);
            });
        }
    }
}