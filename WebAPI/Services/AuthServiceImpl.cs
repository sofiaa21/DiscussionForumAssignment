namespace WebAPI.Services;

using System.ComponentModel.DataAnnotations;
using Domain.Models;
using EfcDataAccess;

public class AuthServiceImpl:IAuthService
{

    private readonly PostContext context;


    private User user = new()
    {
        UserName = "sofia",
        Password = "password"
    };
    
    public AuthServiceImpl(PostContext context)
    {
        this.context = context;
        context.Users.Add(user);
    }


    public Task<User> ValidateUser(string username, string password)
    {
        List<User> users =context.Users.ToList();

        User? userLoggingIn = users.FirstOrDefault(u =>
            u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));

        if (userLoggingIn == null)
        {
            throw new Exception("User doesn't exist");
        }

        if (!userLoggingIn.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return Task.FromResult(userLoggingIn);
    }

    public Task RegisterUser(User user)
    {
        List<User> users =context.Users.ToList();
        if (string.IsNullOrEmpty(user.UserName))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            throw new ValidationException("Password cannot be null");
        }
        // Do more user info validation here

        // save to persistence instead of list

        users.Add(user);
        return Task.CompletedTask;
    }
}