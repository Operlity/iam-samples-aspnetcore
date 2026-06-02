using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP.NetCore_Web_App.Pages;

[Authorize]
public class ProfileModel : PageModel
{
    public void OnGet()
    {
    }
}
