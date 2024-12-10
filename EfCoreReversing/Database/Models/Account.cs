namespace EfCoreReversing.Database.Models;

internal enum AccountType
{
    Admin,
    User,
    Banned,
}

internal class Account
{
    public int Id { get; set; }
    
    public string Username { get; set; }

    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public AccountType AccountType { get; set; }
}