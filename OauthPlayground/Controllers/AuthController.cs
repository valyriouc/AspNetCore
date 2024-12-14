using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OauthPlayground;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    [HttpGet]
    public IActionResult Login()
    {
        return Challenge(new AuthenticationProperties() { RedirectUri = "http://localhost:5238/api/auth" },
            GoogleDefaults.AuthenticationScheme);
    }
    
}