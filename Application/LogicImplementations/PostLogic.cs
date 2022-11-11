namespace Application.LogicImplementations;

using DAOs;
using Domain.DTOs;
using Domain.Models;
using LogicInterfaces;

public class PostLogic:IPostLogic
{
    private readonly IUserDao userDao;
    private readonly IPostDao postDao;

    public PostLogic(IUserDao userDao, IPostDao postDao)
    {
        this.userDao = userDao;
        this.postDao = postDao;
    }


    public async Task<Post> CreateAsync(PostCreationDto dto)
    {
        User? author = await userDao.GetByUsernameAsync(dto.OwnerUsername);
        if (author == null)
        {
            throw new Exception($"That's not your username! ");
        }

        Post postToCreate = new Post(author, dto.Title, dto.Body);
        ValidatePost(postToCreate);
        Post createdPost = await postDao.CreateAsync(postToCreate);
        return createdPost;
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchPostParameters)
    {
        return postDao.GetAsync(searchPostParameters);
    }

    public async Task<PostBasicDto> GetByIdAsync(int id)
    {
        Post? post = await postDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Post with id {id} doesnt exit");
        }

        return new PostBasicDto(post.Id, post.Author.UserName, post.Title, post.Body);
    }

    private void ValidatePost(Post postToBeCreated)
    {
        if (string.IsNullOrEmpty(postToBeCreated.Title))
            throw new Exception("Post title can't be empty!");

        
        string postBody = postToBeCreated.Body;
        if (string.IsNullOrEmpty(postBody))
            throw new Exception("Post body can't be empty!");
        if (postBody.Length > 300)
            throw new Exception("Post can't be longer than 300 characters!");
    }

}