namespace Application.DAOs;

using Domain.DTOs;
using Domain.Models;

public interface IUserDao
{
    Task<User> CreateAsync(User user);
    Task<IEnumerable<User>> GetAsync(SearchParametersDto searchParameters);
    Task<User?> GetByUsernameAsync(string userName);
}