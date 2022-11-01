namespace Domain.Models;

public class Post
{
    public User Author { get; }
    public string Title { get; set; }
    public string Body { get; set; }


    public Post(User author, string title, string body)
    {
        Author = author;
        Title = title;
        Body = body;
    }
}