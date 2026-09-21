using TraineeHub.Domain.Entities;
using TraineeHub.Domain.Enum;

namespace TraineeHub.Infrastructure.Persistence;

public static class DbSeeder
{
    public static void Seed(TraineeHubDbContext context)
    {
        if (context.Topics.Any())
            return;

        // -----------------------------
        // Trainees
        // -----------------------------

        var trainee1 = new Trainee
        {
            FullName = "Ahmed Ali",
            Email = "ahmed@test.com"
        };

        var trainee2 = new Trainee
        {
            FullName = "Mohammed Hassan",
            Email = "mohammed@test.com"
        };

        var trainee3 = new Trainee
        {
            FullName = "Sara Khaled",
            Email = "sara@test.com"
        };

        var trainee4 = new Trainee
        {
            FullName = "Omar Salem",
            Email = "omar@test.com"
        };

        // -----------------------------
        // Topics
        // -----------------------------

        var topic1 = new Topic
        {
            Title = "C#",
            Description = "C# Programming Language"
        };

        var topic2 = new Topic
        {
            Title = "SQL",
            Description = "Database Queries and Design"
        };

        var topic3 = new Topic
        {
            Title = "ASP.NET Core",
            Description = "Building Web APIs with .NET"
        };

        // -----------------------------
        // Assignments
        // -----------------------------

        var assignment1 = new Assignment
        {
            Title = "C# Basics",
            Description = "Variables, Loops, Conditions",
            Difficulty = 1,
            DutDate = DateTime.UtcNow.AddDays(7),
            Topic = topic1
        };

        var assignment2 = new Assignment
        {
            Title = "OOP in C#",
            Description = "Classes and Inheritance",
            Difficulty = 2,
            DutDate = DateTime.UtcNow.AddDays(10),
            Topic = topic1
        };

        var assignment3 = new Assignment
        {
            Title = "SQL Queries",
            Description = "SELECT, JOIN, WHERE",
            Difficulty = 2,
            DutDate = DateTime.UtcNow.AddDays(7),
            Topic = topic2
        };

        var assignment4 = new Assignment
        {
            Title = "Database Design",
            Description = "Normalization and ERD",
            Difficulty = 3,
            DutDate = DateTime.UtcNow.AddDays(12),
            Topic = topic2
        };

        var assignment5 = new Assignment
        {
            Title = "Build Web API",
            Description = "Create REST API with ASP.NET Core",
            Difficulty = 4,
            DutDate = DateTime.UtcNow.AddDays(14),
            Topic = topic3
        };

        // -----------------------------
        // Submissions
        // -----------------------------

        var submission1 = new Submission
        {
            Assignment = assignment1,
            Trainee = trainee1,
            Notes = "Completed all tasks",
            Stauts = SubmissionStatus.Approve
        };

        var submission2 = new Submission
        {
            Assignment = assignment1,
            Trainee = trainee2,
            Notes = "Need review",
            Stauts = SubmissionStatus.Pending
        };

        var submission3 = new Submission
        {
            Assignment = assignment2,
            Trainee = trainee1,
            Notes = "OOP implemented",
            Stauts = SubmissionStatus.Approve
        };

        var submission4 = new Submission
        {
            Assignment = assignment3,
            Trainee = trainee3,
            Notes = "Queries working",
            Stauts = SubmissionStatus.Approve
        };

        var submission5 = new Submission
        {
            Assignment = assignment4,
            Trainee = trainee4,
            Notes = "Database schema attached",
            Stauts = SubmissionStatus.Pending
        };

        var submission6 = new Submission
        {
            Assignment = assignment5,
            Trainee = trainee2,
            Notes = "API ready for testing",
            Stauts = SubmissionStatus.Pending
        };

        // -----------------------------
        // Save Data
        // -----------------------------

        context.Trainees.AddRange(
            trainee1,
            trainee2,
            trainee3,
            trainee4
        );

        context.Topics.AddRange(
            topic1,
            topic2,
            topic3
        );

        context.Assignments.AddRange(
            assignment1,
            assignment2,
            assignment3,
            assignment4,
            assignment5
        );

        context.Submissions.AddRange(
            submission1,
            submission2,
            submission3,
            submission4,
            submission5,
            submission6
        );

        context.SaveChanges();
    }
}