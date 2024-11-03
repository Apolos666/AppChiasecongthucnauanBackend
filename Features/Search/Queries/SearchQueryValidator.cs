using FluentValidation;

namespace AppChiaSeCongThucNauAnBackend.Features.Search.Queries;

public class SearchQueryValidator : AbstractValidator<SearchQuery>
{
    public SearchQueryValidator()
    {
        RuleFor(x => x.SearchTerm)
            .NotEmpty().WithMessage("Từ khóa tìm kiếm không được để trống")
            .MinimumLength(2).WithMessage("Từ khóa tìm kiếm phải có ít nhất 2 ký tự")
            .MaximumLength(50).WithMessage("Từ khóa tìm kiếm không được vượt quá 50 ký tự");
    }
} 