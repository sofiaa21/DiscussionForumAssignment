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
            throw new Exception($"User with username: {dto.OwnerUsername} was not found");
        }

        Post postToCreate = new Post(author, dto.Title, dto.Body);

        ValidatePost(postToCreate);
        Post createdPost = await postDao.CreateAsync(postToCreate);
        return createdPost;
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