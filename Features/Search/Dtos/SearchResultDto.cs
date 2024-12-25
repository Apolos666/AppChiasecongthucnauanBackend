namespace AppChiaSeCongThucNauAnBackend.Features.Search.Dtos;

public class SearchResultDto
{
    public List<UserSearchResultDto> Users { get; set; } = new();
    public List<RecipeSearchResultDto> Recipes { get; set; } = new();
}

public class UserSearchResultDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
public class RecipeSearchResultDto 
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string ChefName { get; set; }
    public string? ThumbnailUrl { get; set; }
    public bool IsApproved { get; set; }
}
