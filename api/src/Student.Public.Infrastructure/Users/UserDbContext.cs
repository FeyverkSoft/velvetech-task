using System;
using Microsoft.EntityFrameworkCore;
using Student.Public.Domain.Users.Entity;

namespace Student.Public.Infrastructure.Users
{
    public sealed class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

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

                b.Property(g => g.CreateDate)
                    .IsRequired();
                b.Property(g => g.UpdateDate);
                b.Property(g => g.DeletedDate);
                b.Property(g => g.Password);
                b.Property(g => g.Login);

                b.Property(g => g.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(512)
                    .IsRequired();

                b.Property(g => g.Status)
                    .HasColumnName("Status")
                    .HasConversion<String>()
                    .HasMaxLength(32)
                    .IsRequired();

                b.Property(l => l.ConcurrencyTokens)
                    .IsRequired()
                    .IsConcurrencyToken();
            });
        }
    }
}