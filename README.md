# Employees & Departments Web API with JWT Authentication

An **ASP.NET Core Web API** that manages employees and departments, secured with **JWT** authentication, plus two consumer clients: a **WinForms** desktop app and an **HTML / jQuery AJAX** page.

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?logo=microsoftsqlserver&logoColor=white)
![JWT](https://img.shields.io/badge/Auth-JWT-000000?logo=jsonwebtokens)

## Features

- **Accounts**: register and log in with ASP.NET Identity; login returns a **JWT** token
- **Employees** (`/api/Employee`, protected with `[Authorize]`): get all, get by id, get by name, create, update, delete, and employee-with-department details
- **Departments** (`/api/Department`, protected): get all, get by id with its employees
- **Model binding demo** (`/api/Bind`): route constraints (`{id:int}`, `{name:alpha}`), `[FromBody]` and route values
- **DTOs** for clean API responses (e.g. `EmployeeWithDepartmentDto`, `DepartmentWithEmployees`)
- **Repository pattern** with Dependency Injection
- **Swagger** for testing

## Consumers

- `WinFormsConsumer` — a Windows Forms app that calls the API with `HttpClient`
- `HtmlConsumer.html` — a simple page that calls the API with jQuery AJAX

## Tech stack

ASP.NET Core 8 Web API · Entity Framework Core · SQL Server · ASP.NET Identity · JWT Bearer · Swagger · WinForms · jQuery

## Getting started

1. Clone the repo and open `WepApiDemo.sln` in Visual Studio 2022.
2. Set your connection string and a long random `JWT:Secret` in **User Secrets** (don't commit real secrets).
3. Run `dotnet ef database update`, then start `WepApiDemo` and open `/swagger`.
4. Call `POST /api/Accounts/LogIn`, copy the token and use **Authorize → Bearer <token>** in Swagger.

## Author

**Samir Mohamed Hamed** — Backend .NET Developer
[LinkedIn](https://www.linkedin.com/in/samir-mohamed-dotnet) · [GitHub](https://github.com/Samirmohamedhamed)
