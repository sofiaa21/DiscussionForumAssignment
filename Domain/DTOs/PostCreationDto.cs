namespace Domain.DTOs;

using Models;

public class PostCreationDto
{
    public string OwnerUsername { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }

    public PostCreationDto(string ownerUsername, string title, string body)
    {
        OwnerUsername = ownerUsername;
        Title = title;
        Body = body;
    }
}