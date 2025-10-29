using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MVCWebApp_CRUD.Models;

public partial class SchoolDbContext : DbContext
{
    public SchoolDbContext()
    {
    }

    public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Course__3214EC07A8990877");

            entity.ToTable("Course");

            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Subject).WithMany(p => p.Courses)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Course__SubjectI__4F7CD00D");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Student__3214EC07302DEB17");

            entity.ToTable("Student");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasMany(d => d.Courses).WithMany(p => p.Students)
                .UsingEntity<Dictionary<string, object>>(
                    "StudentCourse",
                    r => r.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__StudentCo__Cours__5070F446"),
                    l => l.HasOne<Student>().WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__StudentCo__Stude__5165187F"),
                    j =>
                    {
                        j.HasKey("StudentId", "CourseId").HasName("PK__StudentC__5E57FC83CBAE2EDC");
                        j.ToTable("StudentCourse");
                    });
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Subject__3214EC074422E7D2");

            entity.ToTable("Subject");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
