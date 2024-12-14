using Microsoft.AspNetCore.Mvc;

namespace DevToClone.Controllers;

[ApiController]
public class TestController : ControllerBase
{

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello world");
    }
    
}