# 🏥 FRESHARK Backend – ASP.NET Core Web API

Welcome to the **FRESHARK Backend** project.  
This is a scalable and well-structured **ASP.NET Core Web API** including:
This project follows a clean **Layered Architecture** with best practices for enterprise-level applications.

---

## 🚀 Getting Started (Local Development)

### ✅ Prerequisites

- .NET 8 SDK or later
- MySQL
- Visual Studio / VS Code
- Git

---

## 🔐 Database Connection Setup

⚠️ **Security Warning**

> **Do NOT use real production credentials inside GitHub.**  
> The following connection string is for **local development only**.

### Example `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=127.0.0.1;Port=3306;Database=alzetonaclinc;User=alzetona;Password=4321;"
}
