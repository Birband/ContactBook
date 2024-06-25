namespace ContactBook.Application.Common.Interfaces.Authentication;

/// <summary>
/// Interface for generating JWT token
/// </summary>
public interface IJwtTokenGenerator
{
    /// <summary>
    /// Generate JWT token for user
    /// </summary>
    /// <param name="userId">User Id</param>
    /// <param name="email">User email</param>
    /// <returns>JWT token</returns>
    string GenerateToken(Guid userId, string email);
}
