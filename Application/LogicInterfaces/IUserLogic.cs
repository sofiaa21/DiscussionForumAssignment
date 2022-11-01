namespace Application.LogicInterfaces;

using Domain.DTOs;
using Domain.Models;

public interface IUserLogic
{
    public Task<User> CreateAsync(UserCreationDto dto);
    // public Task<IEnumerable<User> GetAsync()

}