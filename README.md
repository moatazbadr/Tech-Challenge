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

### 📥 Installation And How to Use

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
---

## 📝 How to Use

### 🔐 Authentication

1. **Register a new user**
   - Endpoint: `POST /api/auth/register`
   - Request body:
     ```json
     {
       "username": "your_username",
       "email": "your@email.com",
       "password": "your_password",
       "confirmPassword":"your_password"
     }
     ```

2. **Login**
   - Endpoint: `POST /api/auth/login`
   - Request body:
     ```json
     {
       "username": "your_username",
       "password": "your_password"
     }
     ```
   - Response will include a JWT token for authenticated requests as follows :
    {
      "success":false,
      "message":"login success",
      "token":<your_token>
    }


### 📇 Contact Management

*All contact endpoints require JWT authentication (include token in Authorization header)*

1. **Create a contact**
   - Endpoint: `POST /api/AddressBook/getAllAddressBooks`
   - Request body:
     ```json
     {
       "firstName": "John",
       "lastName": "Doe",
       "email": "john@example.com",
       "phone": "1234567890",
       "address": "123 Main St",
       "birthDate":"2001-11-20"
     }
     ```

2. **Get all contacts**
   - Endpoint: `GET /api/AddressBook/getAllAddressBooks`

3. **Get a specific contact**
   - Endpoint: `GET /api/AddressBook/GetById/{id}`

4. **Delete a contact**
   - Endpoint: `DELETE /api/AddressBook/DeleteAddressBook/{id}`

### 🔍 API Documentation
After running the application, access Swagger UI at:
  -http://localhost:<port>/swagger
  -(Replace `<port>` with your actual port number, typically 5000 or 5001)
