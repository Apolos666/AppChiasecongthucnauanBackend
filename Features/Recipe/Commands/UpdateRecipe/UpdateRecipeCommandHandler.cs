using AppChiaSeCongThucNauAnBackend.Data;
using AppChiaSeCongThucNauAnBackend.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AppChiaSeCongThucNauAnBackend.Features.Recipe.Commands.UpdateRecipe;

public class UpdateRecipeCommandHandler : IRequestHandler<UpdateRecipeCommand, bool>
{
    private readonly AppDbContext _context;

    public UpdateRecipeCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
    {
        // Tìm recipe cần update
        var recipe = await _context.Recipes
            .FirstOrDefaultAsync(r => r.Id == request.RecipeId && r.UserId == request.UserId,
                cancellationToken);

        // Nếu không tìm thấy recipe hoặc không phải chủ sở hữu
        if (recipe == null)
        {
            return false;
        }

        // Cập nhật thông tin cơ bản
        recipe.Title = request.RecipeDto.Title;
        recipe.Ingredients = request.RecipeDto.Ingredients;
        recipe.Instructions = request.RecipeDto.Instructions;
        recipe.RecipeCategoryId = request.RecipeDto.RecipeCategoryId;

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}