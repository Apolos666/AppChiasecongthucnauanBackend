namespace AppChiaSeCongThucNauAnBackend.Models;

public class RecipeLike
{
    public Guid UserId { get; set; }
    public Guid RecipeId { get; set; }

    public User User { get; set; }
    public Recipe Recipe { get; set; }
}
