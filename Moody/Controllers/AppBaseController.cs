using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moody.Database;
using Moody.Models;

namespace Moody.Controllers;

[ApiController]
[Authorize(Roles = "User, Admin")]
[Route("api/[controller]")]
public class AppBaseController(AppDbContext dbContext) : ControllerBase
{
    protected AppDbContext DbContext { get; } = dbContext;

    protected AccountModel? CurrentAccount
    {
        get
        {
            Claim? idClaim = this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid);
            
            if (idClaim is null || !int.TryParse(idClaim.Value, out int id))
            {
                return null;
            }

            AccountModel? model = DbContext.Accounts.FirstOrDefault(x => x.Id == id);

            return model!;
        }
    }
}