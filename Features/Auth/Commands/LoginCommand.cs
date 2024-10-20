using AppChiaSeCongThucNauAnBackend.Features.Auth.DTOs;
using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.Auth.Commands;

public record LoginCommand(LoginDto LoginDto) : IRequest<string>;


