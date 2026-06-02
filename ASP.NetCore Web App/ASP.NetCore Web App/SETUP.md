# Quick Setup Guide

## Step 1: Configure IdentityHub Settings

Open `appsettings.json` and update the following values:

```json
"IdentityHub": {
  "Authority": "https://your-identityhub-domain.com",     // Your IdentityHub server URL
  "ClientId": "your-client-id",                            // Client ID from IdentityHub
  "ClientSecret": "your-client-secret",                    // Client Secret from IdentityHub
  "ResponseType": "code",                                  // Keep as "code"
  "Scope": "openid profile email",                         // Adjust scopes as needed
  "CallbackPath": "/signin-oidc",                          // Keep default
  "SignedOutCallbackPath": "/signout-callback-oidc"        // Keep default
}
```

## Step 2: Configure IdentityHub Client

In your IdentityHub admin console:

1. Create a new OIDC client
2. Set **Client ID** and **Client Secret**
3. Add Redirect URIs:
   - Development: `https://localhost:7xxx/signin-oidc`
   - Production: `https://yourdomain.com/signin-oidc`
4. Add Post Logout Redirect URIs:
   - Development: `https://localhost:7xxx/signout-callback-oidc`
   - Production: `https://yourdomain.com/signout-callback-oidc`
5. Enable scopes: `openid`, `profile`, `email`
6. Set Grant Type: **Authorization Code**
7. Save the configuration

## Step 3: Run the Application

### Using Visual Studio
1. Press F5 or click "Start"
2. Browser will open automatically

### Using Command Line
```bash
cd "ASP.NetCore Web App"
dotnet run
```

### Using Visual Studio Code
```bash
dotnet run --project "ASP.NetCore Web App/ASP.NetCore Web App.csproj"
```

## Step 4: Test the Flow

1. Open browser to `https://localhost:7xxx`
2. Click **"Sign In with IdentityHub"**
3. You'll be redirected to IdentityHub login page
4. Enter your credentials
5. After successful login, you'll be redirected to the **Welcome** page
6. Navigate to **Dashboard** and **Profile** pages
7. Click **Logout** to sign out

## Application Pages

| Page | URL | Authentication | Description |
|------|-----|----------------|-------------|
| Home | `/` | Public | Landing page with login button |
| Welcome | `/Welcome` | Required | First page after login |
| Dashboard | `/Dashboard` | Required | Protected dashboard page |
| Profile | `/Profile` | Required | User profile with claims |
| Privacy | `/Privacy` | Public | Privacy policy |
| Login | `/Login` | Public | Triggers OIDC login flow |
| Logout | `/Logout` | Public | Signs out user |

## Troubleshooting

### Issue: "Unable to connect to IdentityHub"
**Solution**: Check that the `Authority` URL in appsettings.json is correct and accessible

### Issue: "Redirect URI mismatch"
**Solution**: Ensure the Redirect URI in IdentityHub matches your application URL exactly

### Issue: "Invalid client"
**Solution**: Verify ClientId and ClientSecret in appsettings.json match IdentityHub configuration

### Issue: "Cannot access protected pages"
**Solution**: Make sure you're logged in. Protected pages require authentication.

## Next Steps

- Customize the Welcome page
- Add more protected pages
- Implement role-based authorization
- Add custom claims
- Customize the UI/styling

## Support

For more help, see the main README.md file or visit:
- https://learn.microsoft.com/aspnet/core/security/authentication/
- https://openid.net/connect/
