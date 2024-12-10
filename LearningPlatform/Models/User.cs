using Microsoft.Data.Sqlite;

namespace LearningPlatform.Models;

internal sealed class User
{
    public uint Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}
