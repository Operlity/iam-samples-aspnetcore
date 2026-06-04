using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP.NetCore_Web_App.Pages;

public class LoginModel : PageModel
{
    public IActionResult OnGet(string? returnUrl = null, bool forceLogin = false)
    {
        if (User.Identity?.IsAuthenticated == true && !forceLogin)
        {
            return RedirectToPage("/Welcome");
        }

        var redirectUrl = Url.Page("/Welcome");
        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            redirectUrl = returnUrl;
        }

        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
        if (forceLogin)
        {
            properties.Parameters["prompt"] = "login";
        }

        return Challenge(properties, OpenIdConnectDefaults.AuthenticationScheme);
    }
}
