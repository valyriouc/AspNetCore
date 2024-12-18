using System.Security.Cryptography;
using System.Text;

namespace Moody.Helpers;

public static class HashHelper
{
    public static string CreateSha512Hash(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentException("Input cannot be null or empty.", nameof(input));
        }

        using SHA512 sha512 = SHA512.Create();
        
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        byte[] hashBytes = sha512.ComputeHash(inputBytes);
        
        return Convert.ToBase64String(hashBytes);
    }
}