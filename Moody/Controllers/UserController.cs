using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Moody.Database;
using Moody.Helpers;
using Moody.Models;

namespace Moody.Controllers;

public class UserController(AppDbContext dbContext) : AppBaseController(dbContext)
{
    [HttpGet]
    public IActionResult Get()
    {
        AccountModel account = CurrentAccount!;
        return Ok(new UserTransfer(account));
    }

    [HttpPost("email")]
    public async Task<IActionResult> UpdateEmail([FromBody] UpdateEmail payload)
    {
        AccountModel? accountModel = await DbContext.Accounts.FindAsync(CurrentAccount);

        if (accountModel is null)
        {
            return BadRequest();
        }
        
        accountModel.UpdateEmail(payload.Email);
        
        DbContext.Accounts.Update(accountModel);
        await DbContext.SaveChangesAsync();
        
        await HttpContext.SignOutAsync();
        
        return Ok("Updated the email");
    }
    
    [HttpPost("username")]
    public async Task<IActionResult> UpdateUsername([FromBody] UpdateUsername payload)
    {
        AccountModel? accountModel = await DbContext.Accounts.FindAsync(CurrentAccount);

        if (accountModel is null)
        {
            return BadRequest();
        }
        
        accountModel.UpdateUsername(payload.Username);
        
        DbContext.Accounts.Update(accountModel);
        await DbContext.SaveChangesAsync();

        await HttpContext.SignOutAsync();
        return Ok("Updated the username");
    }

    [HttpPost("password")]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePassword payload)
    {
        AccountModel? accountModel = await DbContext.Accounts.FindAsync(CurrentAccount);

        if (accountModel is null)
        {
            return BadRequest();
        }
        
        string hashOld = HashHelper.CreateSha512Hash(payload.OldPassword);
        string hashNew = HashHelper.CreateSha512Hash(payload.NewPassword);

        if (hashOld != accountModel.Password)
        {
            return BadRequest("Old password is incorrect");
        }
        
        accountModel.UpdatePassword(hashNew);
        
        DbContext.Accounts.Update(accountModel);
        await DbContext.SaveChangesAsync();
        
        await HttpContext.SignOutAsync();
        return Ok("Updated the password");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync()
    {
        AccountModel? accountModel = await DbContext.Accounts.FindAsync(CurrentAccount);
        if (accountModel is null)
        {
            return BadRequest();
        }
        
        DbContext.Accounts.Remove(accountModel);
        await DbContext.SaveChangesAsync();
        
        await HttpContext.SignOutAsync();
        return Ok("Deleted the account");
    }
}

public class UpdateEmail
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }
}

public class UpdateUsername
{
    [MinLength(6)]
    public string Username { get; set; }
}

public class UpdatePassword
{
    [MinLength(12)]
    [JsonPropertyName("new_password")]
    [Required]
    public string NewPassword { get; set; }    
    
    [MinLength(12)]
    [Required]
    [JsonPropertyName("old_password")]
    public string OldPassword { get; set; }
}

public class UserTransfer
{
    public string Username { get; set; }

    public string Email { get; set; }
    
    
    public UserTransfer(AccountModel account)
    {
        Username = account.Username;
        Email = account.Email;
    }
}