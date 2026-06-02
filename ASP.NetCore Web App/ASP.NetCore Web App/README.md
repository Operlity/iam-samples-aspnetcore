# ASP.NET Core Web Application with IdentityHub Authentication

This is a sample ASP.NET Core Razor Pages application demonstrating secure authentication using IdentityHub with OpenID Connect (OIDC).

## Features

- **Secure Authentication**: Enterprise-grade authentication with IdentityHub
- **OpenID Connect (OIDC)**: Industry-standard authentication protocol
- **Single Sign-On (SSO)**: Seamless login experience
- **Protected Pages**: Role-based access control
- **Session Management**: Secure cookie-based sessions
- **User Claims**: Access to user profile information

## Technology Stack

- ASP.NET Core 10.0
- Razor Pages
- OpenID Connect Authentication
- Cookie Authentication
- Bootstrap 5

## Project Structure

```
ASP.NetCore Web App/
├── Pages/
│   ├── Index.cshtml              # Home page (first landing page)
│   ├── Welcome.cshtml            # Welcome page (after login)
│   ├── Dashboard.cshtml          # Protected dashboard
│   ├── Profile.cshtml            # User profile page
│   ├── Login.cshtml              # Login redirect page
│   ├── Logout.cshtml             # Logout page
│   ├── Privacy.cshtml            # Privacy page
│   └── Shared/
│       └── _Layout.cshtml        # Layout with navigation
├── Program.cs                     # Application configuration
├── appsettings.json              # Configuration settings
└── wwwroot/                      # Static files
```

## Application Flow

1. **User opens application** → Lands on Home page (Index.cshtml)
2. **User clicks "Sign In"** → Redirects to IdentityHub login page
3. **User enters credentials** → IdentityHub authenticates user
4. **Successful authentication** → Redirects back to Welcome page
5. **User navigates** → Access Dashboard, Profile, and other protected pages
6. **User clicks "Logout"** → Signs out and returns to Home page

## Configuration

### 1. Update appsettings.json

Replace the IdentityHub configuration values with your actual settings:

```json
{
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

### 2. IdentityHub Configuration

In your IdentityHub admin panel, configure the following:

- **Redirect URI**: `https://localhost:7xxx/signin-oidc` (replace with your app URL)
- **Post Logout Redirect URI**: `https://localhost:7xxx/signout-callback-oidc`
- **Allowed Scopes**: `openid`, `profile`, `email`
- **Grant Type**: Authorization Code Flow

## Getting Started

### Prerequisites

- .NET 10.0 SDK
- Visual Studio 2026 or VS Code
- IdentityHub account and credentials

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/Operlity/iam-samples-aspnetcore.git
   cd "ASP.NetCore Web App"
   ```

2. Restore NuGet packages:
   ```bash
   dotnet restore
   ```

3. Update `appsettings.json` with your IdentityHub configuration

4. Run the application:
   ```bash
   dotnet run
   ```

5. Open browser and navigate to `https://localhost:7xxx`

## Pages Overview

### Public Pages
- **Home (/)**: Landing page with Sign In button
- **Privacy (/Privacy)**: Privacy policy page

### Protected Pages (Require Authentication)
- **Welcome (/Welcome)**: First page after successful login with user information
- **Dashboard (/Dashboard)**: Protected dashboard with statistics
- **Profile (/Profile)**: User profile with claims information

## Authentication Features

### Authorization Code Flow
- Secure OAuth 2.0 flow
- Server-side token exchange
- Client secret protection

### Session Management
- Secure HTTP-only cookies
- Sliding expiration (60 minutes)
- HTTPS enforcement
- SameSite protection

### Token Validation
- Issuer validation
- Audience validation
- Lifetime validation
- Clock skew tolerance

## Security Best Practices

✅ HTTPS enforcement  
✅ Secure cookies (HTTP-only, Secure, SameSite)  
✅ Authorization Code Flow (most secure OIDC flow)  
✅ Token validation  
✅ Session fixation protection  
✅ Protected routes with `[Authorize]` attribute  

## Troubleshooting

### Common Issues

1. **Redirect Loop**
   - Check that Redirect URI in IdentityHub matches your application URL
   - Verify ClientId and ClientSecret are correct

2. **401 Unauthorized**
   - Ensure `[Authorize]` attribute is on protected pages
   - Check that authentication middleware is configured in Program.cs

3. **Cannot Access Claims**
   - Verify `GetClaimsFromUserInfoEndpoint = true` in OpenID Connect options
   - Check that required scopes are configured

## Development

### Adding New Protected Pages

1. Create a new Razor Page
2. Add `[Authorize]` attribute to the PageModel:
   ```csharp
   [Authorize]
   public class MyPageModel : PageModel
   {
	   // Page logic
   }
   ```

### Adding Role-Based Authorization

```csharp
[Authorize(Roles = "Admin")]
public class AdminPageModel : PageModel
{
	// Admin only page
}
```

## NuGet Packages

- `Microsoft.AspNetCore.Authentication.OpenIdConnect` (10.0.0)
- `Microsoft.AspNetCore.Authentication.Cookies` (2.2.0)

## License

MIT License

## Support

For issues and questions:
- GitHub Issues: https://github.com/Operlity/iam-samples-aspnetcore/issues
- Documentation: [Microsoft Identity Platform](https://learn.microsoft.com/azure/active-directory/develop/)

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.
