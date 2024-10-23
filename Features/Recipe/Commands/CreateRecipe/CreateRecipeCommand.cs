using AppChiaSeCongThucNauAnBackend.Features.Recipe.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AppChiaSeCongThucNauAnBackend.Features.Recipe.Commands.CreateRecipe;

public record CreateRecipeCommand(CreateRecipeDto RecipeDto, IFormFileCollection Files, Guid UserId) : IRequest<Guid>;
