namespace AppChiaSeCongThucNauAnBackend.Models;

public class RecipeCategory
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; }
    public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
