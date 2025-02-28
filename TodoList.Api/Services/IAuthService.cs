using TodoList.Api.Models;

namespace TodoList.Api.Services;

public interface IAuthService
{
    Task<User?> ValidateUserAsync(string username, string password);
    Task<User?> RegisterUserAsync(string username, string password);
    string GenerateJwtToken(User user);
    string HashPassword(string password);
}
