namespace Application.LogicImplementations;

using DAOs;
using Domain.DTOs;
using Domain.Models;
using LogicInterfaces;

public class UserLogic:IUserLogic
{
    private readonly IUserDao userDao;

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }

    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        User? existing = await userDao.GetByUsernameAsync(dto.UserName);

        if (existing != null)
        {
            throw new Exception("User with this username already exists!");
        }

        ValidateData(dto);
        User toCreate = new User
        {
            UserName = dto.UserName,
            Password = dto.Password
        };

        User createdUser = await userDao.CreateAsync(toCreate);
        return createdUser;
    }

    public Task<IEnumerable<User>> GetAsync(SearchParametersDto searchParameters)
    {
        return userDao.GetAsync(searchParameters);
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