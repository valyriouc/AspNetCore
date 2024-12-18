using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace Moody.Models;

public enum AccountType
{
    User,
    Admin,
    Banned,
}

[Table("accounts")]
public class AccountModel(string username, string email, string password, AccountType accountType)
{
    [Key]
    [Column("id")]
    public int Id { get; }

    [Column("username")] 
    public string Username { get; private set; } = username;

    [Column("email")]
    public string Email { get; private set; } = email;

    [Column("password")]
    public string Password { get; private set; } = password;

    [Column("account_type")]
    public AccountType AccountType { get; } = accountType;

    public void UpdatePassword(string password) => Password = password;

    public void UpdateUsername(string username) => Username = username;

    public void UpdateEmail(string email) => Email = email;

    public ClaimsPrincipal CreateLoginInformation()
    {
        List<Claim> claims =
        [
            new(ClaimTypes.Sid, Id.ToString()),
            new(ClaimTypes.Name, Username),
            new(ClaimTypes.Email, Email)
        ];
        
        ClaimsIdentity identity = new ClaimsIdentity(claims);
        ClaimsPrincipal principal = new ClaimsPrincipal(identity);
        return principal;
    }
}