# TraineeHub.Web MVC

## Overview
TraineeHub.Web is an ASP.NET Core MVC application built for managing trainees, topics, assignments, and submissions workflow.

The application uses:
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Redis (for dashboard caching)
- Docker Compose (to run Redis locally)

---

## Features

### Trainees
- List trainees
- Create trainee
- Edit trainee
- View trainee details

### Topics
- List topics
- Create topic
- View topic details

### Assignments
- Add assignments under a topic
- View assignments inside topic details

### Submissions Workflow
- Submit assignment
- Select trainee
- Select assignment
- Add notes
- Review submissions
- Filter submissions by status
- Approve / Reject submissions

### Validation Rules
- Email must be in valid format
- Title is required and minimum length is enforced
- Difficulty must be between 1 and 5
- Due date must be a future date

### Bonus - Dashboard + Redis Cache
- Dashboard page shows:
  - Total trainees
  - Total topics
  - Total assignments
  - Submission counts by status
- Dashboard data is cached for 60 seconds using Redis
- Refresh Cache button clears cached dashboard data
- Redis runs locally using Docker Compose

---

## Technologies Used
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Redis
- Docker Compose

---

## Project Structure

- `TraineeHub.Domain` → Entities and Enums
- `TraineeHub.Infrastructure` → DbContext and data access
- `TraineeHub.Web` → MVC UI (Controllers, Views, ViewModels)

---

## How to Run

### 1) Clone or open the solution
Open the solution in Visual Studio.

### 2) Configure database connection
Make sure the connection string is correct in:

```json
TraineeHub.Web/appsettings.json