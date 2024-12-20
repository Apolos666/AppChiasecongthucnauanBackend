namespace AppChiaSeCongThucNauAnBackend.Models;

public class Comment
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid RecipeId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }

    public User User { get; set; }
    public Recipe Recipe { get; set; }
}
