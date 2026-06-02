# Secure ASP.NET Core Web App with IdentityHub & Contact Directory

An enterprise-grade, open-source sample ASP.NET Core Razor Pages application demonstrating secure **OpenID Connect (OIDC)** authentication via **IdentityHub (Operlity IAM)** integrated with a fully featured, local **SQLite Contact Management Module** featuring multi-user data isolation.

---

## ✨ Features

* **🔒 Enterprise-Grade Security**: Industry-standard OIDC Authorization Code Flow with server-side token exchange and session management.
* **⚡ Auto-Login Redirection**: Seamless user experience that instantly redirects unauthenticated users to the IdentityHub login page upon launching the application.
* **📂 Contacts Directory**: A fully integrated CRUD module supporting:
  * **Add Contact**: Easy creation with automated field validation.
  * **Edit Contact**: Inline modal editing with dynamic form pre-filling.
  * **Delete Contact**: Destructive action modals with verification alerts.
* **🛡️ Multi-User Data Isolation**: Contacts are isolated at the user-identity level. Even in a shared database, users can only see, search, and manage the contacts they created.
* **🧬 Robust Claims-Fallback**: Safe identity extraction that inspects standard OIDC claims (`Name`, `Email`, `Subject/NameIdentifier`, and `Preferred Username`) to ensure compatibility across diverse IAM servers.
* **🎨 Premium Glassmorphic UI**: Beautiful responsive dashboard cards, dynamic HSL gradient avatars based on name initials, real-time client-side queries, and sliding toast feedback notifications.
* **📦 Hands-Free Local Database**: Built-in SQLite database (`contacts.db`) that initializes automatically on app startup using EF Core `EnsureCreated()`. No manual setup or command-line migrations are required.

---

## 🛠️ Technology Stack

* **Framework**: ASP.NET Core 10.0 (Razor Pages)
* **Data Access**: Entity Framework Core 10.0
* **Database**: Local SQLite Database
* **Authentication**: OpenID Connect (OIDC) & Cookie Authentication
* **Styling**: Bootstrap 5 + Vanilla CSS Custom Tokens (Glassmorphism & Gradients)

---

## 🚀 Quick-Start Command Line Guide

Follow these simple CLI commands to get the application up and running locally:

### 1. Prerequisites
Ensure you have the [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) installed on your system.

### 2. Clone and Navigate
```bash
git clone https://github.com/Operlity/iam-samples-aspnetcore.git
cd iam-samples-aspnetcore
```

### 3. Restore Dependencies
Restore NuGet packages from the official NuGet registry:
```bash
dotnet restore
```

### 4. Build the Application
Perform a clean build to verify compilation:
```bash
dotnet build
```

### 5. Run the Server
Launch the server in development mode:
```bash
dotnet run --project "ASP.NetCore Web App/ASP.NetCore Web App.csproj"
```
Once launched, open your browser and navigate to `https://localhost:7284` (or the HTTP port `http://localhost:5066`). You will be instantly redirected to the secure login page!

---

## ⚙️ Configuration (`appsettings.json`)

Configure your OIDC client parameters in the `appsettings.json` file inside the project directory:

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
> Make sure to register the redirect URIs in your **IdentityHub Admin Console** client settings:
> * **Allowed Redirect URI**: `https://localhost:7284/signin-oidc`
> * **Allowed Post-Logout Redirect URI**: `https://localhost:7284/signout-callback-oidc`

---

## 📁 Repository Cleanliness (Open Source Ready)

To ensure this remains a clean and secure open-source repository:
* **Excluded Local Databases**: `contacts.db` and its temporary transaction caches (`contacts.db-shm`, `contacts.db-wal`) are automatically ignored in the `.gitignore` to prevent committing test records.
* **Excluded User Caches**: Visual Studio's local `.vs/` cache folders and `*.user` settings files are fully ignored to keep the repository clear of environment-specific profiles.

---

## 📚 Developer CLI Cheat Sheet

Here are useful commands to help you develop, debug, and test:

* **Clean Build Output**:
  ```bash
  dotnet clean
  ```
* **Format Code**:
  ```bash
  dotnet format
  ```
* **Verify Database Table Records directly (if SQLite CLI is installed)**:
  ```bash
  sqlite3 "ASP.NetCore Web App/ASP.NetCore Web App/contacts.db" "SELECT * FROM Contacts;"
  ```

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).
