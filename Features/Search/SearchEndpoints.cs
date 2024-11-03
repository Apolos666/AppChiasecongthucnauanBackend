using AppChiaSeCongThucNauAnBackend.Features.Search.Queries;
using Carter;
using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.Search;

public class SearchEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/search").WithTags("Search");

        group.MapGet("/", Search)
            .WithName("Search")
            .RequireAuthorization();
    }

    private async Task<IResult> Search(
        [AsParameters] SearchParameters parameters,
        IMediator mediator)
    {
        if (string.IsNullOrWhiteSpace(parameters.Q))
        {
            return Results.BadRequest("Từ khóa tìm kiếm không được để trống");
        }

        var query = new SearchQuery(parameters.Q);
        var result = await mediator.Send(query);

        return Results.Ok(result);
    }
}

public class SearchParameters
{
    public string Q { get; set; } = string.Empty;
} 