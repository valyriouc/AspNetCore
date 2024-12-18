using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moody.Database;
using Moody.Helpers;
using Moody.Models;

namespace Moody.Controllers;


[AllowAnonymous]
public class AuthController(AppDbContext dbContext) : AppBaseController(dbContext)
{
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginTransfer payload)
    {
        AccountModel? accountModel = DbContext.Accounts.FirstOrDefault(x => x.Email == payload.Email);

        if (accountModel is null)
        {
            return BadRequest("Email and password are incorrect");
        }
        
        var hash = HashHelper.CreateSha512Hash(payload.Password);
        if (hash != accountModel.Password)
        {
            return BadRequest("Email and password are incorrect");
        }

        await HttpContext.SignInAsync(accountModel.CreateLoginInformation());

        return Ok("Login was successful");
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterTransfer payload)
    {
        AccountModel? accountModel = DbContext.Accounts.FirstOrDefault(x => x.Email == payload.Email);

        if (accountModel is not null)
        {
            return BadRequest("Email is already in use");
        }

        string hashed = HashHelper.CreateSha512Hash(payload.Password);
        AccountModel current = new(payload.Username, payload.Email, hashed, AccountType.User);
        
        EntityEntry<AccountModel> result = DbContext.Accounts.Add(current);
        await DbContext.SaveChangesAsync();
        
        await HttpContext.SignInAsync(result.Entity.CreateLoginInformation());
        
        return Ok("Registration was successful");
    }
    
    [Authorize]
    [HttpGet("logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        await HttpContext.SignOutAsync();
        return Ok("Logout was successful");
    }
}

public class LoginTransfer
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    
    [Required]
    [MinLength(12)]
    public string Password { get; set; }
}

public class RegisterTransfer
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Username { get; set; }
    
    [Required]
    [MinLength(12)]
    public string Password { get; set; }
}