using FluentValidation;

namespace AppChiaSeCongThucNauAnBackend.Features.Recipe.Commands.CreateRecipe;

public class CreateRecipeCommandValidator : AbstractValidator<CreateRecipeCommand>
{
    public CreateRecipeCommandValidator()
    {
        RuleFor(x => x.RecipeDto.Title).NotEmpty().MaximumLength(255);
        RuleFor(x => x.RecipeDto.Ingredients).NotEmpty();
        RuleFor(x => x.RecipeDto.Instructions).NotEmpty();
        RuleFor(x => x.RecipeDto.RecipeCategoryId).NotEmpty();
    }
}

