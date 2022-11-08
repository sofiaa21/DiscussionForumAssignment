namespace Application.DAOs;

using Domain.DTOs;
using Domain.Models;

public interface IPostDao
{
    Task<Post> CreateAsync(Post post);
    Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchPostParameter);
}