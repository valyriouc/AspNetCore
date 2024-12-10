using EfCoreReversing.Database;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreReversing.Controllers;

[ApiController]
[Route("api/[controller]")]
internal class MyBaseController : ControllerBase
{
    public MyDbContext DbContext { get; }

    public MyBaseController(MyDbContext dbContext)
    {
        DbContext = dbContext;
    }
}