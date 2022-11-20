namespace Domain.Models;

public class Post
{
    public User Author { get; private set; }
    public string Title { get; private set; }
    public string Body { get;  private set; }

    public int Id { get; set; }

    public Post(User author, string title, string body)
    {
        Author = author;
        Title = title;
        Body = body;
    }

    private Post()
    {}
   
}