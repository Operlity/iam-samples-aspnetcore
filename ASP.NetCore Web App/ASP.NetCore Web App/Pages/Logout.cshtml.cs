using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP.NetCore_Web_App.Pages;

public class LogoutModel : PageModel
{
    public async Task<IActionResult> OnGetAsync()
    {
        // Default Sign Out: local app sign-out and redirect directly to login
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToPage("/Login", new { returnUrl = "/Welcome", forceLogin = true });
    }

    public IActionResult OnGetCentral()
    {
        // Optional: central sign-out at Identity Provider
        return SignOut(
            new AuthenticationProperties
            {
                RedirectUri = Url.Page("/Login", new { returnUrl = "/Welcome", forceLogin = true }) ?? "/Login?returnUrl=%2FWelcome&forceLogin=true"
            },
            CookieAuthenticationDefaults.AuthenticationScheme,
            OpenIdConnectDefaults.AuthenticationScheme
        );
    }
}
