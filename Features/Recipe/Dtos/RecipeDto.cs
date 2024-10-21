namespace AppChiaSeCongThucNauAnBackend.Features.Recipe.Dtos;

public class RecipeDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Ingredients { get; set; }
    public string Instructions { get; set; }
    public Guid RecipeCategoryId { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<string> MediaUrls { get; set; }
}
