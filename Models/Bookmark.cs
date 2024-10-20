namespace AppChiaSeCongThucNauAnBackend.Models;

public class Bookmark
{
    public Guid Id { get; set; }
    public Guid RecipeId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }

    public Recipe Recipe { get; set; }
    public User User { get; set; }
}
