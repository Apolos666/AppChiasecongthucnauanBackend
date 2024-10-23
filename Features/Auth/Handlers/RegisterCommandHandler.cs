using AppChiaSeCongThucNauAnBackend.Data;
using AppChiaSeCongThucNauAnBackend.Features.Auth.Commands;
using AppChiaSeCongThucNauAnBackend.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace AppChiaSeCongThucNauAnBackend.Features.Auth.Handlers;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly AppDbContext _context;

    public RegisterCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.RegisterDto.Email, cancellationToken);
        if (existingUser != null)
        {
            throw new Exception("Email đã tồn tại");
        }

        var user = new Models.User
        {
            Email = request.RegisterDto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.RegisterDto.Password),
            Name = request.RegisterDto.Name
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return "Đăng ký thành công";
    }
}

