namespace FileData.DAOs;

using Application.DAOs;
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
}