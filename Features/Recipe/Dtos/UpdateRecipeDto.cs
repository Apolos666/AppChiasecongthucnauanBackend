namespace AppChiaSeCongThucNauAnBackend.Features.Recipe.Dtos;

public class UpdateRecipeDto
{
    public string Title { get; set; }
    public string Ingredients { get; set; }
    public string Instructions { get; set; }
    public bool IsApproved { get; set; }
    public List<string> MediaUrls { get; set; } 
    public List<string> RemovedMediaUrls { get; set; }
}

