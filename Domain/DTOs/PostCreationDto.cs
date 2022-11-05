namespace Domain.DTOs;

using Models;

public class PostCreationDto
{
    public string OwnerUsername { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }

    public PostCreationDto(string title, string body)
    {
        
        Title = title;
        Body = body;
    }
}