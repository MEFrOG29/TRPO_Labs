using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace lab1.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<LectureMaterial> LectureMaterials { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<TestQuestion> TestQuestions { get; set; }

    public virtual DbSet<Topic> Topics { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.ToTable("assignments");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(datetime('now', 'localtime'))")
                .HasColumnName("createdAt");
            entity.Property(e => e.Deadline).HasColumnName("deadline");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.MaxPoints).HasColumnName("maxPoints");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.TopicsId).HasColumnName("topicsId");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Assignment).HasForeignKey<Assignment>(d => d.Id);
        });

        modelBuilder.Entity<LectureMaterial>(entity =>
        {
            entity.ToTable("lectureMaterials");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(datetime('now', 'localtime'))")
                .HasColumnName("createdAt");
            entity.Property(e => e.FileUrl).HasColumnName("fileUrl");
            entity.Property(e => e.MaterialType).HasColumnName("materialType");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.TopicsId).HasColumnName("topicsId");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.LectureMaterial).HasForeignKey<LectureMaterial>(d => d.Id);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("roles");

            entity.HasIndex(e => e.Name, "roles_name_IDX").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.ToTable("tests");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(datetime('now', 'localtime'))")
                .HasColumnName("createdAt");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.LimitMinutes).HasColumnName("limitMinutes");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.TopicId).HasColumnName("topicId");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Test).HasForeignKey<Test>(d => d.Id);
        });

        modelBuilder.Entity<TestQuestion>(entity =>
        {
            entity.ToTable("testQuestions");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AnswersJson).HasColumnName("answersJson");
            entity.Property(e => e.Points)
                .HasDefaultValue(1)
                .HasColumnName("points");
            entity.Property(e => e.QuestionText).HasColumnName("questionText");
            entity.Property(e => e.QuestionType)
                .HasDefaultValue("single")
                .HasColumnName("questionType");
            entity.Property(e => e.TestId).HasColumnName("testId");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.TestQuestion).HasForeignKey<TestQuestion>(d => d.Id);
        });

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.ToTable("topics");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(datetime('now', 'localtime'))")
                .HasColumnName("createdAt");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Title).HasColumnName("title");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_IDX").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(datetime('now', 'localtime'))")
                .HasColumnName("createdAt");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.FirstName).HasColumnName("firstName");
            entity.Property(e => e.LastName).HasColumnName("lastName");
            entity.Property(e => e.PasswordHash).HasColumnName("passwordHash");
            entity.Property(e => e.RoleId).HasColumnName("roleId");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.User).HasForeignKey<User>(d => d.Id);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
