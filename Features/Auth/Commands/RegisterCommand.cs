using AppChiaSeCongThucNauAnBackend.Features.Auth.DTOs;
using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.Auth.Commands;

public record RegisterCommand(RegisterDto RegisterDto) : IRequest<string>;
