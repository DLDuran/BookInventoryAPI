# 📚 BookInventory API

[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-4169E1?logo=postgresql)](https://www.postgresql.org/)
[![JWT](https://img.shields.io/badge/JWT-Authentication-000000?logo=json-web-tokens)](https://jwt.io/)

BookInventory API is a secure, enterprise-grade RESTful Web API designed to track reading habits, manage personal book catalogs, and compute reading metrics in real time. 

## 🎯 Project Purpose

In standard CRUD applications, backend code often becomes tightly coupled, making it difficult to scale or adapt to multiple frontends. Additionally, handling partial updates dynamically without risking data corruption is a common challenge. 

This project solves this by providing a highly scalable, decoupled backend service acting as a single source of truth for any future user interface (Blazor, Flutter, or Razor Pages). It features robust user isolation, optimized data querying, and a defensive partial-update workflow.

## 🏗️ Architecture (Clean Architecture)

This project implements a strict Clean Architecture approach to ensure maintainability, testability, and a clear separation of concerns across four distinct layers:

* **`BookInventory.Domain`**: Contains pure business logic, core entities, and Enums completely decoupled from external frameworks.
* **`BookInventory.Application`**: Implements the Service Layer Pattern, Data Transfer Objects (DTOs), and business rules execution.
* **`BookInventory.Infrastructure`**: The data access layer. It configures the database context using Entity Framework Core (Code-First) and implements the Repository Pattern to abstract PostgreSQL interactions.
* **`BookInventory.Api`**: The presentation layer containing optimized REST controllers managing the HTTP request/response pipeline.

## 💻 Tech Stack & Rationale

* **Backend:** .NET 10 / ASP.NET Core Web API.
* **Database:** PostgreSQL. Selected for its reliability and strict data integrity.
* **Data Access:** Entity Framework Core. Chosen for its robust Code-First migration capabilities and LINQ translation engine.
* **Security:** JSON Web Tokens (JWT). Implemented for stateless, secure user authentication.
* **API Documentation:** Scalar API Reference. Replaced standard Swagger for a more modern, interactive developer onboarding experience.

## ✨ Key Features & Implementation 

* **Advanced Security & Token Rotation:** Integrated secure authentication via JWT. Created a custom base controller (ApiControllerBase) to securely parse claims (like NameIdentifier) directly from the user's identity context. Additionally, implemented a fully functional Token Refresh Rotation workflow (/api/auth/refresh) to handle secure, stateless session extensions.
* **Smart Partial Updates (`PATCH`):** Engineered a dynamic validation defensive layer. By utilizing non-null checks on incoming DTOs, the API updates *only* the fields explicitly provided by the user (e.g., `PagesRead` or `CoverImagePath`), flawlessly preserving database records from unintended `null` overwrites.
* **LINQ-Optimized Metrics Engine:** Built an isolated statistics module using high-performance queries to aggregate data dynamically—computing total pages read, tracking percentages of completed books, and reading trends per user.
* **Zero-Overhead Maintainability:** The backend is 100% decoupled from the UI, meaning mobile or web frontends can be hooked up instantly without rewriting backend logic.

## 🚦 Core API Endpoints

### 🔐 Authentication & Identity (`/api/auth`)
* `POST /register` - Public registration for new users.
* `POST /login` - User login (Validates credentials and issues JWT Access & Refresh tokens).
* `POST /refresh` - Token rotation endpoint (Validates and rotates expired sessions seamlessly).

### 👤 User Profile (`/api/users`)
* `GET /profile` - Retrieves the profile data of the currently authenticated user (Protected).

### Book Management (`/api/books`)
* `GET /` - List all books belonging to the authenticated user.
* `GET /{id}` - Retrieve details of a specific book by its ID.
* `POST /` - Add a new book to the inventory.
* `PATCH /{id}` - Smart partial update (Modifies only the explicitly provided fields).
* `DELETE /{id}` - Remove a book from the user's inventory.

### Analytics (`/api/statistics`)
* `GET /` - Computed summary of the user's reading progress.

## 🚀 Getting Started (Local Development)

### 1. Prerequisites
* .NET 10 SDK or higher.
* PostgreSQL server running locally or via Docker.
* An IDE like Visual Studio 2026, VS Code, or JetBrains Rider.

### 2. Database Connection Setup
For security reasons, the database connection string should be configured using the .NET Secret Manager or `appsettings.Development.json`.

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=BookInventoryDb;Username=postgres;Password=your_password"
}
