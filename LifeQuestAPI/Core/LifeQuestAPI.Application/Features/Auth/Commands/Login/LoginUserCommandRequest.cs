using MediatR;

namespace LifeQuestAPI.Application.Features.Auth.Commands.Login;

public sealed record LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
