namespace AppChiaSeCongThucNauAnBackend.Features.User.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string? Bio { get; set; }
    public string? SocialMedia { get; set; }
    public List<RecipeDto> Recipes { get; set; }
    public List<UserFollowDto> Following { get; set; }
    public List<UserFollowDto> Followers { get; set; }
}

public class RecipeDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<string> MediaUrls { get; set; }
}

public class UserFollowDto
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
}
