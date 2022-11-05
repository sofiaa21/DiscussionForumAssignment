namespace Application.DAOs;

using Domain.Models;

public interface IPostDao
{
    Task<Post> CreateAsync(Post post);
}