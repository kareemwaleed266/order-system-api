# 🛒 Order Management System API – ASP.NET Core Web API

A clean, secure, and scalable **ASP.NET Core 8.0 Web API** for managing products, customers, orders, invoices and online payments.  
Built with **Entity Framework Core**, **ASP.NET Identity + JWT**, **Stripe API**, and a 4-layer architecture that keeps the codebase testable and maintainable.

---

## 🚀 Features

- ✅ **JWT Authentication & Role-based Authorization** (Admin / Manager / Customer)  
- 🛍️ **Product Management** – CRUD, filtering, searching, sorting, pagination  
- 👥 **Customer Management** – add, edit, delete customers  
- 📦 **Order & Order-Item Handling** – create orders, status flow, totals & discounts  
- 🧾 **Invoice Generation** linked to orders  
- 💳 **Stripe Payment Integration** – create payment-intent & mark orders paid  
- 🔁 **Caching** via `[Cached(seconds)]` attribute on GET endpoints  
- ✉️ **Email Helper** – sends order confirmation e-mails (pluggable SMTP)  
- 🧮 **Generic Repository + Unit-of-Work** patterns  
- 🔍 **Swagger UI** with Bearer token support for testing  
- 🧱 **AutoMapper** profiles for clean DTO ↔ Entity mapping  

---

## 🛠️ Tech Stack

| Purpose / Layer | Technology                              |
| --------------- | --------------------------------------- |
| Framework       | **ASP.NET Core 8.0**                    |
| ORM             | **Entity Framework Core**               |
| Authentication  | **ASP.NET Identity + JWT**              |
| Payments        | **Stripe API**                          |
| Database        | **SQL Server**                          |
| Mapping         | **AutoMapper**                          |
| API Docs        | **Swagger / Swashbuckle**               |
| Patterns        | Generic Repository • Unit-of-Work       |

---

## 🧱 Project Structure (4-Layer Architecture)

### 1️⃣ **Presentation Layer** – `Order.Api`
- `Controllers/` – Product, Order, Invoice, Payment, Customer, User  
- `Middlewares/` – global exception handling, JWT check, CORS  
- `Extensions/` – DI registration, Identity, Swagger, Caching  
- `Program.cs` – app startup (Stripe keys, JWT, AutoMapper, database seeding)

---

### 2️⃣ **Business Layer** – `Order.Service`
- `Services/` – `ProductService`, `OrderService`, `PaymentService`, …  
- `HandleResponses/` – standardized response / exception wrappers  
- Helper utilities: e-mail sender, PDF invoice (optional)

---

### 3️⃣ **Data Access Layer** – `Order.Repository`
- `Interfaces/` – `IGenericRepository`, `IUnitOfWork`, domain-specific repos  
- `Repositories/` – generic & concrete repository implementations

---

### 4️⃣ **Infrastructure Layer** – `Order.Data`
- `Context/` – `OrderDbContext`, `IdentityDbContext`  
- `Entities/` – `Product`, `Customer`, `Order`, `OrderItem`, `Invoice`, `AppUser`, `AppRole`  
- `Configurations/` – Fluent-API entity configurations  
- `Migrations/` – EF Core migration history

---

### ⚙️ App Entry & Config
- `Program.cs` – DI, middleware pipeline, Stripe configuration, JWT options  
- `appsettings.json` – connection strings, JWT secret, Stripe keys, SMTP settings

---

## 📦 Getting Started

### 1️⃣ Clone the Repository
```bash
git clone https://github.com/kareemwaleed266/order-management-system-api.git
cd Task
