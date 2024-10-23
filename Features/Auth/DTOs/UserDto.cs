namespace AppChiaSeCongThucNauAnBackend.Features.Auth.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string? Bio { get; set; }
    public string? SocialMedia { get; set; }
}