namespace AppChiaSeCongThucNauAnBackend.Features.Recipe.Dtos;

public class UpdateRecipeDto
{
    public string Title { get; set; }
    public string Ingredients { get; set; }
    public string Instructions { get; set; }
    public Guid RecipeCategoryId { get; set; }
    public List<IFormFile> NewMediaFiles { get; set; }
    public List<string> DeletedMediaUrls { get; set; }
}

