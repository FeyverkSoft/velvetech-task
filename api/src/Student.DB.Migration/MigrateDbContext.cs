using System;
using Student.DB.Migrations.Entities;
using Microsoft.EntityFrameworkCore;

namespace Student.DB.Migrations
{
    public class MigrateDbContext : DbContext
    {
        public MigrateDbContext(DbContextOptions<MigrateDbContext> options)
            : base(options) { }

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

                b.HasMany(g => g.Students)
                    .WithOne()
                    .HasPrincipalKey(_ => _.Id)
                    .HasForeignKey(_ => _.MentorId);


                b.Property(l => l.ConcurrencyTokens)
                    .IsRequired()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<Entities.Student>(b =>
            {
                b.ToTable(nameof(Student));

                b.HasIndex(t => t.Id)
                    .IsUnique();
                b.HasKey(t => t.Id);
                b.Property(t => t.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedNever()
                    .IsRequired();

                b.HasIndex(t => t.PublicId)
                    .IsUnique();

                b.Property(t => t.LastName)
                    .HasDefaultValue(40)
                    .IsRequired();
                b.Property(t => t.FirstName)
                    .HasMaxLength(40)
                    .IsRequired();
                b.Property(t => t.SecondName)
                    .HasMaxLength(60);
                b.Property(t => t.Gender)
                    .HasConversion<String>()
                    .HasMaxLength(16)
                    .IsRequired();

                b.Property(t => t.PublicId)
                    .HasMaxLength(16);

                b.Property(t => t.MentorId);
                b.Property(t => t.CreateDate);
                b.Property(t => t.UpdateDate);
                b.Property(t => t.Status)
                    .HasConversion<String>()
                    .HasMaxLength(16)
                    .IsRequired();

                b.Property(t => t.ConcurrencyTokens)
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<RefreshToken>(builder =>
            {
                builder.ToTable("RefreshToken");
                builder.HasKey(refreshToken => refreshToken.Id);
                builder.Property(refreshToken => refreshToken.Id)
                    .HasColumnName("RefreshTokenId")
                    .HasMaxLength(64)
                    .ValueGeneratedNever();
                builder.Property(refreshToken => refreshToken.UserId)
                    .IsRequired();
                builder.Property(refreshToken => refreshToken.ExpireDate)
                    .IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}