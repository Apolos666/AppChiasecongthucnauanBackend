namespace AppChiaSeCongThucNauAnBackend.Models;

public class RecipeMedia
{
    public Guid Id { get; set; }
    public Guid RecipeId { get; set; }
    public string MediaUrl { get; set; }

    public Recipe Recipe { get; set; }
}
