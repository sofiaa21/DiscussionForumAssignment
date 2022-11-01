namespace HttpClients;

using Domain.DTOs;
using Domain.Models;

public interface IUserService
{
    Task<User> Create(UserCreationDto dto);
    Task<IEnumerable<User>> GetUsers(string? usernameContains = null);
}