namespace WebAPI.Services;

using Domain.Models;

public interface IAuthService
{
    Task<User> ValidateUser(string username, string password);
    Task RegisterUser(User user);
}