# 🚀 TraineeHub

A complete multi-layered training management system built step-by-step using **.NET**, covering Console Apps, SQL, EF Core, MVC, Caching, Messaging, and API documentation.

---

## 📌 Overview

TraineeHub is designed for team leads to manage:

* Trainees
* Topics
* Assignments
* Submissions & Review Workflow (Approve / Reject)

The project evolves across multiple stages (CLI → Database → EF Core → MVC → Caching → API Docs), demonstrating clean architecture and real-world backend practices.

---

## 🧱 Solution Structure

```
TraineeHub.sln
│
├── src/
│   ├── TraineeHub.Domain          # Entities + Enums (pure domain)
│   ├── TraineeHub.Infrastructure  # EF Core, DbContext, Persistence
│   ├── TraineeHub.Cli             # Console App (initial interface)
|   ├── TraineeHub.Appliction     
|   ├── TraineeHub.ِApi                        
│   └── TraineeHub.Web             # ASP.NET Core MVC UI
│
├
│
└── README.md
```

---

## ⚙️ Features

### ✅ Core Features

* Manage trainees, topics, assignments
* Submission workflow (submit / approve / reject)
* Validation rules (email, difficulty, due date, etc.)

### ✅ CLI (Console App)

* Command-based interface:

  ```
  trainee add
  trainee list
  topic add
  topic list
  assignment add
  assignment list --topic <id>
  submission add
  submission list --trainee <id>
  submission approve <id>
  submission reject <id>
  ```

### ✅ Database (SQL Server)

* Fully designed schema with:

  * PK / FK relationships
  * Constraints (CHECK, NOT NULL, UNIQUE)
  * Indexes for performance

### ✅ EF Core Integration

* Code-first migrations
* Relationships configured:

  * Topic → Assignments (1-M)
  * Assignment → Submissions (1-M)
  * Trainee → Submissions (1-M)

### ✅ MVC Web App

* Full UI with:

  * Trainees CRUD
  * Topics + Assignments
  * Submission workflow
* Filtering submissions by status
* Server-side validation

### ✅ Redis Caching (Bonus)

* Dashboard page with:

  * Totals (trainees, topics, assignments)
  * Submissions grouped by status
* Cached for 60 seconds
* Manual cache refresh button

### ✅ RabbitMQ (Messaging Integration)

* Event publishing for system actions (e.g., submission created)
* Decoupled architecture for scalability

### ✅ Swagger / OpenAPI

* Interactive API documentation
* XML comments included
* Response types documented

---

## 🛠️ Prerequisites

Make sure you have:

* .NET SDK (10 )
* SQL Server (local)
* Redis (optional for caching)
* RabbitMQ (optional for messaging)
* Visual Studio 

---

## ▶️ How to Run

### 1️⃣ Clone the repository

```
git clone https://github.com/your-username/TraineeHub.git
cd TraineeHub
```

---

### 2️⃣ Setup Database

Update connection string in:

```
appsettings.json
```

Then run:

```
dotnet ef database update --project src/TraineeHub.Infrastructure --startup-project src/TraineeHub.Web
```

---

### 3️⃣ Run CLI App

```
cd src/TraineeHub.Cli
dotnet run
```

---

### 4️⃣ Run MVC Web App

```
cd src/TraineeHub.Web
dotnet run
```

Open in browser:

```
https://localhost:xxxx
```

---

### 5️⃣ Swagger UI (API)

```
https://localhost:xxxx/swagger
```

---

## 🧪 What to Test

### Trainees

* Create new trainee
* Validate email format
* View list

### Topics & Assignments

* Create topic
* Add assignments under topic
* Validate difficulty (1–5)
* Validate future due date

### Submissions

* Submit assignment
* Approve / Reject submission
* Filter by status

### Dashboard (Redis)

* View cached statistics
* Refresh cache manually

##

---

## 📊 Performance Optimizations (EF Core)

* `AsNoTracking()` for read-only queries
* Projection (`Select`) instead of full entity loading
* Indexed columns for faster filtering

---
## 🧩 Technologies Used

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Redis
- RabbitMQ
- Swagger / OpenAPI
- Clean Architecture

## 🧠 Architecture Notes

* Clean separation of concerns:

  * Domain → Business logic
  * Infrastructure → Data access
  * UI (CLI / MVC) → Presentation
* Easy to extend (API, Microservices, etc.)
* Ready for production-level improvements

---

## 📬 Future Improvements

* Authentication & Authorization
* Role-based access (Admin / Trainee)
* Email notifications
* Docker full setup (optional next step)

---

## 👨‍💻 Author

Developed as part of a structured backend training program covering real-world .NET development practices.

---

## ⭐ Final Notes

This project is built step-by-step to simulate real production evolution:

```
Console → SQL → EF Core → MVC → Caching → Messaging → API Docs
```

If you understand this project deeply, you're already thinking like a backend engineer — not just writing code.

---
