namespace AppChiaSeCongThucNauAnBackend.Features.Recipes.Dtos;
public class RecipesDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string ChefName { get; set; }
    public int LikesCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public string ImageUrl { get; set; }
    public bool IsTrending { get; set; }
}