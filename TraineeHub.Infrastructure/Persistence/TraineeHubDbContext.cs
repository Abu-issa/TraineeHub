using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using TraineeHub.Domain.Entities;

namespace TraineeHub.Infrastructure.Persistence;

public class TraineeHubDbContext : DbContext
{
    public TraineeHubDbContext(DbContextOptions<TraineeHubDbContext> options)
        : base(options)
    {
    }

    public DbSet<Trainee> Trainees { get; set; }

    public DbSet<Topic> Topics { get; set; }

    public DbSet<Assignment> Assignments { get; set; }

    public DbSet<Submission> Submissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Topic 1-* Assignments
        modelBuilder.Entity<Assignment>()
            .HasOne(a => a.Topic)
            .WithMany(t => t.Assignments)
            .HasForeignKey(a => a.TopicTd);

        // Assignment 1-* Submissions
        modelBuilder.Entity<Submission>()
            .HasOne(s => s.Assignment)
            .WithMany(a => a.Submissions)
            .HasForeignKey(s => s.AssignmentId);

        // Trainee 1-* Submissions
        modelBuilder.Entity<Submission>()
            .HasOne(s => s.Trainee)
            .WithMany(t => t.Submissions)
            .HasForeignKey(s => s.TraineerId);

        // Constraints

        modelBuilder.Entity<Trainee>()
            .Property(t => t.FullName)
            .IsRequired()
            .HasMaxLength(150);

        modelBuilder.Entity<Trainee>()
       .Property(t => t.Email)
       .IsRequired()
       .HasMaxLength(63);

        //topic

        modelBuilder.Entity<Topic>()
        .Property(t => t.Description)
        .HasMaxLength(250);


        modelBuilder.Entity<Topic>()
            .Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(100);

        // assignment
        modelBuilder.Entity<Assignment>()
      .Property(a => a.Description)
      .HasMaxLength(250);

        modelBuilder.Entity<Assignment>()
            .Property(a => a.Title)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Assignment>()
       .Property(a => a.Difficulty)
       .IsRequired();

        modelBuilder.Entity<Assignment>()
       .HasCheckConstraint(
           "CK_Assignment_Difficulty",
           "Difficulty >= 1 AND Difficulty <= 5"
       );


        // submission

        modelBuilder.Entity<Submission>()
       .Property(s => s.Notes)
       .HasMaxLength(150);

        modelBuilder.Entity<Submission>()
            .Property(s => s.Stauts)
            .IsRequired()
        .HasConversion<string>();



















    }
}
