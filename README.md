# 📘 User Registration & Contact Management API

A .NET Core Web API application that allows users to sign up, sign in, and manage their personal address book. Each user can perform full CRUD operations on their own contacts.

---

## ⚙️ Tech Stack

- **Backend:** ASP.NET Core (.NET 8)
- **Authentication:** JWT (JSON Web Tokens)
- **Database:** SQL Server
- **ORM:** Entity Framework Core
- **Tools:** Swagger, Postman

---

## 🚀 Getting Started

### 🔧 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- [Postman](https://www.postman.com/) (optional, for API testing)

---

### 📥 Installation

```bash
# Clone the repository
git clone https://github.com/your-username/contact-manager-api.git

# Navigate to the project folder
cd contact-manager-api

# Restore dependencies
dotnet restore
# Apply migrations to create the database
dotnet ef database update

# Run the API
dotnet run
