using AppChiaSeCongThucNauAnBackend.Data;
using AppChiaSeCongThucNauAnBackend.Features.Auth.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AppChiaSeCongThucNauAnBackend.Models;

namespace AppChiaSeCongThucNauAnBackend.Features.Auth.Handlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public LoginCommandHandler(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.LoginDto.Email, cancellationToken);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.LoginDto.Password, user.Password))
        {
            throw new Exception("Email hoặc mật khẩu không đúng");
        }

        var token = GenerateJwtToken(user);
        return token;
    }

    private string GenerateJwtToken(Models.User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpirationInMinutes"])),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

