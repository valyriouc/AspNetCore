using Microsoft.AspNetCore.Mvc;

namespace OauthPlayground;

[ApiController]
// [Authorize("AdminPolicy")]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("This is secret");
    }
}