namespace AppChiaSeCongThucNauAnBackend.Features.RecipeCategory.Dtos;

public class RecipeCategoryDto
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; }
}

public class CreateRecipeCategoryDto
{
    public string CategoryName { get; set; }
}

public class UpdateRecipeCategoryDto
{
    public string CategoryName { get; set; }
}