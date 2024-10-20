namespace AppChiaSeCongThucNauAnBackend.Models;

public class Recipe
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Ingredients { get; set; }
    public string Instructions { get; set; }
    public Guid RecipeCategoryId { get; set; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }

    public User User { get; set; }
    public RecipeCategory RecipeCategory { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<Bookmark> Bookmarks { get; set; }
    public ICollection<RecipeLike> RecipeLikes { get; set; }
    public ICollection<RecipeMedia> RecipeMedia { get; set; }
}
