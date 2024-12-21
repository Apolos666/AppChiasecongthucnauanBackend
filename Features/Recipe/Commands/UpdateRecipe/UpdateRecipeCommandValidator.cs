using FluentValidation;

namespace AppChiaSeCongThucNauAnBackend.Features.Recipe.Commands.UpdateRecipe;

public class UpdateRecipeCommandValidator : AbstractValidator<UpdateRecipeCommand>
{
    public UpdateRecipeCommandValidator()
    {
        RuleFor(x => x.RecipeDto.Title).NotEmpty().MaximumLength(255);
        RuleFor(x => x.RecipeDto.Ingredients).NotEmpty();
        RuleFor(x => x.RecipeDto.Instructions).NotEmpty();
    }
}

