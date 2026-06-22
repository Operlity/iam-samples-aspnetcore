# Secure ASP.NET Core Web App with IdentityHub & Contact Directory

A sample ASP.NET Core Razor Pages application demonstrating secure **OpenID Connect (OIDC)** authentication via **IdentityHub** integrated with a local **SQLite Contact Management Module** featuring multi-user data isolation.

## ✨ Features

* **🔒 Secure Authentication**: OpenID Connect (OIDC) Authorization Code Flow with server-side token validation and cookie session management.
* **⚡ Auto-Login Redirection**: Automatically redirects unauthenticated users to the IdentityHub login page upon application startup.
* **📂 Contacts Directory**: A fully integrated CRUD module (Add, Edit, Delete Contacts) built with Entity Framework Core and SQLite.
* **🛡️ Multi-User Data Isolation**: Contacts are isolated at the user-identity level; users can only view and manage contacts they created.
* **🎨 Modern Responsive UI**: Redesigned UI using Bootstrap 5, featuring interactive modals, dynamic user avatars, and real-time toast feedback notifications.

## 🛠️ Technology Stack

* **Framework**: ASP.NET Core 10.0 (Razor Pages)
* **Data Access**: Entity Framework Core 10.0 (SQLite)
* **Authentication**: OpenID Connect (OIDC) & Cookie Authentication

## 🚀 Setup and Run Guide

Follow these steps to get the application running locally:

### 1. Prerequisites
Ensure you have the [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) installed on your system.

### 2. Configure Configuration (`appsettings.json`)
Configure your OIDC client parameters in `ASP.NetCore Web App/ASP.NetCore Web App/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=contacts.db"
  },
  "IdentityHub": {
    "Authority": "https://your-identityhub-domain.com",
    "ClientId": "your-client-id",
    "ClientSecret": "your-client-secret",
    "ResponseType": "code",
    "Scope": "openid profile email",
    "CallbackPath": "/signin-oidc",
    "SignedOutCallbackPath": "/signout-callback-oidc"
  }
}
```

> [!IMPORTANT]
> Make sure to configure the corresponding redirect URIs in your **IdentityHub Client Admin Panel**:
> * **Allowed Redirect URI**: `https://localhost:7284/signin-oidc`
> * **Allowed Post-Logout Redirect URI**: `https://localhost:7284/signout-callback-oidc`

### 3. Run the Application
Open a terminal in the repository root and run:

```bash
# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Start the web server
dotnet run --project "ASP.NetCore Web App/ASP.NetCore Web App/ASP.NetCore Web App.csproj"
```

Once running, navigate to `https://localhost:7284` in your browser. You will be redirected to the secure login page.

## 📁 Repository Guidelines

* **Database Exclusions**: The SQLite database file (`contacts.db`) and its transaction logs are automatically ignored by Git to prevent committing local data.
* **VS Code / VS Caches**: Visual Studio user files (`*.user`) and cache folders (`.vs/`) are ignored to keep the repository clean.

## 📄 License

This project is licensed under the [MIT License](LICENSE).
