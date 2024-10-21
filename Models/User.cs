namespace AppChiaSeCongThucNauAnBackend.Models;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string? Bio { get; set; }
    public string? SocialMedia { get; set; }

    public ICollection<Recipe> Recipes { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<Bookmark> Bookmarks { get; set; }
    public ICollection<RecipeLike> RecipeLikes { get; set; }
    public ICollection<UserConversation> UserConversations { get; set; }
    public ICollection<UserFollow> Following { get; set; }
    public ICollection<UserFollow> Followers { get; set; }
}
