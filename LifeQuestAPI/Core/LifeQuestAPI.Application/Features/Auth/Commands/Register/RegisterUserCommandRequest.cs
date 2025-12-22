using MediatR;

namespace LifeQuestAPI.Application.Features.Auth.Commands.Register;

public sealed record RegisterUserCommandRequest : IRequest<RegisterUserCommandResponse>
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
