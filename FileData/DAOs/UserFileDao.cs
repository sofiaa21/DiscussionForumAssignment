namespace FileData.DAOs;

using Application.DAOs;
using Domain.DTOs;
using Domain.Models;

public class UserFileDao:IUserDao
{
    private readonly FileContext context;

    public UserFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<User> CreateAsync(User user)
    {
        int id = 1;
        if (context.Users.Any())
        {
            id = context.Users.Max(u => u.Id);
            id++;
        }

        user.Id = id;
        context.Users.Add(user);
        context.SaveChanges();
        return Task.FromResult(user);
    }

    public Task<IEnumerable<User>> GetAsync(SearchParametersDto searchParameters)
    {
        IEnumerable<User> users = context.Users.AsEnumerable();
        if (searchParameters.UsernameContains != null)
        {
            users = context.Users.Where(u =>
                u.UserName.Contains(searchParameters.UsernameContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(users);
    }

    public Task<User?> GetByUsernameAsync(string userName)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)
        );
        return Task.FromResult(existing);    }

    public Task<User?> GetByIdAsync(int userId)
    {
        User? existing = context.Users.FirstOrDefault(u => u.Id == userId);
        return Task.FromResult(existing);
    }
}