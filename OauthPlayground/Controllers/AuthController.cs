using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OauthPlayground;

// https://www.freecodecamp.org/news/how-to-build-an-spa-with-vuejs-and-c-using-net-core/

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    [HttpGet]
    public IActionResult Login()
    {
        return Challenge(new AuthenticationProperties() { RedirectUri = "/signin-google" },
            GoogleDefaults.AuthenticationScheme);
    }
    
    [HttpGet("google-response")]
    [Authorize]
    public async Task<IActionResult> GoogleResponse()
    {
        AuthenticateResult result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        var claims = result.Principal!.Identities.FirstOrDefault()!.Claims.Select(claim => new
        {
            claim.Issuer,
            claim.OriginalIssuer,
            claim.Type,
            claim.Value
        });
        return Ok(claims);
    }

    [HttpGet("google-logout")]
    public async Task<IActionResult> GoogleLogout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok("Signed out successfully");
    }
}