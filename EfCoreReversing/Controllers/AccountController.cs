using EfCoreReversing.Database;
using EfCoreReversing.Database.Models;
using EfCoreReversing.Transfer.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreReversing.Controllers;

internal class AccountController : MyBaseController
{
    public AccountController(MyDbContext dbContext) : base(dbContext)
    {
        
    }

    [HttpGet("/{id}")]
    public IActionResult Get([FromRoute] uint id)
    {
        Account? account = DbContext.Accounts.FirstOrDefault(a => a.Id == id);
        
        if (account is null)
        {
            return BadRequest($"No access to account with id {id}");
        }

        return Ok();
    }

    [HttpPost("create/")]
    public IActionResult Create([FromBody]AccountTransfer payload)
    {
        return Ok();
    }

    [HttpPost("update/")]
    public IActionResult Update([FromBody] AccountTransfer payload)
    {
        return Ok();
    }

    [HttpDelete("delete/{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        return Ok();
    }
}