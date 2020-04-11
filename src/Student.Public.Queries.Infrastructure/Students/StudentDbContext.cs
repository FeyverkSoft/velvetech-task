using System;
using Microsoft.EntityFrameworkCore;

namespace Student.Public.Queries.Infrastructure.Students
{
    public sealed class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) { }

        internal DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(b =>
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
                b.Property(t => t.PublicId)
                    .HasMaxLength(16);

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

                b.Property(t => t.MentorId);
                b.Property(t => t.CreateDate);
                b.Property(t => t.Status)
                    .HasConversion<String>();
            });
        }
    }
}
