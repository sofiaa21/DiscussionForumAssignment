namespace Application.LogicImplementations;

using DAOs;
using Domain.DTOs;
using Domain.Models;
using LogicInterfaces;

public class UserLogic:IUserLogic
{
    private readonly IUserDao UserDao;

    public UserLogic(IUserDao UserDao)
    {
        this.UserDao = UserDao;
    }

    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        User? existing = await UserDao.GetByUsernameAsync(dto.UserName);

        if (existing != null)
        {
            throw new Exception("User with this username already exists!");
        }

        ValidateData(dto);
        User toCreate = new User
        {
            UserName = dto.UserName
        };

        User createdUser = await UserDao.CreateAsync(toCreate);
        return createdUser;
    }

    private static void ValidateData(UserCreationDto userToCreate)
    {
        string username = userToCreate.UserName;
        if (username.Length<5 )
        {
            throw new Exception("Username must be at least 5 characters!");
        }

        if (username.Length > 21)
        {
            throw new Exception("Username must be less than 21 characters!");
        }
    }
}