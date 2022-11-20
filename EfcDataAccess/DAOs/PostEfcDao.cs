namespace EfcDataAccess.DAOs;

using Application.DAOs;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public class PostEfcDao:IPostDao
{

    private readonly PostContext context;

    public PostEfcDao(PostContext context)
    {
        this.context = context;
    }
    public async Task<Post> CreateAsync(Post post)
    {
        EntityEntry<Post> added = await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchPostParameter)
    {
        IQueryable<Post> query = context.Posts.Include(
            post => post.Author).AsQueryable();

        if (!string.IsNullOrEmpty(searchPostParameter.Username))
        {
            query = 
                query.Where(post => post.Author.UserName.ToLower()
                    .Equals(searchPostParameter.Username.ToLower()));
        }

        if (!string.IsNullOrEmpty(searchPostParameter.TitleContains))
        {
            query = query.Where(post => post.Title.ToLower()
                .Contains(searchPostParameter.TitleContains.ToLower()));
        }

        List<Post> result = await query.ToListAsync();
        return result;
    }

    public async Task<Post?> GetByIdAsync(int postId)
    {
        Post? found = await context.Posts
            .Include(post => post.Author)
            .SingleOrDefaultAsync(post => post.Id == postId);
        return found;
    }
}