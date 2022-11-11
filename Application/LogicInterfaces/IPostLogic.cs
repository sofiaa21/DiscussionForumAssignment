namespace Application.LogicInterfaces;

using Domain.DTOs;
using Domain.Models;

public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreationDto dto);
    Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchPostParameters);

    Task<PostBasicDto> GetByIdAsync(int id);
}