namespace HttpClients.ClientImplementations;

using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;

public class PostHttpClient:IPostService
{
    private readonly HttpClient client;


    public PostHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task CreateAsync(PostCreationDto dto)
    {
        HttpResponseMessage responseMessage = await client.PostAsJsonAsync("/Post", dto);
        if (!responseMessage.IsSuccessStatusCode)
        {
            string content = await responseMessage.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
        
    }

    public async Task<ICollection<Post>> GetAsync(string? userName, string? titleContains)
    {
        string query = ConstructQuery(userName, titleContains);
        
        HttpResponseMessage response = await client.GetAsync("/Post");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Post> posts = JsonSerializer.Deserialize<ICollection<Post>>(content, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        })!;
        return posts;
    }

    private string ConstructQuery(string? userName, string? titleContains)
    {
        string query = "";
        if (!string.IsNullOrEmpty(userName))
        {
            query += $"username={userName}";
        }

        if (!string.IsNullOrEmpty(titleContains))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"titlecontains={titleContains}";
        }

        return query;

    }
}