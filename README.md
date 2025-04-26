> ⚠️ **Warning**: This repository is still under development. Features may change or break without notice.


# 📧 Bulk Email Sender in ASP.NET Core and Blazor

A full-stack Bulk Email Sender application built with **ASP.NET Core Web API** and **Blazor** front-end, implementing **Clean Architecture** principles for scalability, maintainability, and separation of concerns. This system enables efficient bulk email management and delivery using robust tools like **MailKit**, **MimeKit**, and **Entity Framework Core**.

---

## 🚀 Features

- ✅ Send bulk emails via SMTP
- ✅ MIME and HTML email formatting
- ✅ Blazor front-end for seamless user experience
- ✅ Clean Architecture with layered structure
- ✅ SQL Server for data persistence
- ✅ Repository & Unit of Work patterns
- ✅ Easily extensible and testable codebase

---

## 🛠 Tech Stack

- **ASP.NET Core Web API**
- **Blazor** (WebAssembly)
- **Entity Framework Core**
- **SQL Server**
- **MailKit** & **MimeKit**
- **System.Net.Mail**
- **Clean Architecture**
- **IUnitOfWork & Repository Pattern**

---

## 📁 Project Structure

```
/src
│
├── /BlazorUI                  → Blazor Front-End (WASM)
│
└── /EmailSender.Core          → Backend & Core Layers
    ├── /API                   → ASP.NET Core Web API (Presentation Layer)
    ├── /Application           → Application Logic (Use Cases, DTOs, Interfaces)
    ├── /Domain                → Domain Models & Core Entities
    ├── /Infrastructure        → Email Services, External Integrations
    ├── /Persistence           → EF Core, DbContext, Repository Implementations
    └── /Shared                → Common Utilities, Constants
```

---

## 📦 Key NuGet Packages

- `MailKit`
- `MimeKit`
- `System.Net.Mail`
- `Microsoft.EntityFrameworkCore`
- `Microsoft.AspNetCore.Components`
- `AutoMapper` *(optional)*
- `Blazored.Toast` *(optional for UI notifications)*

---

## 🧰 Getting Started

### ✅ Prerequisites

- [.NET 9 or later](https://dotnet.microsoft.com/download)
- SQL Server
- Visual Studio 2022+ or VS Code

---

### ⚙️ Setup Instructions

1. **Clone the repository**

```bash
git clone https://github.com/vagaisuresh/EmailSender.git
cd EmailSender/src
```

2. **Update database connection string**

In `EmailSender/API/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=BulkEmailDb;Trusted_Connection=True;"
}
```

3. **Apply EF Core Migrations**

```bash
cd EmailSender
dotnet ef database update --project Infrastructure
```

4. **Run the API**

```bash
cd API
dotnet run
```

5. **Run the Blazor UI**

```bash
cd ../../BlazorUI
dotnet run
```

---

## 🌐 API Overview

### Send Email

**Endpoint**: `POST /api/email/send-bulk`

**Payload Example**:

```json
{
  "subject": "Weekly Newsletter",
  "body": "<p>Hello subscribers!</p>",
  "recipients": ["john@example.com", "jane@example.com"]
}
```

---

## 💻 Blazor UI Features

- Form to send emails
- Input validation and error display
- Real-time status updates (optional via SignalR)
- Success/failure alerts
- Can be extended for email templates, SMTP config UI, etc.

> *(You can add screenshots or gifs here for a visual preview!)*

---

## 🧪 Testing

- Test APIs via **Swagger UI** at `https://localhost:{port}/swagger`
- Use **Postman** or **Thunder Client** for additional testing
- Front-end available at `https://localhost:{blazorPort}`

---

## 🤝 Contributing

Open to contributions! Please fork the repo, create a branch, and submit a pull request.

---

## 📄 License

MIT License. Feel free to use, modify, and distribute.

---
