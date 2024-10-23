namespace AppChiaSeCongThucNauAnBackend.Features.Recipe.Dtos;

public class CreateRecipeDto
{
    public string Title { get; set; }
    public string Ingredients { get; set; }
    public string Instructions { get; set; }
    public Guid RecipeCategoryId { get; set; }
}

