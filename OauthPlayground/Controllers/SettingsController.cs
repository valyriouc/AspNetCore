using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OauthPlayground;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
public class SettingsController : ControllerBase
{
    private List<Setting> settings =
    [
        new Setting()
        {
            Name = "Item1",
            Value = "Hello"
        },
        new Setting()
        {
            Name = "Item2",
            Value = "World"
        }
    ];

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(settings);
    }

    [HttpGet("test")]
    [AllowAnonymous]
    public IActionResult Test()
    {
        return Ok("Hello world");
    }
}

public class Setting
{
    public string Name { get; set; }
    
    public string Value { get; set; }
}