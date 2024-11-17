namespace AppChiaSeCongThucNauAnBackend.Features.Recipe.Dtos;

public class RecipeDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string Title { get; set; }
    public string Ingredients { get; set; }
    public string Instructions { get; set; }
    public Guid RecipeCategoryId { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<string> MediaUrls { get; set; }
    public int LikesCount { get; set; }
    public bool IsLiked { get; set; }
    public List<CommentDto> Comments { get; set; }
}

public class CommentDto
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
}
