namespace FileData.DAOs;

using Application.DAOs;
using Domain.DTOs;
using Domain.Models;

public class PostFileDao: IPostDao
{

    private readonly FileContext context;


    public PostFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<Post> CreateAsync(Post post)
    {
        context.Posts.Add(post);
        context.SaveChanges();
        return Task.FromResult(post);
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchPostParameter)
    {
        IEnumerable<Post> result = context.Posts.AsEnumerable();

        if (!string.IsNullOrEmpty(searchPostParameter.Username))
        {
            result = context.Posts.Where(post =>
                post.Author.UserName.Equals(searchPostParameter.Username, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(searchPostParameter.TitleContains))
        {
            result = result.Where(post =>
                post.Title.Contains(searchPostParameter.TitleContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(result);
    }
}