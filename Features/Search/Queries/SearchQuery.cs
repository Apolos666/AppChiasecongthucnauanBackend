using MediatR;
using AppChiaSeCongThucNauAnBackend.Features.Search.Dtos;
namespace AppChiaSeCongThucNauAnBackend.Features.Search.Queries;

public record SearchQuery(string SearchTerm) : IRequest<SearchResultDto>;