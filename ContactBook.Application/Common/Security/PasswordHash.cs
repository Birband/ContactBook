using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ContactBook.Application.Common.Security;
public static class PasswordHash
{
// Hash a password with a unique salt
    public static string HashPassword(string password)
    {
        // Generate a unique salt
        byte[] salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // Hash the password with the salt
        var hashedPassword = HashPasswordWithSalt(password, salt);

        // Combine salt and hashed password for storage
        return $"{Convert.ToBase64String(salt)}:{hashedPassword}";
    }

    // Verify a hashed password
    public static bool VerifyPassword(string hashedPasswordWithSalt, string password)
    {
        var parts = hashedPasswordWithSalt.Split(':');
        if (parts.Length != 2)
        {
            throw new FormatException("Unexpected password format.");
        }

        var salt = Convert.FromBase64String(parts[0]);
        var storedHashedPassword = parts[1];

        // Hash the provided password with the stored salt
        string hashOfInput = HashPasswordWithSalt(password, salt);

        // Compare the hashed password with the stored hashed password
        return hashOfInput == storedHashedPassword;
    }

    private static string HashPasswordWithSalt(string password, byte[] salt)
    {
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
    }
}